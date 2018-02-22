import msbuilder

proj = r'..\SDK\Source\Virgil.SDK.NetFx\Virgil.SDK.NetFx.csproj'
spec = r'..\SDK\Source\Virgil.SDK.NetFx\Virgil.SDK.NetFx.nuspec'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)