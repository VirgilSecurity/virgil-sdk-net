<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [CardModel](#T-Virgil-SDK-Models-CardModel 'Virgil.SDK.Models.CardModel')
  - [AuthorizedBy](#P-Virgil-SDK-Models-CardModel-AuthorizedBy 'Virgil.SDK.Models.CardModel.AuthorizedBy')
  - [CreatedAt](#P-Virgil-SDK-Models-CardModel-CreatedAt 'Virgil.SDK.Models.CardModel.CreatedAt')
  - [CustomData](#P-Virgil-SDK-Models-CardModel-CustomData 'Virgil.SDK.Models.CardModel.CustomData')
  - [Hash](#P-Virgil-SDK-Models-CardModel-Hash 'Virgil.SDK.Models.CardModel.Hash')
  - [Id](#P-Virgil-SDK-Models-CardModel-Id 'Virgil.SDK.Models.CardModel.Id')
  - [Identity](#P-Virgil-SDK-Models-CardModel-Identity 'Virgil.SDK.Models.CardModel.Identity')
  - [PublicKey](#P-Virgil-SDK-Models-CardModel-PublicKey 'Virgil.SDK.Models.CardModel.PublicKey')
- [CardsServiceClient](#T-Virgil-SDK-Clients-CardsServiceClient 'Virgil.SDK.Clients.CardsServiceClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-CardsServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.CardsServiceClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Create(identityInfo,publicKeyId,privateKey,privateKeyPassword,customData)](#M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.CardsServiceClient.Create(Virgil.SDK.Identities.IdentityInfo,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityInfo,publicKey,privateKey,privateKeyPassword,customData)](#M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.CardsServiceClient.Create(Virgil.SDK.Identities.IdentityInfo,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Get(cardId)](#M-Virgil-SDK-Clients-CardsServiceClient-Get-System-Guid- 'Virgil.SDK.Clients.CardsServiceClient.Get(System.Guid)')
  - [GetAsync(cardId)](#M-Virgil-SDK-Clients-CardsServiceClient-GetAsync-System-Guid- 'Virgil.SDK.Clients.CardsServiceClient.GetAsync(System.Guid)')
  - [Revoke(cardId,identityInfo,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-CardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String- 'Virgil.SDK.Clients.CardsServiceClient.Revoke(System.Guid,Virgil.SDK.Identities.IdentityInfo,System.Byte[],System.String)')
  - [Search(identityValue,identityType,includeUnauthorized)](#M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}- 'Virgil.SDK.Clients.CardsServiceClient.Search(System.String,System.String,System.Nullable{System.Boolean})')
  - [Search(identityValue,identityType)](#M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType- 'Virgil.SDK.Clients.CardsServiceClient.Search(System.String,Virgil.SDK.Identities.IdentityType)')
- [ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Http-ConnectionBase-#ctor-System-String,System-Uri- 'Virgil.SDK.Http.ConnectionBase.#ctor(System.String,System.Uri)')
  - [AccessTokenHeaderName](#F-Virgil-SDK-Http-ConnectionBase-AccessTokenHeaderName 'Virgil.SDK.Http.ConnectionBase.AccessTokenHeaderName')
  - [Errors](#F-Virgil-SDK-Http-ConnectionBase-Errors 'Virgil.SDK.Http.ConnectionBase.Errors')
  - [AccessToken](#P-Virgil-SDK-Http-ConnectionBase-AccessToken 'Virgil.SDK.Http.ConnectionBase.AccessToken')
  - [BaseAddress](#P-Virgil-SDK-Http-ConnectionBase-BaseAddress 'Virgil.SDK.Http.ConnectionBase.BaseAddress')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.ConnectionBase.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
  - [GetNativeRequest(request)](#M-Virgil-SDK-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Http.ConnectionBase.GetNativeRequest(Virgil.SDK.Http.IRequest)')
  - [Send(request)](#M-Virgil-SDK-Http-ConnectionBase-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Http.ConnectionBase.Send(Virgil.SDK.Http.IRequest)')
  - [ThrowException\`\`1(message,exception)](#M-Virgil-SDK-Http-ConnectionBase-ThrowException``1-System-Net-Http-HttpResponseMessage,System-Func{System-Int32,System-String,``0}- 'Virgil.SDK.Http.ConnectionBase.ThrowException``1(System.Net.Http.HttpResponseMessage,System.Func{System.Int32,System.String,``0})')
  - [TryParseErrorCode(content)](#M-Virgil-SDK-Http-ConnectionBase-TryParseErrorCode-System-String- 'Virgil.SDK.Http.ConnectionBase.TryParseErrorCode(System.String)')
- [DynamicKeyCache](#T-Virgil-SDK-Clients-DynamicKeyCache 'Virgil.SDK.Clients.DynamicKeyCache')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-DynamicKeyCache-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.DynamicKeyCache.#ctor(Virgil.SDK.Http.IConnection)')
  - [GetApplicationCards()](#M-Virgil-SDK-Clients-DynamicKeyCache-GetApplicationCards-System-String- 'Virgil.SDK.Clients.DynamicKeyCache.GetApplicationCards(System.String)')
  - [GetServiceCard(servicePublicKeyId)](#M-Virgil-SDK-Clients-DynamicKeyCache-GetServiceCard-System-String- 'Virgil.SDK.Clients.DynamicKeyCache.GetServiceCard(System.String)')
- [EmailVerifier](#T-Virgil-SDK-Identities-EmailVerifier 'Virgil.SDK.Identities.EmailVerifier')
  - [#ctor()](#M-Virgil-SDK-Identities-EmailVerifier-#ctor-System-Guid,Virgil-SDK-Clients-IIdentityServiceClient- 'Virgil.SDK.Identities.EmailVerifier.#ctor(System.Guid,Virgil.SDK.Clients.IIdentityServiceClient)')
  - [ActionId](#P-Virgil-SDK-Identities-EmailVerifier-ActionId 'Virgil.SDK.Identities.EmailVerifier.ActionId')
  - [Confirm(code,timeToLive,countToLive)](#M-Virgil-SDK-Identities-EmailVerifier-Confirm-System-String,System-Int32,System-Int32- 'Virgil.SDK.Identities.EmailVerifier.Confirm(System.String,System.Int32,System.Int32)')
- [EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.EndpointClient.#ctor(Virgil.SDK.Http.IConnection)')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.EndpointClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Cache](#F-Virgil-SDK-Clients-EndpointClient-Cache 'Virgil.SDK.Clients.EndpointClient.Cache')
  - [Connection](#F-Virgil-SDK-Clients-EndpointClient-Connection 'Virgil.SDK.Clients.EndpointClient.Connection')
  - [EndpointApplicationId](#F-Virgil-SDK-Clients-EndpointClient-EndpointApplicationId 'Virgil.SDK.Clients.EndpointClient.EndpointApplicationId')
  - [Send()](#M-Virgil-SDK-Clients-EndpointClient-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.EndpointClient.Send(Virgil.SDK.Http.IRequest)')
  - [Send\`\`1()](#M-Virgil-SDK-Clients-EndpointClient-Send``1-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.EndpointClient.Send``1(Virgil.SDK.Http.IRequest)')
- [Ensure](#T-Virgil-SDK-Helpers-Ensure 'Virgil.SDK.Helpers.Ensure')
  - [ArgumentNotNull(value,name)](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNull-System-Object,System-String- 'Virgil.SDK.Helpers.Ensure.ArgumentNotNull(System.Object,System.String)')
  - [ArgumentNotNullOrEmptyString(value,name)](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String- 'Virgil.SDK.Helpers.Ensure.ArgumentNotNullOrEmptyString(System.String,System.String)')
- [ICardsServiceClient](#T-Virgil-SDK-Clients-ICardsServiceClient 'Virgil.SDK.Clients.ICardsServiceClient')
  - [Create(identityInfo,publicKeyId,privateKey,privateKeyPassword,customData)](#M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.ICardsServiceClient.Create(Virgil.SDK.Identities.IdentityInfo,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityInfo,publicKey,privateKey,privateKeyPassword,customData)](#M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.ICardsServiceClient.Create(Virgil.SDK.Identities.IdentityInfo,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Get(cardId)](#M-Virgil-SDK-Clients-ICardsServiceClient-Get-System-Guid- 'Virgil.SDK.Clients.ICardsServiceClient.Get(System.Guid)')
  - [GetAsync(cardId)](#M-Virgil-SDK-Clients-ICardsServiceClient-GetAsync-System-Guid- 'Virgil.SDK.Clients.ICardsServiceClient.GetAsync(System.Guid)')
  - [PublishAsync()](#M-Virgil-SDK-Clients-ICardsServiceClient-PublishAsync-Virgil-SDK-VirgilCardRequest- 'Virgil.SDK.Clients.ICardsServiceClient.PublishAsync(Virgil.SDK.VirgilCardRequest)')
  - [Revoke(cardId,identityInfo,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-ICardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String- 'Virgil.SDK.Clients.ICardsServiceClient.Revoke(System.Guid,Virgil.SDK.Identities.IdentityInfo,System.Byte[],System.String)')
  - [Search(identityValue,identityType,includeUnauthorized)](#M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}- 'Virgil.SDK.Clients.ICardsServiceClient.Search(System.String,System.String,System.Nullable{System.Boolean})')
  - [Search(identityValue,identityType)](#M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType- 'Virgil.SDK.Clients.ICardsServiceClient.Search(System.String,Virgil.SDK.Identities.IdentityType)')
  - [SearchAsync(identity,identityType)](#M-Virgil-SDK-Clients-ICardsServiceClient-SearchAsync-System-String,System-String- 'Virgil.SDK.Clients.ICardsServiceClient.SearchAsync(System.String,System.String)')
- [IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')
  - [BaseAddress](#P-Virgil-SDK-Http-IConnection-BaseAddress 'Virgil.SDK.Http.IConnection.BaseAddress')
  - [Send(request)](#M-Virgil-SDK-Http-IConnection-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Http.IConnection.Send(Virgil.SDK.Http.IRequest)')
- [ICryptoProvider](#T-Virgil-SDK-Cryptography-ICryptoProvider 'Virgil.SDK.Cryptography.ICryptoProvider')
  - [Decrypt(cipherData,password)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String- 'Virgil.SDK.Cryptography.ICryptoProvider.Decrypt(System.Byte[],System.String)')
  - [Decrypt(cipherData,recipientId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Virgil.SDK.Cryptography.ICryptoProvider.Decrypt(System.Byte[],System.String,System.Byte[],System.String)')
  - [Encrypt(data,password)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-String- 'Virgil.SDK.Cryptography.ICryptoProvider.Encrypt(System.Byte[],System.String)')
  - [Encrypt(data,recipients)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Virgil.SDK.Cryptography.ICryptoProvider.Encrypt(System.Byte[],System.Collections.Generic.IDictionary{System.String,System.Byte[]})')
  - [GenerateKeypair()](#M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair 'Virgil.SDK.Cryptography.ICryptoProvider.GenerateKeypair')
  - [GenerateKeypair()](#M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair-Virgil-SDK-Cryptography-KeyPairType- 'Virgil.SDK.Cryptography.ICryptoProvider.GenerateKeypair(Virgil.SDK.Cryptography.KeyPairType)')
  - [Sign(data,privateKey,privateKeyPassword)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Sign-System-Byte[],System-Byte[],System-String- 'Virgil.SDK.Cryptography.ICryptoProvider.Sign(System.Byte[],System.Byte[],System.String)')
  - [Verify(data,signData,publicKey)](#M-Virgil-SDK-Cryptography-ICryptoProvider-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Virgil.SDK.Cryptography.ICryptoProvider.Verify(System.Byte[],System.Byte[],System.Byte[])')
- [IdentityConfirmationResponse](#T-Virgil-SDK-Identities-IdentityConfirmationResponse 'Virgil.SDK.Identities.IdentityConfirmationResponse')
  - [Type](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-Type 'Virgil.SDK.Identities.IdentityConfirmationResponse.Type')
  - [ValidationToken](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-ValidationToken 'Virgil.SDK.Identities.IdentityConfirmationResponse.ValidationToken')
  - [Value](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-Value 'Virgil.SDK.Identities.IdentityConfirmationResponse.Value')
- [IdentityConnection](#T-Virgil-SDK-Http-IdentityConnection 'Virgil.SDK.Http.IdentityConnection')
  - [#ctor(baseAddress)](#M-Virgil-SDK-Http-IdentityConnection-#ctor-System-Uri- 'Virgil.SDK.Http.IdentityConnection.#ctor(System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-IdentityConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.IdentityConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo')
  - [Type](#P-Virgil-SDK-Identities-IdentityInfo-Type 'Virgil.SDK.Identities.IdentityInfo.Type')
  - [ValidationToken](#P-Virgil-SDK-Identities-IdentityInfo-ValidationToken 'Virgil.SDK.Identities.IdentityInfo.ValidationToken')
  - [Value](#P-Virgil-SDK-Identities-IdentityInfo-Value 'Virgil.SDK.Identities.IdentityInfo.Value')
- [IdentityModel](#T-Virgil-SDK-Models-IdentityModel 'Virgil.SDK.Models.IdentityModel')
  - [CreatedAt](#P-Virgil-SDK-Models-IdentityModel-CreatedAt 'Virgil.SDK.Models.IdentityModel.CreatedAt')
  - [Id](#P-Virgil-SDK-Models-IdentityModel-Id 'Virgil.SDK.Models.IdentityModel.Id')
  - [Type](#P-Virgil-SDK-Models-IdentityModel-Type 'Virgil.SDK.Models.IdentityModel.Type')
  - [Value](#P-Virgil-SDK-Models-IdentityModel-Value 'Virgil.SDK.Models.IdentityModel.Value')
- [IdentityServiceClient](#T-Virgil-SDK-Clients-IdentityServiceClient 'Virgil.SDK.Clients.IdentityServiceClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-IdentityServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.IdentityServiceClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Confirm(actionId,code,timeToLive,countToLive)](#M-Virgil-SDK-Clients-IdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IdentityServiceClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(identityValue,identityType,validationToken)](#M-Virgil-SDK-Clients-IdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String- 'Virgil.SDK.Clients.IdentityServiceClient.IsValid(System.String,Virgil.SDK.Identities.VerifiableIdentityType,System.String)')
  - [Verify(identityValue,identityType,extraFields)](#M-Virgil-SDK-Clients-IdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IdentityServiceClient.Verify(System.String,Virgil.SDK.Identities.VerifiableIdentityType,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [VerifyEmail(emailAddress,extraFields)](#M-Virgil-SDK-Clients-IdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IdentityServiceClient.VerifyEmail(System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
- [IdentityServiceServiceException](#T-Virgil-SDK-Exceptions-IdentityServiceServiceException 'Virgil.SDK.Exceptions.IdentityServiceServiceException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-IdentityServiceServiceException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.IdentityServiceServiceException.#ctor(System.Int32,System.String)')
- [IdentityType](#T-Virgil-SDK-Identities-IdentityType 'Virgil.SDK.Identities.IdentityType')
  - [Application](#F-Virgil-SDK-Identities-IdentityType-Application 'Virgil.SDK.Identities.IdentityType.Application')
  - [Email](#F-Virgil-SDK-Identities-IdentityType-Email 'Virgil.SDK.Identities.IdentityType.Email')
- [IdentityVerificationResponse](#T-Virgil-SDK-Identities-IdentityVerificationResponse 'Virgil.SDK.Identities.IdentityVerificationResponse')
  - [ActionId](#P-Virgil-SDK-Identities-IdentityVerificationResponse-ActionId 'Virgil.SDK.Identities.IdentityVerificationResponse.ActionId')
- [IEmailVerifier](#T-Virgil-SDK-Identities-IEmailVerifier 'Virgil.SDK.Identities.IEmailVerifier')
  - [ActionId](#P-Virgil-SDK-Identities-IEmailVerifier-ActionId 'Virgil.SDK.Identities.IEmailVerifier.ActionId')
  - [Confirm(code,timeToLive,countToLive)](#M-Virgil-SDK-Identities-IEmailVerifier-Confirm-System-String,System-Int32,System-Int32- 'Virgil.SDK.Identities.IEmailVerifier.Confirm(System.String,System.Int32,System.Int32)')
- [IIdentityServiceClient](#T-Virgil-SDK-Clients-IIdentityServiceClient 'Virgil.SDK.Clients.IIdentityServiceClient')
  - [Confirm(actionId,code,timeToLive,countToLive)](#M-Virgil-SDK-Clients-IIdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IIdentityServiceClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(identityValue,identityType,validationToken)](#M-Virgil-SDK-Clients-IIdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String- 'Virgil.SDK.Clients.IIdentityServiceClient.IsValid(System.String,Virgil.SDK.Identities.VerifiableIdentityType,System.String)')
  - [Verify(identityValue,identityType,extraFields)](#M-Virgil-SDK-Clients-IIdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IIdentityServiceClient.Verify(System.String,Virgil.SDK.Identities.VerifiableIdentityType,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [VerifyEmail(emailAddress,extraFields)](#M-Virgil-SDK-Clients-IIdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IIdentityServiceClient.VerifyEmail(System.String,System.Collections.Generic.IDictionary{System.String,System.String})')
- [IPrivateKeysServiceClient](#T-Virgil-SDK-Clients-IPrivateKeysServiceClient 'Virgil.SDK.Clients.IPrivateKeysServiceClient')
  - [Destroy(cardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Destroy-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IPrivateKeysServiceClient.Destroy(System.Guid,System.Byte[],System.String)')
  - [Get(cardId,identityInfo)](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo- 'Virgil.SDK.Clients.IPrivateKeysServiceClient.Get(System.Guid,Virgil.SDK.Identities.IdentityInfo)')
  - [Get(cardId,identityInfo,responsePassword)](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-String- 'Virgil.SDK.Clients.IPrivateKeysServiceClient.Get(System.Guid,Virgil.SDK.Identities.IdentityInfo,System.String)')
  - [Stash(cardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Stash-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IPrivateKeysServiceClient.Stash(System.Guid,System.Byte[],System.String)')
- [IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest')
  - [Body](#P-Virgil-SDK-Http-IRequest-Body 'Virgil.SDK.Http.IRequest.Body')
  - [Endpoint](#P-Virgil-SDK-Http-IRequest-Endpoint 'Virgil.SDK.Http.IRequest.Endpoint')
  - [Headers](#P-Virgil-SDK-Http-IRequest-Headers 'Virgil.SDK.Http.IRequest.Headers')
  - [Method](#P-Virgil-SDK-Http-IRequest-Method 'Virgil.SDK.Http.IRequest.Method')
- [IResponse](#T-Virgil-SDK-Http-IResponse 'Virgil.SDK.Http.IResponse')
  - [Body](#P-Virgil-SDK-Http-IResponse-Body 'Virgil.SDK.Http.IResponse.Body')
  - [Headers](#P-Virgil-SDK-Http-IResponse-Headers 'Virgil.SDK.Http.IResponse.Headers')
  - [StatusCode](#P-Virgil-SDK-Http-IResponse-StatusCode 'Virgil.SDK.Http.IResponse.StatusCode')
- [IServiceHub](#T-Virgil-SDK-Clients-IServiceHub 'Virgil.SDK.Clients.IServiceHub')
  - [Cards](#P-Virgil-SDK-Clients-IServiceHub-Cards 'Virgil.SDK.Clients.IServiceHub.Cards')
  - [Identity](#P-Virgil-SDK-Clients-IServiceHub-Identity 'Virgil.SDK.Clients.IServiceHub.Identity')
  - [PrivateKeys](#P-Virgil-SDK-Clients-IServiceHub-PrivateKeys 'Virgil.SDK.Clients.IServiceHub.PrivateKeys')
- [IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache')
  - [GetServiceCard(servicePublicKeyId)](#M-Virgil-SDK-Clients-IServiceKeyCache-GetServiceCard-System-String- 'Virgil.SDK.Clients.IServiceKeyCache.GetServiceCard(System.String)')
- [IVirgilService](#T-Virgil-SDK-Clients-IVirgilService 'Virgil.SDK.Clients.IVirgilService')
- [KeyPairType](#T-Virgil-SDK-Cryptography-KeyPairType 'Virgil.SDK.Cryptography.KeyPairType')
  - [Default](#F-Virgil-SDK-Cryptography-KeyPairType-Default 'Virgil.SDK.Cryptography.KeyPairType.Default')
  - [EC_BP256R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP256R1 'Virgil.SDK.Cryptography.KeyPairType.EC_BP256R1')
  - [EC_BP384R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP384R1 'Virgil.SDK.Cryptography.KeyPairType.EC_BP384R1')
  - [EC_BP512R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP512R1 'Virgil.SDK.Cryptography.KeyPairType.EC_BP512R1')
  - [EC_Curve25519](#F-Virgil-SDK-Cryptography-KeyPairType-EC_Curve25519 'Virgil.SDK.Cryptography.KeyPairType.EC_Curve25519')
  - [EC_SECP192K1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192K1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP192K1')
  - [EC_SECP192R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192R1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP192R1')
  - [EC_SECP224K1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224K1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP224K1')
  - [EC_SECP224R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224R1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP224R1')
  - [EC_SECP256K1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256K1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP256K1')
  - [EC_SECP256R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256R1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP256R1')
  - [EC_SECP384R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP384R1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP384R1')
  - [EC_SECP521R1](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP521R1 'Virgil.SDK.Cryptography.KeyPairType.EC_SECP521R1')
  - [RSA_1024](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_1024 'Virgil.SDK.Cryptography.KeyPairType.RSA_1024')
  - [RSA_2048](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_2048 'Virgil.SDK.Cryptography.KeyPairType.RSA_2048')
  - [RSA_256](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_256 'Virgil.SDK.Cryptography.KeyPairType.RSA_256')
  - [RSA_3072](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_3072 'Virgil.SDK.Cryptography.KeyPairType.RSA_3072')
  - [RSA_4096](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_4096 'Virgil.SDK.Cryptography.KeyPairType.RSA_4096')
  - [RSA_512](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_512 'Virgil.SDK.Cryptography.KeyPairType.RSA_512')
  - [RSA_8192](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_8192 'Virgil.SDK.Cryptography.KeyPairType.RSA_8192')
- [Localization](#T-Virgil-SDK-Localization 'Virgil.SDK.Localization')
  - [Culture](#P-Virgil-SDK-Localization-Culture 'Virgil.SDK.Localization.Culture')
  - [ExceptionDomainValueDomainIdentityIsInvalid](#P-Virgil-SDK-Localization-ExceptionDomainValueDomainIdentityIsInvalid 'Virgil.SDK.Localization.ExceptionDomainValueDomainIdentityIsInvalid')
  - [ExceptionIdentityVerificationIsNotSent](#P-Virgil-SDK-Localization-ExceptionIdentityVerificationIsNotSent 'Virgil.SDK.Localization.ExceptionIdentityVerificationIsNotSent')
  - [ExceptionPublicKeyNotFound](#P-Virgil-SDK-Localization-ExceptionPublicKeyNotFound 'Virgil.SDK.Localization.ExceptionPublicKeyNotFound')
  - [ExceptionStringCanNotBeEmpty](#P-Virgil-SDK-Localization-ExceptionStringCanNotBeEmpty 'Virgil.SDK.Localization.ExceptionStringCanNotBeEmpty')
  - [ExceptionStringLengthIsInvalid](#P-Virgil-SDK-Localization-ExceptionStringLengthIsInvalid 'Virgil.SDK.Localization.ExceptionStringLengthIsInvalid')
  - [ExceptionUserDataAlreadyExists](#P-Virgil-SDK-Localization-ExceptionUserDataAlreadyExists 'Virgil.SDK.Localization.ExceptionUserDataAlreadyExists')
  - [ExceptionUserDataClassSpecifiedIsInvalid](#P-Virgil-SDK-Localization-ExceptionUserDataClassSpecifiedIsInvalid 'Virgil.SDK.Localization.ExceptionUserDataClassSpecifiedIsInvalid')
  - [ExceptionUserDataConfirmationEntityNotFound](#P-Virgil-SDK-Localization-ExceptionUserDataConfirmationEntityNotFound 'Virgil.SDK.Localization.ExceptionUserDataConfirmationEntityNotFound')
  - [ExceptionUserDataConfirmationTokenInvalid](#P-Virgil-SDK-Localization-ExceptionUserDataConfirmationTokenInvalid 'Virgil.SDK.Localization.ExceptionUserDataConfirmationTokenInvalid')
  - [ExceptionUserDataIntegrityConstraintViolation](#P-Virgil-SDK-Localization-ExceptionUserDataIntegrityConstraintViolation 'Virgil.SDK.Localization.ExceptionUserDataIntegrityConstraintViolation')
  - [ExceptionUserDataIsNotConfirmedYet](#P-Virgil-SDK-Localization-ExceptionUserDataIsNotConfirmedYet 'Virgil.SDK.Localization.ExceptionUserDataIsNotConfirmedYet')
  - [ExceptionUserDataNotFound](#P-Virgil-SDK-Localization-ExceptionUserDataNotFound 'Virgil.SDK.Localization.ExceptionUserDataNotFound')
  - [ExceptionUserDataValueIsRequired](#P-Virgil-SDK-Localization-ExceptionUserDataValueIsRequired 'Virgil.SDK.Localization.ExceptionUserDataValueIsRequired')
  - [ExceptionUserDataWasAlreadyConfirmed](#P-Virgil-SDK-Localization-ExceptionUserDataWasAlreadyConfirmed 'Virgil.SDK.Localization.ExceptionUserDataWasAlreadyConfirmed')
  - [ExceptionUserIdHadBeenConfirmed](#P-Virgil-SDK-Localization-ExceptionUserIdHadBeenConfirmed 'Virgil.SDK.Localization.ExceptionUserIdHadBeenConfirmed')
  - [ExceptionUserInfoDataValidationFailed](#P-Virgil-SDK-Localization-ExceptionUserInfoDataValidationFailed 'Virgil.SDK.Localization.ExceptionUserInfoDataValidationFailed')
  - [ExceptionVirgilServiceNotInitialized](#P-Virgil-SDK-Localization-ExceptionVirgilServiceNotInitialized 'Virgil.SDK.Localization.ExceptionVirgilServiceNotInitialized')
  - [ResourceManager](#P-Virgil-SDK-Localization-ResourceManager 'Virgil.SDK.Localization.ResourceManager')
- [Obfuscator](#T-Virgil-SDK-Utils-Obfuscator 'Virgil.SDK.Utils.Obfuscator')
  - [PBKDF(value,algorithm,iterations,salt)](#M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-String,System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32- 'Virgil.SDK.Utils.Obfuscator.PBKDF(System.String,System.String,Virgil.Crypto.Foundation.VirgilPBKDF.Hash,System.UInt32)')
  - [PBKDF(bytes,algorithm,iterations,salt)](#M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-Byte[],System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32- 'Virgil.SDK.Utils.Obfuscator.PBKDF(System.Byte[],System.String,Virgil.Crypto.Foundation.VirgilPBKDF.Hash,System.UInt32)')
- [PrivateKeyModel](#T-Virgil-SDK-Models-PrivateKeyModel 'Virgil.SDK.Models.PrivateKeyModel')
  - [CardId](#P-Virgil-SDK-Models-PrivateKeyModel-CardId 'Virgil.SDK.Models.PrivateKeyModel.CardId')
  - [PrivateKey](#P-Virgil-SDK-Models-PrivateKeyModel-PrivateKey 'Virgil.SDK.Models.PrivateKeyModel.PrivateKey')
- [PrivateKeysConnection](#T-Virgil-SDK-Http-PrivateKeysConnection 'Virgil.SDK.Http.PrivateKeysConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Http-PrivateKeysConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Http.PrivateKeysConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.PrivateKeysConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [PrivateKeysServiceClient](#T-Virgil-SDK-Clients-PrivateKeysServiceClient 'Virgil.SDK.Clients.PrivateKeysServiceClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-PrivateKeysServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.PrivateKeysServiceClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
- [PublicKeyModel](#T-Virgil-SDK-Models-PublicKeyModel 'Virgil.SDK.Models.PublicKeyModel')
  - [CreatedAt](#P-Virgil-SDK-Models-PublicKeyModel-CreatedAt 'Virgil.SDK.Models.PublicKeyModel.CreatedAt')
  - [Id](#P-Virgil-SDK-Models-PublicKeyModel-Id 'Virgil.SDK.Models.PublicKeyModel.Id')
  - [Value](#P-Virgil-SDK-Models-PublicKeyModel-Value 'Virgil.SDK.Models.PublicKeyModel.Value')
- [PublicServiceConnection](#T-Virgil-SDK-Http-PublicServiceConnection 'Virgil.SDK.Http.PublicServiceConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Http-PublicServiceConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Http.PublicServiceConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-PublicServiceConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.PublicServiceConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')
  - [#ctor()](#M-Virgil-SDK-Http-Request-#ctor 'Virgil.SDK.Http.Request.#ctor')
  - [Body](#P-Virgil-SDK-Http-Request-Body 'Virgil.SDK.Http.Request.Body')
  - [Endpoint](#P-Virgil-SDK-Http-Request-Endpoint 'Virgil.SDK.Http.Request.Endpoint')
  - [Headers](#P-Virgil-SDK-Http-Request-Headers 'Virgil.SDK.Http.Request.Headers')
  - [Method](#P-Virgil-SDK-Http-Request-Method 'Virgil.SDK.Http.Request.Method')
- [RequestExtensions](#T-Virgil-SDK-Http-RequestExtensions 'Virgil.SDK.Http.RequestExtensions')
  - [EncryptJsonBody(request,card)](#M-Virgil-SDK-Http-RequestExtensions-EncryptJsonBody-Virgil-SDK-Http-Request,Virgil-SDK-Models-CardModel- 'Virgil.SDK.Http.RequestExtensions.EncryptJsonBody(Virgil.SDK.Http.Request,Virgil.SDK.Models.CardModel)')
  - [SignRequest(request,cardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Http.RequestExtensions.SignRequest(Virgil.SDK.Http.Request,System.Guid,System.Byte[],System.String)')
  - [SignRequest(request,privateKey,privateKeyPassword)](#M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Byte[],System-String- 'Virgil.SDK.Http.RequestExtensions.SignRequest(Virgil.SDK.Http.Request,System.Byte[],System.String)')
  - [WithBody(request,body)](#M-Virgil-SDK-Http-RequestExtensions-WithBody-Virgil-SDK-Http-Request,System-Object- 'Virgil.SDK.Http.RequestExtensions.WithBody(Virgil.SDK.Http.Request,System.Object)')
  - [WithEndpoint(request,endpoint)](#M-Virgil-SDK-Http-RequestExtensions-WithEndpoint-Virgil-SDK-Http-Request,System-String- 'Virgil.SDK.Http.RequestExtensions.WithEndpoint(Virgil.SDK.Http.Request,System.String)')
- [RequestMethod](#T-Virgil-SDK-Http-RequestMethod 'Virgil.SDK.Http.RequestMethod')
- [Response](#T-Virgil-SDK-Http-Response 'Virgil.SDK.Http.Response')
  - [Body](#P-Virgil-SDK-Http-Response-Body 'Virgil.SDK.Http.Response.Body')
  - [Headers](#P-Virgil-SDK-Http-Response-Headers 'Virgil.SDK.Http.Response.Headers')
  - [StatusCode](#P-Virgil-SDK-Http-Response-StatusCode 'Virgil.SDK.Http.Response.StatusCode')
- [ResponseVerifyClient](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Virgil.SDK.Clients.ResponseVerifyClient')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.ResponseVerifyClient.#ctor(Virgil.SDK.Http.IConnection)')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.ResponseVerifyClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Send()](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.ResponseVerifyClient.Send(Virgil.SDK.Http.IRequest)')
  - [Send\`\`1(request)](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.ResponseVerifyClient.Send``1(Virgil.SDK.Http.IRequest)')
  - [VerifyResponse(nativeResponse,publicKey)](#M-Virgil-SDK-Clients-ResponseVerifyClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]- 'Virgil.SDK.Clients.ResponseVerifyClient.VerifyResponse(Virgil.SDK.Http.IResponse,System.Byte[])')
- [ServiceHub](#T-Virgil-SDK-ServiceHub 'Virgil.SDK.ServiceHub')
  - [#ctor()](#M-Virgil-SDK-ServiceHub-#ctor-Virgil-SDK-ServiceHubConfig- 'Virgil.SDK.ServiceHub.#ctor(Virgil.SDK.ServiceHubConfig)')
  - [Cards](#P-Virgil-SDK-ServiceHub-Cards 'Virgil.SDK.ServiceHub.Cards')
  - [Identity](#P-Virgil-SDK-ServiceHub-Identity 'Virgil.SDK.ServiceHub.Identity')
  - [PrivateKeys](#P-Virgil-SDK-ServiceHub-PrivateKeys 'Virgil.SDK.ServiceHub.PrivateKeys')
  - [BuildCardsClient()](#M-Virgil-SDK-ServiceHub-BuildCardsClient 'Virgil.SDK.ServiceHub.BuildCardsClient')
  - [BuildIdentityClient()](#M-Virgil-SDK-ServiceHub-BuildIdentityClient 'Virgil.SDK.ServiceHub.BuildIdentityClient')
  - [BuildPrivateKeysClient()](#M-Virgil-SDK-ServiceHub-BuildPrivateKeysClient 'Virgil.SDK.ServiceHub.BuildPrivateKeysClient')
  - [Create()](#M-Virgil-SDK-ServiceHub-Create-System-String- 'Virgil.SDK.ServiceHub.Create(System.String)')
  - [Create()](#M-Virgil-SDK-ServiceHub-Create-Virgil-SDK-ServiceHubConfig- 'Virgil.SDK.ServiceHub.Create(Virgil.SDK.ServiceHubConfig)')
  - [Initialize()](#M-Virgil-SDK-ServiceHub-Initialize 'Virgil.SDK.ServiceHub.Initialize')
- [ServiceHubConfig](#T-Virgil-SDK-ServiceHubConfig 'Virgil.SDK.ServiceHubConfig')
  - [#ctor()](#M-Virgil-SDK-ServiceHubConfig-#ctor-System-String- 'Virgil.SDK.ServiceHubConfig.#ctor(System.String)')
  - [UseAccessToken()](#M-Virgil-SDK-ServiceHubConfig-UseAccessToken-System-String- 'Virgil.SDK.ServiceHubConfig.UseAccessToken(System.String)')
  - [WithIdentityServiceAddress()](#M-Virgil-SDK-ServiceHubConfig-WithIdentityServiceAddress-System-String- 'Virgil.SDK.ServiceHubConfig.WithIdentityServiceAddress(System.String)')
  - [WithPrivateServicesAddress()](#M-Virgil-SDK-ServiceHubConfig-WithPrivateServicesAddress-System-String- 'Virgil.SDK.ServiceHubConfig.WithPrivateServicesAddress(System.String)')
  - [WithPublicServicesAddress()](#M-Virgil-SDK-ServiceHubConfig-WithPublicServicesAddress-System-String- 'Virgil.SDK.ServiceHubConfig.WithPublicServicesAddress(System.String)')
  - [WithStagingEnvironment()](#M-Virgil-SDK-ServiceHubConfig-WithStagingEnvironment 'Virgil.SDK.ServiceHubConfig.WithStagingEnvironment')
- [ServiceIdentities](#T-Virgil-SDK-Clients-ServiceIdentities 'Virgil.SDK.Clients.ServiceIdentities')
  - [IdentityService](#F-Virgil-SDK-Clients-ServiceIdentities-IdentityService 'Virgil.SDK.Clients.ServiceIdentities.IdentityService')
  - [PrivateService](#F-Virgil-SDK-Clients-ServiceIdentities-PrivateService 'Virgil.SDK.Clients.ServiceIdentities.PrivateService')
  - [PublicService](#F-Virgil-SDK-Clients-ServiceIdentities-PublicService 'Virgil.SDK.Clients.ServiceIdentities.PublicService')
- [ServiceSignVerificationServiceException](#T-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException 'Virgil.SDK.Exceptions.ServiceSignVerificationServiceException')
  - [#ctor(message)](#M-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException-#ctor-System-String- 'Virgil.SDK.Exceptions.ServiceSignVerificationServiceException.#ctor(System.String)')
- [ValidationTokenGenerator](#T-Virgil-SDK-Utils-ValidationTokenGenerator 'Virgil.SDK.Utils.ValidationTokenGenerator')
  - [Generate(identityValue,identityType,privateKey,privateKeyPassword)](#M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-String,System-String,System-Byte[],System-String- 'Virgil.SDK.Utils.ValidationTokenGenerator.Generate(System.String,System.String,System.Byte[],System.String)')
  - [Generate(identityValue,identityType,privateKey,privateKeyPassword)](#M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-Guid,System-String,System-String,System-Byte[],System-String- 'Virgil.SDK.Utils.ValidationTokenGenerator.Generate(System.Guid,System.String,System.String,System.Byte[],System.String)')
- [VerifiableIdentityType](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Virgil.SDK.Identities.VerifiableIdentityType')
  - [Email](#F-Virgil-SDK-Identities-VerifiableIdentityType-Email 'Virgil.SDK.Identities.VerifiableIdentityType.Email')
- [VerificationRequestIsNotSentServiceException](#T-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException 'Virgil.SDK.Exceptions.VerificationRequestIsNotSentServiceException')
  - [#ctor()](#M-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException-#ctor 'Virgil.SDK.Exceptions.VerificationRequestIsNotSentServiceException.#ctor')
- [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer')
  - [FromBase64String()](#M-Virgil-SDK-VirgilBuffer-FromBase64String-System-String- 'Virgil.SDK.VirgilBuffer.FromBase64String(System.String)')
  - [FromBytes()](#M-Virgil-SDK-VirgilBuffer-FromBytes-System-Byte[]- 'Virgil.SDK.VirgilBuffer.FromBytes(System.Byte[])')
  - [FromHexString()](#M-Virgil-SDK-VirgilBuffer-FromHexString-System-String- 'Virgil.SDK.VirgilBuffer.FromHexString(System.String)')
  - [FromString()](#M-Virgil-SDK-VirgilBuffer-FromString-System-String- 'Virgil.SDK.VirgilBuffer.FromString(System.String)')
  - [ToBase64String()](#M-Virgil-SDK-VirgilBuffer-ToBase64String 'Virgil.SDK.VirgilBuffer.ToBase64String')
  - [ToBytes()](#M-Virgil-SDK-VirgilBuffer-ToBytes 'Virgil.SDK.VirgilBuffer.ToBytes')
  - [ToHexString()](#M-Virgil-SDK-VirgilBuffer-ToHexString 'Virgil.SDK.VirgilBuffer.ToHexString')
  - [ToString()](#M-Virgil-SDK-VirgilBuffer-ToString 'Virgil.SDK.VirgilBuffer.ToString')
- [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard')
  - [#ctor()](#M-Virgil-SDK-VirgilCard-#ctor-Virgil-SDK-Clients-Models-VirgilCardModel- 'Virgil.SDK.VirgilCard.#ctor(Virgil.SDK.Clients.Models.VirgilCardModel)')
  - [CreatedAt](#P-Virgil-SDK-VirgilCard-CreatedAt 'Virgil.SDK.VirgilCard.CreatedAt')
  - [Data](#P-Virgil-SDK-VirgilCard-Data 'Virgil.SDK.VirgilCard.Data')
  - [Id](#P-Virgil-SDK-VirgilCard-Id 'Virgil.SDK.VirgilCard.Id')
  - [Identity](#P-Virgil-SDK-VirgilCard-Identity 'Virgil.SDK.VirgilCard.Identity')
  - [IdentityType](#P-Virgil-SDK-VirgilCard-IdentityType 'Virgil.SDK.VirgilCard.IdentityType')
  - [IsConfirmed](#P-Virgil-SDK-VirgilCard-IsConfirmed 'Virgil.SDK.VirgilCard.IsConfirmed')
  - [IsGlobal](#P-Virgil-SDK-VirgilCard-IsGlobal 'Virgil.SDK.VirgilCard.IsGlobal')
  - [PublicKey](#P-Virgil-SDK-VirgilCard-PublicKey 'Virgil.SDK.VirgilCard.PublicKey')
  - [RevokedAt](#P-Virgil-SDK-VirgilCard-RevokedAt 'Virgil.SDK.VirgilCard.RevokedAt')
  - [Version](#P-Virgil-SDK-VirgilCard-Version 'Virgil.SDK.VirgilCard.Version')
  - [Encrypt(data)](#M-Virgil-SDK-VirgilCard-Encrypt-Virgil-SDK-VirgilBuffer- 'Virgil.SDK.VirgilCard.Encrypt(Virgil.SDK.VirgilBuffer)')
  - [Find(identity)](#M-Virgil-SDK-VirgilCard-Find-System-String- 'Virgil.SDK.VirgilCard.Find(System.String)')
  - [Get(id)](#M-Virgil-SDK-VirgilCard-Get-System-Guid- 'Virgil.SDK.VirgilCard.Get(System.Guid)')
  - [Verify(data,signature)](#M-Virgil-SDK-VirgilCard-Verify-Virgil-SDK-VirgilBuffer,Virgil-SDK-VirgilBuffer- 'Virgil.SDK.VirgilCard.Verify(Virgil.SDK.VirgilBuffer,Virgil.SDK.VirgilBuffer)')
- [VirgilCardModel](#T-Virgil-SDK-Clients-Models-VirgilCardModel 'Virgil.SDK.Clients.Models.VirgilCardModel')
- [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest')
  - [#ctor()](#M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,System-String,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.VirgilCardRequest.#ctor(System.String,System.String,System.String,System.Boolean,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [#ctor()](#M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,Virgil-SDK-VirgilKey,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.VirgilCardRequest.#ctor(System.String,System.String,Virgil.SDK.VirgilKey,System.Boolean,System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Data](#P-Virgil-SDK-VirgilCardRequest-Data 'Virgil.SDK.VirgilCardRequest.Data')
  - [Fingerprint](#P-Virgil-SDK-VirgilCardRequest-Fingerprint 'Virgil.SDK.VirgilCardRequest.Fingerprint')
  - [Id](#P-Virgil-SDK-VirgilCardRequest-Id 'Virgil.SDK.VirgilCardRequest.Id')
  - [Identity](#P-Virgil-SDK-VirgilCardRequest-Identity 'Virgil.SDK.VirgilCardRequest.Identity')
  - [IdentityType](#P-Virgil-SDK-VirgilCardRequest-IdentityType 'Virgil.SDK.VirgilCardRequest.IdentityType')
  - [IsGlobal](#P-Virgil-SDK-VirgilCardRequest-IsGlobal 'Virgil.SDK.VirgilCardRequest.IsGlobal')
  - [PublicKey](#P-Virgil-SDK-VirgilCardRequest-PublicKey 'Virgil.SDK.VirgilCardRequest.PublicKey')
  - [Signs](#P-Virgil-SDK-VirgilCardRequest-Signs 'Virgil.SDK.VirgilCardRequest.Signs')
  - [AddSign(cardId,sign)](#M-Virgil-SDK-VirgilCardRequest-AddSign-System-Guid,System-Byte[]- 'Virgil.SDK.VirgilCardRequest.AddSign(System.Guid,System.Byte[])')
  - [Export()](#M-Virgil-SDK-VirgilCardRequest-Export 'Virgil.SDK.VirgilCardRequest.Export')
  - [Import()](#M-Virgil-SDK-VirgilCardRequest-Import-System-String- 'Virgil.SDK.VirgilCardRequest.Import(System.String)')
- [VirgilConfig](#T-Virgil-SDK-VirgilConfig 'Virgil.SDK.VirgilConfig')
  - [Initialize(accessToken)](#M-Virgil-SDK-VirgilConfig-Initialize-System-String- 'Virgil.SDK.VirgilConfig.Initialize(System.String)')
  - [Reset()](#M-Virgil-SDK-VirgilConfig-Reset 'Virgil.SDK.VirgilConfig.Reset')
  - [SetCryptoProvider()](#M-Virgil-SDK-VirgilConfig-SetCryptoProvider-Virgil-SDK-Cryptography-ICryptoProvider- 'Virgil.SDK.VirgilConfig.SetCryptoProvider(Virgil.SDK.Cryptography.ICryptoProvider)')
  - [SetKeyStorageProvider()](#M-Virgil-SDK-VirgilConfig-SetKeyStorageProvider-Virgil-SDK-Storage-IKeyStorageProvider- 'Virgil.SDK.VirgilConfig.SetKeyStorageProvider(Virgil.SDK.Storage.IKeyStorageProvider)')
  - [SetServiceHub()](#M-Virgil-SDK-VirgilConfig-SetServiceHub-Virgil-SDK-Clients-IServiceHub- 'Virgil.SDK.VirgilConfig.SetServiceHub(Virgil.SDK.Clients.IServiceHub)')
- [VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException')
- [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey')
  - [#ctor()](#M-Virgil-SDK-VirgilKey-#ctor 'Virgil.SDK.VirgilKey.#ctor')
  - [Create(keyName)](#M-Virgil-SDK-VirgilKey-Create-System-String- 'Virgil.SDK.VirgilKey.Create(System.String)')
  - [Create(keyName,publicKey,privateKey)](#M-Virgil-SDK-VirgilKey-Create-System-String,System-Byte[],System-Byte[]- 'Virgil.SDK.VirgilKey.Create(System.String,System.Byte[],System.Byte[])')
  - [Exists(keyName)](#M-Virgil-SDK-VirgilKey-Exists-System-String- 'Virgil.SDK.VirgilKey.Exists(System.String)')
  - [RevokeAssociatedVirgilCard()](#M-Virgil-SDK-VirgilKey-RevokeAssociatedVirgilCard 'Virgil.SDK.VirgilKey.RevokeAssociatedVirgilCard')
- [VirgilKeyExtensions](#T-Virgil-SDK-VirgilKeyExtensions 'Virgil.SDK.VirgilKeyExtensions')
  - [Sign(key,plaintext)](#M-Virgil-SDK-VirgilKeyExtensions-Sign-Virgil-SDK-VirgilKey,System-String- 'Virgil.SDK.VirgilKeyExtensions.Sign(Virgil.SDK.VirgilKey,System.String)')
- [VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilServiceException.#ctor(System.Int32,System.String)')
  - [#ctor(message)](#M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-String- 'Virgil.SDK.Exceptions.VirgilServiceException.#ctor(System.String)')
  - [ErrorCode](#P-Virgil-SDK-Exceptions-VirgilServiceException-ErrorCode 'Virgil.SDK.Exceptions.VirgilServiceException.ErrorCode')
- [VirgilServiceNotInitializedException](#T-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException 'Virgil.SDK.Exceptions.VirgilServiceNotInitializedException')
  - [#ctor()](#M-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException-#ctor 'Virgil.SDK.Exceptions.VirgilServiceNotInitializedException.#ctor')
- [VirgilServicePrivateServicesException](#T-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException 'Virgil.SDK.Exceptions.VirgilServicePrivateServicesException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilServicePrivateServicesException.#ctor(System.Int32,System.String)')
- [VirgilServicePublicServicesException](#T-Virgil-SDK-Exceptions-VirgilServicePublicServicesException 'Virgil.SDK.Exceptions.VirgilServicePublicServicesException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilServicePublicServicesException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilServicePublicServicesException.#ctor(System.Int32,System.String)')

<a name='assembly'></a>
# Virgil.SDK [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-Virgil-SDK-Models-CardModel'></a>
## CardModel [#](#T-Virgil-SDK-Models-CardModel 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Models

##### Summary

Represents full virgil card object returned from virgil cards service

<a name='P-Virgil-SDK-Models-CardModel-AuthorizedBy'></a>
### AuthorizedBy `property` [#](#P-Virgil-SDK-Models-CardModel-AuthorizedBy 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets a value indicating whether this instance is confirmed.

<a name='P-Virgil-SDK-Models-CardModel-CreatedAt'></a>
### CreatedAt `property` [#](#P-Virgil-SDK-Models-CardModel-CreatedAt 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the created at date.

<a name='P-Virgil-SDK-Models-CardModel-CustomData'></a>
### CustomData `property` [#](#P-Virgil-SDK-Models-CardModel-CustomData 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the custom data.

<a name='P-Virgil-SDK-Models-CardModel-Hash'></a>
### Hash `property` [#](#P-Virgil-SDK-Models-CardModel-Hash 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the hash.

<a name='P-Virgil-SDK-Models-CardModel-Id'></a>
### Id `property` [#](#P-Virgil-SDK-Models-CardModel-Id 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the identifier.

<a name='P-Virgil-SDK-Models-CardModel-Identity'></a>
### Identity `property` [#](#P-Virgil-SDK-Models-CardModel-Identity 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the identity.

<a name='P-Virgil-SDK-Models-CardModel-PublicKey'></a>
### PublicKey `property` [#](#P-Virgil-SDK-Models-CardModel-PublicKey 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the public key.

<a name='T-Virgil-SDK-Clients-CardsServiceClient'></a>
## CardsServiceClient [#](#T-Virgil-SDK-Clients-CardsServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Virgil Card resource endpoints.

##### See Also

- [Virgil.SDK.Clients.EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
- [Virgil.SDK.Clients.ICardsServiceClient](#T-Virgil-SDK-Clients-ICardsServiceClient 'Virgil.SDK.Clients.ICardsServiceClient')

<a name='M-Virgil-SDK-Clients-CardsServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-CardsServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [CardsServiceClient](#T-Virgil-SDK-Clients-CardsServiceClient 'Virgil.SDK.Clients.CardsServiceClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The services key cache. |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityInfo,publicKeyId,privateKey,privateKeyPassword,customData) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new card with specified identity and existing public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | The information about identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier in Virgil Services. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. Pass this parameter if your private key is encrypted with password |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The dictionary of key/value pairs with custom values that can be used by different applications |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityInfo,publicKey,privateKey,privateKeyPassword,customData) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new card with specified identity and public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | The information about identity. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The generated public key value. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. Pass this parameter if your private key is encrypted with password |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The dictionary of key/value pairs with custom values that can be used by different applications |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Get-System-Guid-'></a>
### Get(cardId) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the card by ID.

##### Returns

Virgil card model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The card ID. |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-GetAsync-System-Guid-'></a>
### GetAsync(cardId) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-GetAsync-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') by specified identifier.

##### Returns

An instance of [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') entity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') identifier. |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String-'></a>
### Revoke(cardId,identityInfo,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Revokes the specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The card ID. |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | Validation token for card's identity. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}-'></a>
### Search(identityValue,identityType,includeUnauthorized) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the private cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. |
| identityType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of identifier. |
| includeUnauthorized | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil cards will be included in output. Optional |

<a name='M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType-'></a>
### Search(identityValue,identityType) `method` [#](#M-Virgil-SDK-Clients-CardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the global cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. |
| identityType | [Virgil.SDK.Identities.IdentityType](#T-Virgil-SDK-Identities-IdentityType 'Virgil.SDK.Identities.IdentityType') | The type of identifier. |

<a name='T-Virgil-SDK-Http-ConnectionBase'></a>
## ConnectionBase [#](#T-Virgil-SDK-Http-ConnectionBase 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary



<a name='M-Virgil-SDK-Http-ConnectionBase-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Http-ConnectionBase-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application token. |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='F-Virgil-SDK-Http-ConnectionBase-AccessTokenHeaderName'></a>
### AccessTokenHeaderName `constants` [#](#F-Virgil-SDK-Http-ConnectionBase-AccessTokenHeaderName 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The access token header name

<a name='F-Virgil-SDK-Http-ConnectionBase-Errors'></a>
### Errors `constants` [#](#F-Virgil-SDK-Http-ConnectionBase-Errors 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The error code to message mapping dictionary

<a name='P-Virgil-SDK-Http-ConnectionBase-AccessToken'></a>
### AccessToken `property` [#](#P-Virgil-SDK-Http-ConnectionBase-AccessToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Access Token

<a name='P-Virgil-SDK-Http-ConnectionBase-BaseAddress'></a>
### BaseAddress `property` [#](#P-Virgil-SDK-Http-ConnectionBase-BaseAddress 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Base address for the connection.

<a name='M-Virgil-SDK-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles exception responses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='M-Virgil-SDK-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Http-IRequest-'></a>
### GetNativeRequest(request) `method` [#](#M-Virgil-SDK-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Produces native HTTP request.

##### Returns

HttpRequestMessage

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest') | The request. |

<a name='M-Virgil-SDK-Http-ConnectionBase-Send-Virgil-SDK-Http-IRequest-'></a>
### Send(request) `method` [#](#M-Virgil-SDK-Http-ConnectionBase-Send-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends an HTTP request to the API.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest') | The HTTP request details. |

<a name='M-Virgil-SDK-Http-ConnectionBase-ThrowException``1-System-Net-Http-HttpResponseMessage,System-Func{System-Int32,System-String,``0}-'></a>
### ThrowException\`\`1(message,exception) `method` [#](#M-Virgil-SDK-Http-ConnectionBase-ThrowException``1-System-Net-Http-HttpResponseMessage,System-Func{System-Int32,System-String,``0}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Parses service http response and throws apropriate exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | Message received from service |
| exception | [System.Func{System.Int32,System.String,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Int32,System.String,``0}') | Exception factory |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Virgil exception child class |

<a name='M-Virgil-SDK-Http-ConnectionBase-TryParseErrorCode-System-String-'></a>
### TryParseErrorCode(content) `method` [#](#M-Virgil-SDK-Http-ConnectionBase-TryParseErrorCode-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Parses service response to retrieve error code

##### Returns

Parsed error code

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| content | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Http body of service response |

<a name='T-Virgil-SDK-Clients-DynamicKeyCache'></a>
## DynamicKeyCache [#](#T-Virgil-SDK-Clients-DynamicKeyCache 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides cached value of known public key for channel encryption and response verification

##### See Also

- [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache')

<a name='M-Virgil-SDK-Clients-DynamicKeyCache-#ctor-Virgil-SDK-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Clients-DynamicKeyCache-#ctor-Virgil-SDK-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [DynamicKeyCache](#T-Virgil-SDK-Clients-DynamicKeyCache 'Virgil.SDK.Clients.DynamicKeyCache') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The Virgil Public Services connection instance. |

<a name='M-Virgil-SDK-Clients-DynamicKeyCache-GetApplicationCards-System-String-'></a>
### GetApplicationCards() `method` [#](#M-Virgil-SDK-Clients-DynamicKeyCache-GetApplicationCards-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Get the application Virgil Card.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Clients-DynamicKeyCache-GetServiceCard-System-String-'></a>
### GetServiceCard(servicePublicKeyId) `method` [#](#M-Virgil-SDK-Clients-DynamicKeyCache-GetServiceCard-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the service's public key by specified identifier.

##### Returns

An instance of [PublicKeyModel](#T-Virgil-SDK-Models-PublicKeyModel 'Virgil.SDK.Models.PublicKeyModel'), that represents Public Key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The service's public key identifier. |

<a name='T-Virgil-SDK-Identities-EmailVerifier'></a>
## EmailVerifier [#](#T-Virgil-SDK-Identities-EmailVerifier 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents a class for email address verification.

<a name='M-Virgil-SDK-Identities-EmailVerifier-#ctor-System-Guid,Virgil-SDK-Clients-IIdentityServiceClient-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-Identities-EmailVerifier-#ctor-System-Guid,Virgil-SDK-Clients-IIdentityServiceClient- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [EmailVerifier](#T-Virgil-SDK-Identities-EmailVerifier 'Virgil.SDK.Identities.EmailVerifier') class.

##### Parameters

This constructor has no parameters.

<a name='P-Virgil-SDK-Identities-EmailVerifier-ActionId'></a>
### ActionId `property` [#](#P-Virgil-SDK-Identities-EmailVerifier-ActionId 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the verification process ID.

<a name='M-Virgil-SDK-Identities-EmailVerifier-Confirm-System-String,System-Int32,System-Int32-'></a>
### Confirm(code,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Identities-EmailVerifier-Confirm-System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code that was recived on email box. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to limit the lifetime of the token in seconds (maximum value is 60 * 60 * 24 * 365 = 1 year) |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to restrict the number of token usages (maximum value is 100) |

<a name='T-Virgil-SDK-Clients-EndpointClient'></a>
## EndpointClient [#](#T-Virgil-SDK-Clients-EndpointClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides a base implementation of HTTP client for the Virgil Security services.

<a name='M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The service key cache. |

<a name='F-Virgil-SDK-Clients-EndpointClient-Cache'></a>
### Cache `constants` [#](#F-Virgil-SDK-Clients-EndpointClient-Cache 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The cache

<a name='F-Virgil-SDK-Clients-EndpointClient-Connection'></a>
### Connection `constants` [#](#F-Virgil-SDK-Clients-EndpointClient-Connection 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The connection

<a name='F-Virgil-SDK-Clients-EndpointClient-EndpointApplicationId'></a>
### EndpointApplicationId `constants` [#](#F-Virgil-SDK-Clients-EndpointClient-EndpointApplicationId 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The endpoint application identifier

<a name='M-Virgil-SDK-Clients-EndpointClient-Send-Virgil-SDK-Http-IRequest-'></a>
### Send() `method` [#](#M-Virgil-SDK-Clients-EndpointClient-Send-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP request.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Clients-EndpointClient-Send``1-Virgil-SDK-Http-IRequest-'></a>
### Send\`\`1() `method` [#](#M-Virgil-SDK-Clients-EndpointClient-Send``1-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP POST request. Attempts to map the response body to an object of type `TResult`

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Helpers-Ensure'></a>
## Ensure [#](#T-Virgil-SDK-Helpers-Ensure 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Helpers

##### Summary

Ensure input parameters

<a name='M-Virgil-SDK-Helpers-Ensure-ArgumentNotNull-System-Object,System-String-'></a>
### ArgumentNotNull(value,name) `method` [#](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNull-System-Object,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks an argument to ensure it isn't null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The argument value to check |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument |

<a name='M-Virgil-SDK-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String-'></a>
### ArgumentNotNullOrEmptyString(value,name) `method` [#](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks a string argument to ensure it isn't null or empty.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument value to check |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument |

<a name='T-Virgil-SDK-Clients-ICardsServiceClient'></a>
## ICardsServiceClient [#](#T-Virgil-SDK-Clients-ICardsServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityInfo,publicKeyId,privateKey,privateKeyPassword,customData) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new card with specified identity and existing public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | The information about identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier in Virgil Services. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. Pass this parameter if your private key is encrypted with password |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The dictionary of key/value pairs with custom values that can be used by different applications |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityInfo,publicKey,privateKey,privateKeyPassword,customData) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Create-Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new card with specified identity and public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | The information about identity. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The generated public key value. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. Pass this parameter if your private key is encrypted with password |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The dictionary of key/value pairs with custom values that can be used by different applications |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Get-System-Guid-'></a>
### Get(cardId) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the card by ID.

##### Returns

Virgil card model.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The card ID. |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-GetAsync-System-Guid-'></a>
### GetAsync(cardId) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-GetAsync-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') by specified identifier.

##### Returns

An instance of [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') entity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') identifier. |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-PublishAsync-Virgil-SDK-VirgilCardRequest-'></a>
### PublishAsync() `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-PublishAsync-Virgil-SDK-VirgilCardRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Publishes a new [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') by specified [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest') ticket to Virgil Cards Service.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String-'></a>
### Revoke(cardId,identityInfo,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Revoke-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Revokes the specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The card ID. |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') | Validation identityInfo for card's identity. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}-'></a>
### Search(identityValue,identityType,includeUnauthorized) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,System-String,System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the private cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. Required. |
| identityType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identity type. Optional. |
| includeUnauthorized | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | The request parameter specifies whether an unconfirmed Virgil Cards should be included in the search result. |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType-'></a>
### Search(identityValue,identityType) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-Search-System-String,Virgil-SDK-Identities-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the global cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. Required. |
| identityType | [Virgil.SDK.Identities.IdentityType](#T-Virgil-SDK-Identities-IdentityType 'Virgil.SDK.Identities.IdentityType') | The type of identifier. Optional. |

<a name='M-Virgil-SDK-Clients-ICardsServiceClient-SearchAsync-System-String,System-String-'></a>
### SearchAsync(identity,identityType) `method` [#](#M-Virgil-SDK-Clients-ICardsServiceClient-SearchAsync-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches for the Virgil global Cards by specified criteria.

##### Returns

The collection of [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The user's identity value. |
| identityType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The user's identity type. |

<a name='T-Virgil-SDK-Http-IConnection'></a>
## IConnection [#](#T-Virgil-SDK-Http-IConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

A connection for making HTTP requests against URI endpoints.

<a name='P-Virgil-SDK-Http-IConnection-BaseAddress'></a>
### BaseAddress `property` [#](#P-Virgil-SDK-Http-IConnection-BaseAddress 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Base address for the connection.

<a name='M-Virgil-SDK-Http-IConnection-Send-Virgil-SDK-Http-IRequest-'></a>
### Send(request) `method` [#](#M-Virgil-SDK-Http-IConnection-Send-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends an HTTP request to the API.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest') | The HTTP request details. |

<a name='T-Virgil-SDK-Cryptography-ICryptoProvider'></a>
## ICryptoProvider [#](#T-Virgil-SDK-Cryptography-ICryptoProvider 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Cryptography

##### Summary

The [ICryptoProvider](#T-Virgil-SDK-Cryptography-ICryptoProvider 'Virgil.SDK.Cryptography.ICryptoProvider') interface provides a cryptographic operations in applications, such as signature generation and verification, and encryption and decryption.

Developers making use of the [ICryptoProvider](#T-Virgil-SDK-Cryptography-ICryptoProvider 'Virgil.SDK.Cryptography.ICryptoProvider') interface are expected to be aware of the security concerns associated with both the design and implementation of the various algorithms provided.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String-'></a>
### Decrypt(cipherData,password) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts the cipher data using recipient's password.

##### Returns

A byte array containing the result of decrypt operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The cipher data to be decrypted. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that was used to encrypt specified cipher data. |

##### Remarks

This method decrypts a cipher data that is ecrypted using the  method.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String,System-Byte[],System-String-'></a>
### Decrypt(cipherData,recipientId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts the specified cipher data using recipient's identifier and `Private key`.

##### Returns

A byte array containing the result from performing the operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The cipher data to be decrypted. |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The unique ID, that identifies the recipient. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key`'s password |

##### Remarks

This method decrypts a data that is ecrypted using the  method.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-String-'></a>
### Encrypt(data,password) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the data using specified password.

##### Returns

A byte array containing the result from performing the operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that uses to encrypt specified data. |

##### Remarks

This method encrypts a data that is decrypted using the  method.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}-'></a>
### Encrypt(data,recipients) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the data for the specified dictionary of recipients, where Key is recipient ID and Value is Public Key.

##### Returns

A byte array containing the result from performing the operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |
| recipients | [System.Collections.Generic.IDictionary{System.String,System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.Byte[]}') | The dictionary of recipients Public Keys |

##### Remarks

This method encrypts a data that is decrypted using the  method.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair'></a>
### GenerateKeypair() `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates the key pair, with default parameters.

##### Returns

A new [KeyPair](#T-Virgil-SDK-Cryptography-KeyPair 'Virgil.SDK.Cryptography.KeyPair') generated instance.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair-Virgil-SDK-Cryptography-KeyPairType-'></a>
### GenerateKeypair() `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-GenerateKeypair-Virgil-SDK-Cryptography-KeyPairType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates the key pair by specified type.

##### Returns

A new [KeyPair](#T-Virgil-SDK-Cryptography-KeyPair 'Virgil.SDK.Cryptography.KeyPair') generated instance.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Sign-System-Byte[],System-Byte[],System-String-'></a>
### Sign(data,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Sign-System-Byte[],System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs the signature generation operation with the signer's `Private Key` and the data to be signed.

##### Returns

A byte array containing the result from performing the operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be signed. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key`'s password |

<a name='M-Virgil-SDK-Cryptography-ICryptoProvider-Verify-System-Byte[],System-Byte[],System-Byte[]-'></a>
### Verify(data,signData,publicKey) `method` [#](#M-Virgil-SDK-Cryptography-ICryptoProvider-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs the signature verification operation with the signer's `Public Key`.

##### Returns

A value that indicates whether the specified signature is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data that was signed with sender's `Private Key`. |
| signData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signature to be verified. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The sender's `Public Key`. |

<a name='T-Virgil-SDK-Identities-IdentityConfirmationResponse'></a>
## IdentityConfirmationResponse [#](#T-Virgil-SDK-Identities-IdentityConfirmationResponse 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents an confirmed identity information.

<a name='P-Virgil-SDK-Identities-IdentityConfirmationResponse-Type'></a>
### Type `property` [#](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the type.

<a name='P-Virgil-SDK-Identities-IdentityConfirmationResponse-ValidationToken'></a>
### ValidationToken `property` [#](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-ValidationToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the validation token.

<a name='P-Virgil-SDK-Identities-IdentityConfirmationResponse-Value'></a>
### Value `property` [#](#P-Virgil-SDK-Identities-IdentityConfirmationResponse-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the value.

<a name='T-Virgil-SDK-Http-IdentityConnection'></a>
## IdentityConnection [#](#T-Virgil-SDK-Http-IdentityConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

A connection for making HTTP requests against URI endpoints for identity api services.

##### See Also

- [Virgil.SDK.Http.ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase')
- [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')

<a name='M-Virgil-SDK-Http-IdentityConnection-#ctor-System-Uri-'></a>
### #ctor(baseAddress) `constructor` [#](#M-Virgil-SDK-Http-IdentityConnection-#ctor-System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityConnection](#T-Virgil-SDK-Http-IdentityConnection 'Virgil.SDK.Http.IdentityConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Http-IdentityConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Http-IdentityConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles exception responses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Identities-IdentityInfo'></a>
## IdentityInfo [#](#T-Virgil-SDK-Identities-IdentityInfo 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents an identity information.

<a name='P-Virgil-SDK-Identities-IdentityInfo-Type'></a>
### Type `property` [#](#P-Virgil-SDK-Identities-IdentityInfo-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the type.

<a name='P-Virgil-SDK-Identities-IdentityInfo-ValidationToken'></a>
### ValidationToken `property` [#](#P-Virgil-SDK-Identities-IdentityInfo-ValidationToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the validation token.

<a name='P-Virgil-SDK-Identities-IdentityInfo-Value'></a>
### Value `property` [#](#P-Virgil-SDK-Identities-IdentityInfo-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the value.

<a name='T-Virgil-SDK-Models-IdentityModel'></a>
## IdentityModel [#](#T-Virgil-SDK-Models-IdentityModel 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Models

##### Summary

Represents identity object returned from virgil services

<a name='P-Virgil-SDK-Models-IdentityModel-CreatedAt'></a>
### CreatedAt `property` [#](#P-Virgil-SDK-Models-IdentityModel-CreatedAt 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the created at date.

<a name='P-Virgil-SDK-Models-IdentityModel-Id'></a>
### Id `property` [#](#P-Virgil-SDK-Models-IdentityModel-Id 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the identifier.

<a name='P-Virgil-SDK-Models-IdentityModel-Type'></a>
### Type `property` [#](#P-Virgil-SDK-Models-IdentityModel-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the type.

<a name='P-Virgil-SDK-Models-IdentityModel-Value'></a>
### Value `property` [#](#P-Virgil-SDK-Models-IdentityModel-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the value.

<a name='T-Virgil-SDK-Clients-IdentityServiceClient'></a>
## IdentityServiceClient [#](#T-Virgil-SDK-Clients-IdentityServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods for validating and authorization a different types of identities.

<a name='M-Virgil-SDK-Clients-IdentityServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-IdentityServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityServiceClient](#T-Virgil-SDK-Clients-IdentityServiceClient 'Virgil.SDK.Clients.IdentityServiceClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The cache. |

<a name='M-Virgil-SDK-Clients-IdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,code,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Clients-IdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier that was obtained on verification step. |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code that was recived on email box. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to limit the lifetime of the token in seconds (maximum value is 60 * 60 * 24 * 365 = 1 year) |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to restrict the number of token usages (maximum value is 100) |

<a name='M-Virgil-SDK-Clients-IdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String-'></a>
### IsValid(identityValue,identityType,validationToken) `method` [#](#M-Virgil-SDK-Clients-IdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns true if validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of identity. |
| identityType | [Virgil.SDK.Identities.VerifiableIdentityType](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Virgil.SDK.Identities.VerifiableIdentityType') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The validation token. |

<a name='M-Virgil-SDK-Clients-IdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Verify(identityValue,identityType,extraFields) `method` [#](#M-Virgil-SDK-Clients-IdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| identityType | [Virgil.SDK.Identities.VerifiableIdentityType](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Virgil.SDK.Identities.VerifiableIdentityType') | The type of identity. |
| extraFields | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') |  |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Clients-IdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IdentityServiceClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

<a name='M-Virgil-SDK-Clients-IdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### VerifyEmail(emailAddress,extraFields) `method` [#](#M-Virgil-SDK-Clients-IdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initiates a process to verify a specified email address.

##### Returns

The verification identuty class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| emailAddress | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The email address you are going to verify. |
| extraFields | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | In some cases it could be necessary to pass some parameters and receive them back in an email. For this special case an optional `extraFields` dictionary can be used. All values passed in `extraFields` parameter will be passed back in an email in a hidden form with extra hidden fields. |

<a name='T-Virgil-SDK-Exceptions-IdentityServiceServiceException'></a>
## IdentityServiceServiceException [#](#T-Virgil-SDK-Exceptions-IdentityServiceServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Base exception for all Identity Service exceptions

##### See Also

- [Virgil.SDK.Exceptions.VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException')

<a name='M-Virgil-SDK-Exceptions-IdentityServiceServiceException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-IdentityServiceServiceException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityServiceServiceException](#T-Virgil-SDK-Exceptions-IdentityServiceServiceException 'Virgil.SDK.Exceptions.IdentityServiceServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Identities-IdentityType'></a>
## IdentityType [#](#T-Virgil-SDK-Identities-IdentityType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents identity type

<a name='F-Virgil-SDK-Identities-IdentityType-Application'></a>
### Application `constants` [#](#F-Virgil-SDK-Identities-IdentityType-Application 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The application identity type

<a name='F-Virgil-SDK-Identities-IdentityType-Email'></a>
### Email `constants` [#](#F-Virgil-SDK-Identities-IdentityType-Email 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The email identity type

<a name='T-Virgil-SDK-Identities-IdentityVerificationResponse'></a>
## IdentityVerificationResponse [#](#T-Virgil-SDK-Identities-IdentityVerificationResponse 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents virgil verify response

<a name='P-Virgil-SDK-Identities-IdentityVerificationResponse-ActionId'></a>
### ActionId `property` [#](#P-Virgil-SDK-Identities-IdentityVerificationResponse-ActionId 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the action identifier.

<a name='T-Virgil-SDK-Identities-IEmailVerifier'></a>
## IEmailVerifier [#](#T-Virgil-SDK-Identities-IEmailVerifier 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents a class for identity confirmation.

<a name='P-Virgil-SDK-Identities-IEmailVerifier-ActionId'></a>
### ActionId `property` [#](#P-Virgil-SDK-Identities-IEmailVerifier-ActionId 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the ID of identity verification process.

<a name='M-Virgil-SDK-Identities-IEmailVerifier-Confirm-System-String,System-Int32,System-Int32-'></a>
### Confirm(code,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Identities-IEmailVerifier-Confirm-System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code that was recived on email box. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to limit the lifetime of the token in seconds (maximum value is 60 * 60 * 24 * 365 = 1 year) |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The parameter is used to restrict the number of token usages (maximum value is 100) |

<a name='T-Virgil-SDK-Clients-IIdentityServiceClient'></a>
## IIdentityServiceClient [#](#T-Virgil-SDK-Clients-IIdentityServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Interface that specifies communication with Virgil Security Identity service.

<a name='M-Virgil-SDK-Clients-IIdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,code,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Clients-IIdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| code | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The count to live. |

<a name='M-Virgil-SDK-Clients-IIdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String-'></a>
### IsValid(identityValue,identityType,validationToken) `method` [#](#M-Virgil-SDK-Clients-IIdentityServiceClient-IsValid-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks whether the validation token is valid for specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of identity. |
| identityType | [Virgil.SDK.Identities.VerifiableIdentityType](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Virgil.SDK.Identities.VerifiableIdentityType') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value that represents validation token for Virgil Identity Service. |

<a name='M-Virgil-SDK-Clients-IIdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Verify(identityValue,identityType,extraFields) `method` [#](#M-Virgil-SDK-Clients-IIdentityServiceClient-Verify-System-String,Virgil-SDK-Identities-VerifiableIdentityType,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| identityType | [Virgil.SDK.Identities.VerifiableIdentityType](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Virgil.SDK.Identities.VerifiableIdentityType') | The identity type that going to be verified. |
| extraFields | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') |  |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Clients-IIdentityServiceClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IIdentityServiceClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

<a name='M-Virgil-SDK-Clients-IIdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### VerifyEmail(emailAddress,extraFields) `method` [#](#M-Virgil-SDK-Clients-IIdentityServiceClient-VerifyEmail-System-String,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initiates a process to verify a specified email address.

##### Returns

The verification identuty class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| emailAddress | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The email address you are going to verify. |
| extraFields | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | In some cases it could be necessary to pass some parameters and receive them back in an email. For this special case an optional `extraFields` dictionary can be used. All values passed in `extraFields` parameter will be passed back in an email in a hidden form with extra hidden fields. |

<a name='T-Virgil-SDK-Clients-IPrivateKeysServiceClient'></a>
## IPrivateKeysServiceClient [#](#T-Virgil-SDK-Clients-IPrivateKeysServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

<a name='M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Destroy-System-Guid,System-Byte[],System-String-'></a>
### Destroy(cardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Destroy-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Deletes the private key from service by specified card ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo-'></a>
### Get(cardId,identityInfo) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') |  |

##### Remarks

Random password will be generated to encrypt server response

<a name='M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-String-'></a>
### Get(cardId,identityInfo,responsePassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Get-System-Guid,Virgil-SDK-Identities-IdentityInfo,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| identityInfo | [Virgil.SDK.Identities.IdentityInfo](#T-Virgil-SDK-Identities-IdentityInfo 'Virgil.SDK.Identities.IdentityInfo') |  |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Stash-System-Guid,System-Byte[],System-String-'></a>
### Stash(cardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysServiceClient-Stash-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Uploads private key to private key store.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Http-IRequest'></a>
## IRequest [#](#T-Virgil-SDK-Http-IRequest 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

Represent a generic HTTP request

<a name='P-Virgil-SDK-Http-IRequest-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Http-IRequest-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the requests body.

<a name='P-Virgil-SDK-Http-IRequest-Endpoint'></a>
### Endpoint `property` [#](#P-Virgil-SDK-Http-IRequest-Endpoint 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the endpoint. Does not include server base address

<a name='P-Virgil-SDK-Http-IRequest-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Http-IRequest-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the http headers.

<a name='P-Virgil-SDK-Http-IRequest-Method'></a>
### Method `property` [#](#P-Virgil-SDK-Http-IRequest-Method 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the request method.

<a name='T-Virgil-SDK-Http-IResponse'></a>
## IResponse [#](#T-Virgil-SDK-Http-IResponse 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

Represents a generic HTTP response

<a name='P-Virgil-SDK-Http-IResponse-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Http-IResponse-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Raw response body.

<a name='P-Virgil-SDK-Http-IResponse-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Http-IResponse-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Information about the API.

<a name='P-Virgil-SDK-Http-IResponse-StatusCode'></a>
### StatusCode `property` [#](#P-Virgil-SDK-Http-IResponse-StatusCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The response status code.

<a name='T-Virgil-SDK-Clients-IServiceHub'></a>
## IServiceHub [#](#T-Virgil-SDK-Clients-IServiceHub 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Represents all exposed virgil services

<a name='P-Virgil-SDK-Clients-IServiceHub-Cards'></a>
### Cards `property` [#](#P-Virgil-SDK-Clients-IServiceHub-Cards 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests for `Virgil Card` resources.

<a name='P-Virgil-SDK-Clients-IServiceHub-Identity'></a>
### Identity `property` [#](#P-Virgil-SDK-Clients-IServiceHub-Identity 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests `Identity` service resources.

<a name='P-Virgil-SDK-Clients-IServiceHub-PrivateKeys'></a>
### PrivateKeys `property` [#](#P-Virgil-SDK-Clients-IServiceHub-PrivateKeys 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests for `Private Key` resources.

<a name='T-Virgil-SDK-Clients-IServiceKeyCache'></a>
## IServiceKeyCache [#](#T-Virgil-SDK-Clients-IServiceKeyCache 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides cached value of known public key for channel encryption

<a name='M-Virgil-SDK-Clients-IServiceKeyCache-GetServiceCard-System-String-'></a>
### GetServiceCard(servicePublicKeyId) `method` [#](#M-Virgil-SDK-Clients-IServiceKeyCache-GetServiceCard-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the service's public key by specified identifier.

##### Returns

An instance of [CardModel](#T-Virgil-SDK-Models-CardModel 'Virgil.SDK.Models.CardModel'), that represents service card.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The service's public key identifier. |

<a name='T-Virgil-SDK-Clients-IVirgilService'></a>
## IVirgilService [#](#T-Virgil-SDK-Clients-IVirgilService 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Interface that specifies the Virgil Security service.

<a name='T-Virgil-SDK-Cryptography-KeyPairType'></a>
## KeyPairType [#](#T-Virgil-SDK-Cryptography-KeyPairType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Cryptography

##### Summary

The type of key pair.

<a name='F-Virgil-SDK-Cryptography-KeyPairType-Default'></a>
### Default `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-Default 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Recommended most safe type

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_BP256R1'></a>
### EC_BP256R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP256R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

256-bits Brainpool curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_BP384R1'></a>
### EC_BP384R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP384R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

384-bits Brainpool curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_BP512R1'></a>
### EC_BP512R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_BP512R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

512-bits Brainpool curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_Curve25519'></a>
### EC_Curve25519 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_Curve25519 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Curve25519

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192K1'></a>
### EC_SECP192K1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192K1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

192-bits "Koblitz" curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192R1'></a>
### EC_SECP192R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP192R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

192-bits NIST curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224K1'></a>
### EC_SECP224K1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224K1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

224-bits "Koblitz" curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224R1'></a>
### EC_SECP224R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP224R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

224-bits NIST curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256K1'></a>
### EC_SECP256K1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256K1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

256-bits "Koblitz" curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256R1'></a>
### EC_SECP256R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP256R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

256-bits NIST curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP384R1'></a>
### EC_SECP384R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP384R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

384-bits NIST curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP521R1'></a>
### EC_SECP521R1 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-EC_SECP521R1 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

521-bits NIST curve

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_1024'></a>
### RSA_1024 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_1024 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 1024 bit (not recommended)

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_2048'></a>
### RSA_2048 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_2048 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 2048 bit

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_256'></a>
### RSA_256 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_256 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 256 bit (not recommended)

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_3072'></a>
### RSA_3072 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_3072 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 3072 bit

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_4096'></a>
### RSA_4096 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_4096 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 4096 bit

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_512'></a>
### RSA_512 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_512 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 512 bit (not recommended)

<a name='F-Virgil-SDK-Cryptography-KeyPairType-RSA_8192'></a>
### RSA_8192 `constants` [#](#F-Virgil-SDK-Cryptography-KeyPairType-RSA_8192 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

RSA 8192 bit

<a name='T-Virgil-SDK-Localization'></a>
## Localization [#](#T-Virgil-SDK-Localization 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-Virgil-SDK-Localization-Culture'></a>
### Culture `property` [#](#P-Virgil-SDK-Localization-Culture 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class.

<a name='P-Virgil-SDK-Localization-ExceptionDomainValueDomainIdentityIsInvalid'></a>
### ExceptionDomainValueDomainIdentityIsInvalid `property` [#](#P-Virgil-SDK-Localization-ExceptionDomainValueDomainIdentityIsInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to Domain value specified for the domain identity is invalid.

<a name='P-Virgil-SDK-Localization-ExceptionIdentityVerificationIsNotSent'></a>
### ExceptionIdentityVerificationIsNotSent `property` [#](#P-Virgil-SDK-Localization-ExceptionIdentityVerificationIsNotSent 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to Identity verification request is not sent.

<a name='P-Virgil-SDK-Localization-ExceptionPublicKeyNotFound'></a>
### ExceptionPublicKeyNotFound `property` [#](#P-Virgil-SDK-Localization-ExceptionPublicKeyNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to Public Key is not found.

<a name='P-Virgil-SDK-Localization-ExceptionStringCanNotBeEmpty'></a>
### ExceptionStringCanNotBeEmpty `property` [#](#P-Virgil-SDK-Localization-ExceptionStringCanNotBeEmpty 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to String can not be empty.

<a name='P-Virgil-SDK-Localization-ExceptionStringLengthIsInvalid'></a>
### ExceptionStringLengthIsInvalid `property` [#](#P-Virgil-SDK-Localization-ExceptionStringLengthIsInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to String parameter should have length less than {0}.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataAlreadyExists'></a>
### ExceptionUserDataAlreadyExists `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataAlreadyExists 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User data with same fields is already exists..

<a name='P-Virgil-SDK-Localization-ExceptionUserDataClassSpecifiedIsInvalid'></a>
### ExceptionUserDataClassSpecifiedIsInvalid `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataClassSpecifiedIsInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data class specified is invalid.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataConfirmationEntityNotFound'></a>
### ExceptionUserDataConfirmationEntityNotFound `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataConfirmationEntityNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data confirmation entity not found.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataConfirmationTokenInvalid'></a>
### ExceptionUserDataConfirmationTokenInvalid `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataConfirmationTokenInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data confirmation identityInfo invalid.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataIntegrityConstraintViolation'></a>
### ExceptionUserDataIntegrityConstraintViolation `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataIntegrityConstraintViolation 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data integrity constraint violation.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataIsNotConfirmedYet'></a>
### ExceptionUserDataIsNotConfirmedYet `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataIsNotConfirmedYet 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to The user data is not confirmed yet.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataNotFound'></a>
### ExceptionUserDataNotFound `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User data is not found.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataValueIsRequired'></a>
### ExceptionUserDataValueIsRequired `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataValueIsRequired 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to The user data value is required.

<a name='P-Virgil-SDK-Localization-ExceptionUserDataWasAlreadyConfirmed'></a>
### ExceptionUserDataWasAlreadyConfirmed `property` [#](#P-Virgil-SDK-Localization-ExceptionUserDataWasAlreadyConfirmed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data was already confirmed and does not need further confirmation.

<a name='P-Virgil-SDK-Localization-ExceptionUserIdHadBeenConfirmed'></a>
### ExceptionUserIdHadBeenConfirmed `property` [#](#P-Virgil-SDK-Localization-ExceptionUserIdHadBeenConfirmed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to This user id had been confirmed earlier.

<a name='P-Virgil-SDK-Localization-ExceptionUserInfoDataValidationFailed'></a>
### ExceptionUserInfoDataValidationFailed `property` [#](#P-Virgil-SDK-Localization-ExceptionUserInfoDataValidationFailed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User info data validation failed.

<a name='P-Virgil-SDK-Localization-ExceptionVirgilServiceNotInitialized'></a>
### ExceptionVirgilServiceNotInitialized `property` [#](#P-Virgil-SDK-Localization-ExceptionVirgilServiceNotInitialized 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to The Access Token is not provider..

<a name='P-Virgil-SDK-Localization-ResourceManager'></a>
### ResourceManager `property` [#](#P-Virgil-SDK-Localization-ResourceManager 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns the cached ResourceManager instance used by this class.

<a name='T-Virgil-SDK-Utils-Obfuscator'></a>
## Obfuscator [#](#T-Virgil-SDK-Utils-Obfuscator 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Utils

##### Summary

Provides a helper methods to obfuscate the data.

<a name='M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-String,System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32-'></a>
### PBKDF(value,algorithm,iterations,salt) `method` [#](#M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-String,System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Derives the obfuscated data from incoming parameters using PBKDF function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value to be hashed. |
| algorithm | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The hash algorithm [Hash](#T-Virgil-Crypto-Foundation-VirgilPBKDF-Hash 'Virgil.Crypto.Foundation.VirgilPBKDF.Hash') type. |
| iterations | [Virgil.Crypto.Foundation.VirgilPBKDF.Hash](#T-Virgil-Crypto-Foundation-VirgilPBKDF-Hash 'Virgil.Crypto.Foundation.VirgilPBKDF.Hash') | The count of iterations. |
| salt | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The hash salt. |

<a name='M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-Byte[],System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32-'></a>
### PBKDF(bytes,algorithm,iterations,salt) `method` [#](#M-Virgil-SDK-Utils-Obfuscator-PBKDF-System-Byte[],System-String,Virgil-Crypto-Foundation-VirgilPBKDF-Hash,System-UInt32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Derives the obfuscated data from incoming parameters using PBKDF function.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bytes | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The value to be hashed. |
| algorithm | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The hash algorithm [Hash](#T-Virgil-Crypto-Foundation-VirgilPBKDF-Hash 'Virgil.Crypto.Foundation.VirgilPBKDF.Hash') type. |
| iterations | [Virgil.Crypto.Foundation.VirgilPBKDF.Hash](#T-Virgil-Crypto-Foundation-VirgilPBKDF-Hash 'Virgil.Crypto.Foundation.VirgilPBKDF.Hash') | The count of iterations. |
| salt | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The hash salt. |

<a name='T-Virgil-SDK-Models-PrivateKeyModel'></a>
## PrivateKeyModel [#](#T-Virgil-SDK-Models-PrivateKeyModel 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Models

##### Summary

Represents private key service grab response

<a name='P-Virgil-SDK-Models-PrivateKeyModel-CardId'></a>
### CardId `property` [#](#P-Virgil-SDK-Models-PrivateKeyModel-CardId 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the virgil card identifier.

<a name='P-Virgil-SDK-Models-PrivateKeyModel-PrivateKey'></a>
### PrivateKey `property` [#](#P-Virgil-SDK-Models-PrivateKeyModel-PrivateKey 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the private key.

<a name='T-Virgil-SDK-Http-PrivateKeysConnection'></a>
## PrivateKeysConnection [#](#T-Virgil-SDK-Http-PrivateKeysConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

A connection for making HTTP requests against URI endpoints for public keys service.

##### See Also

- [Virgil.SDK.Http.ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase')
- [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')

<a name='M-Virgil-SDK-Http-PrivateKeysConnection-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Http-PrivateKeysConnection-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysConnection](#T-Virgil-SDK-Http-PrivateKeysConnection 'Virgil.SDK.Http.PrivateKeysConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles private keys service exception responses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Clients-PrivateKeysServiceClient'></a>
## PrivateKeysServiceClient [#](#T-Virgil-SDK-Clients-PrivateKeysServiceClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

##### See Also

- [Virgil.SDK.Clients.EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
- [Virgil.SDK.Clients.IPrivateKeysServiceClient](#T-Virgil-SDK-Clients-IPrivateKeysServiceClient 'Virgil.SDK.Clients.IPrivateKeysServiceClient')

<a name='M-Virgil-SDK-Clients-PrivateKeysServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-PrivateKeysServiceClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysServiceClient](#T-Virgil-SDK-Clients-PrivateKeysServiceClient 'Virgil.SDK.Clients.PrivateKeysServiceClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The known key provider. |

<a name='T-Virgil-SDK-Models-PublicKeyModel'></a>
## PublicKeyModel [#](#T-Virgil-SDK-Models-PublicKeyModel 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Models

##### Summary

Represent public key object returned from virgil public keys service

<a name='P-Virgil-SDK-Models-PublicKeyModel-CreatedAt'></a>
### CreatedAt `property` [#](#P-Virgil-SDK-Models-PublicKeyModel-CreatedAt 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the created at date.

<a name='P-Virgil-SDK-Models-PublicKeyModel-Id'></a>
### Id `property` [#](#P-Virgil-SDK-Models-PublicKeyModel-Id 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the identifier.

<a name='P-Virgil-SDK-Models-PublicKeyModel-Value'></a>
### Value `property` [#](#P-Virgil-SDK-Models-PublicKeyModel-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the public key.

<a name='T-Virgil-SDK-Http-PublicServiceConnection'></a>
## PublicServiceConnection [#](#T-Virgil-SDK-Http-PublicServiceConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

A connection for making HTTP requests against URI endpoints for public api services.

##### See Also

- [Virgil.SDK.Http.ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase')
- [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')

<a name='M-Virgil-SDK-Http-PublicServiceConnection-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Http-PublicServiceConnection-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicServiceConnection](#T-Virgil-SDK-Http-PublicServiceConnection 'Virgil.SDK.Http.PublicServiceConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Http-PublicServiceConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Http-PublicServiceConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles public keys service exception responses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Http-Request'></a>
## Request [#](#T-Virgil-SDK-Http-Request 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

[IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest') default implementation"/>

##### See Also

- [Virgil.SDK.Http.IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest')

<a name='M-Virgil-SDK-Http-Request-#ctor'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-Http-Request-#ctor 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') class.

##### Parameters

This constructor has no parameters.

<a name='P-Virgil-SDK-Http-Request-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Http-Request-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the requests body.

<a name='P-Virgil-SDK-Http-Request-Endpoint'></a>
### Endpoint `property` [#](#P-Virgil-SDK-Http-Request-Endpoint 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the endpoint. Does not include server base address

<a name='P-Virgil-SDK-Http-Request-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Http-Request-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the http headers.

<a name='P-Virgil-SDK-Http-Request-Method'></a>
### Method `property` [#](#P-Virgil-SDK-Http-Request-Method 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the request method.

<a name='T-Virgil-SDK-Http-RequestExtensions'></a>
## RequestExtensions [#](#T-Virgil-SDK-Http-RequestExtensions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

Extensions to help construct http requests

<a name='M-Virgil-SDK-Http-RequestExtensions-EncryptJsonBody-Virgil-SDK-Http-Request,Virgil-SDK-Models-CardModel-'></a>
### EncryptJsonBody(request,card) `method` [#](#M-Virgil-SDK-Http-RequestExtensions-EncryptJsonBody-Virgil-SDK-Http-Request,Virgil-SDK-Models-CardModel- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the json body.

##### Returns

[Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') | The request. |
| card | [Virgil.SDK.Models.CardModel](#T-Virgil-SDK-Models-CardModel 'Virgil.SDK.Models.CardModel') | The Virgil Card dto. |

<a name='M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Guid,System-Byte[],System-String-'></a>
### SignRequest(request,cardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Signs the request.

##### Returns

[Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') | The request. |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Byte[],System-String-'></a>
### SignRequest(request,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Http-RequestExtensions-SignRequest-Virgil-SDK-Http-Request,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Signs the request.

##### Returns

[Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') | The request. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Http-RequestExtensions-WithBody-Virgil-SDK-Http-Request,System-Object-'></a>
### WithBody(request,body) `method` [#](#M-Virgil-SDK-Http-RequestExtensions-WithBody-Virgil-SDK-Http-Request,System-Object- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Withes the body.

##### Returns

[Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') | The request. |
| body | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The body. |

<a name='M-Virgil-SDK-Http-RequestExtensions-WithEndpoint-Virgil-SDK-Http-Request,System-String-'></a>
### WithEndpoint(request,endpoint) `method` [#](#M-Virgil-SDK-Http-RequestExtensions-WithEndpoint-Virgil-SDK-Http-Request,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sets the request enpoint

##### Returns

[Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.Request](#T-Virgil-SDK-Http-Request 'Virgil.SDK.Http.Request') | The request. |
| endpoint | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The endpoint. |

<a name='T-Virgil-SDK-Http-RequestMethod'></a>
## RequestMethod [#](#T-Virgil-SDK-Http-RequestMethod 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

Represents HTTP request methods

<a name='T-Virgil-SDK-Http-Response'></a>
## Response [#](#T-Virgil-SDK-Http-Response 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

[IResponse](#T-Virgil-SDK-Http-IResponse 'Virgil.SDK.Http.IResponse') default implementation

<a name='P-Virgil-SDK-Http-Response-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Http-Response-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Raw response body.

<a name='P-Virgil-SDK-Http-Response-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Http-Response-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Information about the API.

<a name='P-Virgil-SDK-Http-Response-StatusCode'></a>
### StatusCode `property` [#](#P-Virgil-SDK-Http-Response-StatusCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The response status code.

<a name='T-Virgil-SDK-Clients-ResponseVerifyClient'></a>
## ResponseVerifyClient [#](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides a base implementation of HTTP client for the Virgil Security services which provide response signature.

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ResponseVerifyClient](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Virgil.SDK.Clients.ResponseVerifyClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ResponseVerifyClient](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Virgil.SDK.Clients.ResponseVerifyClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The service key cache. |

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest-'></a>
### Send() `method` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP request.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest-'></a>
### Send\`\`1(request) `method` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP POST request. Attempts to map the response body to an object of type `TResult`

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Http.IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TResult |  |

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]-'></a>
### VerifyResponse(nativeResponse,publicKey) `method` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the HTTP response with specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nativeResponse | [Virgil.SDK.Http.IResponse](#T-Virgil-SDK-Http-IResponse 'Virgil.SDK.Http.IResponse') | An instance of HTTP response. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A public key to be used for verification. |

<a name='T-Virgil-SDK-ServiceHub'></a>
## ServiceHub [#](#T-Virgil-SDK-ServiceHub 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

Represents all exposed virgil services

<a name='M-Virgil-SDK-ServiceHub-#ctor-Virgil-SDK-ServiceHubConfig-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-ServiceHub-#ctor-Virgil-SDK-ServiceHubConfig- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ServiceHub](#T-Virgil-SDK-ServiceHub 'Virgil.SDK.ServiceHub') class.

##### Parameters

This constructor has no parameters.

<a name='P-Virgil-SDK-ServiceHub-Cards'></a>
### Cards `property` [#](#P-Virgil-SDK-ServiceHub-Cards 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests for `Card` resources.

<a name='P-Virgil-SDK-ServiceHub-Identity'></a>
### Identity `property` [#](#P-Virgil-SDK-ServiceHub-Identity 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests Identity service resources.

<a name='P-Virgil-SDK-ServiceHub-PrivateKeys'></a>
### PrivateKeys `property` [#](#P-Virgil-SDK-ServiceHub-PrivateKeys 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a client that handle requests for `PrivateKey` resources.

<a name='M-Virgil-SDK-ServiceHub-BuildCardsClient'></a>
### BuildCardsClient() `method` [#](#M-Virgil-SDK-ServiceHub-BuildCardsClient 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Builds a Cards client instance.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHub-BuildIdentityClient'></a>
### BuildIdentityClient() `method` [#](#M-Virgil-SDK-ServiceHub-BuildIdentityClient 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Builds a Identity Service client instance.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHub-BuildPrivateKeysClient'></a>
### BuildPrivateKeysClient() `method` [#](#M-Virgil-SDK-ServiceHub-BuildPrivateKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Builds a Private Key client instance.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHub-Create-System-String-'></a>
### Create() `method` [#](#M-Virgil-SDK-ServiceHub-Create-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates new [ServiceHub](#T-Virgil-SDK-ServiceHub 'Virgil.SDK.ServiceHub') instances with default configuration for specified access token.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHub-Create-Virgil-SDK-ServiceHubConfig-'></a>
### Create() `method` [#](#M-Virgil-SDK-ServiceHub-Create-Virgil-SDK-ServiceHubConfig- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates new [ServiceHub](#T-Virgil-SDK-ServiceHub 'Virgil.SDK.ServiceHub') instances with default configuration for specified configuration.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHub-Initialize'></a>
### Initialize() `method` [#](#M-Virgil-SDK-ServiceHub-Initialize 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes an implementation of the ServiceHub class.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-ServiceHubConfig'></a>
## ServiceHubConfig [#](#T-Virgil-SDK-ServiceHubConfig 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

Represents a configuration file that is applicable to a particular [ServiceHub](#T-Virgil-SDK-ServiceHub 'Virgil.SDK.ServiceHub'). This class cannot be inherited.

<a name='M-Virgil-SDK-ServiceHubConfig-#ctor-System-String-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-ServiceHubConfig-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ServiceHubConfig](#T-Virgil-SDK-ServiceHubConfig 'Virgil.SDK.ServiceHubConfig') class.

##### Parameters

This constructor has no parameters.

<a name='M-Virgil-SDK-ServiceHubConfig-UseAccessToken-System-String-'></a>
### UseAccessToken() `method` [#](#M-Virgil-SDK-ServiceHubConfig-UseAccessToken-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sets the application token to access to the Virgil Security services. This token has to be generated with application private key on Virgil Security portal or manually with SDK Utils.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHubConfig-WithIdentityServiceAddress-System-String-'></a>
### WithIdentityServiceAddress() `method` [#](#M-Virgil-SDK-ServiceHubConfig-WithIdentityServiceAddress-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Overrides default Virgil Identity service address.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHubConfig-WithPrivateServicesAddress-System-String-'></a>
### WithPrivateServicesAddress() `method` [#](#M-Virgil-SDK-ServiceHubConfig-WithPrivateServicesAddress-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Overrides default Virgil Private Keys service address.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHubConfig-WithPublicServicesAddress-System-String-'></a>
### WithPublicServicesAddress() `method` [#](#M-Virgil-SDK-ServiceHubConfig-WithPublicServicesAddress-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Overrides default Virgil Public Keys service address.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-ServiceHubConfig-WithStagingEnvironment'></a>
### WithStagingEnvironment() `method` [#](#M-Virgil-SDK-ServiceHubConfig-WithStagingEnvironment 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes Virgil Securtity services with staging urls.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Clients-ServiceIdentities'></a>
## ServiceIdentities [#](#T-Virgil-SDK-Clients-ServiceIdentities 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Holds known Virgil application ids

<a name='F-Virgil-SDK-Clients-ServiceIdentities-IdentityService'></a>
### IdentityService `constants` [#](#F-Virgil-SDK-Clients-ServiceIdentities-IdentityService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Identity service app id

<a name='F-Virgil-SDK-Clients-ServiceIdentities-PrivateService'></a>
### PrivateService `constants` [#](#F-Virgil-SDK-Clients-ServiceIdentities-PrivateService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Private key service app id

<a name='F-Virgil-SDK-Clients-ServiceIdentities-PublicService'></a>
### PublicService `constants` [#](#F-Virgil-SDK-Clients-ServiceIdentities-PublicService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Public service app id

<a name='T-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException'></a>
## ServiceSignVerificationServiceException [#](#T-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Occurs when service response sign is invalid

##### See Also

- [Virgil.SDK.Exceptions.VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException')

<a name='M-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException-#ctor-System-String-'></a>
### #ctor(message) `constructor` [#](#M-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ServiceSignVerificationServiceException](#T-Virgil-SDK-Exceptions-ServiceSignVerificationServiceException 'Virgil.SDK.Exceptions.ServiceSignVerificationServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='T-Virgil-SDK-Utils-ValidationTokenGenerator'></a>
## ValidationTokenGenerator [#](#T-Virgil-SDK-Utils-ValidationTokenGenerator 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Utils

##### Summary

Provides a helper methods to generate validation token based on application's private key.

<a name='M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-String,System-String,System-Byte[],System-String-'></a>
### Generate(identityValue,identityType,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-String,System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates the validation token based on application's private key.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| identityType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of the identity. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The application's private key. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-Guid,System-String,System-String,System-Byte[],System-String-'></a>
### Generate(identityValue,identityType,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Utils-ValidationTokenGenerator-Generate-System-Guid,System-String,System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates the validation token based on application's private key.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The identity value. |
| identityType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type of the identity. |
| privateKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application's private key. |
| privateKeyPassword | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key password. |

<a name='T-Virgil-SDK-Identities-VerifiableIdentityType'></a>
## VerifiableIdentityType [#](#T-Virgil-SDK-Identities-VerifiableIdentityType 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Identities

##### Summary

Represents a supported identity type that Virgil Service are able to verify.

<a name='F-Virgil-SDK-Identities-VerifiableIdentityType-Email'></a>
### Email `constants` [#](#F-Virgil-SDK-Identities-VerifiableIdentityType-Email 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The email identity type

<a name='T-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException'></a>
## VerificationRequestIsNotSentServiceException [#](#T-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary



<a name='M-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException-#ctor'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException-#ctor 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VerificationRequestIsNotSentServiceException](#T-Virgil-SDK-Exceptions-VerificationRequestIsNotSentServiceException 'Virgil.SDK.Exceptions.VerificationRequestIsNotSentServiceException') class.

##### Parameters

This constructor has no parameters.

<a name='T-Virgil-SDK-VirgilBuffer'></a>
## VirgilBuffer [#](#T-Virgil-SDK-VirgilBuffer 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

The [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') class provide methods to convert data between types.

<a name='M-Virgil-SDK-VirgilBuffer-FromBase64String-System-String-'></a>
### FromBase64String() `method` [#](#M-Virgil-SDK-VirgilBuffer-FromBase64String-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') from specified string is encoded with base-64 digits.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-FromBytes-System-Byte[]-'></a>
### FromBytes() `method` [#](#M-Virgil-SDK-VirgilBuffer-FromBytes-System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') from specified byte array.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-FromHexString-System-String-'></a>
### FromHexString() `method` [#](#M-Virgil-SDK-VirgilBuffer-FromHexString-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') from specified string in hexadecimal representation.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-FromString-System-String-'></a>
### FromString() `method` [#](#M-Virgil-SDK-VirgilBuffer-FromString-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') from specified string.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-ToBase64String'></a>
### ToBase64String() `method` [#](#M-Virgil-SDK-VirgilBuffer-ToBase64String 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Converts the buffer to its equivalent string representation that is encoded with base-64 digits.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-ToBytes'></a>
### ToBytes() `method` [#](#M-Virgil-SDK-VirgilBuffer-ToBytes 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Converts the buffer to byte array.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-ToHexString'></a>
### ToHexString() `method` [#](#M-Virgil-SDK-VirgilBuffer-ToHexString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Converts the buffer to its hexadecimal string representation.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilBuffer-ToString'></a>
### ToString() `method` [#](#M-Virgil-SDK-VirgilBuffer-ToString 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Converts the buffer to its equivalent string representation.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-VirgilCard'></a>
## VirgilCard [#](#T-Virgil-SDK-VirgilCard 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

A Virgil Card is the main entity of the Virgil Security services, it includes an information about the user and his public key. The Virgil Card identifies the user by one of his available types, such as an email, a phone number, etc.

<a name='M-Virgil-SDK-VirgilCard-#ctor-Virgil-SDK-Clients-Models-VirgilCardModel-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-VirgilCard-#ctor-Virgil-SDK-Clients-Models-VirgilCardModel- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') class.

##### Parameters

This constructor has no parameters.

<a name='P-Virgil-SDK-VirgilCard-CreatedAt'></a>
### CreatedAt `property` [#](#P-Virgil-SDK-VirgilCard-CreatedAt 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the date and time of Virgil Card creation.

<a name='P-Virgil-SDK-VirgilCard-Data'></a>
### Data `property` [#](#P-Virgil-SDK-VirgilCard-Data 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the custom [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') parameters.

<a name='P-Virgil-SDK-VirgilCard-Id'></a>
### Id `property` [#](#P-Virgil-SDK-VirgilCard-Id 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the unique identifier for the Virgil Card.

<a name='P-Virgil-SDK-VirgilCard-Identity'></a>
### Identity `property` [#](#P-Virgil-SDK-VirgilCard-Identity 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the value of current Virgil Card identity.

<a name='P-Virgil-SDK-VirgilCard-IdentityType'></a>
### IdentityType `property` [#](#P-Virgil-SDK-VirgilCard-IdentityType 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the type of current Virgil Card identity.

<a name='P-Virgil-SDK-VirgilCard-IsConfirmed'></a>
### IsConfirmed `property` [#](#P-Virgil-SDK-VirgilCard-IsConfirmed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a value indicating whether the current [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') identity is confirmed by Virgil Identity service.

<a name='P-Virgil-SDK-VirgilCard-IsGlobal'></a>
### IsGlobal `property` [#](#P-Virgil-SDK-VirgilCard-IsGlobal 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a value indicating whether this [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') is global.

<a name='P-Virgil-SDK-VirgilCard-PublicKey'></a>
### PublicKey `property` [#](#P-Virgil-SDK-VirgilCard-PublicKey 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the Public Key of current Virgil Card.

<a name='P-Virgil-SDK-VirgilCard-RevokedAt'></a>
### RevokedAt `property` [#](#P-Virgil-SDK-VirgilCard-RevokedAt 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the date and time of [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') revocation.

<a name='P-Virgil-SDK-VirgilCard-Version'></a>
### Version `property` [#](#P-Virgil-SDK-VirgilCard-Version 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') version.

<a name='M-Virgil-SDK-VirgilCard-Encrypt-Virgil-SDK-VirgilBuffer-'></a>
### Encrypt(data) `method` [#](#M-Virgil-SDK-VirgilCard-Encrypt-Virgil-SDK-VirgilBuffer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the specified data for current [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') recipient.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [Virgil.SDK.VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') | The data to be encrypted. |

<a name='M-Virgil-SDK-VirgilCard-Find-System-String-'></a>
### Find(identity) `method` [#](#M-Virgil-SDK-VirgilCard-Find-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Finds the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard')s by specified criteria.

##### Returns

A list of found [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard')s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity. |

<a name='M-Virgil-SDK-VirgilCard-Get-System-Guid-'></a>
### Get(id) `method` [#](#M-Virgil-SDK-VirgilCard-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') by specified identifier.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The identifier that represents a [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard'). |

<a name='M-Virgil-SDK-VirgilCard-Verify-Virgil-SDK-VirgilBuffer,Virgil-SDK-VirgilBuffer-'></a>
### Verify(data,signature) `method` [#](#M-Virgil-SDK-VirgilCard-Verify-Virgil-SDK-VirgilBuffer,Virgil-SDK-VirgilBuffer- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the specified data and signature with current [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') recipient.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [Virgil.SDK.VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') | The data to be verified. |
| signature | [Virgil.SDK.VirgilBuffer](#T-Virgil-SDK-VirgilBuffer 'Virgil.SDK.VirgilBuffer') | The signature used to verify the data integrity. |

<a name='T-Virgil-SDK-Clients-Models-VirgilCardModel'></a>
## VirgilCardModel [#](#T-Virgil-SDK-Clients-Models-VirgilCardModel 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients.Models

##### Summary

Represents a Virgil Card data transder object.

<a name='T-Virgil-SDK-VirgilCardRequest'></a>
## VirgilCardRequest [#](#T-Virgil-SDK-VirgilCardRequest 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

The Virgil Card request is a data structure that represents user's identity, Public Key and other data. The request is used to tell the Virgil Cards service that the user's identity and Public Key are valid, this kind of validation can be reached by validating signatures of owner's Private Key and application's Private Key.

<a name='M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,System-String,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,System-String,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest') class.

##### Parameters

This constructor has no parameters.

<a name='M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,Virgil-SDK-VirgilKey,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-VirgilCardRequest-#ctor-System-String,System-String,Virgil-SDK-VirgilKey,System-Boolean,System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest') class.

##### Parameters

This constructor has no parameters.

<a name='P-Virgil-SDK-VirgilCardRequest-Data'></a>
### Data `property` [#](#P-Virgil-SDK-VirgilCardRequest-Data 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the key/value parameters of future [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard').

<a name='P-Virgil-SDK-VirgilCardRequest-Fingerprint'></a>
### Fingerprint `property` [#](#P-Virgil-SDK-VirgilCardRequest-Fingerprint 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the canonical form of current [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') instance.

<a name='P-Virgil-SDK-VirgilCardRequest-Id'></a>
### Id `property` [#](#P-Virgil-SDK-VirgilCardRequest-Id 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets an unique idenitity of the ticket.

<a name='P-Virgil-SDK-VirgilCardRequest-Identity'></a>
### Identity `property` [#](#P-Virgil-SDK-VirgilCardRequest-Identity 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the user's identity value.

<a name='P-Virgil-SDK-VirgilCardRequest-IdentityType'></a>
### IdentityType `property` [#](#P-Virgil-SDK-VirgilCardRequest-IdentityType 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the user's identity type.

<a name='P-Virgil-SDK-VirgilCardRequest-IsGlobal'></a>
### IsGlobal `property` [#](#P-Virgil-SDK-VirgilCardRequest-IsGlobal 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a value indicating whether this [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') is global.

<a name='P-Virgil-SDK-VirgilCardRequest-PublicKey'></a>
### PublicKey `property` [#](#P-Virgil-SDK-VirgilCardRequest-PublicKey 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets a Public Key value.

<a name='P-Virgil-SDK-VirgilCardRequest-Signs'></a>
### Signs `property` [#](#P-Virgil-SDK-VirgilCardRequest-Signs 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the list of digital signatures that was signed the [Fingerprint](#P-Virgil-SDK-VirgilCardRequest-Fingerprint 'Virgil.SDK.VirgilCardRequest.Fingerprint') of current [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest').

<a name='M-Virgil-SDK-VirgilCardRequest-AddSign-System-Guid,System-Byte[]-'></a>
### AddSign(cardId,sign) `method` [#](#M-Virgil-SDK-VirgilCardRequest-AddSign-System-Guid,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Adds a signature of third party Private Keys.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard')'s identifier. |
| sign | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The |

##### Example

var keyPair = VirgilKeyPair.Generate(); var ticket = new VirgilCardTicket("demo@virgilsecurity.com", "email", keyPair.PublicKey()); var ownerSign = CryptoHelper.Sign(ticket.Fingerprint, keyPair.PrivateKey()); var appSign = CryptoHelper.Sign(ticket.Fingerprint, %APP_PRIVATE_KEY%); ticket.AddOwnerSign(ownerSign); ticket.AddSign(%APP_CARD_ID%, appSign);

<a name='M-Virgil-SDK-VirgilCardRequest-Export'></a>
### Export() `method` [#](#M-Virgil-SDK-VirgilCardRequest-Export 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Exports a current [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest') to it's binary representation.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilCardRequest-Import-System-String-'></a>
### Import() `method` [#](#M-Virgil-SDK-VirgilCardRequest-Import-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Imports the [VirgilCardRequest](#T-Virgil-SDK-VirgilCardRequest 'Virgil.SDK.VirgilCardRequest') from it's binary representation.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-VirgilConfig'></a>
## VirgilConfig [#](#T-Virgil-SDK-VirgilConfig 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

The [VirgilConfig](#T-Virgil-SDK-VirgilConfig 'Virgil.SDK.VirgilConfig') is responsible for the initialization of the high-level SDK components.

<a name='M-Virgil-SDK-VirgilConfig-Initialize-System-String-'></a>
### Initialize(accessToken) `method` [#](#M-Virgil-SDK-VirgilConfig-Initialize-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes service clients with API

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token provides an authenticated secure access to the Virgil Security services and is passed with each API call. The access token also allows the API to associate your app’s requests with your Virgil Security developer’s account. |

<a name='M-Virgil-SDK-VirgilConfig-Reset'></a>
### Reset() `method` [#](#M-Virgil-SDK-VirgilConfig-Reset 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Restores the persisted high-level SDK components values to their corresponding default properties.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilConfig-SetCryptoProvider-Virgil-SDK-Cryptography-ICryptoProvider-'></a>
### SetCryptoProvider() `method` [#](#M-Virgil-SDK-VirgilConfig-SetCryptoProvider-Virgil-SDK-Cryptography-ICryptoProvider- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sets the implementation of [ICryptoProvider](#T-Virgil-SDK-Cryptography-ICryptoProvider 'Virgil.SDK.Cryptography.ICryptoProvider') that provides cryptographic operations such as signature generation and verification, and encryption and decryption.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilConfig-SetKeyStorageProvider-Virgil-SDK-Storage-IKeyStorageProvider-'></a>
### SetKeyStorageProvider() `method` [#](#M-Virgil-SDK-VirgilConfig-SetKeyStorageProvider-Virgil-SDK-Storage-IKeyStorageProvider- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sets the implementation of [IKeyStorageProvider](#T-Virgil-SDK-Storage-IKeyStorageProvider 'Virgil.SDK.Storage.IKeyStorageProvider') that provides key storage.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-VirgilConfig-SetServiceHub-Virgil-SDK-Clients-IServiceHub-'></a>
### SetServiceHub() `method` [#](#M-Virgil-SDK-VirgilConfig-SetServiceHub-Virgil-SDK-Clients-IServiceHub- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sets the implementation of [IServiceHub](#T-Virgil-SDK-Clients-IServiceHub 'Virgil.SDK.Clients.IServiceHub') that provides access to Virgil Security services.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Exceptions-VirgilException'></a>
## VirgilException [#](#T-Virgil-SDK-Exceptions-VirgilException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Represents errors occurred during interaction with SDK components.

<a name='T-Virgil-SDK-VirgilKey'></a>
## VirgilKey [#](#T-Virgil-SDK-VirgilKey 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

The [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') object represents an opaque reference to keying material that is managed by the user agent.

<a name='M-Virgil-SDK-VirgilKey-#ctor'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-VirgilKey-#ctor 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Prevents a default instance of the [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') class from being created.

##### Parameters

This constructor has no parameters.

<a name='M-Virgil-SDK-VirgilKey-Create-System-String-'></a>
### Create(keyName) `method` [#](#M-Virgil-SDK-VirgilKey-Create-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') object that represents a new named key, using default key storage provider.

##### Returns

An instance of [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') that represent a newly created key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the key. |

<a name='M-Virgil-SDK-VirgilKey-Create-System-String,System-Byte[],System-Byte[]-'></a>
### Create(keyName,publicKey,privateKey) `method` [#](#M-Virgil-SDK-VirgilKey-Create-System-String,System-Byte[],System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates an instance of [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') object by specified Public/Private key, using default key storage provider.

##### Returns

An instance of [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') that represent a newly created key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the key. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The byte array that represents a Public Key in PEM format. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The byte array that represents a Private Key in PEM format. |

<a name='M-Virgil-SDK-VirgilKey-Exists-System-String-'></a>
### Exists(keyName) `method` [#](#M-Virgil-SDK-VirgilKey-Exists-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the key. |

<a name='M-Virgil-SDK-VirgilKey-RevokeAssociatedVirgilCard'></a>
### RevokeAssociatedVirgilCard() `method` [#](#M-Virgil-SDK-VirgilKey-RevokeAssociatedVirgilCard 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Revokes associated [VirgilCard](#T-Virgil-SDK-VirgilCard 'Virgil.SDK.VirgilCard') from Virgil Cards service.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-VirgilKeyExtensions'></a>
## VirgilKeyExtensions [#](#T-Virgil-SDK-VirgilKeyExtensions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK

##### Summary

Provides a set of static methods that extend [VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') class functionality.

<a name='M-Virgil-SDK-VirgilKeyExtensions-Sign-Virgil-SDK-VirgilKey,System-String-'></a>
### Sign(key,plaintext) `method` [#](#M-Virgil-SDK-VirgilKeyExtensions-Sign-Virgil-SDK-VirgilKey,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Signs the specified plaintext with

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [Virgil.SDK.VirgilKey](#T-Virgil-SDK-VirgilKey 'Virgil.SDK.VirgilKey') | The key. |
| plaintext | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The plaintext. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='T-Virgil-SDK-Exceptions-VirgilServiceException'></a>
## VirgilServiceException [#](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Base exception class for all Virgil Services operations

<a name='M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-String-'></a>
### #ctor(message) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilServiceException-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='P-Virgil-SDK-Exceptions-VirgilServiceException-ErrorCode'></a>
### ErrorCode `property` [#](#P-Virgil-SDK-Exceptions-VirgilServiceException-ErrorCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the error code.

<a name='T-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException'></a>
## VirgilServiceNotInitializedException [#](#T-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

The exception that is thrown when a Virgil Services is not initialized.

<a name='M-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException-#ctor'></a>
### #ctor() `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException-#ctor 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilServiceNotInitializedException](#T-Virgil-SDK-Exceptions-VirgilServiceNotInitializedException 'Virgil.SDK.Exceptions.VirgilServiceNotInitializedException') class.

##### Parameters

This constructor has no parameters.

<a name='T-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException'></a>
## VirgilServicePrivateServicesException [#](#T-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Private service exception

##### See Also

- [Virgil.SDK.Exceptions.VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException')

<a name='M-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilServicePrivateServicesException](#T-Virgil-SDK-Exceptions-VirgilServicePrivateServicesException 'Virgil.SDK.Exceptions.VirgilServicePrivateServicesException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Exceptions-VirgilServicePublicServicesException'></a>
## VirgilServicePublicServicesException [#](#T-Virgil-SDK-Exceptions-VirgilServicePublicServicesException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Public service exception

##### See Also

- [Virgil.SDK.Exceptions.VirgilServiceException](#T-Virgil-SDK-Exceptions-VirgilServiceException 'Virgil.SDK.Exceptions.VirgilServiceException')

<a name='M-Virgil-SDK-Exceptions-VirgilServicePublicServicesException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilServicePublicServicesException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilServicePublicServicesException](#T-Virgil-SDK-Exceptions-VirgilServicePublicServicesException 'Virgil.SDK.Exceptions.VirgilServicePublicServicesException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |
