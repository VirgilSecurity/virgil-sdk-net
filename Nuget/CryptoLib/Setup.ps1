Param (
    [Parameter(Mandatory=$true)]
	[string]$NuGetApiToken
)

Add-Type -assembly "system.io.compression.filesystem"

function Expand-ZIPFile($file, $destination){
    Remove-Item $destination -Recurse -ErrorAction Ignore
    [io.compression.zipfile]::ExtractToDirectory($file, $destination)
}

function SmartCopy {
    param([string] $sourceFile, [string] $destinationFile)
    
    New-Item -ItemType File -Path $destinationFile -Force | Out-Null
    Copy-Item -Path $sourceFile -Destination $destinationFile -Force 
}

# Initialization
# -------------------------------------------------------------------------------------------------------------

# Create temporary directories

$CurrentDir = Get-Location
$TempDir    = New-Item -ItemType Directory -Force -Path "$CurrentDir\temp"
$ExtractDir = New-Item -ItemType Directory -Force -Path "$CurrentDir\temp\extract"
$PackageDir = New-Item -ItemType Directory -Force -Path "$CurrentDir\temp\package"


Copy-Item -Path ".\Dir\install" -Destination $TempDir -recurse -Force

# Extract current version form the file

$CryptoLibVersion = [IO.File]::ReadAllText("$TempDir\install\VERSION").Trim()

# Extracting Crypto Librares
# -------------------------------------------------------------------------------------------------------------

$PortablePackageName    = "virgil-crypto-$CryptoLibVersion-net-windows-6.2"
$MonoAndroidPackageName = "virgil-crypto-$CryptoLibVersion-mono-android-21"
$MonoTouchPackageName   = "virgil-crypto-$CryptoLibVersion-mono-ios-7.0"

..\Tools\TarTool\TarTool.exe "$TempDir\install\net\$MonoAndroidPackageName.tar.gz" $ExtractDir
..\Tools\TarTool\TarTool.exe "$TempDir\install\net\$MonoTouchPackageName.tar.gz" $ExtractDir

Expand-ZIPFile -File "$TempDir\install\net\$PortablePackageName.zip" -Destination "$ExtractDir\$PortablePackageName"

#..\Tools\TarTool\TarTool.exe "$TempDir\install\net\$PortablePackageName.zip" $ExtractDir

# Prepare package
# -------------------------------------------------------------------------------------------------------------

SmartCopy "$ExtractDir\$MonoAndroidPackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\MonoAndroid\Virgil.Crypto.dll"
SmartCopy "$ExtractDir\$MonoAndroidPackageName\lib\virgil_crypto_java_jni.jar" "$PackageDir\build\native\android\virgil_crypto_java_jni.jar"
SmartCopy "$CurrentDir\targets\MonoAndroid.targets" "$PackageDir\build\MonoAndroid\Virgil.Crypto.targets"

SmartCopy "$ExtractDir\$MonoTouchPackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\MonoTouch\Virgil.Crypto.dll"
SmartCopy "$ExtractDir\$MonoTouchPackageName\lib\libvirgil_crypto_net.so" "$PackageDir\build\native\ios\libvirgil_crypto_net.so"
SmartCopy "$CurrentDir\targets\MonoTouch.targets" "$PackageDir\build\MonoTouch\Virgil.Crypto.targets"

SmartCopy "$ExtractDir\$PortablePackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.dll"
SmartCopy "$ExtractDir\$PortablePackageName\lib\x64\virgil_crypto_net.dll" "$PackageDir\build\native\win\x64\virgil_crypto_net.dll"
SmartCopy "$ExtractDir\$PortablePackageName\lib\x86\virgil_crypto_net.dll" "$PackageDir\build\native\win\x86\virgil_crypto_net.dll"
SmartCopy "$CurrentDir\targets\PortableNet.targets" "$PackageDir\build\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\Package.nuspec" "$PackageDir\Package.nuspec"

# Replace version 
(Get-Content "$CurrentDir\Package.nuspec").replace("%version%", $CryptoLibVersion) | Set-Content "$PackageDir\Package.nuspec"


# Updating NuGet
# -------------------------------------------------------------------------------------------------------------

Invoke-Command {..\Tools\NuGet\NuGet.exe update -Self} -ErrorAction Stop

# Publish NuGet package
# -------------------------------------------------------------------------------------------------------------

Invoke-Command {..\Tools\NuGet\NuGet.exe pack .\temp\package\Package.nuspec -Verbosity Detailed} -ErrorAction Stop
Invoke-Command {..\Tools\NuGet\NuGet.exe push ".\Virgil.Crypto.$CryptoLibVersion.nupkg"} -ErrorAction Stop

Exit 1