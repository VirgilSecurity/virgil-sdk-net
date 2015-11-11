#Example C#/.Net

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

var cipherData = VirgilCrypto.Encrypt(data, publicKey);
var sign = VirgilCrypto.Sign(cipherData, myPrivateKey);
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
##Get Public Key and Encrypt Data

```csharp

var data = "Encrypt Me, Pleeeeeeease.";

var searchKey = VirgilKeys.searchKey('demo@virgilsecurity.com');

var encrypt = function(publicKey) { 
    return VirgilCrypto.encrypt(data, publicKey);
};

var sign = function(cipherData) {
    return VirgilCrypto.sign(cipherData, myPrivateKey);
}; 

searchKey.then(encrypt).then(sign);
