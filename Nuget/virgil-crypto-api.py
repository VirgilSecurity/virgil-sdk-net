import msbuilder

proj = r'..\SDK\Source\Virgil.CryptoAPI\Virgil.CryptoAPI.csproj'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)