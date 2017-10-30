# Importing Virgil Key

This guide shows how to export a **Virgil Key** from a Base64 encoded string representation.

Set up your project environment before you begin to import a Virgil Key, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to import a Virgil Key, we need to:

- Initialize **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Choose a Base64 encoded string
- Import the Virgil Key from the Base64 encoded string

```cs
// initialize a buffer from base64 encoded string
var aliceKeyBuffer = VirgilBuffer.From(
    "[BASE64_ENCODED_VIRGIL_KEY]", StringEncoding.Base64);

// import Virgil Key from buffer
var aliceKey = virgil.Keys.Import(aliceKeyBuffer, "[OPTIONAL_KEY_PASSWORD]");
```
