# Revoking Card

This guide shows how to revoke a **Virgil Card** from Virgil Services.

Set up your project environment before you begin to revoke a Virgil Card, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to revoke a Virgil Card, we need to:

- Initialize the **Virgil SDK** and enter Application **credentials** (**App ID**, **App Key**, **App Key password**)

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

- Get Alice's Virgil Card by **ID** from **Virgil Services**
- Revoke Alice's Virgil Card from Virgil Services

```cs
// get a Virgil Card by ID
var aliceCard = await virgil.Cards.GetAsync("[USER_CARD_ID_HERE]");

// revoke a Virgil Card
await virgil.Cards.RevokeAsync(aliceCard);
```
