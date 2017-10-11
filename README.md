# Virgil Security .NET/C# SDK 
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

[Installation](#installation) | [Encryption Example](#encryption-example) | [Initialization](#initialization) | [Documentation](#documentation) | [Reference API][_reference_api] | [Support](#support)

[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few simple steps you can encrypt communication, securely store data, provide passwordless login, and ensure data integrity.

For a full overview head over to our .NET/C# [Get Started][_getstarted] guides.

## Installation

The Virgil .NET SDK is provided as a package named *Virgil.SDK*. The package is distributed via NuGet package management system. 

The package is available for .NET Framework 4.5 and newer.

Installing the package

1. Use NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console)
2. Run `PM> Install-Package Virgil.SDK`

__Next:__ [Get Started with the .NET/C# SDK][_getstarted].

## Encryption Example

Virgil Security makes it super easy to add encryption to any application. With our SDK you create a public [__Virgil Card__][_guide_virgil_cards] for every one of your users and devices. With these in place you can easily encrypt any data in the client.

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

__Next:__ To [get you properly started][_guide_encryption] you'll need to know how to create and store Virgil Cards. Our [Get Started guide][_guide_encryption] will get you there all the way.

__Also:__ [Encrypted communication][_getstarted_encryption] is just one of the few things our SDK can do. Have a look at our guides on  [Encrypted Storage][_getstarted_storage], [Data Integrity][_getstarted_data_integrity] and [Passwordless Login][_getstarted_passwordless_login] for more information.

## Initialization

To use this SDK you need to [sign up for an account](https://developer.virgilsecurity.com/account/signup) and create your first __application__. Make sure to save the __app id__, __private key__ and it's __password__. After this, create an __application token__ for your application to make authenticated requests from your clients.

To initialize the SDK on the client side you will only need the __access token__ you created.

```csharp
var virgil = new VirgilApi("[ACCESS_TOKEN]");
```

> __Note:__ this client will have limited capabilities. For example, it will be able to generate new __Cards__ but it will need a server-side client to transmit these to Virgil.

To initialize the SDK on the server side we will need the __access token__, __app id__ and the __App Key__ you created on the [Developer Dashboard](https://developer.virgilsecurity.com/).

```csharp
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

Next: [Learn more about our the different ways of initializing the .NET/C# SDK][_guide_initialization] in our documentation.

## Documentation

Virgil Security has a powerful set of APIs, and the documentation is there to get you started today.

* [Get Started](https://github.com/VirgilSecurity/virgil-sdk-net/tree/v4/documentation/get-started) documentation
  * [Initialize the SDK][_initialize_root]
  * [Encrypted storage][_getstarted_storage]
  * [Encrypted communication][_getstarted_encryption]
  * [Data integrity][_getstarted_data_integrity]
  * [Passwordless login][_getstarted_passwordless_login]
* [Guides][_guides]
  * [Virgil Cards][_guide_virgil_cards]
  * [Virgil Keys][_guide_virgil_keys]
* [Reference API][_reference_api] 

## License

This library is released under the [3-clause BSD License](LICENSE.md).

## Support

Our developer support team is here to help you. You can find us on [Twitter](https://twitter.com/virgilsecurity) and [email](support).

[support]: mailto:support@virgilsecurity.com
[_getstarted_root]: https://developer.virgilsecurity.com/docs/cs/get-started
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
