# Quickstart C#/.NET

- [Introduction](#introduction)
- [Obtaining an App Token](#obtaining-an-app-token)
- [Install](#install)
- [Use case](#use-case)
    - [Step 1. Generate Keys](#generate-keys)
    - [Step 2. Register User](#register-user)
    - [Step 3. Store Private Key](#store-private-key)
    - [Step 4. Get a Public Key](#get-public-key)
    - [Step 5. Encrypt Data](#encrypt-data)
    - [Step 6. Sign Data](#sign-data)
    - [Step 7. Verify Data](#verify-data)
    - [Step 8. Decrypt Data](#decrypt-data)
- [See also](#see-also)

## Introduction

This guide will help you get started using the Crypto Library and Virgil Keys Service, for the most popular platforms and languages.
This branch focuses on the C#/.NET library implementation and covers it's usage.

## Obtaining an Application Token

First you must create a free Virgil Security developer account by signing up [here](https://developer.virgilsecurity.com/account/signup). Once you have your account you can [sign-in](https://developer.virgilsecurity.com/account/signin) and generate an access token for your application.

The access token provides authenticated secure access to Virgil’s Keys Service and is passed with each API call. The access token also allows the API to associate your app’s requests with your Virgil Security developer account.

```csharp
var keysClient = new Virgil.SDK.KeysClient("%ACCESS_TOKEN%");
```
## Install

You can easily add the Crypto Library or SDK dependency to your project, just follow the examples below:

```
PM> Install-Package Virgil.Crypto
```

Virgil Public Keys SDK:

```
PM> Install-Package Virgil.SDK.Keys
```

## Use Case
**Secure data at transport**, users need to exchange important data (text, audio, video, etc.) without any risks. 
- Sender and recipient create Virgil accounts with a pair of asymmetric keys
    - public key on Virgil Keys service
    - private key on Virgil Private Keys service or locally
- Sender encrypts the data using Virgil crypto library and recipient’s public key
- Sender signs encrypted data with his private key using Virgil crypto library
- Sender securely transfers encrypted data, his digital signature and UDID to recipient without any risk to be revealed
- Recipient verifies that the signature of transferred data is valid using the signature and sender’s public key in Virgil crypto library
- Recipient decrypts the data with his private key using Virgil crypto library
- Decrypted data is provided to the recipient

## Step 1. Create & Publish Keys

Working with Virgil Security Services it is requires the creation of both a public key and a private key. The public key can be made public to anyone using the Virgil Public Keys Service while the private key must be known only to the party or parties who will decrypt the data encrypted with the public key.

The following code example creates a new public/private key pair.

```csharp
var password = "jUfreBR7";
// Private Key password is optional 
var keyPair = CryptoHelper.GenerateKeyPair(password);
```

```csharp
var keysClient = new Virgil.SDK.KeysClient("%ACCESS_TOKEN%");

 var request = await IdentityRequest.Send("test@virgilsecurity.com", IdentityType.Email);
 
 // Use confirmation code sent to your email box.
 var identityProof = await request.Confirm("%CONFIRMATION_CODE%");

await = keysClient.Create(identityProof, keyPair.PublicKey()
```


## Register User

Once you've created a public key you may push it to Virgil’s Keys Service. This will allow other users to send you encrypted data using your public key.

This example shows how to upload a public key and register a new account on Virgil’s Keys Service.

```
var keysService = new PkiClient(new SDK.Keys.Http.Connection(Constants.ApplicationToken, 
    new Uri(Constants.KeysServiceUrl)));

var userData = new UserData
{
    Class = UserDataClass.UserId,
    Type = UserDataType.EmailId,
    Value = "your.email@server.hz"
};

var vPublicKey = await keysService.PublicKeys.Create(publicKey, publicKey, userData);
```

Confirm **User Data** using your user data type (Currently supported only Email).

```
var vUserData = vPublicKey.UserData.First();
var confirmCode = "{YOUR_CODE}"; // Confirmation code you received on your email box.

await keysService.UserData.Confirm(vUserData.UserDataId, 
    confirmCode, vPublicKey.PublicKeyId, privateKey);
```

## Store Private Key

This example shows how to store private keys on Virgil Private Keys service using SDK, this step is optional and you can use your own secure storage.

```
var privateKeysClient = new KeyringClient(new SDK.PrivateKeys.Http.Connection(
    Constants.ApplicationToken, new Uri(Constants.PrivateKeysServiceUrl)));

var containerPassword = "12345678";

// You can choose between two types of container. Easy and Normal.

// Easy   - Virgil’s Keys Service will keep your private keys encrypted with 
//          a container password. All keys should be sent to the service 
//          encrypted with this container password.
// Normal - Storage of the private keys is your responsibility and security 
//          of those passwords and data will be at your own risk.

var containerType = ContainerType.Easy; // ContainerType.Normal

// Initializes an container for private keys storage. 

await privateKeysClient.Container.Initialize(containerType, vPublicKey.PublicKeyId, 
    privateKey, containerPassword);

// Authenticate requests to Virgil’s Private Keys service.

privateKeysClient.Connection.SetCredentials(vUserData.Value, containerPassword);

// Add your private key to Virgil's Private Keys service.

if (containerType == ContainerType.Easy)
{
    // The private key will be encrypted with a container password, 
    // provided upon authentication.
    await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey);
}
else
{
    // use your own password to encrypt the private key.
    var privateKeyPassword = "47N6JwTGUmFvn4Eh";
    await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, 
        privateKey, privateKeyPassword);
}
```

## Get a Recepient's Public Key

Get public key from Public Keys Service.

```
var recepientPublicKey = await keysService.PublicKeys.Search("recepient.email@server.hz");
```

## Encrypt Data

The procedure for encrypting and decrypting documents is simple. For example:

If you want to encrypt the data to Bob, you encrypt it using Bobs's public key (which you can get from Public Keys Service), and Bob decrypts it with his private key. If Bob wants to encrypt data to you, he encrypts it using your public key, and you decrypt it with your private key.

In the example below, we encrypt data using a public key from Virgil’s Public Keys Service.

```
byte[] encryptedData;

using (var cipher = new VirgilCipher())
{
    byte[] recepientId = Encoding.UTF8.GetBytes(recepientPublicKey.PublicKeyId.ToString());
    byte[] data = Encoding.UTF8.GetBytes("Some data to be encrypted");

    cipher.AddKeyRecipient(recepientId, data);
    encryptedData = cipher.Encrypt(data, true);
}
```

## Sign Data

Cryptographic digital signatures use public key algorithms to provide data integrity. When you sign data with a digital signature, someone else can verify the signature, and can prove that the data originated from you and was not altered after you signed it.

The following example applies a digital signature to a public key identifier.

```
byte[] sign;
using (var signer = new VirgilSigner())
{
    sign = signer.Sign(encryptedData, privateKey);
}
```

## Verify Data

To verify that data was signed by a particular party, you must have the following information:

*   The public key of the party that signed the data.
*   The digital signature.
*   The data that was signed.

The following example verifies a digital signature which was signed by the sender.

```
bool isValid;
using (var signer = new VirgilSigner())
{
    isValid = signer.Verify(encryptedData, sign, publicKey);
}
```

## Decrypt Data

The following example illustrates the decryption of encrypted data.

```
var recepientContainerPassword = "UhFC36DAtrpKjPCE";

var recepientPrivateKeysClient = new KeyringClient(new Connection(Constants.ApplicationToken));
recepientPrivateKeysClient.Connection.SetCredentials(
    new Credentials("recepient.email@server.hz", recepientContainerPassword));

var recepientPrivateKey = await recepientPrivateKeysClient.PrivateKeys.Get(recepientPublicKey.PublicKeyId);

byte[] decryptedDate;
using (var cipher = new VirgilCipher())
{
    decryptedDate = cipher.DecryptWithKey(encryptedData, recepientId, recepientPrivateKey.Key);
}
```

## See Also

* [Tutorial Crypto Library](crypto.md)
* [Tutorial Keys SDK](public-keys.md)
* [Tutorial Private Keys SDK](private-keys.md)
