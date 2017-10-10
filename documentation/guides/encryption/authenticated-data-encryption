# Authenticated Data Encryption

This guide is a short tutorial on how to sign then encrypt data with Virgil Security.

This process is called **Authenticated Data Encryption**. It is a form of encryption which simultaneously provides confidentiality, integrity, and authenticity assurances on the encrypted data.  During this procedure you will sign then encrypt data using Alice’s **Virgil Key**, and then Bob’s **Virgil Card**. In order to do this, Alice’s Virgil Key must be loaded from the appropriate storage location, then Bob’s Virgil Card must be searched for, followed by preparation of the data for transmission, which is finally signed and encrypted before being sent.



Set up your project environment before you begin to work, with the [getting started](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client.md) guide.

The Authenticated Data Encryption procedure is shown in the figure below.

![Authenticated Data Encryption](/img/Guides_introduction.png "Authenticated Data Encryption")

In order to <Term title="sign" index="digital-signature" /> and <Term title="encrypt" index="encryption" /> a **message**, Alice has to have:
 - Her Virgil Key
 - Bob's Virgil Card

Let's review how to sign and encrypt data:

1. Developers need to initialize the <Term title="Virgil SDK" index="virgil-sdk" />:

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

2. Alice has to:


  - Load her Virgil Key from secure storage defined by default;
  - Search for Bob's Virgil Cards on <Term title="Virgil Services" index="virgil-services" />;
  - Prepare a message for signature and encryption;
  - Encrypt and sign the message for Bob.

  ```cs
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

To load a Virgil Key from a specific storage, developers need to change the storage path during Virgil SDK initialization.

In many cases you will need the receiver's Virgil Cards. See [Finding Cards](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/virgil-card/finding.md) guide to find them.
