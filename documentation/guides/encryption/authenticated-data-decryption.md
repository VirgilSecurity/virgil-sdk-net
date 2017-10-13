# Authenticated Data Decryption

This guide is short tutorial on how to decrypt and then verify data with Virgil Security.

This process is called **Authenticated Data Decryption**. During this procedure you work with encrypted and signed data, decrypting and verifying them. A recipient uses their **Virgil Key** (to decrypt the data) and **Virgil Card** (to verify data integrity).


Set up your project environment before you begin to work, with the [getting started](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client.md) guide.

The Authenticated Data Decryption procedure is shown in the figure below.

![Virgil Intro](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/img/Guides_introduction.png "Authenticated Data Decryption")

In order to decrypt and verify the message, Bob has to have:
 - His Virgil Key
 - Alice's Virgil Card

Let's review how to decrypt and verify data:

1. Developers need to initialize the **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

2. Then Bob has to:


 - Load his own Virgil Key from secure storage, defined by default
 - Search for Alice's Virgil Card on **Virgil Services**
 - Decrypt the message using his Virgil Key and verify it using Alice's Virgil Card

 ```cs
 // load a Virgil Key from device storage
 var bobKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

 // get a sender's Virgil Card
 var aliceCard = await virgil.Cards.Get("[ALICE_CARD_ID]");

 // decrypt the message
 var originalMessage = bobKey.DecryptThenVerify(ciphertext, aliceCard).ToString();
 ```

To load a Virgil Key from a specific storage, developers need to change the storage path during Virgil SDK initialization.

To decrypt data, you will need Bob's stored Virgil Key. See the [Storing Keys](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/virgil-key/saving.md) guide for more details.
