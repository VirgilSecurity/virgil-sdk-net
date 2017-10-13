# Perfect Forward Secrecy

[Set Up Server](#head1) | [Set Up Clients](#head2) | [Register Users](#head3) | [Initialize PFS Chat](#head4) | [Send & Receive a Message](#head5)

## Introduction
Virgil Perfect Forward Secrecy (PFS) is designed to prevent a possibly compromised long-term secret key from affecting the confidentiality of past communications. In this tutorial, we will be helping two people or IoT devices to communicate with end-to-end **encryption** with PFS enabled.

Create a [Developer account](https://developer.virgilsecurity.com/account/signup) and register your Application to get the possibility to use Virgil Infrastructure.

## <a name="head1"></a> Set Up Server
Your server should be able to authorize your users, store Application's Virgil Key and use **Virgil SDK** for cryptographic operations or for some requests to Virgil Services. You can configure your server using the [Setup Guide](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/server.md).


## <a name="head2"></a> Set Up Clients
Setup the client-side to provide your users with an access token after their registration at your Application Server to authenticate them for further operations and transmit their <Term title="Virgil Cards" index="virgil-card" /> to the server. Configure the client-side using the [Setup Guide](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/client-pfs.md).



## <a name="head3"></a> Register Users
Now you need to register the users who will participate in encrypted communications.

In order to sign and encrypt a message each user must have his own tools, which allow him to perform cryptographic operations, and these tools must contain the necessary information to identify users. In Virgil Security, these tools are the Virgil Key and the Virgil Card.

![Virgil Card](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/img/Card_introduct.png "Create Virgil Card")

When we have already set up the Virgil SDK on the server & client sides, we can finally create Virgil Cards for the users and transmit the Cards to your Server for further publication on Virgil Services.


### Generate Keys and Create Virgil Card
Use the Virgil SDK on the client side to generate a new Key Pair, and then create a user's Virgil Card using the recently generated Virgil Key. All keys are generated and stored on the client side.

In this example, we will pass on the user's username and a password, which will lock in their private encryption key. Each Virgil Card is signed by a user's Virgil Key, which guarantees the Virgil Card's content integrity over its life cycle.

```cs
// generate a new Virgil Key
var aliceKeys = crypto.GenerateKeys();

// save the Virgil Key into the storage
var exportedPrivateKey = crypto.ExportPrivateKey(aliceKeys.PrivateKey, "[KEY_PASSWORD]");
var keyEntry = new KeyEntry
  {
    Name = "[KEY_NAME]",
    Value = exportedPrivateKey
  };
var keyStorage = new DefaultKeyStorage();
keyStorage.Store(keyEntry);

// prepare a request
var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
var request = new PublishCardRequest("alice", "member", exportedPublicKey);

// sign the request
var requestSigner = new RequestSigner(crypto);
requestSigner.SelfSign(request, keyPair.PrivateKey);
requestSigner.AuthoritySign(request, "[APP_ID]", "[APP_KEY]");
```

Warning: Virgil doesn't keep a copy of your Virgil Key. If you lose a Virgil Key, there is no way to recover it.

In order for the Sender to be able to send a message, we also need a Virgil Card associated with the Recipient. It should be noted that recently created user Virgil Cards will be visible only for application users because they are related to the Application.

Read more about Virgil Cards and their types [here](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/virgil-card/creating-card.md).


### Transmit the Cards to Your Server

Next, you must serialize and transmit this cards to your server, where you will Approve & Publish Users' Cards.

```cs
// export the request to a string
var exportedRequest = request.Export();

// transmit the request to the server
TransmitToServer(exportedRequest);
```

Use the [approve & publish users guide](https://github.com/VirgilSecurity/virgil-sdk-net/blob/v4/documentation/guides/configuration/server.md) to publish users Virgil Cards on Virgil Services.



## <a name="head4"></a> Initialize PFS Chat
With the user's Cards in place, we are now ready to initialize a PFS chat. In this case, we will use the Recipient's Private Keys, the Virgil Cards and the Access Token.

In order to begin communicating, Bob must run the initialization:

```cs
var secureChatPreferences = new SecureChatPreferences(
    "[CRYPTO]",
		"[BOB_IDENTITY_CARD]",
		"[BOB_PRIVATE_KEY]",
		"[YOUR_ACCESS_TOKEN_HERE]");

this.SecureChat = new SecureChat(secureChatPreferences);
    try
    {
        await this.SecureChat.RotateKeysAsync(100);
    }
    catch(Exception){
    //...
    }
```

Warning: If Bob does not run the chat initialization, Alice cannot create an initial message.

Then, Alice must run the initialization:

```cs
var secureChatPreferences = new SecureChatPreferences(
  "[CRYPTO]",
	"[ALICE_IDENTITY_CARD]",
	"[ALICE_PRIVATE_KEY]",
	"[YOUR_ACCESS_TOKEN_HERE]");

  this.SecureChat = new SecureChat(secureChatPreferences);
  try
    {
      await this.SecureChat.RotateKeysAsync(100);
    }
 catch (Exception){
 	//...
 }

```

After chat initialization, Alice and Bob can start their PFS communication.

## <a name="head5"></a> Send & Receive a Message

Once Recipients initialized a PFS Chat, they can communicate.

Alice establishes a secure PFS conversation with Bob, encrypts and sends the message to him:

```cs
public void SendMessage(User receiver, string message) {
    // get an active session by receiver's card id
    var session = this.Chat.ActiveSession(receiver.Card.Id);
    if (session == null)
	{
        // start new session with recipient if session wasn't initialized yet
        try
        {
	       	session = await this.chat.StartNewSessionWithAsync(receiver.Card);
       	}
       	catch{
    	   	// Error handling
       	}
    }
    this.SendMessage(receiver, session, message);
}

public void SendMessage(User receiver, SecureSession session, string message) {
    string ciphertext;
    try
    {
        // encrypt the message using previously initialized session
        ciphertext = session.Encrypt(message);
    }
    catch (Exception) {
        // Error handling
    }

    // send a cipher message to recipient using your messaging service
    this.Messenger.SendMessage(receiver.Name, ciphertext)
}
```


Then Bob decrypts the incoming message using the conversation he has just created:


```cs
public void MessageReceived(string senderName, string message) {
    var sender = this.Users.Where(x => x.Name == senderName).FirstOrDefault();
    if (sender == null){
       return;
    }

    this.ReceiveMessage(sender, message);
}

public void ReceiveMessage(User sender, string message) {
    try
    {
        var session = this.Chat.LoadUpSession(sender.Card, message);

        // decrypt message using established session
        var plaintext = session.Decrypt(message);

        // show a message to the user
        Print(plaintext);
    }
    catch (Exception){
        // Error handling
    }
}
```

With the open session, which works in both directions, Alice and Bob can continue PFS encrypted communication.
