import msbuilder

proj = r'..\..\virgil-sdk-crypto-net\Virgil.CryptoImpl\Virgil.CryptoImpl.csproj'
spec = r'..\..\virgil-sdk-crypto-net\Virgil.CryptoImpl\Virgil.CryptoImpl.nuspec'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)