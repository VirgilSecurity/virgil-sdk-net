Param (    
    [Parameter(Mandatory=$true)] [string]$NuGetApiToken,
    [Parameter(Mandatory=$true)] [string]$ActualCryptoLibVersion
)

Add-Type -assembly "system.io.compression.filesystem"

function Expand-ZIPFile($file, $destination){
    Remove-Item $destination -Recurse -ErrorAction Ignore
    [io.compression.zipfile]::ExtractToDirectory($file, $destination)
}

function SmartCopy {
    param([string] $sourceFile, [string] $destinationFile)
    
    New-Item -ItemType File -Path $destinationFile -Force | Out-Null -ErrorAction Stop
    Copy-Item -Path $sourceFile -Destination $destinationFile -Force -ErrorAction Stop 
}

# Initialization
# -------------------------------------------------------------------------------------------------------------

# Create temporary directories

$CurrentDir = Get-Location
$PackageDir = New-Item -ItemType Directory -Force -Path "$CurrentDir\package"

-$CryptoLibVersion = [IO.File]::ReadAllText("$CurrentDir\VERSION").Trim()

# Extracting Crypto Librares
# -------------------------------------------------------------------------------------------------------------

$PortablePackageName    = "virgil-crypto-$CryptoLibVersion-net-windows-6.3"
$MonoAndroidPackageName = "virgil-crypto-$CryptoLibVersion-mono-android-21"
$MonoTouchPackageName   = "virgil-crypto-$CryptoLibVersion-mono-ios-7.0"

Invoke-Command { .\TarTool.exe "$CurrentDir\net\$MonoAndroidPackageName.tgz" } -ErrorAction Stop
Invoke-Command { .\TarTool.exe "$CurrentDir\net\$MonoTouchPackageName.tgz" } -ErrorAction Stop

Expand-ZIPFile -File "$CurrentDir\net\$PortablePackageName.zip" -Destination $CurrentDir

# Prepare package
# -------------------------------------------------------------------------------------------------------------

SmartCopy "$CurrentDir\$MonoAndroidPackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\MonoAndroid\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$MonoAndroidPackageName\lib\virgil_crypto_java_jni.jar" "$PackageDir\build\native\android\virgil_crypto_java_jni.jar"
SmartCopy "$CurrentDir\MonoAndroid.targets" "$PackageDir\build\MonoAndroid\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$MonoTouchPackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\MonoTouch\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$MonoTouchPackageName\lib\libvirgil_crypto_net.so" "$PackageDir\build\native\ios\libvirgil_crypto_net.so"
SmartCopy "$CurrentDir\MonoTouch.targets" "$PackageDir\build\MonoTouch\Virgil.Crypto.targets"

SmartCopy "$CurrentDir\$PortablePackageName\lib\Virgil.Crypto.dll" "$PackageDir\lib\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.dll"
SmartCopy "$CurrentDir\$PortablePackageName\lib\x64\virgil_crypto_net.dll" "$PackageDir\build\native\win\x64\virgil_crypto_net.dll"
SmartCopy "$CurrentDir\$PortablePackageName\lib\x86\virgil_crypto_net.dll" "$PackageDir\build\native\win\x86\virgil_crypto_net.dll"
SmartCopy "$CurrentDir\PortableNet.targets" "$PackageDir\build\portable-net4+sl4+wp7+win8+wpa81\Virgil.Crypto.targets"

# Replace version 
(Get-Content "$CurrentDir\Package.nuspec").replace("%version%", $ActualCryptoLibVersion) | Set-Content "$PackageDir\Package.nuspec"


# Updating NuGet
# -------------------------------------------------------------------------------------------------------------

Invoke-Command {.\NuGet.exe update -Self} -ErrorAction Stop

# Publish NuGet package
# -------------------------------------------------------------------------------------------------------------

Invoke-Command {.\NuGet.exe setApiKey $NuGetApiToken} -ErrorAction Stop

Invoke-Command {.\NuGet.exe pack "$PackageDir\Package.nuspec" -Verbosity Detailed} -ErrorAction Stop
Invoke-Command {.\NuGet.exe push ".\Virgil.Crypto.$ActualCryptoLibVersion.nupkg"} -ErrorAction Stop