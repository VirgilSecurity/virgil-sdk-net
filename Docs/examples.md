#C#/.NET

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:
```
PM> Install-Package Virgil.SDK.Keys
```

##Get Public Key and Encrypt Data

```csharp
// get public key from Virgil Keys service by email address
var publicKey = VirgilKeys.Search("demo@virgilsecurity.com");

var data = "Encrypt Me, Pleeeeeeease.";
var cipherData = VirgilCipher.Encrypt(data, publicKey);
```

##Sign Data

```csharp
var signature = VirgilCipher.Sign(data, privateKey);
```
