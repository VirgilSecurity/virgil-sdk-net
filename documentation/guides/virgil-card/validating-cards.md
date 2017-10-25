# Validating Cards

This guide shows how to validate a **Virgil Card** on a device. As previously noted, each Virgil Card contains a Digital signature that provides data integrity for the Virgil Card over its life cycle. Therefore, developers can verify the Digital Signature at any time.

During the validation process we verify, by default, two signatures:
- **from Virgil Card owner**
- **from Virgil Services**

Additionally, developers can verify the **signature of the application server**.

Set up your project environment before you begin to validate a Virgil Card, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to validate the signature of the Virgil Card owner, **Virgil Services**, and the Application Server, we need to:

```cs
var appPublicKey = VirgilBuffer
    .From("[YOUR_APP_PUBLIC_KEY_HERE]", StringEncoding.Base64);

// initialize High Level Api with custom verifiers
var virgil = new VirgilApi(new VirgilApiContext {
    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
    CardVerifiers = new [] {
        new CardVerifierInfo {
            CardId = "[YOUR_APP_CARD_ID_HERE]",
            PublicKeyData = appPublicKey
        }
    }
});

var aliceCards = await virgil.Cards.FindAsync("alice");
```
