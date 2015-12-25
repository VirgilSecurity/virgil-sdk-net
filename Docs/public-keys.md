# Tutorial C#/.NET Keys SDK 

- [Introduction](#introduction)
- [Install](#install)
- [Obtaining an Access Token](#obtaining-an-access-token)
- [Identity Check](#identity-check)
  - [Request for Verification](#request-for-verification)
  - [Confirm and get Identity Token](#identity-confirmation)
- [Cards and Public Keys](#cards-and-public-keys)
  - [Publish new Card](#publish-a-virgil-card)
  - [Search Cards](#search-cards)
  - [Search Application Cards](#search-application-cards)
  - [Trust the Card](#trust-the-card)
  - [Untrust the Card](#untrust-the-card)
  - [Revoke the Card](#revoke-the-card)
  - [Get a Public Key](#get-public-key)
  - [Revoke Public Key](#revoke-public-key)
- [Private Keys](#private-keys)
  - [Push Private Key](#push-private-key)
  - [Get Private Key](#get-private-key)
  - [Delete Private Key](#delete-private-key)
- [License](#license)
- [Contacts](#contacts)

##Introduction
This tutorial explains how to use Public Keys Service with SDK library in .NET applications. 

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:

```
PM> Install-Package Virgil.SDK.Keys
```

##Obtaining an Access Token

First you must create a free Virgil Security developer account by signing up [here](https://virgilsecurity.com/account/signup). Once you have your account you can [sign in](https://virgilsecurity.com/account/signin) and generate an app token for your application.

The app token provides authenticated secure access to Virgil’s Keys Service and is passed with each API call. The app token also allows the API to associate your app’s requests with your Virgil Security developer account.

Simply add your app token to the client constuctor.

```csharp
var keysService = new KeysClient("{ YOUR_APPLICATION_TOKEN }");
``` 

## Identity Check
Все Virgil Security сервисы тесно связаны с сервисом идентификации, который с помощью определенных механизмов устанавливает факт владения проверяемого identity и как результат генерирует временный токен, который будет использоватся для опираций требующих подтврждения identity.

### Request for Verification
Инициирование процесса верификации identity.

```csharp
var identityRequest = await Identity.VerifyAsync("test1@virgilsecurity.com", IdentityType.Email);
```

### Confirm and get Identity Token
Подтверждение identity и получение временного токена.

```csharp
var identityToken = await identityRequest.ConfirmAsync("%CONFIRMATION_CODE%");
```

## Cards and Public Keys
Основной сущностью в сервисе открытых ключей выступает Virgil Card, которая агрегирует в себе информацию о пользтователе и его публичном ключе. Virgil Card, идентифицирет пользователя по одному из его возможных типов, таких как Email, Phone и т.д. 

### Publish the Card
При регистрации используется identity токен который можно получить [тут](#identity-check).
```csharp
var keyPair = CryptoHelper.GenerateKeyPair();
var myCard = await keysClient.Cards.CreateAsync(identityToken, keyPair.PublicKey(), keyPair.PrivateKey());
```

### Search Cards
Поиск карты по заданным параметрам.
 ```csharp
var foundCards = await keysClient.Cards.SearchAsync("test2@virgilsecurity.com", IdentityType.Email);
```

### Search Application Cards
Поиск карточек приложений по задонному шаблону. Приведенный пример ниже возвращает списк приложений для компании Virgil Security.
 ```csharp
var foundAppCards = await keysClient.Cards.SearchAppAsync("com.virgil.*");
```

### Trust the Card
В рамках экосистемы Virgil Security любой пользователь карты может выступать в качестве центра сертификации. Каждый пользователь может заверить карту другого, и построить на основе этого сеть доверия. 
В приведенном примере ниже показанно как заверить карту пользователя, путем подписи ее hash атирибута.  
 
 ```csharp
var trustedCard = foundCards.First();
await keysClient.Cards.TrustAsync(trustedCard.Id, trustedCard.Hash, myCard.Id, keyPair.PrivateKey());
```

### Untrust the Card
Естественно как и во всех отножениях можно перестать доверять владельцу кары, и в системе Virgil Security это так же не исключение.
```csharp
await keysClient.Cards.Untrust(trustedCard.Id, myCard.Id, keyPair.PrivateKey());
```

### Revoke the Card
Данная операция позволяет удалить кариту из поиска и пометить ее как удаленную. Эта операция не относится к публичному ключю. Для того чтобы отозвать публичный ключ пользуйтесь [этим методом](#revoke-public-key).
```csharp
await keysClient.Cards.Revoke(myCard.Id, keyPair.PrivateKey());
```

## Private Keys
The security of private keys is crucial for public key cryptosystems. Anyone who can obtain a private key can use it to impersonate the rightful owner during all communications and transactions on intranets or on the Internet. Therefore, private keys must be in the possession only of authorized users, and they must be protected from unauthorized use.

-- Virgil Security предоставляет набор инструментов и сервисов по хранению приватных ключей в надежном хранилище, которое позволит синхронизировать ваши приватные ключи между девайсами и приложениями. 
-- Использование этого сервиса опционально.

### Push Private Key
-- Добавить на хранение приватный ключ можно только в том случае если у вас уже зарегистрирован публичный ключ на сервисе публичных ключей.
-- Чтобы сохранить приватный ключ используйте идентификатор публичного ключа на сервисе публичных ключей.

-- Сервер приватных ключей хранит приватные ключи в том виде в котором вы их передали, соответственно мы настоятельно рекомендуем передавать ключи сгенерированные с паролем.

```csharp
await keysClient.PrivateKeys.Push(myCard.PublicKey.Id, keyPair.PrivateKey());
```

### Get Private Key
Для получения приватного ключа нужно пройти предварительную верификацию важего card identity, в которой испльзуется ваш ключ.
  
```csharp
var identityRequest = await Identity.VerifyAsync("test1@virgilsecurity.com", IdentityType.Email);
// use confirmation code that has been sent to you email box.
var identityToken = await identityRequest.ConfirmAsync("%CONFIRMATION_CODE%");
var privateKey = await keysClient.PrivateKeys.Get(identityToken, myCard.PublicKey.Id);
```

### Delete Private Key
Удаляет приватный ключ с сервиса, без возможности его востановления.
  
```csharp
var privateKey = await keysClient.PrivateKeys.Delete(myCard.PublicKey.Id, keyPair.PrivateKey());
```

## See Also

* [Quickstart](quickstart.md)
* [Tutorial Crypto Library](crypto.md)
* [Tutorial Private Keys SDK](private-keys.md)

