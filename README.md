# Virgil Security .NET/C# SDK [![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

[Installation](#installation) | [Encryption Example](#encryption-example) | [Initialization](#initialization) | [Documentation](#documentation) | [Support](#support)

[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few simple steps you can encrypt communication, securely store data, provide passwordless login, and ensure data integrity.

For a full overview head over to our Javascript [Get Started](#_getstarted) guides.

## Installation

The Virgil .NET SDK is provided as a package named *Virgil.SDK*. The package is distributed via NuGet package management system. 

The package is available for .NET Framework 4.5 and newer.

Installing the package

1. Use NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console)
2. Run `PM> Install-Package Virgil.SDK`

__Next:__ [Get Started with the .NET/C# SDK](#_getstarted).

## Encryption Example

Virgil Security makes it super easy to add encryption to any application. With our SDK you create a public [__Virgil Card__](#link_to_virgil_cards_guide) for every one of your users and devices. With these in place you can easily encrypt any data in the client.

```csharp
// find Alice's card(s)
var aliceCards = await virgil.Cards.FindAsync("alice");

// encrypt the message using Alice's cards
var message = "Hello Alice!";
var encryptedMessage = aliceCards.Encrypt(message);

// transmit the message with your preferred technology
this.TransmitMessage(encryptedMessage.ToString(StringEncoding.Base64));
```

The receiving user then uses their stored __private key__ to decrypt the message.


```csharp
// load Alice's Key from storage.
var aliceKey = virgil.Keys.Load("alice_key_1", "mypassword");

// decrypt the message using the key 
var originalMessage = aliceKey.Decrypt(transferData).ToString();
```

__Next:__ To [get you properly started](#_encryption_get_started_guide) you'll need to know how to create and store Virgil Cards. Our [Get Started guide](#_encryption_get_started_guide) will get you there all the way.

__Also:__ [Encrypted communication](#_getstarted_encryption) is just one of the few things our SDK can do. Have a look at our guides on  [Encrypted Storage](#_getstarted_storage), [Data Integrity](#_getstarted_data_integrity) and [Passwordless Login](#_getstarted_passwordless_login) for more information.


## Initialization

To use this SDK you need to [sign up for an account](https://developer.virgilsecurity.com/account/signup) and create your first __application__. Make sure to save the __app id__, __private key__ and it's __password__. After this, create an __application token__ for your application to make authenticated requests from your clients.

To initialize the SDK on the client side you will only need the __access token__ you created.

```csharp
var virgil = new VirgilApi("[ACCESS_TOKEN]");
```

> __Note:__ this client will have [limited capabilities](#guide_on_client_access_permissions). For example, it will be able to generate new __Cards__ but it will need a server-side client to transmit these to Virgil.

To initialize the SDK on the server side we will need the __access token__, __app id__ and the __App Key__ you created on the [Developer Dashboard](https://developer.virgilsecurity.com/).

```javascript
var context = new VirgilApiContext
{
    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
    Credentials = new AppCredentials
    {
        AppId = "[YOUR_APP_ID_HERE]",
        AppKeyData = VirgilBuffer.FromFile("[YOUR_APP_KEY_PATH_HERE]"),
        AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]"
    }
};

var virgil = new VirgilApi(context);
```

Next: [Learn more about our the different ways of initializing the Javascript SDK](#_guide_initialization) in our documentation.

## Documentation

Virgil Security has a powerful set of APIs, and the documentation is there to get you started today.

* [Get Started](#get_started_root) documentation
  * [Initialize the SDK](#initialize_root)
  * [Encrypted storage](#encrypted_storage)
  * [Encrypted communication](#encrypted_comms)
  * [Data integrity](#data_integrity)
  * [Passwordless login](#passwordless_login)
* [Guides](#guides_link)
  * [Virgil Cards](#guide_cards)
  * [Virgil Keys](#guide_keys)
* [Tutorials](#tutorials_link)

Alternatively, head over to our [Reference documentaton](#reference_docs) for in-depth information about every SDK method, it's arguments and return types.

## License

This library is released under the [3-clause BSD License](LICENSE).

## Support

Our developer support team is here to help you. You can find us on [Twitter](https://twitter.com/virgilsecurity) and [email](support).

[support]: mailto:support@virgilsecurity.com
[_getstarted]: https://virgilsecurity.com/docs/sdk/net/
[_getstarted_encryption]: https://virgilsecurity.com/docs/js/encrypted-communication
[_getstarted_storage]: https://virgilsecurity.com/docs/use-cases/secure-data-at-rest
[_getstarted_data_integrity]: https://virgilsecurity.com/docs/use-cases/data-verification
[_getstarted_passwordless_login]: https://virgilsecurity.com/docs/use-cases/passwordless-authentication
[_guide_initialization]: https://virgilsecurity.com/docs/sdk/net/get-started
