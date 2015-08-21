# Quickstart

This quickstart illustrates a series of examples that will help you become familiar with the Virgil Security Services and Crypto Library.

- [Introduction](#introduction)
- [Obtaining an App Token](#obtaining-an-app-token)
- [Install](#install)
- [Usage examples](#usage-examples)
    - [Generate Keys](#generate-keys)
    - [Register User](#register-user)
    - [Get Public Key](#get-public-key)
    - [Encrypt Data](#encrypt-data)
    - [Decrypt Data](#decrypt-data)
    - [Sign Data](#sign-data)
    - [Verify Data](#verify-data)
- [License](#license)
- [Contacts](#contacts)

## Introduction

This guide will help you get started using the Crypto Library and Virgil Keys Service, for the most popular platforms and languages

## Obtaining an Application Token

First you must create a free Virgil Security developer account by signing up [here](https://virgilsecurity.com/signup). Once you have your account you can [sign in](https://virgilsecurity.com/signin) and generate an app token for your application.

The app token provides authenticated secure access to Virgil’s Keys Service and is passed with each API call. The app token also allows the API to associate your app’s requests with your Virgil Security developer account.

Simply add your app token to the HTTP header for each request:

``` {.c#}    
X-VIRGIL-APPLICATION-TOKEN: { YOUR_APPLICATION_TOKEN } 
```

## Install

There are several ways to install and use the Crypto Library and Virgil’s SDK in your environment.

1.  Install with [Package Management System](#package-management-system)
2.  [Download](/documents/csharp/downloads) from our web site
3.  [Build](/documents/csharp/crypto-lib#build) by yourself

### Package Management Systems

**Virgil Security** supports most of popular package management systems. You can easily add the Crypto Library dependency to your project, just follow the examples below.

``` {.c#}   
PM> Install-Package Virgil.Crypto
```
Virgil Public Keys SDK:
``` {.c#} 
PM> Install-Package Virgil.SDK.Keys
```
Virgil Private Keys SDK:
``` {.c#} 
PM> Install-Package Virgil.SDK.PrivateKeys
```

## Generate Keys

Working with Virgil Security Services it is requires the creation of both a public key and a private key. The public key can be made public to anyone using the Virgil Public Keys Service while the private key must be known only to the party or parties who will decrypt the data encrypted with the public key.

> Private keys should never be stored verbatim or in plain text on a local computer.
> 
> <footer>If you need to store a private key, you should use a secure key container depending on your platform. You also can use Virgil Keys Service to store and synchronize private keys. This will allows you to easily synchronize private keys between clients’ devices and their applications. Please read more about [Virgil Private Keys Service](/documents/csharp/keys-private-service).</footer>

The following code example creates a new public/private key pair.

    using Virgil.Crypto;
    using Virgil.SDK.Keys
    using Virgil.SDK.PrivateKeys                 
    ...

    byte[] publicKey;
    byte[] privateKey;

    using (var keyPair = new VirgilKeyPair())
    {
        publicKey = keyPair.PublicKey();
        privateKey = keyPair.PrivateKey();
    }

## Register User

Once you've created a public key you may push it to Virgil’s Keys Service. This will allow other users to send you encrypted data using your public key.

This example shows how to upload a public key and register a new account on Virgil’s Keys Service.

Full source code examples are available on [GitHub](https://github.com/VirgilSecurity/virgil-net/blob/master/Samples/Examples/PublishKeysExample.cs) in public access.

    var keysService = new PkiClient(new SDK.Keys.Http.Connection(Constants.ApplicationToken, 
    	new Uri(Constants.KeysServiceUrl)));

    var userData = new UserData
    {
        Class = UserDataClass.UserId,
        Type = UserDataType.EmailId,
        Value = "your.email@server.hz"
    };

    var vPublicKey = await keysService.PublicKeys.Create(publicKey, publicKey, userData);

Confirm **User Data** using your user data type (Currently supported only Email).

    var vUserData = vPublicKey.UserData.First();
    var confirmCode = ""; // Confirmation code you received on your email box.

    await keysService.UserData.Confirm(vUserData.UserDataId, confirmCode, vPublicKey.PublicKeyId, privateKey);

## Store Private Key

This example shows how to store private keys on Virgil Private Keys service using SDK, this step is optional and you can use your own secure storage.

    var privateKeysClient = new KeyringClient(new SDK.PrivateKeys.Http.Connection(Constants.ApplicationToken, 
    	new Uri(Constants.PrivateKeysServiceUrl)));

    var containerPassword = "12345678";

    // You can choose between few types of container. Easy and Normal
    //   Easy   - service keeps your private keys encrypted with container password, all keys should be sent 
    //            encrypted with container password, before sent to the service.
    //   Normal - responsibility for the security of the private keys at your own risk. 

    var containerType = ContainerType.Easy; // ContainerType.Normal

    // Initializes an container for private keys storage. 

    await privateKeysClient.Container.Initialize(containerType, vPublicKey.PublicKeyId, 
    	privateKey, containerPassword);

    // Authenticate requests to Virgil Private Keys service.

    privateKeysClient.Connection.SetCredentials(vUserData.Value, containerPassword);

    // Add your private key to Virgil Private Keys service.

    if (containerType == ContainerType.Easy)
    {
        // private key will be encrypted with container password, provided on authentication
        await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey);
    }
    else
    {
        // use your own password to encrypt the private key.
        var privateKeyPassword = "47N6JwTGUmFvn4Eh";
        await privateKeysClient.PrivateKeys.Add(vPublicKey.PublicKeyId, privateKey, privateKeyPassword);
    }

## Get a Public Key

Get public key from Public Keys Service.

    var recepientPublicKey = await keysService.PublicKeys.Search("recepient.email@server.hz");

## Encrypt Data

The procedure for encrypting and decrypting documents is simple. For example:

If you want to encrypt the data to Bob, you encrypt it using Bobs's public key (which you can get from Public Keys Service), and Bob decrypts it with his private key. If Bob wants to encrypt data to you, he encrypts it using your public key, and you decrypt it with your private key.

In the example below, we encrypt data using a public key from Virgil’s Public Keys Service.

    byte[] encryptedData;

    using (var cipher = new VirgilCipher())
    {
        byte[] recepientId = Encoding.UTF8.GetBytes(recepientPublicKey.PublicKeyId.ToString());
        byte[] data = Encoding.UTF8.GetBytes("Some data to be encrypted");

        cipher.AddKeyRecipient(recepientId, data);
        encryptedData = cipher.Encrypt(data, true);
    }

## Sign Data

Cryptographic digital signatures use public key algorithms to provide data integrity. When you sign data with a digital signature, someone else can verify the signature, and can prove that the data originated from you and was not altered after you signed it.

The following example applies a digital signature to a public key identifier.

    byte[] sign;
    using (var signer = new VirgilSigner())
    {
        sign = signer.Sign(encryptedData, privateKey);
    }

## Verify Data

To verify that data was signed by a particular party, you must have the following information:

*   The public key of the party that signed the data.
*   The digital signature.
*   The data that was signed.

The following example verifies a digital signature which was signed by the sender.

    bool isValid;
    using (var signer = new VirgilSigner())
    {
        isValid = signer.Verify(encryptedData, sign, publicKey);
    }

## Decrypt Data

The following example illustrates the decryption of encrypted data.

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

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>
