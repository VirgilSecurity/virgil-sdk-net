import msbuilder

proj = r'..\SDK\Source\Virgil.CryptoApi\Virgil.CryptoAPI.csproj'
spec = r'..\SDK\Source\Virgil.CryptoApi\Virgil.CryptoAPI.spec'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)