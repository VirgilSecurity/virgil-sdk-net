#Example C#/.Net

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:
```
PM> Install-Package Virgil.SDK.Keys
```

##Get Public Key and Encrypt Data (C#/Java)

```csharp
// get public key from Virgil Keys service by email address
var publicKey = VirgilKeys.Search("demo@virgilsecurity.com");

var data = "Encrypt Me, Pleeeeeeease.";

var cipherData = VirgilCipher.Encrypt(data, publicKey);
var sign = VirgilCipher.Sign(cipherData, myPrivateKey);
```

#Example Javascript

##Install
Use bower or npm to install Virgil.SDK packages.
```
bower install virgil-browsers
```
```
npm install virgil-browsers
```
##Get Public Key and Encrypt Data (Javascript)

```csharp
// get public key from Virgil Keys service by email address
var publicKey = VirgilKeys.Search("demo@virgilsecurity.com");

var data = "Encrypt Me, Pleeeeeeease.";

var cipherData = VirgilCipher.Encrypt(data, publicKey);
var sign = VirgilCipher.Sign(cipherData, myPrivateKey);
```

