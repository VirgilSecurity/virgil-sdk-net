Param (    
    [Parameter(Mandatory=$true)]
	[string]$NuGetApiToken,
    [Parameter(Mandatory=$true)]
    [string]$ActualCryptoLibVersion
)

Add-Type -assembly "system.io.compression.filesystem"

# Initialization
# -------------------------------------------------------------------------------------------------------------

# Create temporary directories

$CurrentDir = Get-Location
$PackageDir = New-Item -ItemType Directory -Force -Path "$CurrentDir\package"


$CryptoLibVersion = ""

Try
{
    $CryptoLibVersion = [IO.File]::ReadAllText("$CurrentDir\VERSION").Trim()

    

    if ([string]::IsNullOrWhiteSpace($CryptoLibVersion)){
        throw [System.IO.FileNotFoundException] "Version could not be parsed"
    }
}
Catch
{
    throw [System.IO.FileNotFoundException] "Version is not provided"
}


# Extracting Crypto Librares
# -------------------------------------------------------------------------------------------------------------


function Expand-ZIPFile($file, $destination){
    Try
    {
        [io.compression.zipfile]::ExtractToDirectory($file, $destination)
    }
    Catch
    {
        throw $_.Exception
    }
}

function SmartCopy {
    param([string] $sourceFile, [string] $destinationFile)
    
    Try
    {
        New-Item -ItemType File -Path $destinationFile -Force | Out-Null -ErrorAction Stop
        Copy-Item -Path $sourceFile -Destination $destinationFile -Force -ErrorAction Stop  
    }
    Catch
    {
        throw $_.Exception
    }  
}

$NetWindows_Package_Path = Get-ChildItem $CurrentDir\net | Where-Object {$_.Name -match "virgil-crypto-$CryptoLibVersion-net-windows-[0-9]+.[0-9]+.zip$"}
$MonoAndroid_Package_Path = Get-ChildItem $CurrentDir\net | Where-Object {$_.Name -match "virgil-crypto-$CryptoLibVersion-mono-android-[0-9]+.tgz$"}
$MonoiOS_Package_Path = Get-ChildItem $CurrentDir\net | Where-Object {$_.Name -match "virgil-crypto-$CryptoLibVersion-mono-ios-[0-9]+.[0-9]+.tgz$"}
$MonoMac_Package_Path = Get-ChildItem $CurrentDir\net | Where-Object {$_.Name -match "virgil-crypto-$CryptoLibVersion-mono-darwin-[0-9]+.[0-9]+-universal.tgz$"}
$XamarinMac_Package_Path = $MonoMac_Package_Path

if ([string]::IsNullOrWhiteSpace($NetWindows_Package_Path)){
    throw [System.IO.FileNotFoundException] "Windows .Net package is not found"
}

if ([string]::IsNullOrWhiteSpace($MonoAndroid_Package_Path)){
    throw [System.IO.FileNotFoundException] "Android Mono package is not found"
}

if ([string]::IsNullOrWhiteSpace($MonoiOS_Package_Path)){
    throw [System.IO.FileNotFoundException] "iOS Mono package is not found"
}

if ([string]::IsNullOrWhiteSpace($MonoMac_Package_Path)){
    throw [System.IO.FileNotFoundException] "Mac Mono package is not found"
}

if ([string]::IsNullOrWhiteSpace($XamarinMac_Package_Path)){
    throw [System.IO.FileNotFoundException] "Mac Xamarin package is not found"
}

Expand-ZIPFile -File "$CurrentDir\net\$NetWindows_Package_Path" -Destination $CurrentDir

$Result = Invoke-Command { .\TarTool.exe "$CurrentDir\net\$MonoAndroid_Package_Path" } -ErrorAction Stop
if ($Result){
    throw [System.Exception] "Extracting $MonoAndroid_Package_Path file is failed"
}

$Result = Invoke-Command { .\TarTool.exe "$CurrentDir\net\$MonoiOS_Package_Path" } -ErrorAction Stop
if ($Result){
    throw [System.Exception] "Extracting $MonoiOS_Package_Path file is failed"
}

