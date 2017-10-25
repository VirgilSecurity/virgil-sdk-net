# Exporting Virgil Key


This guide shows how to export a **Virgil Key** to the string representation.

Set up your project environment before you begin to export a Virgil Key, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to export the Virgil Key:

- Initialize **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Alice Generates a Virgil Key
- After Virgil Key generation, developers can export Alice's Virgil Key to a Base64 encoded string

```cs
// generate a new Virgil Key
var aliceKey = virgil.Keys.Generate();

// export the Virgil Key
var exportedAliceKey = aliceKey.Export("[OPTIONAL_KEY_PASSWORD]")
    .ToString(StringEncoding.Base64);
```
