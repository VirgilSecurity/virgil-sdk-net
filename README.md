# Virgil .NET/Xamarin SDK [![Build status](https://ci.appveyor.com/api/projects/status/kqs4lqw426gbpccm/branch/release?svg=true)](https://ci.appveyor.com/project/unlim-it/virgil-sdk-net/branch/release) [![Nuget package](https://img.shields.io/nuget/v/Virgil.SDK.svg)](https://www.nuget.org/packages/Virgil.SDK/)

Virgil Services SDK allows you easily integrate end-to-end encryption to any kind of solution. To learn more, visit our [Developer Center](https://virgilsecurity.com/api-docs).

### Target frameworks
 - .NET Framework 4.0 and newer.
 - .NET Portable Framework

### Prerequisites
 - Visual Studio 2013 RTM Update 2 and newer (Windows)
 - Xamarin Studio 5.x and newer (Windows, Mac)
 - MonoDevelop 4.x and newer (Windows, Mac, Linux)

### Supported Platforms
 - Mac OS, Windows 7 and newer, Ubuntu
 - Android, iOS

## Install
Use NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install Virgil.SDK package running the command:

```
PM> Install-Package Virgil.SDK
```

## Obtaining an Access Token

First you must create a free Virgil Security developer’s account by signing up [here](https://developer.virgilsecurity.com/account/signup). Once you have your account you can [sign in](https://developer.virgilsecurity.com/account/signin) and generate an access token for your application.

The access token provides an authenticated secure access to the Public Keys Service and is passed with each API call. The access token also allows the API to associate your app’s requests with your Virgil Security developer’s account.

## Import

```csharp
using Virgil.SDK;
using Virgil.Crypto;
```

## Initialization

Simply add your access token to the class builder.

```csharp
var serviceHub = ServiceHub.Create("%ACCESS_TOKEN%");
```
or you can customize initialization using your own parameters 

```csharp
var config = ServiceHubConfig
    .UseAccessToken("%ACCESS_TOKEN%")
    .WithCardsService("%CARDS_SERVICE_URL%", "%CARDS_SERVICE_PUBLIC_KEY%")
    .WithPrivateKeysService("%PRIVATE_KEYS_SERVICE_URL%", "%PRIVATE_KEYS_SERVICE_PUBLIC_KEY%")
    .WithIdentityServices("%IDENTITY_SERVICE_URL%", "%IDENTITY_SERVICE_PUBLIC_KEY%");

var serviceHub = ServiceHub.Create(config);
```

| Parameter                        | Mandatory | Description                    |
|----------------------------------|-----------|--------------------------------|
| ACCESS_TOKEN                     | YES       | The access token provides authenticated secure access to Virgil Keys Services and is passed with each API call, described in [this step](#obtaining-an-access-token). |
| CARDS_SERVICE_URL                | NO        | The **Virgil Cards** service URL.  |
| CARDS_SERVICE_PUBLIC_KEY         | NO        | The **Virgil Cards** service Public Key. Uses to verify service responses and prevent MitM attacks. |
| PRIVATE_KEYS_SERVICE_URL         | NO        | The **Virgil Private Keys** service URL. |
| PRIVATE_KEYS_SERVICE_PUBLIC_KEY  | NO        | The **Virgil Private Keys** service Public Key. Uses to verify service responses and prevent MitM attacks. |
| IDENTITY_SERVICE_URL             | NO        | The **Virgil Identity** service URL. |
| IDENTITY_SERVICE_PUBLIC_KEY      | NO        | The **Virgil Identity** service Public Key. Uses to verify service responses and prevent MitM attacks. |

## Cards and Public Keys
A Virgil Card is the main entity of the Public Keys Service, it includes the information about the user and his public key. The Virgil Card identifies the user by one of his available types, such as an email, a phone number, etc.

The Virgil Card might be *global* and *private*. The difference is whether Virgil Services take part in [the Identity verification](#identities). 

*Global Cards* are created with the validation token received after verification in Virgil Identity Service. Any developer with Virgil account can create a global Virgil Card and you can be sure that the account with a particular email has been verified and the email owner is really the Identity owner.

*Private Cards* are created when a developer is using his own service for verification instead of Virgil Identity Service or avoids verification at all. In this case validation token is generated using app's Private Key created on our [Developer portal](https://developer.virgilsecurity.com/dashboard/).   

### Publish new Virgil *private* Card
Creating a Virgil *private* Card with a newly generated key pair.

```csharp
// generate a new public/private key pair
var keyPair = VirgilKeyPair.Generate();

// create a ticket that will be used to publish a card
var ticket = VirgilTicket.Create("demo", "user_name", keyPair.PrivateKey());
ticket.SignWithAppKey('%APP_PRIVATE_KEY%', '%APP_PRIVATE_KEY_PASSWORD%');

// publish a new Virgil *private* Card to Virgil Cards Service
var newCard = await serviceHub.Cards.PublishPrivateAsync(ticket);
```

| Parameter                        | Mandatory | Description                            |
|----------------------------------|-----------|----------------------------------------|
| APP_PRIVATE_KEY                  | YES       | The app's Private Key created on [Developer portal](https://developer.virgilsecurity.com/dashboard/) at the time of registration application |
| APP_PRIVATE_KEY_PASSWORD         | NO        | The app's Private Key password. Uses only if the Private Key is encrypted. |

Creating an unauthorized Virgil *private* Card. Pay attention that this Card will not be included in default search, you will have to set an additional attribute to include the *private* Cards into your search, see an [example](#search-for-virgil-cards).

```csharp
// generate a new public/private key pair
var keyPair = VirgilKeyPair.Generate();

// create a ticket that will be used to publish a card
var ticket = VirgilTicket.Create("demo", "user_name", keyPair.PrivateKey());

// publish a new Virgil *private* Card to Virgil Cards Service
var newCard = await serviceHub.Cards.PublishPrivateAsync(ticket);
```

### Publish new Virgil global Card


```csharp
// confirm an identity using Virgil Identity service API
var emailVerifier = await serviceHub.Identity.VerifyEmailAsync("demo@virgilsecurity.com");
var confirmedIdentity = await emailVerifier.ConfirmAsync("%CONFIRMATION_CODE%");

// generate a new public/private key pair
var keyPair = VirgilKeyPair.Generate();

// create a ticket that will be used to publish a card
var ticket = VirgilTicket.Create(confirmedIdentity, keyPair.PrivateKey());

// publish a new Virgil *global* Card to Virgil Cards Service
var newCard = await serviceHub.Cards.PublishGlobalAsync(ticket);
```

| Parameter                        | Mandatory | Description                            |
|----------------------------------|-----------|----------------------------------------|
| CONFIRMATION_CODE                | YES       | The confirmation code from received email message, which sent during verification request. |

### Search for Virgil Card

Search for a Virgil *private* Card.

```csharp
// search for the Virgil private Cards
var foundCards = await serviceHub.Cards.SearchPrivateAsync("demo", "user_name");

// search for the Virgil private Cards, including cards
// without application signature.
var foundCards = await serviceHub.Cards.SearchPrivateAsync("demo", "user_name", true);
```

Search for a Virgil *global* Card.

```csharp
// search for the Virgil global Card with email identity
var foundCards = await serviceHub.Cards.SearchGlobalAsync("demo@virgilsecurity.com", "email");

// search for the Virgil global Card with application identity
var foundCards = await serviceHub.Cards.SearchGlobalAsync("com.virgilsecurity.identity", "app");
```

### Get a Virgil Card

```csharp
var theCard = async serviceHub.Cards.GetAsync(%VIRGIL_CARD_ID%)
```

| Parameter          | Mandatory | Description                 |
|--------------------|-----------|-----------------------------|
| VIRGIL_CARD_ID     | YES       | The Virgil Card identifier. |


### Revoke a Virgil Card

```csharp
var cardId = %VIRGIL_CARD_ID%;
var privateKey = %VIRGIL_CARD_PRIVATE_KEY%;
var privateKeyPassword = %VIRGIL_CARD_PRIVATE_KEY_PASSWORD%;

await serviceHub.Cards.RevokeAsync(cardId, privateKey, privateKeyPassword);
```

| Parameter                         | Mandatory | Description                 |
|-----------------------------------|-----------|-----------------------------|
| VIRGIL_CARD_ID                    | YES       | The Virgil Card identifier. |
| VIRGIL_CARD_PRIVATE_KEY           | YES       | The Private Key associated with specified Virgil Card. |
| VIRGIL_CARD_PRIVATE_KEY_PASSWORD  | NO        | The Private Key password. Only if Private Key is encrypted |

## Private Keys

The security of private keys is crucial for the public key cryptosystems. Anyone who can obtain a private key can use it to impersonate the rightful owner during all communications and transactions on intranets or on the internet. Therefore, private keys must be in the possession only of authorized users, and they must be protected from unauthorized use.

Virgil Security provides a set of tools and services for storing private keys in a safe storage which lets you synchronize your private keys between the devices and applications.

Usage of this service is optional.

### Stash a Private Key 

Private key can be added for storage only in case you have already registered a public key on the Public Keys Service.

Use the public key identifier on the Public Keys Service to save the private keys.

The Private Keys Service stores private keys the original way as they were transferred. That’s why we strongly recommend transferring the keys which were generated with a password.

```csharp
var cardId = %VIRGIL_CARD_ID%;
var privateKey = %VIRGIL_CARD_PRIVATE_KEY%;
var privateKeyPassword = %VIRGIL_CARD_PRIVATE_KEY_PASSWORD%;

await serviceHub.PrivateKeys.StashAsync(cardId, privateKey, privateKeyPassword);
```

| Parameter                         | Mandatory | Description                 |
|-----------------------------------|-----------|-----------------------------|
| VIRGIL_CARD_ID                    | YES       | The Virgil Card identifier. |
| VIRGIL_CARD_PRIVATE_KEY           | YES       | The Private Key associated with specified Virgil Card. |
| VIRGIL_CARD_PRIVATE_KEY_PASSWORD  | NO        | The Private Key password. Only if Private Key is encrypted |


### Get a Private Key

To get a private key you need to pass a prior verification of the Virgil *global* Card where your public key is used.

```csharp
// confirm an identity using Virgil Identity service API
var emailVerifier = await serviceHub.Identity.VerifyEmailAsync("demo@virgilsecurity.com");
var confirmedIdentity = await emailVerifier.ConfirmAsync("%CONFIRMATION_CODE%");

var cardId = %VIRGIL_CARD_ID%;
var privateKey = await serviceHub.PrivateKeys.GetAsync(cardId, confirmedIdentity);
```

| Parameter                         | Mandatory | Description                 |
|-----------------------------------|-----------|-----------------------------|
| VIRGIL_CARD_ID                    | YES       | The Virgil Card identifier. |
| CONFIRMATION_CODE                 | YES       | The confirmation code from received email message, which sent during verification request. |


### Unstash a Private Key

This operation deletes the private key from the service without a possibility to be restored.

```csharp
var cardId = %VIRGIL_CARD_ID%;
var privateKey = %VIRGIL_CARD_PRIVATE_KEY%;
var privateKeyPassword = "%VIRGIL_CARD_PRIVATE_KEY_PASSWORD%";

await serviceHub.PrivateKeys.UnstashAsync(card_id, privateKey, privateKeyPassword);
```

| Parameter                         | Mandatory | Description                 |
|-----------------------------------|-----------|-----------------------------|
| VIRGIL_CARD_ID                    | YES       | The Virgil Card identifier. |
| VIRGIL_CARD_PRIVATE_KEY           | YES       | The Private Key associated with specified Virgil Card. |
| VIRGIL_CARD_PRIVATE_KEY_PASSWORD  | NO        | The Private Key password. Only if Private Key is encrypted |

## Release Notes
 - Please read the latest note here: [https://github.com/VirgilSecurity/virgil-sdk-net/releases](https://github.com/VirgilSecurity/virgil-sdk-net/releases)

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>

