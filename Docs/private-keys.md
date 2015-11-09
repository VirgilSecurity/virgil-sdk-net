# Tutorial C#/.NET Private Keys SDK 

- [Introduction](#introduction)
- [Install](#install)
- [Obtaining an Application Token](#obtaining-an-application-token)
  - [Container Initialization](#container-initialization)
  - [Get Container Type](#get-container-type)
  - [Delete Container](#delete-container)
  - [Reset Container's Password](#reset-container-password)
  - [Get Private Key](#get-private-key)
  - [Add Private Key to Container](#add-private-key-to-container)
  - [Delete Private Key from Container](#delete-private-key-from-container)
- [See Also](#see-also)

##Introduction
This tutorial explains how to use Private Keys Service with SDK library in .NET applications. 

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.PrivateKeys package, running the command:

```
PM> Install-Package Virgil.SDK.PrivateKeys
```

##Obtaining an Application Token

First you must create a free Virgil Security developer account by signing up [here](https://virgilsecurity.com/account/signup). Once you have your account you can [sign in](https://virgilsecurity.com/account/signin) and generate an app token for your application.

The app token provides authenticated secure access to Virgil’s Keys Service and is passed with each API call. The app token also allows the API to associate your app’s requests with your Virgil Security developer account.

Simply add your app token to SDK client constructor.

```
var keyringClient = new KeyringClient("{ YOUR_APPLICATION_TOKEN }");
```

##Container Initialization
Initializes an easy private keys container, all private keys encrypted with account password, and server can decrypt them in case you forget container password.

```csharp
await keyringClient.Container.Initialize(ContainerType.Easy, Constants.PublicKeyId, 
  Constants.PrivateKey, password);
```

Initializes a normal private keys container, all private keys encrypted on the client side and server can't decrypt them.

```csharp
await keyringClient.Container.Initialize(ContainerType.Normal, Constants.PublicKeyId, 
  Constants.PrivateKey, password);
```

See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/InitializeContainerForPrivateKeys.cs)

##Get Container Type
The example below shows how to get the Container Type.

```csharp
var containerType = await keyringClient.Container.GetContainerType({ PUBLIC_KEY_ID });
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/GetContainerType.cs)

##Search Public Key
The example below shows how to search a **Public Key** by **User Data** identity. 

```csharp
var keysService = new KeysClient(Constants.AppToken); // use your application access token
var publicKey = await keysService.PublicKeys.Search(EmailId);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/SearchPublicKey.cs)

##Update Public Key
The example below shows how to update a **Public Key** key. You can use this method in case if your Private Key has been stolen.

```csharp
await keysService.PublicKeys.Update(Constants.PublicKeyId, newPublicKey, 
    newPrivateKey, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/UpdatePublicKey.cs)

##Reset Public Key
The example below shows how to reset a **Public Key** key. You can use this method in case if you lost your Private Key.

```csharp
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
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/ResetPublicKey.cs)

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
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/DeletePublicKey.cs)

You also can delete **Public Key** with **Private Key** without confirmation.

```csharp
await keysService.PublicKeys.Delete(Constants.PublicKeyId, Constants.PrivateKey);
```

See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/DeletePublicKeySigned.cs)

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
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/InsertUserDataIdentity.cs)

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
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/InsertUserDataInformation.cs)

##Delete User Data
The example below shows how to delete **User Data** from existing **Public Key** by **User Data** ID.
```csharp
await keysService.UserData.Delete(userData.UserDataId, Constants.PublicKeyId, Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/DeleteUserData.cs)

##Resend Confirmation for User Data
The example below shows how to re-send confirmation code to **User Data** Indentity.
```csharp
await keysService.UserData.ResendConfirmation(userData.UserDataId, Constants.PublicKeyId, 
    Constants.PrivateKey);
```
See full example [here...](https://github.com/VirgilSecurity/virgil-net/blob/master/Examples/SDK/ResendUserDataConfirmation.cs)
