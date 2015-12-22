# Quickstart C#/.NET

- [Introduction](#introduction)
- [Obtaining an App Token](#obtaining-an-app-token)
- [Install](#install)
- [Use case](#use-case)
    - [Step 0. Preperation](#step_0_preperation)
    - [Step 1. Create and Publish the Keys](#step_1_create and publish the keys)
    - [Step 2. Encrypt and Sign](#step_2_encrypt_and_sign)
    - [Step 3. Store Private Key](#store-private-key)
- [See also](#see-also)

## Introduction
-- Несколько слов о юзкейсе и ссылку на пункт меню
This guide will help you get started using the Crypto Library and Virgil Keys Service, for the most popular platforms and languages.
This branch focuses on the C#/.NET library implementation and covers it's usage.

## Obtaining an Access Token

First you must create a free Virgil Security developer account by signing up [here](https://developer.virgilsecurity.com/account/signup). Once you have your account you can [signin](https://developer.virgilsecurity.com/account/signin) and generate an access token for your application.

The access token provides authenticated secure access to Virgil’s Keys Service and is passed with each API call. The access token also allows the API to associate your app’s requests with your Virgil Security developer account.

--Use this token to initialize the SDK clint [here...](#preperation)

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

## Initialization

```csharp
var keysClient = new Virgil.SDK.KeysClient("%ACCESS_TOKEN%");
```

## Step 1. Create and Publish the Keys
-- генерируем ключи и паблишим их на сервис публичных ключей где они будут доступны в публичном доступе чтобы другие пользователи в часности ресипиет могли проверять или зашифровать данные для владельца этого ключа

The following code example creates a new public/private key pair.

```csharp
var password = "jUfreBR7";
// the private key's password is optional 
var keyPair = CryptoHelper.GenerateKeyPair(password); 
```
-- проверяем действительно ли пользователь владеет предоставленным адресом электронной почты и получаем временный токен для регистрации открытого ключа на сервисе публичных ключей

```csharp
var identityRequest = Identity.VerifyAsync("sender-test@virgilsecurity.com", IdentityType.Email);

// use confirmation code sent to your email box.
var identityToken = await identityRequest.ConfirmAsync("%CONFIRMATION_CODE%");
```
-- Регистрируем Virgil Card в которая состоит из публичного ключа и идентификатора Email Address. Карта будет использоваться для идентификации публичного ключа и поиском его в сервисе открытых ключей

```csharp
var senderCard = await keysClient.Cards.Create(identityToken, keyPair.PublicKey());
```

## Step 2. Encrypt and Sign
Для того чтобы зашифровать сообщение собеседнику нам нужно найти его публичный ключ на сервисе публичных ключей. И своим приватным ключем подписать зашифрованное сообщение для того чтобы собеседник мог быть уверен что это сообщение было отосланно от заявленного отправителя.

```csharp
var message = "Encrypt me, Please!!!";

var recipientCards = await keysClient.Cards.Search("recipient-test@virgilsecurity.com", IdentityType.Email);
var recipients = recipientCards.ToDictionary(it => it.Id, it => it.PublicKey);

var encryptedMessage = CryptoHelper.Encrypt(message, recipients);
var signature = CryptoHelper.Sign(cipherText, keyPair.PrivateKey());
```

## Step 3. Send an Email to Recipient
-- компонуем сообщение с подписью в одну структуру и отсылаем письмо собеседнику используя простой SMTP клиент.

```csharp
var encryptedBody = new EncryptedBody
{
    Content = encryptedMessage,
    Signature = signature
};

var encryptedBodyJson = JsonConvert.SerializeObject(encryptedBody);
await mailClient.SendAsync("recipient-test@virgilsecurity.com", "Secure the Future", encryptedBodyJson);
```

## Step 4. Receiving an Email by Recipient
-- на стороне собеседника мы получаем зашифрованное письмо используя простой маил клиент.

```csharp
// get first email with specified subject using simple mail client
var email = await mailClient.GetBySubjectAsync("recipient-test@virgilsecurity.com", "Secure the Future");

var encryptedBody = JsonConvert.Deserialize<EncryptedBody>(email.Body);
```

## Step 5. Verify and Decrypt
-- убеждаемся что письмо пришло от заявленного отправителя получая его карту на сервисе публичных ключей. И в случае успе][a расшифровуем письмо используя приватный ключ собеседника 

```csharp
var senderCard = await keysClient.Search(email.From, IdentityType.Email);

var isValid = CryptoHelper.Verify(encryptedBody.Content, encryptedBody.Sign, senderCard.PublicKey);
if (isValid)
{
    throw new Exception("Signature is not valid.");
}
    
var originalMessage = CryptoHelper.Decrypt(encryptedBody.Content, recipientKeyPair.PrivateKey());
```

## See Also

* [Tutorial Crypto Library](crypto.md)
* [Tutorial Keys SDK](public-keys.md)
* [Tutorial Private Keys SDK](private-keys.md)
