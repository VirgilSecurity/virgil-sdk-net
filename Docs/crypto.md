# Tutorial C#/.NET Crypto Library

- [Introduction](#introduction)
- [Install](#install)
    - [Generate Keys](#generate-keys)
    - [Encrypt Data](#encrypt-data)
    - [Sign Data](#sign-data)
    - [Verify Data](#verify-data)
    - [Decrypt Data](#decrypt-data)
- [See Also](#see-also)

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:

```
PM> Install-Package Virgil.Crypto
```

## Generate Keys

The following code example creates a new public/private key pair.

```csharp
byte[] publicKey;
byte[] privateKey;

using (var keyPair new VirgilKeyPair())
{
    publicKey = keyPair.PublicKey();
    privateKey = keyPair.PrivateKey();
}
```

You also can generate key pair with encrypted **Private Key**, just passing password to constructor of VirgilKeyPair:

```csharp
var passwordData = Encoding.UTF8.GetBytes(password)

using (var keyPair = new VirgilKeyPair(passwordData))
{
    ...
}
```

## Encrypt Data

The procedure for encrypting and decrypting documents is simple. For example:

If you want to encrypt the data to Bob, you encrypt it using Bobs's public key (which you can get from Public Keys Service), and Bob decrypts it with his private key. If Bob wants to encrypt data to you, he encrypts it using your public key, and you decrypt it with your private key.

In the example below, we encrypt data using a public key from Virgil’s Public Keys Service.

```csharp
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

```csharp
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

```csharp
bool isValid;
using (var signer = new VirgilSigner())
{
    isValid = signer.Verify(encryptedData, sign, publicKey);
}
```

## Decrypt Data

The following example illustrates the decryption of encrypted data.

```csharp
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
