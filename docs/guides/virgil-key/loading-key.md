# Loading Keys

This guide shows how to load a private **Virgil Key**, which is stored on the device. The key must be loaded when Alice wants to **sign** some data, **decrypt** any encrypted content, and perform cryptographic operations.

Set up your project environment before you begin to load a Virgil Key, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to load the Virgil Key from the default storage:

- Initialize the **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Alice has to load her Virgil Key from the protected storage and enter the Virgil Key's password

```cs
// load a Virgil Key from storage
var aliceKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");
```

To load a Virgil Key from a specific storage, developers need to change the storage path during Virgil SDK initialization.
