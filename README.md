# Virgil Security .NET/C# SDK [![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)


## Table of Conents

* [Overview](#overview)
* [Introduction](#introduction)
* [Getting Started](#getting-started.md)
* [Programming Guide](#programming-guide)
  * [Cards Management](#)
    * [Initialization](#)
    * [Registration a new Cards](#)
    * [Search for the Cards](#)
    * [Revocation Cards](#)
  * [Cryptography](#)
    * [Key Generation](#)
    * [Importing and Exporting Keys](#)
    * [Encrypting Data](#)
    * [Decrypting Data](#)
    * [Signing Data](#)
    * [Verifying Digital Signature](#)
    * [Calculate Fingerprint](#)
  * [Key Storage](#)
  * [High Level](#)
* [Release Notes](#)
* [License](#)
* [Contacts](#)

## Programming Guide
The major components used when building a secure .NET/C# app with Virgil Security

### Cards Management

#### Initialization

Simply add your access token to the class builder.

```csharp
// Initialize a class which provides an API client for Cards management.
var client = new VirgilClient("%ACCESS_TOKEN%");
```
or you can customize initialization using your own parameters 

```csharp
var parameters = new VirgilClientParams("%ACCESS_TOKEN%");

parameters.SetCardsServiceAddress("https://cards.virgilsecurity.com");
parameters.SetReadOnlyCardsServiceAddress("https://cards-ro.virgilsecurity.com");
parameters.SetIdentityServiceAddress("https://identity.virgilsecurity.com");

var client = new VirgilClient(parameters);
```

#### Registration a new Cards

The following code sample illustrates registration a new Virgil Card in *application* scope. 

```csharp
// Initialize a class which provides an API for cryptographic operations.
var crypto = new VirgilCrypto();

// Generate new Public/Private keypair and export the Public key to be used for Card registration.

var privateKey = crypto.GenerateKey();
var exportedPublicKey = crypto.ExportPublicKey(privateKey.PublicKey);

// Prepare a new Card registration request.

var registrationRequest = new RegistrationRequest("Alice", "username", exportedPublicKey);

// Calculate a fingerprint from request's canonical form.

var fingerprint = crypto.CalculateFingerprint(registrationRequest.CanonicalForm);

// Sign a request fingerprint using bouth owner and application Private keys. 

var ownerSignature = crypto.Sign(fingerprint, privateKey);
var appSignature = crypto.Sign(fingerprint, %APP_PRIVATE_KEY%);

request.SetOwnerSignature(fingerprint, ownerSignature);
request.SetApplicationSignature(%APP_ID%, appSignature);

var card = await client.RegisterCardAsync(request);
```

The following code sample illustrates registration a new Virgil Card in *global* scope. 

```csharp
// Initialize a class which provides an API for cryptographic operations.
var crypto = new VirgilCrypto();

// Generate new Public/Private keypair and export the Public key to be used for Card registration.

var privateKey = crypto.GenerateKey();
var exportedPublicKey = crypto.ExportPublicKey(privateKey.PublicKey);

// Prepare a new Card registration request.

var registrationRequest = new GlobalRegistrationRequest("alice@virgilsecurity.com", exportedPublicKey);

// Calculate a fingerprint from request's canonical form.

var fingerprint = crypto.CalculateFingerprint(registrationRequest.CanonicalForm);

// Sign a request fingerprint using bouth owner's Private key. 

var ownerSignature = crypto.Sign(fingerprint, privateKey);

request.SetOwnerSignature(fingerprint, ownerSignature);

// Send the Card registration request

var registrationDetails = await client.BeginGlobalCardRegisterationAsync(request);

// Confirm the Card registration using confirmation code received on specified email address.

var registrationDetails = await client.BeginGlobalCardRegisterationAsync(request);
```



## Release Notes
 - Please read the latest note here: [https://github.com/VirgilSecurity/virgil-sdk-net/releases](https://github.com/VirgilSecurity/virgil-sdk-net/releases)

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>

