
# Client Configuration

[Install SDK](#head1) | [Obtain an Access Token](#head2) | [Initialize SDK](#head3)

In order to use the Virgil Infrastructure, set up your client and implement the required mechanisms using the following guide.

<Info> Don't see your language? Virgil is also available as a [REST API](/references). </Info>


## <a name="head1"></a> Install SDK

The Virgil .NET SDK is provided as a package named Virgil.SDK. The package is distributed via NuGet package management system.

The package is available for:
- .NET Framework 4.5 and newer.

Prerequisites:
- Visual Studio 2013 RTM Update 2 and newer (Windows)

Installing the package:

1. Use NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console)
2. Run PM> Install-Package Virgil.SDK

## <a name="head2"></a> Obtain an Access Token

When users want to start sending and receiving messages in a browser or mobile device, Virgil can't trust them right away. Clients have to be provided with a unique identity, thus,  you'll need to give your users the Access Token that tells Virgil who they are and what they can do.

Each your client must send to you the Access Token request with their registration request. Then, your service that will be responsible for handling access requests must handle them in case of users successful registration on your Application server.

```
// an example of an Access Token representation
AT.7652ee415726a1f43c7206e4b4bc67ac935b53781f5b43a92540e8aae5381b14
```

## <a name="head3"></a> Initialize SDK

### With a Token
With the Access Token we can initialize the Virgil PFS SDK on the client-side to start doing fun stuff like sending and receiving messages. To initialize the <Term title="Virgil SDK" index="virgil-sdk" /> on a client-side you need to use the following code:

```cs
var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
```

### Without a Token

In case of a <Term title="Global Virgil Card" index="global-virgil-card" /> creation you don't need to initialize the SDK with the Access Token. For more information about the Global Virgil Card creation check out the [Creating Global Card guide](/guides/virgil-card/creating-global).

Use the following code to initialize the Virgil SDK without the Access Token.

```cs
var virgil = new VirgilApi();
```
