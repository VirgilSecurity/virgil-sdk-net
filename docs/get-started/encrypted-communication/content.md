# Encrypted Communication

 [Set Up Your Server](#head1) | [Set Up Your Clients](#head2) | [Register Users](#head3) | [Sign & Encrypt](#head4) | [Decrypt & Verify](#head5)
 
## Introduction
It is very easy to encrypt data for secure communications in a few easy steps. In this tutorial, we will be helping two people communicate with full (end-to-end) encryption.

Due to limited time and resources, developers often resort to third-party solutions to transfer data, which do not have an open source API, a full cycle of data security that would ensure integrity and confidentiality, thus, all of your data could be read by the third party. Virgil offers a solution without these weaknesses.

![Encrypted Communication](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/docs/img/encrypted_communication_intro.png)

See our tutorial on [Virgil & Twilio Programmable Chat](https://github.com/VirgilSecurity/virgil-demo-twilio) for best practices.

## <a name="head1"></a>Set Up Your Server

Your server should be able to authorize your users, store Application's Virgil Key and use Virgil SDK for cryptographic operations or for some requests to Virgil Services. You can configure your server using the [Setup Guide](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/docs/guides/configuration/server.md).

## <a name="head2"></a>Set Up Your Clients

Set up the client-side to provide your users with an access token after their registration at your Application Server to authenticate them for further operations and transmit their Virgil Cards to the server. Configure the client-side using the [Setup Guide](/guides/configuration/client-side).

## <a name="head3"></a>Register Users

Now you need to register the users who will participate in encrypted communications.

In order to sign and encrypt a message each user must have his own tools, which allow him to perform cryptographic operations, and these tools must contain the necessary information to identify users. In Virgil Security, these tools are the Virgil Key and the Virgil Card.

![Virgil Card](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/docs/img/Card_introduct.png "Create Virgil Card")

When we have already set up the Virgil SDK on the server & client sides, we can finally create Virgil Cards for the users and transmit the Cards to your Server for further publication on Virgil Services.

### Generate Keys and Create Virgil Card

Use the Virgil SDK on the client side to generate a new Key Pair, and then create a user's Virgil Card using the recently generated Virgil Key. All keys are generated and stored on the client side.

In this example, we will pass on the user's username and a password, which will lock in their private encryption key. Each Virgil Card is signed by a user's Virgil Key, which guarantees the Virgil Card's content integrity over its life cycle.

```cs
// generate a new Virgil Key
var aliceKey = virgil.Keys.Generate();

// save the Virgil Key into storage
aliceKey.Save("[KEY_NAME]", "[KEY_PASSWORD]");

// create a Virgil Card
var aliceCard = virgil.Cards.Create("alice", aliceKey);
```

Warning: Virgil doesn't keep a copy of your Virgil Key. If you lose a Virgil Key, there is no way to recover it.

To send a message, Sender needs a Virgil Card associated with the Recipient. Note: Recently created user Virgil Cards are visible only for application users because they are related to the Application.

### Transmit the Cards to Your Server

Next, you must serialize and transmit these Cards to your server, where you will Approve & Publish Users' Cards.

```cs
// export a Virgil Card to string
var exportedCard = aliceCard.Export();

// transmit the Virgil Card to the server
TransmitToServer(exportedCard);
```

## <a name="head4"></a>Sign & Encrypt a Message

With the user's Cards in place, we are now ready to encrypt a message for encrypted communication. In this case, we will encrypt the message using the Recipient's Virgil Card.

As previously noted we encrypt data for secure communication, but a recipient also must be sure that no third party modified any of the message's content and that they can trust a sender, which is why we provide Data Integrity by adding a Digital Signature. Therefore we must digitally sign data first and then encrypt.

![Virgil Intro](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/docs/img/Guides_introduction.png)

In order to sign then encrypt messages, the Sender must load their own recently generated Virgil Key and search for the receiver's Virgil Cards at Virgil Services, where all Virgil Cards are saved.

```csharp
// load a Virgil Key from device storage
var aliceKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

// search for Virgil Cards
var bobCards = await virgil.Cards.FindAsync("bob");

// prepare the message
var message = "Hey Bob, how's it going?";

// sign and encrypt the message
var ciphertext = aliceKey.SignThenEncrypt(message, bobCards)
    .ToString(StringEncoding.Base64);
```

To sign a message, you will need to load Alice's Virgil Key. 

Now the Receiver can verify that the message was sent by a specific Sender.

### Transmission

With the signature in place, the Sender is now ready to transmit the signed and encrypted message to the Receiver.

See our tutorial on [Virgil & Twilio Programmable Chat](https://github.com/VirgilSecurity/virgil-demo-twilio) for best practices.

# <a name="head5"></a> Decrypt a Message & Verify its Signature

Once the Recipient receives the signed and encrypted message, he can decrypt and validate the message. Thus, proving that the message has not been tampered with, by verifying the signature against the Sender's Virgil Card.

In order to decrypt the encrypted message and then verify the signature, we need to load a private receiver's Virgil Key and search for the sender's Virgil Card at Virgil Services.

```csharp
// load a Virgil Key from device storage
var bobKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

// get a sender's Virgil Card
var aliceCard = await virgil.Cards.Get("[ALICE_CARD_ID]");

// decrypt the message
var originalMessage = bobKey.DecryptThenVerify(ciphertext, aliceCard).ToString();
```