$Result = Invoke-Command { .\TarTool.exe "$CurrentDir\net\$MonoMac_Package_Path" } -ErrorAction Stop
if ($Result){
    throw [System.Exception] "Extracting $MonoMac_Package_Path file is failed"
}

$NetWindows_Package_Name  = [System.IO.Path]::GetFileNameWithoutExtension($NetWindows_Package_Path)
$MonoAndroid_Package_Name = [System.IO.Path]::GetFileNameWithoutExtension($MonoAndroid_Package_Path)
$MonoiOS_Package_Name     = [System.IO.Path]::GetFileNameWithoutExtension($MonoiOS_Package_Path)
$MonoMac_Package_Name     = [System.IO.Path]::GetFileNameWithoutExtension($MonoMac_Package_Path)
$XamarinMac_Package_Name  = [System.IO.Path]::GetFileNameWithoutExtension($XamarinMac_Package_Path)

SmartCopy "$CurrentDir\$MonoAndroid_Package_Name\lib\Virgil.Crypto.dll" "$PackageDir\lib\monoandroid\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$MonoAndroid_Package_Name\lib\virgil_crypto_java_jni.jar" "$PackageDir\build\native\android\virgil_crypto_java_jni.jar"
SmartCopy "$CurrentDir\MonoAndroid.targets" "$PackageDir\build\monoandroid\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$MonoiOS_Package_Name\lib\Virgil.Crypto.dll" "$PackageDir\lib\monotouch\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$MonoiOS_Package_Name\lib\libvirgil_crypto_net.so" "$PackageDir\build\native\ios\libvirgil_crypto_net.so"
SmartCopy "$CurrentDir\MonoTouch.targets" "$PackageDir\build\monotouch\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$MonoMac_Package_Name\lib\Virgil.Crypto.dll" "$PackageDir\lib\monomac\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$MonoMac_Package_Name\lib\libvirgil_crypto_net.so" "$PackageDir\build\native\mac\libvirgil_crypto_net.so"
SmartCopy "$CurrentDir\MonoMac.targets" "$PackageDir\build\monomac\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$XamarinMac_Package_Name\lib\Virgil.Crypto.dll" "$PackageDir\lib\xamarinmac20\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$XamarinMac_Package_Name\lib\libvirgil_crypto_net.so" "$PackageDir\build\native\mac\libvirgil_crypto_net.dylib"
SmartCopy "$CurrentDir\XamarinMac.targets" "$PackageDir\build\xamarinmac20\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$NetWindows_Package_Name\lib\Virgil.Crypto.dll" "$PackageDir\lib\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$NetWindows_Package_Name\lib\x64\virgil_crypto_net.dll" "$PackageDir\build\native\win\x64\virgil_crypto_net.dll"
SmartCopy "$CurrentDir\$NetWindows_Package_Name\lib\x86\virgil_crypto_net.dll" "$PackageDir\build\native\win\x86\virgil_crypto_net.dll"
SmartCopy "$CurrentDir\PortableNet.targets" "$PackageDir\build\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.targets"

# Prepare NuGet package
# ---------------------------------------------------------------------------------------------------------------------------------

(Get-Content "$CurrentDir\Package.nuspec").replace("%version%", $ActualCryptoLibVersion) | Set-Content "$PackageDir\Package.nuspec"

# Updating NuGet package
# ---------------------------------------------------------------------------------------------------------------------------------

Invoke-Command {.\NuGet.exe update -Self} -ErrorAction Stop

# Publish NuGet package
# -------------------------------------------------------------------------------------------------------------

Invoke-Command {.\NuGet.exe setApiKey $NuGetApiToken} -ErrorAction Stop
Invoke-Command {.\NuGet.exe pack "$PackageDir\Package.nuspec" -Verbosity Detailed} -ErrorAction Stop
Invoke-Command {.\NuGet.exe push "$PackageDir\Virgil.Crypto.$ActualCryptoLibVersion.nupkg"} -ErrorAction Stop