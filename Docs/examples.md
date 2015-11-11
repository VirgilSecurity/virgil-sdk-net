#C#/.NET

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:
```
PM> Install-Package Virgil.SDK.Keys
```

##Get Public Key and Encrypt Data

```csharp
var data = "Encrypt Me, Pleeeeeeease.";

var publicKey = keysClient.Search("demo@virgilsecurity.com");

var cipherData = cipher.Encrypt(data, publicKey);
```
