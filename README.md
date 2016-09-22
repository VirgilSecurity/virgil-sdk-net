# .NET/C# SDK Programming Guide
The major components used when building a secure .NET/C# app with Virgil Security

## Table of Contents

* [Initialization](#initialization)
* [Cards Management](#)
  * [Create a Card](#)
  * [Search for the Cards](#)
  * [Revoke a Card](#)
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

## Cards Management

### Initialization

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

