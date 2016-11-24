import msbuilder

proj = r'..\SDK\Source\Virgil.SDK.Crypto\Virgil.SDK.Crypto.csproj'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)