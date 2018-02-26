import msbuilder

proj = r'..\SDK\Source\Virgil.SDK.NetFx\Virgil.SDK.NetFx.csproj'
spec = r'..\SDK\Source\Virgil.SDK.NetFx\Virgil.SDK.NetFx.nuspec'
nuget = r'C:\Users\Vasilina\Documents\projects\sdk-net2\virgil-sdk-net\SDK\Source\.nuget\NuGet.exe'
output = r'.\output'

builder = msbuilder.MsBuilder()
builder.build(proj)
builder.pack(proj, output)