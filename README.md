# .NET/C# SDK Programming Guide

Welcome to the .NET SDK Programming Guide for C#. This guide is a practical introduction to creating apps for the Windows/Xamarin platform that make use of Virgil Security features. The code examples in this guide are written in C#. 

In this guide you will find code for every task you'll need to implement to create an application using Virgil Security. It also includes a description of the main classes and methods. The aim of this guide is to get you up and running quickly. You should be able to copy and paste the code provided into your own apps and use it with the minumum of changes.

## Table of Contents

* [Setting up your project](#setting-up-your-project)
* [User and App Credentials](#user-and-app-credentials)
* [Creating a Virgil Card](#creating-a-virgil_card)
  * [Collect an App Credentials](#collect-an-app-creadentials)
  * [Generate a new Keys](#generate-a-new-keys)
  * [Prepare a Creation Request](#prepare-a-creation-request)
* [Search for the Virgil Cards](#search-for-the-virgil-cards)
* [Revoking a Virgil Card](#revoking-a-virgil-card)
* [Keys Management](#keys_management)
  * [Keys Generation](#keys_generation)
  * [Import/Export Keys](#import_export_keys)
* [Encryption](#)
  * [Encrypt Data](#)
  * [Decrypt Data](#)
  * [Sign Data](#)
  * [Verify Digital Signature](#)
  * [Calculate Fingerprint](#)
* [Key Storage](#)
* [High Level](#)
* [Release Notes](#)
* [License](#)
* [Contacts](#)

## Setting up your project

The Virgil SDK is provided as a package named *Virgil.SDK*. The package is distributed through NuGet package management system.

### Target frameworks

* .NET Framework 4.0 and newer.

### Prerequisites

* Visual Studio 2013 RTM Update 2 and newer (Windows)
* Xamarin Studio 5.x and newer (Windows, Mac)
* MonoDevelop 4.x and newer (Windows, Mac, Linux)

### Installing the package

1. Use NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console)
2. Run ```PM> Install-Package Virgil.SDK```

## User and App Credentials

When you register an application on the Virgil developer [dashboard](https://developer.virgilsecurity.com/dashboard), we provide you with an *appID*, *appKey* and *accessToken*.

* **appID** is uniquely identifies your application in our services, it also uses to identify the Public key generated in a pair with *appKey*, for example: ```af6799a2f26376731abb9abf32b5f2ac0933013f42628498adb6b12702df1a87```
* **appKey** is a Private key that uses to perform creation and revocation a *Virgil Cards* (Public key) in Virgil services. Also the *appKey* can be used for cryptographic operations to take a part in application logic. The *appKey* generated at the time of creation application and have to be saved in secure place. 
* **accessToken** is a unique string value that provides an authenticated secure access to the Virgil services and is passed with each API call. The *accessToken* also allows the API to associate your app’s requests with your Virgil developer’s account. 

## Connecting to Virgil
Before you can make use of any Virgil services features in your app, you must first initialize ```VirgilClient``` class. You use the ```VirgilClient``` object to get access to Create, Revoke and Search a *Virgil Cards* (Public keys). 

### Initializing an API Client

To create an instance of a *VirgilClient* class, just call its constructor with your application *accessToken* you generated on developer deshboard.

Namespace: ```Virgil.SDK.Client```

```csharp
var client = new VirgilClient("[YOUR_ACCESS_TOKEN_HERE]");
```

you also can customize initialization using your own parameters

```csharp
var parameters = new VirgilClientParams("[YOUR_ACCESS_TOKEN_HERE]");

parameters.SetCardsServiceAddress("https://cards.virgilsecurity.com");
parameters.SetReadOnlyCardsServiceAddress("https://cards-ro.virgilsecurity.com");
parameters.SetIdentityServiceAddress("https://identity.virgilsecurity.com");

var client = new VirgilClient(parameters);
```

### Initializing Crypto
The *VirgilCrypto* class provides cryptographic operations in applications, such as hashing, signature generation and verification, and encryption and decryption.

Namespace: ```Virgil.SDK.Cryptography```

```csharp
var crypto = new VirgilCrypto();
```

## Creating a Virgil Card

A *Virgil Card* is the main entity of the Virgil services, it includes the information about the user and his public key. The *Virgil Card* identifies the user/device by one of his types. 

### Collect an App Credentials
Collect an *appID* and *appKey* for your app. These parametes are required to create a Virgil Card in your app scope. 

```csharp
var appID = "[YOUR_APP_ID_HERE]";
var appKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]";
var appKeyData = File.ReadAllBytes("[YOUR_APP_KEY_PATH_HERE]");
var appKey = crypto.ImportKey(appKeyData, appKeyPassword);
```

### Generate a new Keys
Generate a new Public/Private keypair using *VirgilCrypto* class. 

```csharp
var privateKey = crypto.GenerateKey();
// export Public key from the Private key
var exportedPublicKey = crypto.ExportPublicKey(privateKey);
```
### Prepare a Creation Request

```csharp
var creationRequest = CreateCardRequest.Create("Alice", "username", exportedPublicKey);
```

then, you need to calculate fingerprint of request that will be used in the future as Virgil Card ID. 
```csharp
var fingerprint = crypto.CalculateFingerprint(creationRequest.Snapshot);
```

```csharp
var ownerSignature = crypto.SignFingerprint(fingerprint, privateKey);
var appSignature = crypto.SignFingerprint(fingerprint, appKey);

request.AppendSignature(fingerprint, ownerSignature);
request.AppendSignature(appID, appSignature);

var card = await client.RegisterCardAsync(request);
```

## Cryptography
### Generate Keys
The following code sample illustrates keypair generation. The default algorithm is ed25519
```csharp
 var keypair = crypto.GenerateKey();
```

### Import and Export Keys
You can export and import your public/private keys to/from supported wire representation
```csharp
 var exportedPrivateKey = crypto.ExportPrivateKey(keypair.PrivateKey);
 var exportedPublicKey = crypto.ExportPublicKey(keypair.PublicKey);
 ...
 
 var privateKey = crypto.ImportKey(exportedPrivateKey);
 var publicKey = crypto.ImportPublicKey(exportedPublicKey)
```

### Encrypt Data
Data encryption using ECIES scheme with AES-GCM. You can encrypt either stream or a byte array
There also can be more than one recipient
```csharp
 var plaintext = new byte[100]
 var ciphertext = crypto.Encrypt(plaintext, alice.PublicKey, bob.PublicKey)
 
  using (FileStream in = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
  using (FileStream out = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None)) 
        {
         crypto.Encrypt(in, out, alice.PublicKey, bob.PublicKey)
        }
 
```

### Decrypt Data
You can decrypt either stream or a byte array using tour private key
```csharp
 var ciphertext = new byte[100]{...}
 var plaintext = crypto.Decrypt(ciphertext, alice.PrivateKey)
 
  using (FileStream in = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
  using (FileStream out = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.None)) 
        {
         crypto.Decrypt(in, out, alice.PrivateKey)
        }
 
```

### Sign Data
Sign the SHA-384 fingerprint of either stream or a byte array using your private key
```csharp
 var signature = crypto.Sign(data, alice.PrivateKey);
 using (FileStream in = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
 {
  signature = crypto.Sign(in, alice.PrivateKey);
 }
 
```

### Verify Digital Signature
Verify the signature of the SHA-384 fingerprint of either stream or a byte array using public key
```csharp
 var verifyResult = crypto.Verify(data, alice.PublicKey);

 using (FileStream in = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
 {
  verifyResult = crypto.Verify(in, alice.PublicKey);
 }

```
### Calculate Fingerprint
The default Fingerprint algorithm is SHA-256. The hash is then converted to HEX
```csharp
 var fingerprint = crypto.CalculateFingerprint(data)
```

## Release Notes
 - Please read the latest note here: [https://github.com/VirgilSecurity/virgil-sdk-net/releases](https://github.com/VirgilSecurity/virgil-sdk-net/releases)

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>

