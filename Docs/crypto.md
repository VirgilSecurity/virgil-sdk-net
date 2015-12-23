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
var keyPair = CryptoHelper.GenerateKeyPair();
```

You also can generate key pair with encrypted **Private Key**, just using one of overloaded constructors

```csharp
var password = "TafaSuf4";
var keyPair = CryptoHelper.GenerateKeyPair(password);
```

In example below you can see simply generated Public/Private keypair without password.

```
-----BEGIN PUBLIC KEY-----
MIGbMBQGByqGSM49AgEGCSskAwMCCAEBDQOBggAEWIH2SohavmLdRwEJ/VWbFcWr
rU+g7Z/BkI+E1L5JF7Jlvi1T1ed5P0/JCs+K0ZBM/0hip5ThhUBKK2IMbeFjS3Oz
zEsWKgDn8j3WqTb8uaKIFWWG2jEEnU/8S81Bgpw6CyxbCTWoB+0+eDYO1pZesaIS
Tv6dTclx3GljHpFRdZQ=
-----END PUBLIC KEY-----

-----BEGIN EC PRIVATE KEY-----
MIHaAgEBBEAaKrInIcjTeBI6B0mX+W4gMpu84iJtlPxksCQ1Dv+8iM/lEwx3nWWf
ol6OvLkmG/qP9RqyXkTSCW+QONiN9JCEoAsGCSskAwMCCAEBDaGBhQOBggAEWIH2
SohavmLdRwEJ/VWbFcWrrU+g7Z/BkI+E1L5JF7Jlvi1T1ed5P0/JCs+K0ZBM/0hi
p5ThhUBKK2IMbeFjS3OzzEsWKgDn8j3WqTb8uaKIFWWG2jEEnU/8S81Bgpw6Cyxb
CTWoB+0+eDYO1pZesaISTv6dTclx3GljHpFRdZQ=
-----END EC PRIVATE KEY-----
```

Here is what encrypted Private Key looks like:

```
-----BEGIN ENCRYPTED PRIVATE KEY-----
MIIBKTA0BgoqhkiG9w0BDAEDMCYEIJjDIF2KRj7u86Up1ZB4yHHKhqMg5C/OW2+F
mG5gpI+3AgIgAASB8F39JXRBTK5hyqEHCLcCTbtLKijdNH3t+gtCrLyMlfSfK49N
UTREjF/CcojkyDVs9M0y5K2rTKP0S/LwUWeNoO0zCT6L/zp/qIVy9wCSAr+Ptenz
MR6TLtglpGqpG4bhjqLNR2I96IufFmK+ZrJvJeZkRiMXQSWbPavepnYRUAbXHXGB
a8HWkrjKPHW6KQxKkotGRLcThbi9cDtH+Cc7FvwT80O7qMyIFQvk8OUJdY3sXWH4
5tol7pMolbalqtaUc6dGOsw6a4UAIDaZhT6Pt+v65LQqA34PhgiCxQvJt2UOiPdi
SFMQ8705Y2W1uTexqw==
-----END ENCRYPTED PRIVATE KEY-----
```

See working example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/Crypto/GenerateKeyPair.cs)

## Encrypt Data

The procedure for encrypting and decrypting data is simple. For example:

If you want to encrypt the data to Bob, you encrypt it using Bobs's public key (which you can get from Public Keys Service), and Bob decrypts it with his private key. If Bob wants to encrypt data to you, he encrypts it using your public key, and you decrypt it with your private key.

Crypto Library allows to encrypt data for several types of recipient's user data like **Public Key** and Password. This means that you can encrypt data with some password or with **Public Key** generated with **Crypto Library**. 

Encrypt text with password:

```csharp
var textToEncrypt = "Encrypt me, Please!!!";
var password = "TafaSuf4";

var cipherText = CryptoHelper.Encrypt(textToEncrypt, password);
```

Encrypt text with public key:

```csharp
var keyPair = CryptoHelper.GenerateKeyPair();
var cipherText = CryptoHelper.Encrypt(textToEncrypt, "RecipientID" ,password);
```

And of course you can mix this types as well, see how it works in example below:

```csharp
var textToEncrypt = "Encrypt me, Please!!!";
byte[] cipherData;

using (var cipher = new VirgilCipher())
{
    cipher.AddPasswordRecipient(password);
    cipher.AddKeyRecipient(keyRecepinet.Id, keyRecepinet.PublicKey);

    cipherData = cipher.Encrypt(Encoding.UTF8.GetBytes(textToEncrypt), true);
}
```



See working example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/Crypto/Encryption.cs)

## Sign Data

Cryptographic digital signatures use public key algorithms to provide data integrity. When you sign data with a digital signature, someone else can verify the signature, and can prove that the data originated from you and was not altered after you signed it.

The following example applies a digital signature to a public key identifier.

```csharp
var originalText = "Sign me, Please!!!";

var keyPair = CryptoHelper.GenerateKeyPair();
var signature = CryptoHelper.Sign(originalText, keyPair.PrivateKey());
```

See working example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/Crypto/SingAndVerify.cs)

## Verify Data

To verify that data was signed by a particular party, you must have the following information:

*   The public key of the party that signed the data.
*   The digital signature.
*   The data that was signed.

The following example verifies a digital signature which was signed by the sender.

```csharp
var isValid = CryptoHelper.Verify(originalText, signature, keyPair.PublicKey());
```

See working example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/Crypto/SingAndVerify.cs)

## Decrypt Data

The following example illustrates the decryption of encrypted data with recipient's **Private Key**.

```csharp
var decryptedText = CryptoHelper.Decrypt(cipherText, "RecipientId", keyPair.PrivateKey());
```

Use password to decrypt the data

```csharp
var decryptedText = CryptoHelper.Decrypt(cipherText, password);
```

See working example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/Crypto/Encryption.cs)

## See Also

* [Quickstart](quickstart.md)
* [Tutorial Keys SDK](public-keys.md)
* [Tutorial Private Keys SDK](private-keys.md)
