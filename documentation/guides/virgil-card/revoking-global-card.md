# Revoking Global Card

This guide shows how to revoke a **Global Virgil Card**.

Set up your project environment before you begin to revoke a Global Virgil Card, with the [getting started](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client.md) guide.

In order to revoke a Global Virgil Card, we need to:

-  Initialize the Virgil SDK

```cs
var virgil = new VirgilApi(new VirgilApiContext
{
  AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
  Credentials = new AppCredentials
  {
      AppId = "[YOUR_APP_ID_HERE]",
      AppKey = VirgilBuffer.FromFile("[YOUR_APP_KEY_FILEPATH_HERE]"),
      AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]",
  }
});
```

- Load Alice's <Term title="Virgil Key" index="virgil-key" /> from the secure storage provided by default
- Load Alice's Virgil Card from <Term title="Virgil Services" index="virgil-services" />
- Initiate the Card's identity verification process
- Confirm the Card's identity using a <Term title="confirmation code" index="confirmation-code" />
- Revoke the Global Virgil Card from Virgil Services

```cs
// load a Virgil Key from storage
var aliceKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

// load a Virgil Card from Virgil Services
var aliceCard = virgil.Cards.GetAsync("[USER_CARD_ID_HERE]");

// initiate an identity verification process.
var attempt = await aliceCard.CheckIdentityAsync();

// grab a validation token
var token = await attempt.ConfirmAsync(new EmailConfirmation("[CONFIRMATION_CODE]"));

// revoke a Global Virgil Card
await virgil.Cards.RevokeGlobalAsync(aliceCard, aliceKey, token);
```
