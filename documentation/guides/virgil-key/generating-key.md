# Generating Keys

This guide shows how to generate a **Virgil Key**.  The Virgil Key is a Private Key, which never leaves its device. It allows only those who hold the key to sign data and decrypt data that was encrypted with the Private Key's associated Public Key.

Set up your project environment before you begin to generate a Virgil Key, with the [getting started](/documentation/guides/configuration/client.md) guide.

The Virgil Key generation procedure is shown in the figure below.

![Virgil Key Intro](/documentation/img/Key_introduction.png "Keys generation")

There are two options to generate a Virgil Key:
- With the default key pair type
- With a specific key pair type


1. To generate a Virgil Key with the default type:


- Developers need to initialize the **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Then Alice generates a new Virgil Key

```cs
// generate a new Virgil Key
var aliceKey = virgil.Keys.Generate();
```

Warning: Virgil doesn't keep a copy of your Virgil Key. If you lose a Virgil Key, there is no way to recover it.

2. To generate a Virgil Key with a specific type, we need to:


- Choose the preferred type and initialize **Virgil Crypto** with this type;
- Initialize the Virgil SDK with a custom Virgil Crypto instance;
- Generate a new Virgil Key.

```cs
// initialize Crypto with specific key pair type
var crypto = new VirgilCrypto(KeyPairType.EC_BP512R1);

var context = new VirgilApiContext();
context.SetCrypto(crypto);

// initialize Virgil SDK using
var virgil = new VirgilApi(context);

// generate a new Virgil Key
var aliceKey = virgil.Keys.Generate();
```
