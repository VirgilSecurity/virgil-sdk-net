# Virgil Security .NET/C# SDK
[![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

[Installation](#installation) | [Encryption Example](#encryption-example) | [Initialization](#initialization) | [Documentation](#documentation) | [Support](#support)

[Virgil Security](https://virgilsecurity.com) provides a set of APIs for adding security to any application. In a few simple steps you can encrypt communication, securely store data, provide passwordless login, and ensure data integrity.

To initialize and use Virgil SDK, you need to have [Virgil Developer Account](https://developer.virgilsecurity.com/account/signin).

## Installation

To install Virgil SDK package, use below guides:

1. [Client Configuration](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client-configuration.md)
2. [Client Configuration (PFS)](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client-pfs-configuration.md)
3. [Server Configuration](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/server-configuration.md)


## Initialization

Be sure that you signed up and log in to [developer account](https://developer.virgilsecurity.com/account/signin).
You need to save the __app id__, __private key__ and __password__. After this, create an __application token__ for your application to make authenticated requests from your clients.
To find the code for initializing Virgil SDK, choose the option:
1. [Initialize SDK for Client](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client.md#-initialize-sdk)
2. [Initialize SDK for Client (PFS)](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/client-pfs.md#-initialize-sdk)
3. [Initialize SDK for Server](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4-docs-review/documentation/guides/configuration/server.md#-initialize-sdk)

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
