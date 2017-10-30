# Verifying Signature

This guide is a short tutorial on how to verify a **Digital Signature** with Virgil Security.

Set up your project environment before starting to verify a Digital Signature, with the [getting started](/docs/guides/configuration/client.md) guide.

The Signature Verification procedure is shown in the figure below.


![Virgil Signature Intro](/docs/img/Signature_introduction.png "Verify Signature")

In order to verify the Digital Signature, Bob has to have Alice's **Virgil Card**.

Let's review the Digital Signature verification process:

- Developers initialize the **Virgil SDK**:

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Bob takes Alice's **Virgil Card ID** and searches for Alice's Virgil Card on **Virgil Services**.
- Bob verifies the signature. If the signature is invalid, Bob receives an error message.

```cs
// search for Virgil Card
var aliceCard = await virgil.Cards.Get("[ALICE_CARD_ID_HERE]");
```

See our guide on [Validating Cards](/docs/guides/virgil-card/validating-card.md) for best practices.
