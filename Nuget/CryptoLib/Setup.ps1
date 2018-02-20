Param (
    [Parameter(Mandatory=$true)]
    [string]$InputDir,
    [Parameter(Mandatory=$true)]
    [string]$OutputDir
)

Add-Type -assembly "system.io.compression"
Add-Type -assembly "system.io.compression.filesystem"
Add-Type -Path "./ICSharpCode.SharpZipLib.dll"

# Initializing Parameters
# -------------------------------------------------------------------------------------

$CURRENT_DIR              = Get-Location
$CRYPTO_LIB_DIR           = New-Object -TypeName System.IO.DirectoryInfo $InputDir
$CRYPTO_LIB_VERSION_FILE  = New-Object -TypeName System.IO.FileInfo "$CRYPTO_LIB_DIR\VERSION"
$CRYPTO_LIBS              = [ordered]@{ Platform = "net-windows";    Name = "net45";       TargetsFile = "$CURRENT_DIR\Targets\net45.targets" },
                            [ordered]@{ Platform = "mono-android";   Name = "monoandroid"; TargetsFile = "$CURRENT_DIR\Targets\monoandroid.targets" },
                            [ordered]@{ Platform = "mono-ios";       Name = "xamarinios";  TargetsFile = "$CURRENT_DIR\Targets\xamarinios.targets" },
                            [ordered]@{ Platform = "mono-macos";    Name = "xamarinmac";  TargetsFile = "$CURRENT_DIR\Targets\xamarinmac.targets" },
                            [ordered]@{ Platform = "mono-tvos"; Name = "xamarintvos";  TargetsFile = "$CURRENT_DIR\Targets\xamarintvos.targets" },
                            [ordered]@{ Platform = "mono-watchos"; Name = "xamarinwatchos";  TargetsFile = "$CURRENT_DIR\Targets\xamarinwatchos.targets" }                            
$NUGET_PACKAGE_DIR        = New-Item -ItemType Directory -Force -Path $OutputDir


function Extract {
    param([System.IO.FileInfo] $archive, [string] $destDir)

    # extract ZIP archive using native tools
    if ($archive.Extension.ToLower() -eq ".zip") {
        [io.compression.zipfile]::ExtractToDirectory($archive.FullName, $destDir)
        return
    }

    # extract TGZ archive using CLI tool
    if ($archive.Extension.ToLower() -eq ".tgz") {

        $inStream = [System.IO.File]::OpenRead($archive.FullName)
        $gzipStream = New-Object -TypeName ICSharpCode.SharpZipLib.GZip.GZipInputStream $inStream
        $tarArchive = [ICSharpCode.SharpZipLib.Tar.TarArchive]::CreateInputTarArchive($gzipStream)
        $tarArchive.ExtractContents($destDir)
        $tarArchive.Close()
        $gzipStream.Close()
        $inStream.Close()

        return
    }
}

# Validating Input Parameters
# -------------------------------------------------------------------------------------

Write-Host $CRYPTO_LIB_DIR

if (!$CRYPTO_LIB_DIR -or !$CRYPTO_LIB_DIR.Exists) {
    Write-Error "Directory with Crypto libraries is not found"
    exit 101
}

if (!$NUGET_PACKAGE_DIR -or !$NUGET_PACKAGE_DIR.Exists) {
    Write-Error "Directory for Nuget package is not created"
    exit 101
}

if (!$CRYPTO_LIB_VERSION_FILE -or !$CRYPTO_LIB_VERSION_FILE.Exists) {
    Write-Error "File with Crypto version is not found"
    exit 101
}

## Extracting Crypto Libraries
# -------------------------------------------------------------------------------------

# load crypto lib version from file
$version = [IO.File]::ReadAllText($CRYPTO_LIB_VERSION_FILE.FullName).Trim()
if ([string]::IsNullOrWhiteSpace($version)) {
    Write-Error "File with CryptoLib version is empty or has invalid format"
    exit 101
}

$cryptoNetDir = "$CRYPTO_LIB_DIR\net"
$nugetLibDir = New-Item -ItemType Directory -Force -Path "$NUGET_PACKAGE_DIR\lib"
$nugetBuildDir = New-Item -ItemType Directory -Force -Path "$NUGET_PACKAGE_DIR\build"
$tempDir = New-Item -ItemType Directory -Force -Path "$CURRENT_DIR\_temp"

foreach ($lib in $CRYPTO_LIBS) {

    $pattern = [string]::Format("virgil-crypto-{0}-{1}-[a-zA-Z0-9-_.]+.(zip|tgz)$", $version, $lib.Platform)

    # find an archive file by specified name pattern
    $file = Get-ChildItem -File $cryptoNetDir | Where-Object { $_.Name -match $pattern }

    if (!$file){
        Write-Error "File with pattern ($pattern) is not found in specified directory ($cryptoNetDir)"
        exit 102
    }

    # remove all files in temp folder from previous iteration
    Remove-Item "$tempDir\*" -Recurse

    # extract archive to temp directory
    Extract $file $tempDir

    # search in temp directory for Virgil.Crypto.dll assembly
    $assembly = Get-ChildItem -Path $tempDir -Filter Virgil.Crypto.dll -Recurse -Force

    if (!$assembly){
        Write-Error "Assembly dll is not found in unzipped archive"
        exit 103
    }

    # move the assembly to specified output folder '%OUTPUT_DIR%\lib\%PLATFORM%\'

    $nugetAssemblyFile = [string]::Format("{0}\{1}\{2}", $nugetLibDir, $lib.Name, $assembly.Name)

    New-Item -ItemType File -Path $nugetAssemblyFile -Force
    Move-Item $assembly.FullName $nugetAssemblyFile -Force

    # move the targets and native libs to specified output folder '%OUTPUT_DIR%\build\%PLATFORM%\'

    $nugetTargetsFile = New-Object -TypeName System.IO.FileInfo ([string]::Format("{0}\{1}\Virgil.Crypto.targets", $nugetBuildDir, $lib.Name))

    New-Item -ItemType File -Path $nugetTargetsFile -Force
    Copy-Item $lib.TargetsFile $nugetTargetsFile -Force
    Get-ChildItem -Path "$($assembly.Directory.FullName)\" -Recurse | Move-Item -Destination $nugetTargetsFile.Directory.FullName
}

# replace version in nuspec file and copy it to the output folder '%OUTPUT_DIR%\'
(Get-Content "$CURRENT_DIR\Package.nuspec").replace("%version%", $version) | Set-Content "$NUGET_PACKAGE_DIR\Package.nuspec"
