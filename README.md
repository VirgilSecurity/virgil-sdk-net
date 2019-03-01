# Virgil Security .NET/C# SDK
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)
[![GitHub license](https://img.shields.io/badge/license-BSD%203--Clause-blue.svg)](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE)


[Introduction](#introduction) | [SDK Features](#sdk-features) | [Installation](#installation) | [Usage Examples](#usage-examples) | [Docs](#docs) | [Support](#support)

## Introduction

<a href="https://developer.virgilsecurity.com/docs"><img width="230px" src="https://cdn.virgilsecurity.com/assets/images/github/logos/virgil-logo-red.png" align="left" hspace="10" vspace="6"></a>[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few simple steps you can encrypt communication, securely store data, provide passwordless login, and ensure data integrity.

The Virgil SDK allows developers to get up and running with Virgil API quickly and add full end-to-end security to their existing digital solutions to become HIPAA and GDPR compliant and more.

## SDK Features
- communicate with [Virgil Cards Service][_cards_service]
- manage users' Public Keys
- store private keys in secure local storage
- use Virgil [Crypto library][_virgil_crypto]
- use your own Crypto



## Installation

The Virgil .NET SDK is provided as a package named Virgil.SDK. The package is distributed via [NuGet package](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package) management system.

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

## Usage Examples

Before start practicing with the usage examples be sure that the SDK is configured. Check out our [SDK configuration guides][_configure_sdk] for more information.

#### Generate and publish user's Cards with Public Keys inside on Cards Service
Use the following lines of code to create and publish a user's Card with Public Key inside on Virgil Cards Service:

```cs
using Virgil.Crypto;
using Virgil.SDK;

var crypto = new VirgilCrypto();

// generate a key pair
var keyPair = crypto.GenerateKeys();

// save a private key into key storage
privateKeyStorage.Store(keyPair.PrivateKey, "Alice");

// publish user's card on the Cards Service
var card = await cardManager.PublishCardAsync(
	new CardParams()
	{
	    PublicKey = keyPair.PublicKey,
	    PrivateKey = keyPair.PrivateKey,
	});
```

#### Sign then encrypt data

Virgil SDK lets you use a user's Private key and his or her Cards to sign, then encrypt any kind of data.

In the following example, we load a Private Key from a customized Key Storage and get recipient's Card from the Virgil Cards Services. Recipient's Card contains a Public Key on which we will encrypt the data and verify a signature.

```cs
using Virgil.CryptoImpl;
using Virgil.SDK;

// prepare a message
var messageToEncrypt = "Hello, Bob!";
var dataToEncrypt = Bytes.FromString(messageToEncrypt);

// prepare a user's private key
var (alicePrivateKey, alicePrivateKeyAdditionalData) = privateKeyStorage.Load("Alice");

// using cardManager search for Bob's cards on Cards Service
var cards = await cardManager.SearchCardsAsync("Bob");
var bobRelevantCardsPublicKeys = cards.Select(x => x.PublicKey).ToArray();

// sign a message with a private key then encrypt using Bob's public keys
var encryptedData = crypto.SignThenEncrypt(dataToEncrypt, alicePrivateKey, bobRelevantCardsPublicKeys);
```

#### Decrypt then verify data
Once the Users receive the signed and encrypted message, they can decrypt it with their own Private Key and verify signature with a Sender's Card:

```cs
using Virgil.CryptoImpl;
using Virgil.SDK;

// prepare a user's private key
var (bobPrivateKey, bobPrivateKeyAdditionalData) = privateKeyStorage.Load("Bob");

// using cardManager search for Alice's cards on Cards Service
var cards = await cardManager.SearchCardsAsync("Alice");
var aliceRelevantCardsPublicKeys = cards.Select(x => x.PublicKey).ToArray();

// decrypt with a private key and verify using one of Alice's public keys
var decryptedData = crypto.DecryptThenVerify(encryptedData, bobPrivateKey, aliceRelevantCardsPublicKeys);
```

## Docs
Virgil Security has a powerful set of APIs, and the documentation below can get you started today.

In order to use the Virgil SDK with your application, you will need to first configure your application. By default, the SDK will attempt to look for Virgil-specific settings in your application but you can change it during SDK configuration.

* [Configure the SDK][_configure_sdk] documentation
  * [Setup authentication][_setup_authentication] to make API calls to Virgil Services
  * [Setup Card Manager][_card_manager] to manage user's Public Keys
  * [Setup Card Verifier][_card_verifier] to verify signatures inside of user's Card
  * [Setup Key storage][_key_storage] to store Private Keys
  * [Setup your own Crypto library][_own_crypto] inside of the SDK
* [More usage examples][_more_examples]
  * [Create & publish a Card][_create_card] that has a Public Key on Virgil Cards Service
  * [Search user's Card by user's identity][_search_card]
  * [Get user's Card by its ID][_get_card]
  * [Use Card for crypto operations][_use_card]
* [Reference API][_reference_api]


## License

This library is released under the [3-clause BSD License](LICENSE.md).

## Support
Our developer support team is here to help you.

You can find us on [Twitter](https://twitter.com/VirgilSecurity) or send us email support@VirgilSecurity.com.

Also, get extra help from our support team on [Slack](https://virgilsecurity.com/join-community).

[_virgil_crypto]: https://github.com/VirgilSecurity/virgil-sdk-crypto-net
[_cards_service]: https://developer.virgilsecurity.com/docs/api-reference/card-service/v5
[_use_card]: https://developer.virgilsecurity.com/docs/cs/how-to/public-key-management/v5/use-card-for-crypto-operation
[_get_card]: https://developer.virgilsecurity.com/docs/cs/how-to/public-key-management/v5/get-card
[_search_card]: https://developer.virgilsecurity.com/docs/cs/how-to/public-key-management/v5/search-card
[_create_card]: https://developer.virgilsecurity.com/docs/cs/how-to/public-key-management/v5/create-card
[_own_crypto]: https://developer.virgilsecurity.com/docs/cs/how-to/setup/v5/setup-own-crypto-library
[_key_storage]: https://developer.virgilsecurity.com/docs/cs/how-to/setup/v5/setup-key-storage
[_card_verifier]: https://developer.virgilsecurity.com/docs/cs/how-to/setup/v5/setup-card-verifier
[_card_manager]: https://developer.virgilsecurity.com/docs/cs/how-to/setup/v5/setup-card-manager
[_setup_authentication]: https://developer.virgilsecurity.com/docs/cs/how-to/setup/v5/setup-authentication
[_reference_api]: https://developer.virgilsecurity.com/docs/api-reference
[_configure_sdk]: https://developer.virgilsecurity.com/docs/how-to#sdk-configuration
[_more_examples]: https://developer.virgilsecurity.com/docs/how-to#public-key-management
