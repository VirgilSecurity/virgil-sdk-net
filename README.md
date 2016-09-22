# .NET/C# SDK Programming Guide

Welcome to the .NET SDK Programming Guide for C#. This guide is a practical introduction to creating apps for the Windows/Xamarin platform that make use of Virgil Security features. The code examples in this guide are written in C#. 

In this guide you will find code for every task you'll need to implement to create an application using Virgil Security. It also includes a description of the main classes and methods. The aim of this guide is to get you up and running quickly. You should be able to copy and paste the code provided into your own apps and use it with the minumum of changes.

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

When you register an application on the Virgil Security developer dashboard, we provide you with an appID that uniquely identifies your app in our services, for example ```a6f7b874ea69329372ad75353314d7bcacd8c0be365023dab195bcac015d6009```.

## Table of Contents

* [Management of Virgil Cards](#)
  * [Create a Virgil Card](#)
  * [Search for the Virgil Cards](#)
  * [Revoke a Virgil Card](#)
* [Cryptography](#)
  * [Generate Keys](#)
  * [Import and Export Keys](#)
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

## Management of Virgil Cards

The Virgil Security uses Public key cryptography, which allows anybody to encrypt data using your Public key. After the message is encrypted, no one can decrypt it unless someone has your Private key. Virgil Security also allows you to electronically "sign" the data with a digital signature, which other people can verify.

To make use of these features, you will first need to create a Public key for yourself and distribute it among your correspondents. 

A *Virgil Card* is the main entity of the Virgil Security services, it includes the information about the user and his Public key. The *Virgil Card* identifies the user or device. 





### Create a Virgil Card


Simply add your access token to the class builder.

```csharp
// Initialize a class which provides an API client for Cards management.
var client = new VirgilClient("%ACCESS_TOKEN%");

// Initialize a class which provides an API for cryptographic operations.
var crypto = new VirgilCrypto();
```
or you can customize initialization using your own parameters 

```csharp
var parameters = new VirgilClientParams("%ACCESS_TOKEN%");

parameters.SetCardsServiceAddress("https://cards.virgilsecurity.com");
parameters.SetReadOnlyCardsServiceAddress("https://cards-ro.virgilsecurity.com");
parameters.SetIdentityServiceAddress("https://identity.virgilsecurity.com");

var client = new VirgilClient(parameters);
```

### Create a Card

The following code sample illustrates registration of new Virgil Card in *application* scope. 

```csharp
// Generate new Public/Private keypair and export the Public key to be used for Card registration.

var privateKey = crypto.GenerateKey();
var exportedPublicKey = crypto.ExportPublicKey(privateKey.PublicKey);

// Prepare a new Card registration request.
var creationRequest = CreationRequest.Create("Alice", "username", exportedPublicKey);

// Calculate a fingerprint from request's canonical form.
var fingerprint = crypto.CalculateFingerprint(creationRequest.CanonicalForm);

// Sign a request fingerprint using bouth owner and application Private keys.
var ownerSignature = crypto.SignFingerprint(fingerprint, privateKey);
var appSignature = crypto.SignFingerprint(fingerprint, %APP_PRIVATE_KEY%);

request.AppendSignature(fingerprint, ownerSignature);
request.AppendSignature(%APP_ID%, appSignature);

var card = await client.RegisterCardAsync(request);
```

The following code sample illustrates registration of new Virgil Card in *global* scope. 

```csharp
// Generate new Public/Private keypair and export the Public key to be used for Card registration.

var privateKey = crypto.GenerateKey();
var exportedPublicKey = crypto.ExportPublicKey(privateKey.PublicKey);

// Prepare a new Card registration request.
var creationRequest = CreationRequest.CreateGlobal("alice@virgilsecurity.com", exportedPublicKey);

// Calculate a fingerprint from request's canonical form.
var fingerprint = crypto.CalculateFingerprint(creationRequest.CanonicalForm);

// Sign a request fingerprint using bouth owner and application Private keys.
var ownerSignature = crypto.SignFingerprint(fingerprint, privateKey);
request.AppendFingerprint(fingerprint, ownerSignature);

// Send the Card creation request
var creationDetails = await client.BeginGlobalCardCreationAsync(request);

// Confirm the Card creation using confirmation code received on specified email address.
var registrationDetails = await client.CompleteGlobalCardCreationAsync(request);
```

### Search for the Cards
The following code sample illustrates search for the Cards by specified criteria.

```csharp
var criteria = new SearchCardsCriteria 
{
    Identities = new [] { "Alice", "Bob" },
    IdentityType = "username",
    Scope = VirgilCardScope.Application
};

var cards = await client.SearchCardsAsync(criteria);
```


## Release Notes
 - Please read the latest note here: [https://github.com/VirgilSecurity/virgil-sdk-net/releases](https://github.com/VirgilSecurity/virgil-sdk-net/releases)

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>

