import msbuilder

projPath = r'..\SDK\Source\Virgil.CryptoApi\VirgilCryptoAPI\VirgilCryptoAPI.csproj'
releasePath = r'..\SDK\Source\Virgil.CryptoApi\VirgilCryptoAPI\bin\Release\\'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.buildAndPack(projPath, releasePath, output)
