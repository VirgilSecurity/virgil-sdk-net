# Virgil Core SDK .NET/C#
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release)
[![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)
[![GitHub license](https://img.shields.io/badge/license-BSD%203--Clause-blue.svg)](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE)


[Introduction](#introduction) | [SDK Features](#sdk-features) | [Installation](#installation) | [Configure SDK](#configure-sdk) | [Usage Examples](#usage-examples) | [Docs](#docs) | [Support](#support)

## Introduction

<a href="https://developer.virgilsecurity.com/docs"><img width="230px" src="https://cdn.virgilsecurity.com/assets/images/github/logos/virgil-logo-red.png" align="left" hspace="10" vspace="6"></a> [Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few simple steps you can encrypt communications, securely store data, and ensure data integrity. Virgil Security products are available for desktop, embedded (IoT), mobile, cloud, and web applications in a variety of modern programming languages.

The Virgil Core SDK is a low-level library that allows developers to get up and running with [Virgil Cards Service API](https://developer.virgilsecurity.com/docs/platform/api-reference/cards-service/) quickly and add end-to-end security to their new or existing digital solutions.

In case you need additional security functionality for multi-device support, group chats and more, try our high-level [Virgil E3Kit framework](https://github.com/VirgilSecurity/awesome-virgil#E3Kit).

## SDK Features
- Communicate with [Virgil Cards Service](https://developer.virgilsecurity.com/docs/platform/api-reference/cards-service/)
- Manage users' public keys
- Encrypt, sign, decrypt and verify data
- Store private keys in secure local storage
- Use [Virgil Crypto Library](https://github.com/VirgilSecurity/virgil-crypto-net)

## Installation

The Virgil Core SDK .NET is provided as a package named Virgil.SDK. The package is distributed via [NuGet package](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package) management system.

  Supported Platforms:
   - .NET Standard 1.1+
   - .NET Framework 4.5+
   - .NET Core 1.0+
   - Universal Windows Platform 10
   - Windows 8.0+
   - Windows Phone 8.1+
   - Xamarin.Android 7.0+
   - Xamarin.iOS 10.0+
   - Xamarin.Mac 3.0+
   - Mono 4.6+ (OSX)

Installing the package using Package Manager Console:

```bash
Run PM> Install-Package Virgil.SDK
```

## Configure SDK

This section contains guides on how to set up Virgil Core SDK modules for authenticating users, managing Virgil Cards and storing private keys.

### Set up authentication

Set up user authentication with tokens that are based on the [JSON Web Token standard](https://jwt.io/) with some Virgil modifications.

In order to make calls to Virgil Services (for example, to publish user's Card on Virgil Cards Service), you need to have a JSON Web Token ("JWT") that contains the user's `identity`, which is a string that uniquely identifies each user in your application.

Credentials that you'll need:

|Parameter|Description|
|--- |--- |
|App ID|ID of your Application at [Virgil Dashboard](https://dashboard.virgilsecurity.com)|
|App Key ID|A unique string value that identifies your account at the Virgil developer portal|
|App Key|A Private Key that is used to sign API calls to Virgil Services. For security, you will only be shown the App Key when the key is created. Don't forget to save it in a secure location for the next step|

#### Set up JWT provider on Client side

Use these lines of code to specify which JWT generation source you prefer to use in your project:

```cs
using Virgil.SDK;

// Get generated token from server-side
Func<TokenContext, Task<string>> obtainTokenCallback = async (ctx) =>
{
     var jwtFromServer = await AuthenticatedQueryToServerSide(tokenContext);
     return jwtFromServer;
};

// Setup AccessTokenProvider
var accessTokenProvider = new CallbackJwtProvider(obtainTokenCallback);
```

#### Generate JWT on Server side

Next, you'll need to set up the `JwtGenerator` and generate a JWT using the Virgil SDK.

Here is an example of how to generate a JWT:

```cs
using Virgil.Crypto;
using Virgil.SDK;

// App Key (you got this Key at Virgil Dashboard)
var appKeyBase64 = "MIGhMF0GCSqGSIb3DQEFDTBQMC8GCSqGSIb3DQEFDDAiBBC7Sg/DbNzhJ/uakTvafUMoAgIUtzAKBggqhkiG9w0CCjAdBglghkgBZQMEASoEEDunQ1yhWZoKaLaDFgjpxRwEQAFdbC8e6103lJrUhY9ahyUA8+4rTJKZCmdTlCDPvoWH/5N5kxbOvTtbxtxevI421z3gRbjAtoWkfWraSLD6gj0=";
var privateKeyData = Bytes.FromString(appKeyBase64, StringEncoding.BASE64);

// Crypto library imports a private key into a necessary format
var crypto = new VirgilCrypto();
var appKey = crypto.ImportPrivateKey(privateKeyData, appKeyPassword);

//  initialize accessTokenSigner that signs users JWTs
var accessTokenSigner = new VirgilAccessTokenSigner();

// use your App Credentials you got at Virgil Dashboard:
var appId = "be00e10e4e1f4bf58f9b4dc85d79c77a"; // App ID
var appKeyId = "70b447e321f3a0fd"; // App Key ID
var ttl = TimeSpan.FromHours(1); // 1 hour (JWT's lifetime)

// setup JWT generator with necessary parameters:
var jwtGenerator = new JwtGenerator(appId, appKey, appKeyId, ttl, accessTokenSigner);

// generate JWT for a user
// remember that you must provide each user with his unique JWT
// each JWT contains unique user's identity (in this case - Alice)
// identity can be any value: name, email, some id etc.
var identity = "Alice";
var aliceJwt = jwtGenerator.GenerateToken(identity);

// as result you get users JWT, it looks like this: "eyJraWQiOiI3MGI0NDdlMzIxZjNhMGZkIiwidHlwIjoiSldUIiwiYWxnIjoiVkVEUzUxMiIsImN0eSI6InZpcmdpbC1qd3Q7dj0xIn0.eyJleHAiOjE1MTg2OTg5MTcsImlzcyI6InZpcmdpbC1iZTAwZTEwZTRlMWY0YmY1OGY5YjRkYzg1ZDc5Yzc3YSIsInN1YiI6ImlkZW50aXR5LUFsaWNlIiwiaWF0IjoxNTE4NjEyNTE3fQ.MFEwDQYJYIZIAWUDBAIDBQAEQP4Yo3yjmt8WWJ5mqs3Yrqc_VzG6nBtrW2KIjP-kxiIJL_7Wv0pqty7PDbDoGhkX8CJa6UOdyn3rBWRvMK7p7Ak"
// you can provide users with JWT at registration or authorization steps
// Send a JWT to client-side
var jwtString = aliceJwt.ToString();
```

For this subsection we've created a sample backend that demonstrates how you can set up your backend to generate the JWTs. To set up and run the sample backend locally, head over to your GitHub repo of choice:

[Node.js](https://github.com/VirgilSecurity/sample-backend-nodejs) | [Golang](https://github.com/VirgilSecurity/sample-backend-go) | [PHP](https://github.com/VirgilSecurity/sample-backend-php) | [Java](https://github.com/VirgilSecurity/sample-backend-java) | [Python](https://github.com/VirgilSecurity/virgil-sdk-python/tree/master#sample-backend-for-jwt-generation)
 and follow the instructions in README.
 
### Set up Card Verifier

Virgil Card Verifier helps you automatically verify signatures of a user's Card, for example when you get a Card from Virgil Cards Service.

By default, `VirgilCardVerifier` verifies only two signatures - those of a Card owner and Virgil Cards Service.

Set up `VirgilCardVerifier` with the following lines of code:

```cs
using Virgil.Crypto;
using Virgil.SDK;

// initialize Crypto library
var cardCrypto = new VirgilCardCrypto();

var yourBackendVerifierCredentials = new VerifierCredentials()
{
	Signer = "YOUR_BACKEND",
	PublicKeyBase64 = publicKeyStr
};

 var yourBackendWhitelist = new Whitelist()
{
    VerifiersCredentials = new List<VerifierCredentials>()
    {
        yourBackendVerifierCredentials
    }
};

var verifier = new VirgilCardVerifier(cardCrypto)
            {
                Whitelists = new List<Whitelist>() { yourBackendWhitelist }
            };
```

### Set up Card Manager

This subsection shows how to set up a Card Manager module to help you manage users' public keys.

With Card Manager you can:
- specify an access Token (JWT) Provider.
- specify a Card Verifier used to verify signatures of your users, your App Server, Virgil Services (optional).

Use the following lines of code to set up the Card Manager:

```cs

// initialize cardManager and specify accessTokenProvider, cardVerifier
var cardManagerParams = new CardManagerParams()
{
    CardCrypto = cardCrypto,
    AccessTokenProvider = accessTokenProvider,
    Verifier = verifier
};

   var cardManager = new CardManager(cardManagerParams);
```

## Usage Examples

Before you start practicing with the usage examples, make sure that the SDK is configured. See the [Configure SDK](#configure-sdk) section for more information.

### Generate and publish Virgil Cards at Cards Service

Use the following lines of code to create a user's Card with a public key inside and publish it at Virgil Cards Service:

```cs
using Virgil.Crypto;
using Virgil.SDK;

var crypto = new VirgilCrypto();

// generate a key pair
var keyPair = crypto.GenerateKeys();

// save Alice private key into key storage
privateKeyStorage.Store(keyPair.PrivateKey, "Alice");

// create and publish user's card with identity Alice on the Cards Service
var card = await cardManager.PublishCardAsync(
    new CardParams()
    {
        PublicKey = keyPair.PublicKey,
        PrivateKey = keyPair.PrivateKey,
    });
```

### Sign then encrypt data

Virgil Core SDK allows you to use a user's private key and their Virgil Cards to sign and encrypt any kind of data.

In the following example, we load a private key from a customized key storage and get recipient's Card from the Virgil Cards Service. Recipient's Card contains a public key which we will use to encrypt the data and verify a signature.

```cs
using Virgil.Crypto;
using Virgil.SDK;

// prepare a message
var messageToEncrypt = "Hello, Bob!";
var dataToEncrypt = Encoding.UTF8.GetBytes(messageToEncrypt);

// load a private key from a device storage
var (alicePrivateKey, alicePrivateKeyAdditionalData) = privateKeyStorage.Load("Alice");

// using cardManager search for Bob's cards on Cards Service
var cards = await cardManager.SearchCardsAsync("Bob");
var bobRelevantCardsPublicKeys = cards.Select(x => x.PublicKey).ToArray();

// sign a message with a private key then encrypt using Bob's public keys
var encryptedData = crypto.SignThenEncrypt(dataToEncrypt, alicePrivateKey, bobRelevantCardsPublicKeys);
```

### Decrypt data and verify signature

Once the user receives the signed and encrypted message, they can decrypt it with their own private key and verify the signature with the sender's Card:

```cs
using Virgil.Crypto;
using Virgil.SDK;

// load a private key from a device storage
var (bobPrivateKey, bobPrivateKeyAdditionalData) = privateKeyStorage.Load("Bob");

// using cardManager search for Alice's cards on Cards Service
var cards = await cardManager.SearchCardsAsync("Alice");
var aliceRelevantCardsPublicKeys = cards.Select(x => x.PublicKey).ToArray();

// decrypt with a private key and verify using one of Alice's public keys
var decryptedData = crypto.DecryptThenVerify(encryptedData, bobPrivateKey, aliceRelevantCardsPublicKeys);
```

### Get Card by its ID

Use the following lines of code to get a user's card from Virgil Cloud by its ID:

```cs
using Virgil.SDK;

// using cardManager get a user's card from the Cards Service
var card = await cardManager.GetCardAsync("f4bf9f7fcbedaba0392f108c59d8f4a38b3838efb64877380171b54475c2ade8");
```

### Get Card by user's identity

For a single user, use the following lines of code to get a user's Card by a user's `identity`:

```cs
using Virgil.SDK;

// using cardManager search for user's cards on Cards Service
var cards = await cardManager.SearchCardsAsync("Bob");
```

## Docs

Virgil Security has a powerful set of APIs, and the [Developer Documentation](https://developer.virgilsecurity.com/) can get you started today.

## License

This library is released under the [3-clause BSD License](LICENSE).

## Support

Our developer support team is here to help you. Find out more information on our [Help Center](https://help.virgilsecurity.com/).

You can find us on [Twitter](https://twitter.com/VirgilSecurity) or send us email support@VirgilSecurity.com.

Also, get extra help from our support team on [Slack](https://virgilsecurity.com/join-community).


