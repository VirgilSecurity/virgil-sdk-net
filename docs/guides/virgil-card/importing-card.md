# Importing Card

This guide shows how to import a **Virgil Card** from the string representation.

Set up your project environment before you begin to import a Virgil Card, with the [getting started](/documentation/guides/configuration/client.md) guide.


In order to import the Virgil Card, we need to:

- Initialize the **Virgil SDK**

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

- Use the code below to import the Virgil Card from its string representation

```cs
// import a Virgil Card from string
var aliceCard = virgil.Cards.Import(exportedAliceCard);
```
