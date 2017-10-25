# Exporting Card

This guide shows how to export a **Virgil Card** to the string representation.

Set up your project environment before you begin to export a Virgil Card, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to export a Virgil Card, we need to:

- Initialize the **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Use the code below to export the Virgil Card to its string representation.

```cs
// export a Virgil Card to string
var exportedAliceCard = aliceCard.Export();
```

The same mechanism works for **Global Virgil Card**.
