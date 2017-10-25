# Virgil Security .NET/C# SDK
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

[Installation](#installation) | [Initialization](#initialization) | [Encryption / Decryption Example](#encryption) | [Documentation](#documentation) | [Support](#support)

[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few steps, you can encrypt communication, securely store data, provide passwordless authentication, and ensure data integrity.

To initialize and use Virgil SDK, you need to have [Developer Account](https://developer.virgilsecurity.com/account/signin).

## Installation

The package is available for .NET Framework 4.5 and newer.

Installing the package using Package Manager Console

```
PM> Install-Package Virgil.SDK
```

For more details about Nuget Package Manager installation take a look at [this guide](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package).

## Initialization

Be sure that you have already registered at the [Dev Portal](https://developer.virgilsecurity.com/account/signin) and created your application.

To initialize the SDK at the __Client Side__ you need only the __Access Token__ created for a client at [Dev Portal](https://developer.virgilsecurity.com/account/signin). The Access Token helps to authenticate client's requests.

```csharp
var virgil = new VirgilApi("[ACCESS_TOKEN]");
```


To initialize the SDK at the __Server Side__ you need the application credentials (__Access Token__, __App ID__, __App Key__ and __App Key Password__) you got during Application registration at the [Dev Portal](https://developer.virgilsecurity.com/account/signin).

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


## Encryption / Decryption Example

Virgil Security simplifies adding encryption to any application. With our SDK you may create unique Virgil Cards for your all users and devices. With users' Virgil Cards, you can easily encrypt any data at Client Side.

```cs
// find Alice's card(s) at Virgil Services
var aliceCards = await virgil.Cards.FindAsync("alice");

// encrypt the message using Alice's Virgil Cards
var message = "Hello Alice!";
var encryptedMessage = aliceCards.Encrypt(message);

// transmit the message with your preferred technology to Alice
this.TransmitMessage(encryptedMessage.ToString(StringEncoding.Base64));
```

Alice uses her Virgil Private Key to decrypt the encrypted message.

```cs
// load Alice's Key from local storage.
var aliceKey = virgil.Keys.Load("alice_key_1", "mypassword");

// decrypt the message using the Alice Virgil Key
var originalMessage = aliceKey.Decrypt(transferData).ToString();
```

__Next:__ On the page below you can find configuration documentation and the list of our guides and use cases where you can see appliance of Virgil .NET/C# SDK.


## Documentation

Virgil Security has a powerful set of APIs and the documentation to help you get started:

* [Get Started](/documentation/get-started) documentation
  * [Encrypted storage](/documentation/get-started/encrypted-storage.md)
  * [Encrypted communication](/documentation/get-started/encrypted-communication.md)
  * [Data integrity](/documentation/get-started/data-integrity.md)
* [Guides](/documentation/guides)
  * [Virgil Cards](/documentation/guides/virgil-card)
  * [Virgil Keys](/documentation/guides/virgil-key)
  * [Encryption](/documentation/guides/encryption)
  * [Signature](/documentation/guides/signature)
* [Configuration](/documentation/guides/configuration)
  * [Set Up Client Side](/documentation/guides/configuration/client.md)
  * [Set Up Server Side](/documentation/guides/configuration/server.md)

__Next__ Also, see our Virgil [.NET/C# SDK for PFS](https://github.com/VirgilSecurity/virgil-pfs-net) Encrypted Communication to protect previously intercepted traffic from being decrypted even if the main Private Key is compromised.

## License

This library is released under the [3-clause BSD License](LICENSE.md).

## Support

Our developer support team is here to help you. You can find us on [Twitter](https://twitter.com/virgilsecurity) and [email][support].

[support]: mailto:support@virgilsecurity.com
