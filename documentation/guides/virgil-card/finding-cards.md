# Finding Cards

This guide shows how to find a **Virgil Card**. As previously noted, all Virgil Cards are saved at **Virgil Services** after their publication. Thus, every user can find their own Virgil Card or another user's Virgil Card on Virgil Services. It should be noted that users' Virgil Cards will only be visible to application users. Global Virgil Cards will be visible to anybody.

Set up your project environment before you begin to find a Virgil Card, with the [getting started](/documentation/guides/configuration/client.md) guide.

In order to search for an **Application** or **Global Virgil Card** you need to initialize the **Virgil SDK**:

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

### Application Cards

There are two ways to find an Application Virgil Card on Virgil Services:

The first one allows developers to get the Virgil Card by its unique **ID**

```cs
var aliceCard = await virgil.Cards.GetAsync("[ALICE_CARD_ID]");
```

The second one allows developers to find Virgil Cards by *identity* and *identityType*

```cs
// search for all User's Virgil Cards.
var aliceCards = await virgil.Cards.FindAsync("alice");

// search for all User's Virgil Cards with identity type 'member'
var bobCards = await virgil.Cards.FindAsync("member", new[] { "bob" });
```

### Global Cards

```cs
// search for all Global Virgil Cards
var bobGlobalCards = await virgil.Cards
.FindGlobalAsync("bob@virgilsecurity.com");
// search for Application Virgil Card
var appCards = await virgil.Cards.FindGlobalAsync("com.username.appname");
```
