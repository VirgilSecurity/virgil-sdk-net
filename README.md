# .NET/C# SDK Programming Guide

Welcome to the .NET SDK Programming Guide for C#. This guide is a practical introduction to creating apps for the Windows/Xamarin platform that make use of Virgil Security features. The code examples in this guide are written in C#. 

In this guide you will find code for every task you need to implement in order to create an application using Virgil Security. It also includes a description of the main classes and methods. The aim of this guide is to get you up and running quickly. You should be able to copy and paste the code provided into your own apps and use it with minumal changes.

## Table of Contents

* [Setting up your project](#setting-up-your-project)
* [User and App Credentials](#user-and-app-credentials)
* [Creating a Virgil Card](#creating-a-virgil-card)
* [Search for Virgil Cards](#search-for-virgil-cards)
* [Validating Virgil Cards](#validating-virgil-cards)
* [Revoking a Virgil Card](#revoking-a-virgil-card)
* [Operations with Crypto Keys](#operations-with-crypto-keys)
  * [Generate Keys](#generate-keys)
  * [Import and Export Keys](#import-and-export-keys)
* [Encryption and Decryption](#encryption-and-decryption)
  * [Encrypt Data](#encrypt-data)
  * [Decrypt Data](#decrypt-data)
* [Generating and Verifying Signatures](#generating-and-verifying-signatures)
  * [Generating a Signature](#generating-a-signature)
  * [Verifying a Signature](#verifying-a-signature)
* [Fingerprint Generation](#fingerprint-generation)
* [Release Notes](#release-notes)

## Setting up your project

The Virgil SDK is provided as a package named *Virgil.SDK*. The package is distributed via NuGet package management system.

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

When you register an application on the Virgil developer's [dashboard](https://developer.virgilsecurity.com/dashboard), we provide you with an *appID*, *appKey* and *accessToken*.

* **appID** uniquely identifies your application in our services, it is also used to identify the Public key generated in a pair with *appKey*, for example: ```af6799a2f26376731abb9abf32b5f2ac0933013f42628498adb6b12702df1a87```
* **appKey** is a Private key that is used to perform creation and revocation of *Virgil Cards* (Public key) in Virgil services. Also the *appKey* can be used for cryptographic operations to take part in application logic. The *appKey* is generated at the time of creation application and has to be saved in secure place. 
* **accessToken** is a unique string value that provides an authenticated secure access to the Virgil services and is passed with each API call. The *accessToken* also allows the API to associate your app’s requests with your Virgil developer’s account. 

## Connecting to Virgil
Before you can use any Virgil services features in your app, you must first initialize ```VirgilClient``` class. You use the ```VirgilClient``` object to get access to Create, Revoke and Search for *Virgil Cards* (Public keys). 

### Initializing an API Client

To create an instance of *VirgilClient* class, just call its constructor with your application's *accessToken* which you generated on developer's deshboard.

Namespace: ```Virgil.SDK.Client```

```csharp
var client = new VirgilClient("[YOUR_ACCESS_TOKEN_HERE]");
```

you can also customize initialization using your own parameters

```csharp
var parameters = new VirgilClientParams("[YOUR_ACCESS_TOKEN_HERE]");

parameters.SetCardsServiceAddress("https://cards.virgilsecurity.com");
parameters.SetReadCardsServiceAddress("https://cards-ro.virgilsecurity.com");
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

Collect an *appID* and *appKey* for your app. These parametes are required to create a Virgil Card in your app scope. 

```csharp
var appID = "[YOUR_APP_ID_HERE]";
var appKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]";
var appKeyData = File.ReadAllBytes("[YOUR_APP_KEY_PATH_HERE]");

var appKey = crypto.ImportPrivateKey(appKeyData, appKeyPassword);
```
Generate a new Public/Private keypair using *VirgilCrypto* class. 

```csharp
var aliceKeys = crypto.GenerateKeys();
```
Prepare request
```csharp
var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
var createCardRequest = new CreateCardRequest("alice", "username", exportedPublicKey);
```

then, use *RequestSigner* class to sign request with owner and app keys.

```csharp
var requestSigner = new RequestSigner(crypto);

requestSigner.SelfSign(createCardRequest, aliceKeys.PrivateKey);
requestSigner.AuthoritySign(createCardRequest, appID, appKey);
```
Publish a Virgil Card
```csharp
var aliceCard = await client.CreateCardAsync(createCardRequest);
```

## Search for Virgil Cards
Performs the `Virgil Card`s search by criteria:
- the *Identities* request parameter is mandatory;
- the *IdentityType* is optional and specifies the *IdentityType* of a `Virgil Card`s to be found;
- the *Scope* optional request parameter specifies the scope to perform search on. Either 'global' or 'application'. The default value is 'application';

```csharp
var client = new VirgilClient("[YOUR_ACCESS_TOKEN_HERE]");
 
var criteria = SearchCriteria.ByIdentities("alice", "bob");
var cards = await client.SearchCardsAsync(criteria);
```
## Validating Virgil Cards
This sample uses *built-in* ```CardValidator``` to validate cards. By default ```CardValidator``` validates only *Cards Service* signature. 

```csharp
// Initialize crypto API
var crypto = new VirgilCrypto();

var validator = new CardValidator(crypto);

// Your can also add another Public Key for verification.
// validator.AddVerifier("[HERE_VERIFIER_CARD_ID]", [HERE_VERIFIER_PUBLIC_KEY]);

// Initialize service client
var client = new VirgilClient("[YOUR_ACCESS_TOKEN_HERE]");
client.SetCardValidator(validator);

try
{
    var criteria = SearchCriteria.ByIdentities("alice", "bob");
    var cards = await client.SearchCardsAsync(criteria);
}
catch (CardValidationException ex)
{
    // ex.InvalidCards
}
```

## Revoking a Virgil Card
Initialize required components.
```csharp
var client = new VirgilClient("[YOUR_ACCESS_TOKEN_HERE]");
var crypto = new VirgilCrypto();

var requestSigner = new RequestSigner(crypto);
```

Collect *App* credentials 
```csharp
var appID = "[YOUR_APP_ID_HERE]";
var appKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]";
var appKeyData = File.ReadAllBytes("[YOUR_APP_KEY_PATH_HERE]");

var appKey = crypto.ImportPrivateKey(appKeyData, appKeyPassword);
```

Prepare revocation request
```csharp
var cardId = "[YOUR_CARD_ID_HERE]";

var revokeRequest = new RevokeCardRequest(cardId, RevocationReason.Unspecified);
requestSigner.AuthoritySign(revokeRequest, appID, appKey);

await client.RevokeCardAsync(revokeRequest);
```

## Operations with Crypto Keys

### Generate Keys
The following code sample illustrates keypair generation. The default algorithm is ed25519

```csharp
 var aliceKeys = crypto.GenerateKeys();
```

### Import and Export Keys
You can export and import your Public/Private keys to/from supported wire representation.

To export Public/Private keys, simply call one of the Export methods:

```csharp
 var exportedPrivateKey = crypto.ExportPrivateKey(aliceKeys.PrivateKey);
 var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
```
 
 To import Public/Private keys, simply call one of the Import methods:
 
 ```csharp
 var privateKey = crypto.ImportPrivateKey(exportedPrivateKey);
 var publicKey = crypto.ImportPublicKey(exportedPublicKey);
```

## Encryption and Decryption

Initialize Crypto API and generate keypair.
```csharp
var crypto = new VirgilCrypto();
```

### Encrypt Data
Data encryption using ECIES scheme with AES-GCM. You can encrypt either stream or a byte array.
There also can be more than one recipient

*Byte Array*
```csharp
var aliceKeys = crypto.GenerateKeys();

var plaintext = Encoding.UTF8.GetBytes("Hello Bob!");
var cipherData = crypto.Encrypt(plaintext, aliceKeys.PublicKey);
```

*Stream*
```csharp 
using (var inputStream = new FileStream("[YOUR_FILE_PATH_HERE]", FileMode.Open))
using (var cipherStream = new FileStream("[YOUR_ENCRYPTED_FILE_PATH_HERE]", FileMode.Create))
{
    crypto.Encrypt(inputStream, cipherStream, aliceKeys.PublicKey);
}
```

### Decrypt Data
You can decrypt either stream or a byte array using your private key

*Byte Array*
```csharp
crypto.Decrypt(cipherData, aliceKeys.PrivateKey);
```

 *Stream*
```csharp 
using (var cipherStream = new FileStream("[YOUR_ENCRYPTED_FILE_PATH_HERE]", FileMode.Open))
using (var resultStream = new FileStream("[YOUR_DECRYPTED_FILE_PATH_HERE]", FileMode.Create))
{
    crypto.Decrypt(cipherStream, resultStream, aliceKeys.PrivateKey);
}
```

## Generating and Verifying Signatures
This section walks you through the steps necessary to use the *VirgilCrypto* to generate a digital signature for data and to verify that a signature is authentic. 

Generate a new Public/Private keypair and *data* to be signed.

```csharp
var alice = crypto.GenerateKeys();

// The data to be signed with alice's Private key
var data = Encoding.UTF8.GetBytes("Hello Bob, How are you?");
```

### Generating a Signature

Sign the SHA-384 fingerprint of either stream or a byte array using your private key. To generate the signature, simply call one of the sign methods:

*Byte Array*
```csharp
var signature = crypto.Sign(data, alice.PrivateKey);
```
*Stream*
```csharp
var fileStream = File.Open("[YOUR_FILE_PATH_HERE]", FileMode.Open, FileAccess.Read, FileShare.None);
using (fileStream)
{
    var signature = crypto.Sign(inputStream, alice.PrivateKey);
}
```
### Verifying a Signature

Verify the signature of the SHA-384 fingerprint of either stream or a byte array using Public key. The signature can now be verified by calling the verify method:

*Byte Array*

```csharp
 var isValid = crypto.Verify(data, signature, alice.PublicKey);
 ```
 
 *Stream*
 
 ```csharp
var fileStream = File.Open("[YOUR_FILE_PATH_HERE]", FileMode.Open, FileAccess.Read, FileShare.None);
using (fileStream)
{
    var isValid = crypto.Verify(fileStream, signature, alice.PublicKey);
}
```
## Fingerprint Generation
The default Fingerprint algorithm is SHA-256. The hash is then converted to HEX
```csharp
var fingerprint = crypto.CalculateFingerprint(content);
```

## Release Notes
 - Please read the latest note here: [https://github.com/VirgilSecurity/virgil-sdk-net/releases](https://github.com/VirgilSecurity/virgil-sdk-net/releases)
