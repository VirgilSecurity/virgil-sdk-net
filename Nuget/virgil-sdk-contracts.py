import msbuilder

proj = r'..\SDK\Source\Virgil.SDK.Contracts\Virgil.SDK.Contracts.csproj'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)