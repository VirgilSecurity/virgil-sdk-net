
### [Introduction](#head1) | [Server-Side](#head2) | [Decrypt & Verify](#head8)

# <a name="head1"></a>Introduction
It is very easy to encrypt data for secure communications in a few easy steps. In this tutorial, we will be helping two people communicate with full (end-to-end) encryption.

Due to limited time and resources, developers often resort to third-party solutions to transfer data, which do not have an open source API, a full cycle of data security that would ensure integrity and confidentiality, thus all of your data could be read by a third-party. Virgil offers a solution without these weaknesses.

# <a name="head2"></a>Setup Your Server


# <a name="head8"></a> Decrypt a message & Verify its signature

Once the Recipient receives the signed and encrypted message, he can decrypt and validate the message. Thus proving that the message has not been tampered with, by verifying the signature against the Sender's Virgil Card.

In order to decrypt the encrypted message and then verify the signature, we need to load a private receiver's Virgil Key and search for the sender's Virgil Card at Virgil Services.

```cs
// load a Virgil Key from device storage
var bobKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

// get a sender's Virgil Card
var aliceCard = await virgil.Cards.Get("[ALICE_CARD_ID]");

// decrypt the message
var originalMessage = bobKey.DecryptThenVerify(ciphertext, aliceCard).ToString();
```
