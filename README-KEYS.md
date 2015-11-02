# Tutorial C#/.NET Keys SDK 

- [Introduction](#introduction)
- [Install](#install)
  - [Register Public Key](#register-public-key)
  - [Get a Public Key](#get-a-public-key)
  - [Search Public Key](#search-public-key)
  - [Update Public Key](#update-public-key)
  - [Reset Public Key](#reset-public-key)
  - [Delete Public Key](#delete-public-key)
  - [Insert User Data](#insert-user-data)
  - [Delete User Data](#delete-user-data)
  - [Confirm User Data](#confirm-user-data)
  - [Resend Confirmation for User Data](#resend-confirmation-for-user-data)
- [License](#license)
- [Contacts](#contacts)

##Introduction
This tutorial explains how to use Public Keys Service with SDK library in .NET applications. 

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:

```
PM> Install-Package Virgil.SDK.Keys
```

##Register Public Key
The example below shows how to register a new **Public Key** for specified application. A **Public Key** should be provided with **User Data** identity. To complete registration this **User Data** must be confirmed with verification code.

```csharp
var userData = new UserData
{
    Class = UserDataClass.UserId,
    Type = UserDataType.EmailId,
    Value = EmailId
};

var keysService = new KeysClient(AppToken);
var result = await keysService.PublicKeys.Create(publicKey, privateKey, userData);

// check an email box for confirmation code.

var userDataId = result.UserData.First().UserDataId;

var confirmationCode = "K5J1E4"; // confirmation code you received on email.
await keysService.UserData.Confirm(userDataId, confirmationCode, result.PublicKeyId, privateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Get a Public Key
The example below shows how to get a **Public Key** by identifier. A **Public Key** identifier is assigned on registration stage and then can be used to access it's access.

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
var publicKey = await keysService.PublicKeys.GetById(Constants.PublicKeyId);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

You also can get a **Public Key** with all **User Data** items by providing **Private Key** signature.

```csharp
var publicKey = await keysService.PublicKeys.SearchExtended(Constants.PublicKeyId, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)


##Search Public Key
The example below shows how to search a **Public Key** by **User Data** identity. 

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
var publicKey = await keysService.PublicKeys.Search(EmailId);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Update Public Key
The example below shows how to update a **Public Key** key. You can use this method in case if your Private Key has been stolen.

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
await keysService.PublicKeys.Update(Constants.PublicKeyId, newPublicKey, 
    newPrivateKey, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Reset Public Key
The example below shows how to reset a **Public Key** key. You can use this method in case if you lost your Private Key.

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
var resetResult = await keysService.PublicKeys.Reset(Constants.PublicKeyId, newPublicKey, newPrivateKey);

// once you reset the Public Key you need to confirm this action with all User Data 
// identities registered for this key.

var resetConfirmation = new PublicKeyOperationComfirmation
{
    ActionToken = resetResult.ActionToken,
    ConfirmationCodes = new[] { "F0G4T3", "D9S6J1" }
};

await keysService.PublicKeys.ConfirmReset(Constants.PublicKeyId, newPrivateKey, resetConfirmation);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Delete Public Key
The example below shows how to delete a **Public Key** without **Private Key**.

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
var resetResult = await keysService.PublicKeys.Delete(Constants.PublicKeyId);

// once you deleted the Public Key you need to confirm this action with all User Data 
// identities registered for this key.

var resetConfirmation = new PublicKeyOperationComfirmation
{
    ActionToken = resetResult.ActionToken,
    ConfirmationCodes = new[] { "F0G4T3", "D9S6J1" }
};

await keysService.PublicKeys.ConfirmDelete(Constants.PublicKeyId, resetConfirmation);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

You also can delete **Public Key** with **Private Key** without confirmation.

```csharp
await keysService.PublicKeys.Delete(Constants.PublicKeyId, Constants.PrivateKey);
```

See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Insert User Data
The example below shows how to add **User Data** Indentity for existing **Public Key**.
```csharp

var userData = new UserData
{
    Class = UserDataClass.UserId, 
    Type = UserDataType.EmailId,
    Value = "virgil-demo+1@freeletter.me"
};

var insertResult = await keysService.UserData.Insert(userData, Constants.PublicKeyId, Constants.PrivateKey);

// check an email box for confirmation code.

var userDataId = insertResult.UserDataId;

var code = "R6H1E4"; // confirmation code you received on email.
await keysService.UserData.Confirm(userDataId, code, Constants.PublicKeyId, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

Use method below to insert **User Data** Information.
```csharp

var userData = new UserData
{
    Class = UserDataClass.UserInfo,
    Type = UserDataType.FirstNameInfo,
    Value = "Denis"
};

await keysService.UserData.Insert(userData, Constants.PublicKeyId, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Delete User Data
The example below shows how to delete **User Data** from existing **Public Key** by **User Data** ID.
```csharp
await keysService.UserData.Delete(userData.UserDataId, Constants.PublicKeyId, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)

##Resend Confirmation for User Data
The example below shows how to re-send confirmation code to **User Data** Indentity.
```csharp
await keysService.UserData.ResendConfirmation(userData.UserDataId, Constants.PublicKeyId, 
    Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/edit/master/README-KEYS.md#register-a-public-key)
