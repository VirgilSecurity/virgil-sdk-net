# Virgil Security .NET/C# SDK
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

[Installation](#installation) | [Initialization](#initialization) | [Documentation](#documentation) | [Encryption / Decryption Example](#encryption) | [Support](#support)

[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few steps you can encrypt communication, securely store data, provide passwordless login, and ensure data integrity.

To initialize and use Virgil SDK, you need to have [Virgil Developer Account](https://developer.virgilsecurity.com/account/signin).

## Installation

To install Virgil SDK package, use below guides:

1. [Install SDK for Client](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client.md#-install-sdk)
2. [Install SDK for Client (PFS)](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client-pfs.md#-install-sdk)
3. [Install SDK for Server](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/server.md#-install-sdk)


## Initialization

Be sure that you have already registered at the [Dev Portal](https://developer.virgilsecurity.com/account/signin) and created an application. If yes - you have the __app id__, the __private key__ and the __password__ to initialize SDK. After this you also need to create an __Access token__ for your application to authenticate requests from your clients.

To find the code for initializing Virgil SDK, choose the option:
1. [Initialize SDK for Client](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client.md#-initialize-sdk)
2. [Initialize SDK for Client (PFS)](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client-pfs.md#-initialize-sdk)
3. [Initialize SDK for Server](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/server.md#-initialize-sdk)

To initialize the SDK on the client side, you only need the __access token__ you created.

On the page below you can find the list of our guides and use cases with Virgil SDK.

## Documentation

Virgil Security has a powerful set of APIs, and the documentation is there to get you started today.

* [Get Started](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/get-started) documentation
  * [Encrypted storage](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/get-started/encrypted-storage.md)
  * [Encrypted communication](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/get-started/encrypted-communication.md)
  * [Data integrity](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/get-started/data-integrity.md)
  * [PFS](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/get-started/perfect-forward-secrecy.md)
* [Guides](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides)
  * [Virgil Cards](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/virgil-card)
  * [Virgil Keys](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/virgil-key)
  Encryption
  Signature

## Encryption / Decryption Example

Virgil Security makes it easy to add encryption to any application. With our SDK you create a Virgil Card for every one of your users and devices. With users' Virgil Cards, you can easily encrypt any data at the client side.
```cs
// find Alice's card(s)
var aliceCards = await virgil.Cards.FindAsync("alice");

// encrypt the message using Alice's cards
var message = "Hello Alice!";
var encryptedMessage = aliceCards.Encrypt(message);

// transmit the message with your preferred technology
this.TransmitMessage(encryptedMessage.ToString(StringEncoding.Base64));
```

The receiver uses his Virgil Private Key to decrypt the message.

```cs
// load Alice's Key from storage.
var aliceKey = virgil.Keys.Load("alice_key_1", "mypassword");

// decrypt the message using the key
var originalMessage = aliceKey.Decrypt(transferData).ToString();
```

## License

This library is released under the [3-clause BSD License](LICENSE.md).

## Support

Our developer support team is here to help you. You can find us on [Twitter](https://twitter.com/virgilsecurity) and [email](support).

[support]: mailto:support@virgilsecurity.com
[_getstarted_root]: https://github.com/VirgilSecurity/virgil-sdk-net/tree/v4/documentation/get-started
[_getstarted]: https://developer.virgilsecurity.com/docs/cs/guides
[_getstarted_encryption]: https://developer.virgilsecurity.com/docs/cs/get-started/encrypted-communication
[_getstarted_storage]: https://developer.virgilsecurity.com/docs/cs/get-started/encrypted-storage
[_getstarted_data_integrity]: https://developer.virgilsecurity.com/docs/cs/get-started/data-integrity
[_getstarted_passwordless_login]: https://developer.virgilsecurity.com/docs/cs/get-started/passwordless-authentication
[_guides]: https://developer.virgilsecurity.com/docs/cs/guides
[_guide_initialization]: https://developer.virgilsecurity.com/docs/cs/guides/settings/install-sdk
[_guide_virgil_cards]: https://developer.virgilsecurity.com/docs/cs/guides/virgil-card/creating
[_guide_virgil_keys]: https://developer.virgilsecurity.com/docs/cs/guides/virgil-key/generating
[_guide_encryption]: https://developer.virgilsecurity.com/docs/cs/guides/encryption/encrypting
[_initialize_root]: https://developer.virgilsecurity.com/docs/cs/guides/settings/initialize-sdk-on-client
[_reference_api]: http://virgilsecurity.github.io/virgil-sdk-net/
