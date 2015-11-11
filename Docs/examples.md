#C#/.NET

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:
```
PM> Install-Package Virgil.SDK.Keys
```

##Get Public Key and Encrypt Data

```csharp
var data = "Encrypt Me, Pleeeeeeease.";

var virgilKey = await keysClient.PublicKeys.Search("virgil-demo@freeletter.me");

var cipher = new VirgilCipher();

cipher.AddKeyRecipient(virgilKey.PublicKeyId, virgilKey.PublicKey);
var cipherData = cipher.Encrypt(data);
```
