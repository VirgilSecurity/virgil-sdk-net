<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [BytesExtensions](#T-Virgil-SDK-Infrastructure-BytesExtensions 'Virgil.SDK.Infrastructure.BytesExtensions')
  - [GetBytes(source,encoding)](#M-Virgil-SDK-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding- 'Virgil.SDK.Infrastructure.BytesExtensions.GetBytes(System.String,System.Text.Encoding)')
  - [GetString(source,encoding)](#M-Virgil-SDK-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding- 'Virgil.SDK.Infrastructure.BytesExtensions.GetString(System.Byte[],System.Text.Encoding)')
- [Cards](#T-Virgil-SDK-Domain-Cards 'Virgil.SDK.Domain.Cards')
  - [#ctor(collection)](#M-Virgil-SDK-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Domain-RecipientCard}- 'Virgil.SDK.Domain.Cards.#ctor(System.Collections.Generic.IEnumerable{Virgil.SDK.Domain.RecipientCard})')
  - [Count](#P-Virgil-SDK-Domain-Cards-Count 'Virgil.SDK.Domain.Cards.Count')
  - [Encrypt(data)](#M-Virgil-SDK-Domain-Cards-Encrypt-System-Byte[]- 'Virgil.SDK.Domain.Cards.Encrypt(System.Byte[])')
  - [Encrypt(text)](#M-Virgil-SDK-Domain-Cards-Encrypt-System-String- 'Virgil.SDK.Domain.Cards.Encrypt(System.String)')
  - [GetEnumerator()](#M-Virgil-SDK-Domain-Cards-GetEnumerator 'Virgil.SDK.Domain.Cards.GetEnumerator')
  - [PrepareSearch(identity)](#M-Virgil-SDK-Domain-Cards-PrepareSearch-System-String- 'Virgil.SDK.Domain.Cards.PrepareSearch(System.String)')
  - [Search(builder)](#M-Virgil-SDK-Domain-Cards-Search-Virgil-SDK-Domain-SearchBuilder- 'Virgil.SDK.Domain.Cards.Search(Virgil.SDK.Domain.SearchBuilder)')
  - [Search(value,type,relations,includeUnconfirmed)](#M-Virgil-SDK-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Domain.Cards.Search(System.String,System.Nullable{Virgil.SDK.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [System#Collections#IEnumerable#GetEnumerator()](#M-Virgil-SDK-Domain-Cards-System#Collections#IEnumerable#GetEnumerator 'Virgil.SDK.Domain.Cards.System#Collections#IEnumerable#GetEnumerator')
- [ConfirmOptions](#T-Virgil-SDK-Domain-ConfirmOptions 'Virgil.SDK.Domain.ConfirmOptions')
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
- [EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.EndpointClient.#ctor(Virgil.SDK.Http.IConnection)')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-EndpointClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.EndpointClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Cache](#F-Virgil-SDK-Clients-EndpointClient-Cache 'Virgil.SDK.Clients.EndpointClient.Cache')
  - [Connection](#F-Virgil-SDK-Clients-EndpointClient-Connection 'Virgil.SDK.Clients.EndpointClient.Connection')
  - [EndpointApplicationId](#F-Virgil-SDK-Clients-EndpointClient-EndpointApplicationId 'Virgil.SDK.Clients.EndpointClient.EndpointApplicationId')
  - [Send()](#M-Virgil-SDK-Clients-EndpointClient-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.EndpointClient.Send(Virgil.SDK.Http.IRequest)')
  - [Send\`\`1()](#M-Virgil-SDK-Clients-EndpointClient-Send``1-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.EndpointClient.Send``1(Virgil.SDK.Http.IRequest)')
  - [VerifyResponse(nativeResponse,publicKey)](#M-Virgil-SDK-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]- 'Virgil.SDK.Clients.EndpointClient.VerifyResponse(Virgil.SDK.Http.IResponse,System.Byte[])')
- [Ensure](#T-Virgil-SDK-Helpers-Ensure 'Virgil.SDK.Helpers.Ensure')
  - [ArgumentNotNull(value,name)](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNull-System-Object,System-String- 'Virgil.SDK.Helpers.Ensure.ArgumentNotNull(System.Object,System.String)')
  - [ArgumentNotNullOrEmptyString(value,name)](#M-Virgil-SDK-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String- 'Virgil.SDK.Helpers.Ensure.ArgumentNotNullOrEmptyString(System.String,System.String)')
- [IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')
  - [BaseAddress](#P-Virgil-SDK-Http-IConnection-BaseAddress 'Virgil.SDK.Http.IConnection.BaseAddress')
  - [Send(request)](#M-Virgil-SDK-Http-IConnection-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Http.IConnection.Send(Virgil.SDK.Http.IRequest)')
- [IdentityClient](#T-Virgil-SDK-Clients-IdentityClient 'Virgil.SDK.Clients.IdentityClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-IdentityClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.IdentityClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Clients-IdentityClient-#ctor-System-String,System-String- 'Virgil.SDK.Clients.IdentityClient.#ctor(System.String,System.String)')
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#M-Virgil-SDK-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(type,value,validationToken)](#M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String- 'Virgil.SDK.Clients.IdentityClient.IsValid(Virgil.SDK.TransferObject.IdentityType,System.String,System.String)')
  - [IsValid(token)](#M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto- 'Virgil.SDK.Clients.IdentityClient.IsValid(Virgil.SDK.TransferObject.IndentityTokenDto)')
  - [Verify(identityValue,type)](#M-Virgil-SDK-Clients-IdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType- 'Virgil.SDK.Clients.IdentityClient.Verify(System.String,Virgil.SDK.TransferObject.IdentityType)')
- [IdentityConnection](#T-Virgil-SDK-Http-IdentityConnection 'Virgil.SDK.Http.IdentityConnection')
  - [#ctor(baseAddress)](#M-Virgil-SDK-Http-IdentityConnection-#ctor-System-Uri- 'Virgil.SDK.Http.IdentityConnection.#ctor(System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-IdentityConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.IdentityConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [IdentityServiceException](#T-Virgil-SDK-Exceptions-IdentityServiceException 'Virgil.SDK.Exceptions.IdentityServiceException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.IdentityServiceException.#ctor(System.Int32,System.String)')
- [IIdentityClient](#T-Virgil-SDK-Clients-IIdentityClient 'Virgil.SDK.Clients.IIdentityClient')
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#M-Virgil-SDK-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IIdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(type,value,validationToken)](#M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String- 'Virgil.SDK.Clients.IIdentityClient.IsValid(Virgil.SDK.TransferObject.IdentityType,System.String,System.String)')
  - [IsValid(token)](#M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto- 'Virgil.SDK.Clients.IIdentityClient.IsValid(Virgil.SDK.TransferObject.IndentityTokenDto)')
  - [Verify(identityValue,type)](#M-Virgil-SDK-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType- 'Virgil.SDK.Clients.IIdentityClient.Verify(System.String,Virgil.SDK.TransferObject.IdentityType)')
- [IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto')
  - [Type](#P-Virgil-SDK-TransferObject-IndentityTokenDto-Type 'Virgil.SDK.TransferObject.IndentityTokenDto.Type')
  - [ValidationToken](#P-Virgil-SDK-TransferObject-IndentityTokenDto-ValidationToken 'Virgil.SDK.TransferObject.IndentityTokenDto.ValidationToken')
  - [Value](#P-Virgil-SDK-TransferObject-IndentityTokenDto-Value 'Virgil.SDK.TransferObject.IndentityTokenDto.Value')
- [IPrivateKeysClient](#T-Virgil-SDK-Clients-IPrivateKeysClient 'Virgil.SDK.Clients.IPrivateKeysClient')
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IPrivateKeysClient.Destroy(System.Guid,System.Byte[],System.String)')
  - [Get(virgilCardId,token)](#M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto- 'Virgil.SDK.Clients.IPrivateKeysClient.Get(System.Guid,Virgil.SDK.TransferObject.IndentityTokenDto)')
  - [Get(virgilCardId,token,responsePassword)](#M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String- 'Virgil.SDK.Clients.IPrivateKeysClient.Get(System.Guid,Virgil.SDK.TransferObject.IndentityTokenDto,System.String)')
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IPrivateKeysClient.Stash(System.Guid,System.Byte[],System.String)')
- [IPublicKeysClient](#T-Virgil-SDK-Clients-IPublicKeysClient 'Virgil.SDK.Clients.IPublicKeysClient')
  - [Get(publicKeyId)](#M-Virgil-SDK-Clients-IPublicKeysClient-Get-System-Guid- 'Virgil.SDK.Clients.IPublicKeysClient.Get(System.Guid)')
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#M-Virgil-SDK-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Virgil.SDK.Clients.IPublicKeysClient.GetExtended(System.Guid,System.Guid,System.Byte[])')
- [IRequest](#T-Virgil-SDK-Http-IRequest 'Virgil.SDK.Http.IRequest')
  - [Body](#P-Virgil-SDK-Http-IRequest-Body 'Virgil.SDK.Http.IRequest.Body')
  - [Endpoint](#P-Virgil-SDK-Http-IRequest-Endpoint 'Virgil.SDK.Http.IRequest.Endpoint')
  - [Headers](#P-Virgil-SDK-Http-IRequest-Headers 'Virgil.SDK.Http.IRequest.Headers')
  - [Method](#P-Virgil-SDK-Http-IRequest-Method 'Virgil.SDK.Http.IRequest.Method')
- [IResponse](#T-Virgil-SDK-Http-IResponse 'Virgil.SDK.Http.IResponse')
  - [Body](#P-Virgil-SDK-Http-IResponse-Body 'Virgil.SDK.Http.IResponse.Body')
  - [Headers](#P-Virgil-SDK-Http-IResponse-Headers 'Virgil.SDK.Http.IResponse.Headers')
  - [StatusCode](#P-Virgil-SDK-Http-IResponse-StatusCode 'Virgil.SDK.Http.IResponse.StatusCode')
- [IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache')
  - [GetServiceCard(servicePublicKeyId)](#M-Virgil-SDK-Clients-IServiceKeyCache-GetServiceCard-System-String- 'Virgil.SDK.Clients.IServiceKeyCache.GetServiceCard(System.String)')
- [IVirgilCardsClient](#T-Virgil-SDK-Clients-IVirgilCardsClient 'Virgil.SDK.Clients.IVirgilCardsClient')
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IVirgilCardsClient.Create(System.String,Virgil.SDK.TransferObject.IdentityType,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IVirgilCardsClient.Create(System.String,Virgil.SDK.TransferObject.IdentityType,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IVirgilCardsClient.Create(Virgil.SDK.TransferObject.IndentityTokenDto,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.IVirgilCardsClient.Create(Virgil.SDK.TransferObject.IndentityTokenDto,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [GetApplicationCard(applicationIdentity)](#M-Virgil-SDK-Clients-IVirgilCardsClient-GetApplicationCard-System-String- 'Virgil.SDK.Clients.IVirgilCardsClient.GetApplicationCard(System.String)')
  - [Revoke(publicKeyId,tokens)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}- 'Virgil.SDK.Clients.IVirgilCardsClient.Revoke(System.Guid,System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto})')
  - [Search(value,type,relations,includeUnconfirmed)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Clients.IVirgilCardsClient.Search(System.String,System.Nullable{Virgil.SDK.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IVirgilCardsClient.Trust(System.Guid,System.String,System.Guid,System.Byte[],System.String)')
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.IVirgilCardsClient.Untrust(System.Guid,System.Guid,System.Byte[],System.String)')
- [IVirgilService](#T-Virgil-SDK-Clients-IVirgilService 'Virgil.SDK.Clients.IVirgilService')
- [Localization](#T-Virgil-SDK-Localization 'Virgil.SDK.Localization')
  - [Culture](#P-Virgil-SDK-Localization-Culture 'Virgil.SDK.Localization.Culture')
  - [ExceptionDomainValueDomainIdentityIsInvalid](#P-Virgil-SDK-Localization-ExceptionDomainValueDomainIdentityIsInvalid 'Virgil.SDK.Localization.ExceptionDomainValueDomainIdentityIsInvalid')
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
  - [ResourceManager](#P-Virgil-SDK-Localization-ResourceManager 'Virgil.SDK.Localization.ResourceManager')
- [PrivateKeysClient](#T-Virgil-SDK-Clients-PrivateKeysClient 'Virgil.SDK.Clients.PrivateKeysClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.PrivateKeysClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-System-String,System-String- 'Virgil.SDK.Clients.PrivateKeysClient.#ctor(System.String,System.String)')
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.PrivateKeysClient.Destroy(System.Guid,System.Byte[],System.String)')
  - [Get(virgilCardId,token)](#M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto- 'Virgil.SDK.Clients.PrivateKeysClient.Get(System.Guid,Virgil.SDK.TransferObject.IndentityTokenDto)')
  - [Get(virgilCardId,token,responsePassword)](#M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String- 'Virgil.SDK.Clients.PrivateKeysClient.Get(System.Guid,Virgil.SDK.TransferObject.IndentityTokenDto,System.String)')
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.PrivateKeysClient.Stash(System.Guid,System.Byte[],System.String)')
- [PrivateKeysConnection](#T-Virgil-SDK-Http-PrivateKeysConnection 'Virgil.SDK.Http.PrivateKeysConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Http-PrivateKeysConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Http.PrivateKeysConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.PrivateKeysConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [PublicKeysClient](#T-Virgil-SDK-Clients-PublicKeysClient 'Virgil.SDK.Clients.PublicKeysClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Clients-PublicKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.PublicKeysClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Clients-PublicKeysClient-#ctor-System-String,System-String- 'Virgil.SDK.Clients.PublicKeysClient.#ctor(System.String,System.String)')
  - [Get(publicKeyId)](#M-Virgil-SDK-Clients-PublicKeysClient-Get-System-Guid- 'Virgil.SDK.Clients.PublicKeysClient.Get(System.Guid)')
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#M-Virgil-SDK-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Virgil.SDK.Clients.PublicKeysClient.GetExtended(System.Guid,System.Guid,System.Byte[])')
- [PublicServicesConnection](#T-Virgil-SDK-Http-PublicServicesConnection 'Virgil.SDK.Http.PublicServicesConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Http-PublicServicesConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Http.PublicServicesConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Http.PublicServicesConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [ResponseVerifyClient](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Virgil.SDK.Clients.ResponseVerifyClient')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.ResponseVerifyClient.#ctor(Virgil.SDK.Http.IConnection)')
  - [#ctor(cache,cache)](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Virgil.SDK.Clients.ResponseVerifyClient.#ctor(Virgil.SDK.Http.IConnection,Virgil.SDK.Clients.IServiceKeyCache)')
  - [Send()](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.ResponseVerifyClient.Send(Virgil.SDK.Http.IRequest)')
  - [Send\`\`1()](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest- 'Virgil.SDK.Clients.ResponseVerifyClient.Send``1(Virgil.SDK.Http.IRequest)')
- [ServiceKeyCache](#T-Virgil-SDK-Clients-ServiceKeyCache 'Virgil.SDK.Clients.ServiceKeyCache')
  - [#ctor(virgilCardsClient)](#M-Virgil-SDK-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Clients-IVirgilCardsClient- 'Virgil.SDK.Clients.ServiceKeyCache.#ctor(Virgil.SDK.Clients.IVirgilCardsClient)')
  - [GetServiceCard(servicePublicKeyId)](#M-Virgil-SDK-Clients-ServiceKeyCache-GetServiceCard-System-String- 'Virgil.SDK.Clients.ServiceKeyCache.GetServiceCard(System.String)')
- [VirgilApplicationIds](#T-Virgil-SDK-Clients-VirgilApplicationIds 'Virgil.SDK.Clients.VirgilApplicationIds')
  - [IdentityService](#F-Virgil-SDK-Clients-VirgilApplicationIds-IdentityService 'Virgil.SDK.Clients.VirgilApplicationIds.IdentityService')
  - [PrivateService](#F-Virgil-SDK-Clients-VirgilApplicationIds-PrivateService 'Virgil.SDK.Clients.VirgilApplicationIds.PrivateService')
  - [PublicService](#F-Virgil-SDK-Clients-VirgilApplicationIds-PublicService 'Virgil.SDK.Clients.VirgilApplicationIds.PublicService')
- [VirgilCardsClient](#T-Virgil-SDK-Clients-VirgilCardsClient 'Virgil.SDK.Clients.VirgilCardsClient')
  - [#ctor(connection)](#M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Http-IConnection- 'Virgil.SDK.Clients.VirgilCardsClient.#ctor(Virgil.SDK.Http.IConnection)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-System-String,System-String- 'Virgil.SDK.Clients.VirgilCardsClient.#ctor(System.String,System.String)')
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.VirgilCardsClient.Create(System.String,Virgil.SDK.TransferObject.IdentityType,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.VirgilCardsClient.Create(System.String,Virgil.SDK.TransferObject.IdentityType,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.VirgilCardsClient.Create(Virgil.SDK.TransferObject.IndentityTokenDto,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Clients.VirgilCardsClient.Create(Virgil.SDK.TransferObject.IndentityTokenDto,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [GetApplicationCard(applicationIdentity)](#M-Virgil-SDK-Clients-VirgilCardsClient-GetApplicationCard-System-String- 'Virgil.SDK.Clients.VirgilCardsClient.GetApplicationCard(System.String)')
  - [Revoke(publicKeyId,tokens)](#M-Virgil-SDK-Clients-VirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}- 'Virgil.SDK.Clients.VirgilCardsClient.Revoke(System.Guid,System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto})')
  - [Search(identityValue,identityType,relations,includeUnconfirmed)](#M-Virgil-SDK-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Clients.VirgilCardsClient.Search(System.String,System.Nullable{Virgil.SDK.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.VirgilCardsClient.Trust(System.Guid,System.String,System.Guid,System.Byte[],System.String)')
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Clients.VirgilCardsClient.Untrust(System.Guid,System.Guid,System.Byte[],System.String)')
- [VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilException.#ctor(System.Int32,System.String)')
  - [#ctor(message)](#M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-String- 'Virgil.SDK.Exceptions.VirgilException.#ctor(System.String)')
  - [ErrorCode](#P-Virgil-SDK-Exceptions-VirgilException-ErrorCode 'Virgil.SDK.Exceptions.VirgilException.ErrorCode')
- [VirgilPrivateServicesException](#T-Virgil-SDK-Exceptions-VirgilPrivateServicesException 'Virgil.SDK.Exceptions.VirgilPrivateServicesException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilPrivateServicesException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilPrivateServicesException.#ctor(System.Int32,System.String)')
- [VirgilPublicServicesException](#T-Virgil-SDK-Exceptions-VirgilPublicServicesException 'Virgil.SDK.Exceptions.VirgilPublicServicesException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Exceptions-VirgilPublicServicesException-#ctor-System-Int32,System-String- 'Virgil.SDK.Exceptions.VirgilPublicServicesException.#ctor(System.Int32,System.String)')

<a name='assembly'></a>
# Virgil.SDK [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-Virgil-SDK-Infrastructure-BytesExtensions'></a>
## BytesExtensions [#](#T-Virgil-SDK-Infrastructure-BytesExtensions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Infrastructure

<a name='M-Virgil-SDK-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding-'></a>
### GetBytes(source,encoding) `method` [#](#M-Virgil-SDK-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the byte representation of string in specified encoding.

##### Returns

Byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default |

<a name='M-Virgil-SDK-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding-'></a>
### GetString(source,encoding) `method` [#](#M-Virgil-SDK-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the string of byte array representation.

##### Returns

String representation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default |

<a name='T-Virgil-SDK-Domain-Cards'></a>
## Cards [#](#T-Virgil-SDK-Domain-Cards 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Domain

##### Summary

Domain entity that represents a list of recipients Virgil Cards.

##### See Also

- [Virgil.SDK.Domain.RecipientCard](#T-Virgil-SDK-Domain-RecipientCard 'Virgil.SDK.Domain.RecipientCard')

<a name='M-Virgil-SDK-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Domain-RecipientCard}-'></a>
### #ctor(collection) `constructor` [#](#M-Virgil-SDK-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Domain-RecipientCard}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [Cards](#T-Virgil-SDK-Domain-Cards 'Virgil.SDK.Domain.Cards') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{Virgil.SDK.Domain.RecipientCard}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Virgil.SDK.Domain.RecipientCard}') | The collection. |

<a name='P-Virgil-SDK-Domain-Cards-Count'></a>
### Count `property` [#](#P-Virgil-SDK-Domain-Cards-Count 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the number of elements in the collection.

<a name='M-Virgil-SDK-Domain-Cards-Encrypt-System-Byte[]-'></a>
### Encrypt(data) `method` [#](#M-Virgil-SDK-Domain-Cards-Encrypt-System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the specified data for all recipient cards in the collection.

##### Returns

Encrypted array of bytes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |

<a name='M-Virgil-SDK-Domain-Cards-Encrypt-System-String-'></a>
### Encrypt(text) `method` [#](#M-Virgil-SDK-Domain-Cards-Encrypt-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the specified text for all recipient cards in the collection.

##### Returns

Encrypted text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |

<a name='M-Virgil-SDK-Domain-Cards-GetEnumerator'></a>
### GetEnumerator() `method` [#](#M-Virgil-SDK-Domain-Cards-GetEnumerator 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns an enumerator that iterates through the collection of Virgil Cards.

##### Returns

An enumerator that can be used to iterate through the collection.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Domain-Cards-PrepareSearch-System-String-'></a>
### PrepareSearch(identity) `method` [#](#M-Virgil-SDK-Domain-Cards-PrepareSearch-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes search builder with fluent interface.

##### Returns

The instance of [SearchBuilder](#T-Virgil-SDK-Domain-SearchBuilder 'Virgil.SDK.Domain.SearchBuilder')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents an identity. |

<a name='M-Virgil-SDK-Domain-Cards-Search-Virgil-SDK-Domain-SearchBuilder-'></a>
### Search(builder) `method` [#](#M-Virgil-SDK-Domain-Cards-Search-Virgil-SDK-Domain-SearchBuilder- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the specified builder.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| builder | [Virgil.SDK.Domain.SearchBuilder](#T-Virgil-SDK-Domain-SearchBuilder 'Virgil.SDK.Domain.SearchBuilder') | The builder. |

<a name='M-Virgil-SDK-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(value,type,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards with specified value, and additional parameters.

##### Returns

The collection of found cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents identity value. |
| type | [System.Nullable{Virgil.SDK.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.TransferObject.IdentityType}') | The type of identity. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | The a list of relations. |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Indicates wherever unconfirmed cards should be included to search. |

<a name='M-Virgil-SDK-Domain-Cards-System#Collections#IEnumerable#GetEnumerator'></a>
### System#Collections#IEnumerable#GetEnumerator() `method` [#](#M-Virgil-SDK-Domain-Cards-System#Collections#IEnumerable#GetEnumerator 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns an enumerator that iterates through a collection of recipient Cards.

##### Returns

An [IEnumerator](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.IEnumerator 'System.Collections.IEnumerator') object that can be used to iterate through the collection.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Domain-ConfirmOptions'></a>
## ConfirmOptions [#](#T-Virgil-SDK-Domain-ConfirmOptions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Domain

##### Summary



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

Handles exception resposnses

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

##### Returns



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

<a name='M-Virgil-SDK-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]-'></a>
### VerifyResponse(nativeResponse,publicKey) `method` [#](#M-Virgil-SDK-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Http-IResponse,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the HTTP response with specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nativeResponse | [Virgil.SDK.Http.IResponse](#T-Virgil-SDK-Http-IResponse 'Virgil.SDK.Http.IResponse') | An instance of HTTP response. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A public key to be used for verification. |

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

<a name='T-Virgil-SDK-Clients-IdentityClient'></a>
## IdentityClient [#](#T-Virgil-SDK-Clients-IdentityClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods for validating and authorization a different types of identities.

<a name='M-Virgil-SDK-Clients-IdentityClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-IdentityClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityClient](#T-Virgil-SDK-Clients-IdentityClient 'Virgil.SDK.Clients.IdentityClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The cache. |

<a name='M-Virgil-SDK-Clients-IdentityClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Clients-IdentityClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityClient](#T-Virgil-SDK-Clients-IdentityClient 'Virgil.SDK.Clients.IdentityClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| confirmationCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The count to live. |

<a name='M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String-'></a>
### IsValid(type,value,validationToken) `method` [#](#M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns true if validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The validation token. |

<a name='M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto-'></a>
### IsValid(token) `method` [#](#M-Virgil-SDK-Clients-IdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns true if validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The identity token DTO that represent Identity and it's type. |

<a name='M-Virgil-SDK-Clients-IdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType-'></a>
### Verify(identityValue,type) `method` [#](#M-Virgil-SDK-Clients-IdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Returns

An instance of [IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| type | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

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

Handles exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Exceptions-IdentityServiceException'></a>
## IdentityServiceException [#](#T-Virgil-SDK-Exceptions-IdentityServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary



##### See Also

- [Virgil.SDK.Exceptions.VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityServiceException](#T-Virgil-SDK-Exceptions-IdentityServiceException 'Virgil.SDK.Exceptions.IdentityServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Clients-IIdentityClient'></a>
## IIdentityClient [#](#T-Virgil-SDK-Clients-IIdentityClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Interface that specifies communication with Virgil Security Identity service.

<a name='M-Virgil-SDK-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| confirmationCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The count to live. |

<a name='M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String-'></a>
### IsValid(type,value,validationToken) `method` [#](#M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IdentityType,System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks whether the validation token is valid for specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value that represents validation token for Virgil Identity Service. |

<a name='M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto-'></a>
### IsValid(token) `method` [#](#M-Virgil-SDK-Clients-IIdentityClient-IsValid-Virgil-SDK-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks whether the validation token is valid for specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The identity token DTO that represents validation token and identity information. |

<a name='M-Virgil-SDK-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType-'></a>
### Verify(identityValue,type) `method` [#](#M-Virgil-SDK-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-TransferObject-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Returns

An instance of [IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| type | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Clients.IIdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

<a name='T-Virgil-SDK-TransferObject-IndentityTokenDto'></a>
## IndentityTokenDto [#](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.TransferObject

##### Summary



<a name='P-Virgil-SDK-TransferObject-IndentityTokenDto-Type'></a>
### Type `property` [#](#P-Virgil-SDK-TransferObject-IndentityTokenDto-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the type.

<a name='P-Virgil-SDK-TransferObject-IndentityTokenDto-ValidationToken'></a>
### ValidationToken `property` [#](#P-Virgil-SDK-TransferObject-IndentityTokenDto-ValidationToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the validation token.

<a name='P-Virgil-SDK-TransferObject-IndentityTokenDto-Value'></a>
### Value `property` [#](#P-Virgil-SDK-TransferObject-IndentityTokenDto-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the value.

<a name='T-Virgil-SDK-Clients-IPrivateKeysClient'></a>
## IPrivateKeysClient [#](#T-Virgil-SDK-Clients-IPrivateKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

<a name='M-Virgil-SDK-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String-'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Deletes the private key from service by specified card ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto-'></a>
### Get(virgilCardId,token) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') |  |

##### Remarks

Random password will be generated to encrypt server response

<a name='M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String-'></a>
### Get(virgilCardId,token,responsePassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') |  |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Virgil-SDK-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String-'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Uploads private key to private key store.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Clients-IPublicKeysClient'></a>
## IPublicKeysClient [#](#T-Virgil-SDK-Clients-IPublicKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

<a name='M-Virgil-SDK-Clients-IPublicKeysClient-Get-System-Guid-'></a>
### Get(publicKeyId) `method` [#](#M-Virgil-SDK-Clients-IPublicKeysClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier.

##### Returns

Public key dto

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='M-Virgil-SDK-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]-'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` [#](#M-Virgil-SDK-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier with extended data.

##### Returns

Extended public key dto response

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The virgil card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |

<a name='T-Virgil-SDK-Http-IRequest'></a>
## IRequest [#](#T-Virgil-SDK-Http-IRequest 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

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

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto'), that represents service card.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The service's public key identifier. |

<a name='T-Virgil-SDK-Clients-IVirgilCardsClient'></a>
## IVirgilCardsClient [#](#T-Virgil-SDK-Clients-IVirgilCardsClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

##### See Also

- [Virgil.SDK.Clients.IVirgilService](#T-Virgil-SDK-Clients-IVirgilService 'Virgil.SDK.Clients.IVirgilService')

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identity. |
| identityType | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of virgil card. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The token. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with confirmed identity and specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-GetApplicationCard-System-String-'></a>
### GetApplicationCard(applicationIdentity) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-GetApplicationCard-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the application card.

##### Returns

Virgil card dto [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| applicationIdentity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application identity. |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}-'></a>
### Revoke(publicKeyId,tokens) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Revokes the specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | Id of public key to revoke. |
| tokens | [System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto}') | List of all tokens for this public key. |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(value,type,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. Required. |
| type | [System.Nullable{Virgil.SDK.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.TransferObject.IdentityType}') | The type of identifier. Optional. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between Virgil cards. Optional |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil cards will be included in output. Optional |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String-'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Trusts the specified card by signing the card's Hash attribute.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusting Virgil Card. |
| trustedCardHash | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trusting Virgil Card Hash value. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The signer virgil card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signer private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String-'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Stops trusting the specified card by deleting the sign digest.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusting Virgil Card. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The owner Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Clients-IVirgilService'></a>
## IVirgilService [#](#T-Virgil-SDK-Clients-IVirgilService 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Interface that specifies the Virgil Security service.

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

Looks up a localized string similar to User Data confirmation token invalid.

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

<a name='P-Virgil-SDK-Localization-ResourceManager'></a>
### ResourceManager `property` [#](#P-Virgil-SDK-Localization-ResourceManager 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns the cached ResourceManager instance used by this class.

<a name='T-Virgil-SDK-Clients-PrivateKeysClient'></a>
## PrivateKeysClient [#](#T-Virgil-SDK-Clients-PrivateKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

##### See Also

- [Virgil.SDK.Clients.EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
- [Virgil.SDK.Clients.IPrivateKeysClient](#T-Virgil-SDK-Clients-IPrivateKeysClient 'Virgil.SDK.Clients.IPrivateKeysClient')

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysClient](#T-Virgil-SDK-Clients-PrivateKeysClient 'Virgil.SDK.Clients.PrivateKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The known key provider. |

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysClient](#T-Virgil-SDK-Clients-PrivateKeysClient 'Virgil.SDK.Clients.PrivateKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String-'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Deletes the private key from service by specified card ID.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto-'></a>
### Get(virgilCardId,token) `method` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') |  |

##### Remarks

Random password will be generated to encrypt server response

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String-'></a>
### Get(virgilCardId,token,responsePassword) `method` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-TransferObject-IndentityTokenDto,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | Valid identity token with |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Password to encrypt server response. Up to 31 characters |

<a name='M-Virgil-SDK-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String-'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Uploads private key to private key store.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

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

Handles private keys service exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Clients-PublicKeysClient'></a>
## PublicKeysClient [#](#T-Virgil-SDK-Clients-PublicKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

<a name='M-Virgil-SDK-Clients-PublicKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Clients-PublicKeysClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicKeysClient](#T-Virgil-SDK-Clients-PublicKeysClient 'Virgil.SDK.Clients.PublicKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The service keys cache. |

<a name='M-Virgil-SDK-Clients-PublicKeysClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Clients-PublicKeysClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicKeysClient](#T-Virgil-SDK-Clients-PublicKeysClient 'Virgil.SDK.Clients.PublicKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Clients-PublicKeysClient-Get-System-Guid-'></a>
### Get(publicKeyId) `method` [#](#M-Virgil-SDK-Clients-PublicKeysClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier.

##### Returns

Public key dto

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='M-Virgil-SDK-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]-'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` [#](#M-Virgil-SDK-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier with extended data.

##### Returns

Extended public key dto response

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The virgil card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |

<a name='T-Virgil-SDK-Http-PublicServicesConnection'></a>
## PublicServicesConnection [#](#T-Virgil-SDK-Http-PublicServicesConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Http

##### Summary

A connection for making HTTP requests against URI endpoints for public api services.

##### See Also

- [Virgil.SDK.Http.ConnectionBase](#T-Virgil-SDK-Http-ConnectionBase 'Virgil.SDK.Http.ConnectionBase')
- [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection')

<a name='M-Virgil-SDK-Http-PublicServicesConnection-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Http-PublicServicesConnection-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicServicesConnection](#T-Virgil-SDK-Http-PublicServicesConnection 'Virgil.SDK.Http.PublicServicesConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles public keys service exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

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
### #ctor(cache,cache) `constructor` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-#ctor-Virgil-SDK-Http-IConnection,Virgil-SDK-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ResponseVerifyClient](#T-Virgil-SDK-Clients-ResponseVerifyClient 'Virgil.SDK.Clients.ResponseVerifyClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cache | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache') | The service key cache. |

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest-'></a>
### Send() `method` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP request.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest-'></a>
### Send\`\`1() `method` [#](#M-Virgil-SDK-Clients-ResponseVerifyClient-Send``1-Virgil-SDK-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP POST request. Attempts to map the response body to an object of type `TResult`

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Clients-ServiceKeyCache'></a>
## ServiceKeyCache [#](#T-Virgil-SDK-Clients-ServiceKeyCache 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides cached value of known public key for channel encryption

##### See Also

- [Virgil.SDK.Clients.IServiceKeyCache](#T-Virgil-SDK-Clients-IServiceKeyCache 'Virgil.SDK.Clients.IServiceKeyCache')

<a name='M-Virgil-SDK-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Clients-IVirgilCardsClient-'></a>
### #ctor(virgilCardsClient) `constructor` [#](#M-Virgil-SDK-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Clients-IVirgilCardsClient- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ServiceKeyCache](#T-Virgil-SDK-Clients-ServiceKeyCache 'Virgil.SDK.Clients.ServiceKeyCache') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardsClient | [Virgil.SDK.Clients.IVirgilCardsClient](#T-Virgil-SDK-Clients-IVirgilCardsClient 'Virgil.SDK.Clients.IVirgilCardsClient') | The Virgil cards client. |

<a name='M-Virgil-SDK-Clients-ServiceKeyCache-GetServiceCard-System-String-'></a>
### GetServiceCard(servicePublicKeyId) `method` [#](#M-Virgil-SDK-Clients-ServiceKeyCache-GetServiceCard-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the service's public key by specified identifier.

##### Returns

An instance of [PublicKeyDto](#T-Virgil-SDK-TransferObject-PublicKeyDto 'Virgil.SDK.TransferObject.PublicKeyDto'), that represents Public Key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The service's public key identifier. |

<a name='T-Virgil-SDK-Clients-VirgilApplicationIds'></a>
## VirgilApplicationIds [#](#T-Virgil-SDK-Clients-VirgilApplicationIds 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Holds known Virgil application ids

<a name='F-Virgil-SDK-Clients-VirgilApplicationIds-IdentityService'></a>
### IdentityService `constants` [#](#F-Virgil-SDK-Clients-VirgilApplicationIds-IdentityService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Identity service app id

<a name='F-Virgil-SDK-Clients-VirgilApplicationIds-PrivateService'></a>
### PrivateService `constants` [#](#F-Virgil-SDK-Clients-VirgilApplicationIds-PrivateService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Private key service app id

<a name='F-Virgil-SDK-Clients-VirgilApplicationIds-PublicService'></a>
### PublicService `constants` [#](#F-Virgil-SDK-Clients-VirgilApplicationIds-PublicService 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Public service app id

<a name='T-Virgil-SDK-Clients-VirgilCardsClient'></a>
## VirgilCardsClient [#](#T-Virgil-SDK-Clients-VirgilCardsClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Clients

##### Summary

Provides common methods to interact with Virgil Card resource endpoints.

##### See Also

- [Virgil.SDK.Clients.EndpointClient](#T-Virgil-SDK-Clients-EndpointClient 'Virgil.SDK.Clients.EndpointClient')
- [Virgil.SDK.Clients.IVirgilCardsClient](#T-Virgil-SDK-Clients-IVirgilCardsClient 'Virgil.SDK.Clients.IVirgilCardsClient')

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilCardsClient](#T-Virgil-SDK-Clients-VirgilCardsClient 'Virgil.SDK.Clients.VirgilCardsClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Http.IConnection](#T-Virgil-SDK-Http-IConnection 'Virgil.SDK.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilCardsClient](#T-Virgil-SDK-Clients-VirgilCardsClient 'Virgil.SDK.Clients.VirgilCardsClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.TransferObject.IdentityType](#T-Virgil-SDK-TransferObject-IdentityType 'Virgil.SDK.TransferObject.IdentityType') | The type of identity. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The token DTO object that contains validation token from Identity information. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Create-Virgil-SDK-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with confirmed identity and specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.TransferObject.IndentityTokenDto](#T-Virgil-SDK-TransferObject-IndentityTokenDto 'Virgil.SDK.TransferObject.IndentityTokenDto') | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-GetApplicationCard-System-String-'></a>
### GetApplicationCard(applicationIdentity) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-GetApplicationCard-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the application card.

##### Returns

Virgil card dto [VirgilCardDto](#T-Virgil-SDK-TransferObject-VirgilCardDto 'Virgil.SDK.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| applicationIdentity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application identity. |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}-'></a>
### Revoke(publicKeyId,tokens) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Revoke-System-Guid,System-Collections-Generic-IEnumerable{Virgil-SDK-TransferObject-IndentityTokenDto}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Revokes the specified public key.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | Id of public key to revoke. |
| tokens | [System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Virgil.SDK.TransferObject.IndentityTokenDto}') | List of all tokens for this public key. |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(identityValue,identityType,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. |
| identityType | [System.Nullable{Virgil.SDK.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.TransferObject.IdentityType}') | The type of identifier. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between Virgil cards. Optional |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil cards will be included in output. Optional |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String-'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Trusts the specified card by signing the card's Hash attribute.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusting Virgil Card. |
| trustedCardHash | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trusting Virgil Card Hash value. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The signer virgil card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signer private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-Virgil-SDK-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String-'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Stops trusting the specified card by deleting the sign digest.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusting Virgil Card. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The owner Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Exceptions-VirgilException'></a>
## VirgilException [#](#T-Virgil-SDK-Exceptions-VirgilException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Base exception class for all Virgil Services operations

##### See Also

- [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception')

<a name='M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-String-'></a>
### #ctor(message) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilException-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='P-Virgil-SDK-Exceptions-VirgilException-ErrorCode'></a>
### ErrorCode `property` [#](#P-Virgil-SDK-Exceptions-VirgilException-ErrorCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the error code.

<a name='T-Virgil-SDK-Exceptions-VirgilPrivateServicesException'></a>
## VirgilPrivateServicesException [#](#T-Virgil-SDK-Exceptions-VirgilPrivateServicesException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Private service exception

##### See Also

- [Virgil.SDK.Exceptions.VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Exceptions-VirgilPrivateServicesException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilPrivateServicesException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilPrivateServicesException](#T-Virgil-SDK-Exceptions-VirgilPrivateServicesException 'Virgil.SDK.Exceptions.VirgilPrivateServicesException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Exceptions-VirgilPublicServicesException'></a>
## VirgilPublicServicesException [#](#T-Virgil-SDK-Exceptions-VirgilPublicServicesException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Exceptions

##### Summary

Public service exception

##### See Also

- [Virgil.SDK.Exceptions.VirgilException](#T-Virgil-SDK-Exceptions-VirgilException 'Virgil.SDK.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Exceptions-VirgilPublicServicesException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Exceptions-VirgilPublicServicesException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilPublicServicesException](#T-Virgil-SDK-Exceptions-VirgilPublicServicesException 'Virgil.SDK.Exceptions.VirgilPublicServicesException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |
