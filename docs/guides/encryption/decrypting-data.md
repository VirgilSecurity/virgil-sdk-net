# Decrypting Data

This guide is a short tutorial on how to **decrypt** encrypted data with Virgil Security.

Decryption is the reverse operation of encryption. As previously noted, Virgil Security allows you to encrypt data using public-key encryption. It's means that only the owner of the related private **Virgil Key** can decrypt the encrypted data.

Set up your project environment before you start to decrypt data, with the [getting started](/docs/guides/configuration/client-configuration.md) guide.

The Data Decryption procedure is shown in the figure below.

![Virgil Encryption Intro](/docs/img/Encryption_introduction.png "Data decryption")

In order to decrypt a **message**, Bob has to have:
 - His Virgil Key

Let's review the data decryption process:

1. Developers need to initialize the **Virgil SDK**:

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

2. Then Bob:


  - Loads the Virgil Key from the secure storage provided by default
  - Decrypts the message using his own Virgil Key

  ```cs
  // load a Virgil Key from device storage
  var bobKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");

  // decrypt a ciphertext using loaded Virgil Key
  var originalMessage = bobKey.Decrypt(ciphertext).ToString();
  ```

To load a Virgil Key from a specific storage, developers need to change the storage path during Virgil SDK initialization.

To decrypt data, you will need Bob's stored Virgil Key. See the [Storing Keys](/docs/guides/virgil-key/saving-key.md) guide for more details.
