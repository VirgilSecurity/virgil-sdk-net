<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [BytesExtensions](#T-Virgil-SDK-Keys-Infrastructure-BytesExtensions 'Virgil.SDK.Keys.Infrastructure.BytesExtensions')
  - [GetBytes(source,encoding)](#M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding- 'Virgil.SDK.Keys.Infrastructure.BytesExtensions.GetBytes(System.String,System.Text.Encoding)')
  - [GetString(source,encoding)](#M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding- 'Virgil.SDK.Keys.Infrastructure.BytesExtensions.GetString(System.Byte[],System.Text.Encoding)')
- [Cards](#T-Virgil-SDK-Keys-Domain-Cards 'Virgil.SDK.Keys.Domain.Cards')
  - [#ctor(collection)](#M-Virgil-SDK-Keys-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Keys-Domain-RecipientCard}- 'Virgil.SDK.Keys.Domain.Cards.#ctor(System.Collections.Generic.IEnumerable{Virgil.SDK.Keys.Domain.RecipientCard})')
  - [Count](#P-Virgil-SDK-Keys-Domain-Cards-Count 'Virgil.SDK.Keys.Domain.Cards.Count')
  - [Encrypt(data)](#M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-Byte[]- 'Virgil.SDK.Keys.Domain.Cards.Encrypt(System.Byte[])')
  - [Encrypt(text)](#M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-String- 'Virgil.SDK.Keys.Domain.Cards.Encrypt(System.String)')
  - [GetEnumerator()](#M-Virgil-SDK-Keys-Domain-Cards-GetEnumerator 'Virgil.SDK.Keys.Domain.Cards.GetEnumerator')
  - [PrepareSearch(identity)](#M-Virgil-SDK-Keys-Domain-Cards-PrepareSearch-System-String- 'Virgil.SDK.Keys.Domain.Cards.PrepareSearch(System.String)')
  - [Search(builder)](#M-Virgil-SDK-Keys-Domain-Cards-Search-Virgil-SDK-Keys-Domain-SearchBuilder- 'Virgil.SDK.Keys.Domain.Cards.Search(Virgil.SDK.Keys.Domain.SearchBuilder)')
  - [Search(value,type,relations,includeUnconfirmed)](#M-Virgil-SDK-Keys-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Keys.Domain.Cards.Search(System.String,System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [System#Collections#IEnumerable#GetEnumerator()](#M-Virgil-SDK-Keys-Domain-Cards-System#Collections#IEnumerable#GetEnumerator 'Virgil.SDK.Keys.Domain.Cards.System#Collections#IEnumerable#GetEnumerator')
- [ConfirmOptions](#T-Virgil-SDK-Keys-Domain-ConfirmOptions 'Virgil.SDK.Keys.Domain.ConfirmOptions')
- [ConnectionBase](#T-Virgil-SDK-Keys-Http-ConnectionBase 'Virgil.SDK.Keys.Http.ConnectionBase')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Keys-Http-ConnectionBase-#ctor-System-String,System-Uri- 'Virgil.SDK.Keys.Http.ConnectionBase.#ctor(System.String,System.Uri)')
  - [AccessTokenHeaderName](#F-Virgil-SDK-Keys-Http-ConnectionBase-AccessTokenHeaderName 'Virgil.SDK.Keys.Http.ConnectionBase.AccessTokenHeaderName')
  - [AccessToken](#P-Virgil-SDK-Keys-Http-ConnectionBase-AccessToken 'Virgil.SDK.Keys.Http.ConnectionBase.AccessToken')
  - [BaseAddress](#P-Virgil-SDK-Keys-Http-ConnectionBase-BaseAddress 'Virgil.SDK.Keys.Http.ConnectionBase.BaseAddress')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Keys-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Keys.Http.ConnectionBase.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
  - [GetNativeRequest(request)](#M-Virgil-SDK-Keys-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Http.ConnectionBase.GetNativeRequest(Virgil.SDK.Keys.Http.IRequest)')
  - [Send(request)](#M-Virgil-SDK-Keys-Http-ConnectionBase-Send-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Http.ConnectionBase.Send(Virgil.SDK.Keys.Http.IRequest)')
- [EndpointClient](#T-Virgil-SDK-Keys-Clients-EndpointClient 'Virgil.SDK.Keys.Clients.EndpointClient')
  - [#ctor(connection)](#M-Virgil-SDK-Keys-Clients-EndpointClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Virgil.SDK.Keys.Clients.EndpointClient.#ctor(Virgil.SDK.Keys.Http.IConnection)')
  - [Send()](#M-Virgil-SDK-Keys-Clients-EndpointClient-Send-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Clients.EndpointClient.Send(Virgil.SDK.Keys.Http.IRequest)')
  - [Send\`\`1()](#M-Virgil-SDK-Keys-Clients-EndpointClient-Send``1-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Clients.EndpointClient.Send``1(Virgil.SDK.Keys.Http.IRequest)')
  - [VerifyResponse(nativeResponse,publicKey)](#M-Virgil-SDK-Keys-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Keys-Http-IResponse,System-Byte[]- 'Virgil.SDK.Keys.Clients.EndpointClient.VerifyResponse(Virgil.SDK.Keys.Http.IResponse,System.Byte[])')
- [Ensure](#T-Virgil-SDK-Keys-Helpers-Ensure 'Virgil.SDK.Keys.Helpers.Ensure')
  - [ArgumentNotNull(value,name)](#M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNull-System-Object,System-String- 'Virgil.SDK.Keys.Helpers.Ensure.ArgumentNotNull(System.Object,System.String)')
  - [ArgumentNotNullOrEmptyString(value,name)](#M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String- 'Virgil.SDK.Keys.Helpers.Ensure.ArgumentNotNullOrEmptyString(System.String,System.String)')
- [IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection')
  - [BaseAddress](#P-Virgil-SDK-Keys-Http-IConnection-BaseAddress 'Virgil.SDK.Keys.Http.IConnection.BaseAddress')
  - [Send(request)](#M-Virgil-SDK-Keys-Http-IConnection-Send-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Http.IConnection.Send(Virgil.SDK.Keys.Http.IRequest)')
- [IdentityClient](#T-Virgil-SDK-Keys-Clients-IdentityClient 'Virgil.SDK.Keys.Clients.IdentityClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache- 'Virgil.SDK.Keys.Clients.IdentityClient.#ctor(Virgil.SDK.Keys.Http.IConnection,Virgil.SDK.Keys.Clients.IServiceKeyCache)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-System-String,System-String- 'Virgil.SDK.Keys.Clients.IdentityClient.#ctor(System.String,System.String)')
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#M-Virgil-SDK-Keys-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Keys.Clients.IdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(type,value,validationToken)](#M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String- 'Virgil.SDK.Keys.Clients.IdentityClient.IsValid(Virgil.SDK.Keys.TransferObject.IdentityType,System.String,System.String)')
  - [IsValid(token)](#M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Virgil.SDK.Keys.Clients.IdentityClient.IsValid(Virgil.SDK.Keys.TransferObject.IndentityTokenDto)')
  - [Send(request)](#M-Virgil-SDK-Keys-Clients-IdentityClient-Send-Virgil-SDK-Keys-Http-IRequest- 'Virgil.SDK.Keys.Clients.IdentityClient.Send(Virgil.SDK.Keys.Http.IRequest)')
  - [Verify(identityValue,type)](#M-Virgil-SDK-Keys-Clients-IdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType- 'Virgil.SDK.Keys.Clients.IdentityClient.Verify(System.String,Virgil.SDK.Keys.TransferObject.IdentityType)')
- [IdentityServiceException](#T-Virgil-SDK-Keys-Exceptions-IdentityServiceException 'Virgil.SDK.Keys.Exceptions.IdentityServiceException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Keys-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String- 'Virgil.SDK.Keys.Exceptions.IdentityServiceException.#ctor(System.Int32,System.String)')
- [IIdentityClient](#T-Virgil-SDK-Keys-Clients-IIdentityClient 'Virgil.SDK.Keys.Clients.IIdentityClient')
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#M-Virgil-SDK-Keys-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Keys.Clients.IIdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)')
  - [IsValid(type,value,validationToken)](#M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String- 'Virgil.SDK.Keys.Clients.IIdentityClient.IsValid(Virgil.SDK.Keys.TransferObject.IdentityType,System.String,System.String)')
  - [IsValid(token)](#M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Virgil.SDK.Keys.Clients.IIdentityClient.IsValid(Virgil.SDK.Keys.TransferObject.IndentityTokenDto)')
  - [Verify(identityValue,type)](#M-Virgil-SDK-Keys-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType- 'Virgil.SDK.Keys.Clients.IIdentityClient.Verify(System.String,Virgil.SDK.Keys.TransferObject.IdentityType)')
- [IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto')
  - [Type](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Type 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto.Type')
  - [ValidationToken](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-ValidationToken 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto.ValidationToken')
  - [Value](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Value 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto.Value')
- [IPrivateKeysClient](#T-Virgil-SDK-Keys-Clients-IPrivateKeysClient 'Virgil.SDK.Keys.Clients.IPrivateKeysClient')
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.IPrivateKeysClient.Destroy(System.Guid,System.Byte[],System.String)')
  - [Get(virgilCardId,token)](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Virgil.SDK.Keys.Clients.IPrivateKeysClient.Get(System.Guid,Virgil.SDK.Keys.TransferObject.IndentityTokenDto)')
  - [Get(virgilCardId,token,responsePassword)](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String- 'Virgil.SDK.Keys.Clients.IPrivateKeysClient.Get(System.Guid,Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.String)')
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.IPrivateKeysClient.Stash(System.Guid,System.Byte[],System.String)')
- [IPublicKeysClient](#T-Virgil-SDK-Keys-Clients-IPublicKeysClient 'Virgil.SDK.Keys.Clients.IPublicKeysClient')
  - [Get(publicKeyId)](#M-Virgil-SDK-Keys-Clients-IPublicKeysClient-Get-System-Guid- 'Virgil.SDK.Keys.Clients.IPublicKeysClient.Get(System.Guid)')
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#M-Virgil-SDK-Keys-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Virgil.SDK.Keys.Clients.IPublicKeysClient.GetExtended(System.Guid,System.Guid,System.Byte[])')
- [IRequest](#T-Virgil-SDK-Keys-Http-IRequest 'Virgil.SDK.Keys.Http.IRequest')
  - [Body](#P-Virgil-SDK-Keys-Http-IRequest-Body 'Virgil.SDK.Keys.Http.IRequest.Body')
  - [Endpoint](#P-Virgil-SDK-Keys-Http-IRequest-Endpoint 'Virgil.SDK.Keys.Http.IRequest.Endpoint')
  - [Headers](#P-Virgil-SDK-Keys-Http-IRequest-Headers 'Virgil.SDK.Keys.Http.IRequest.Headers')
  - [Method](#P-Virgil-SDK-Keys-Http-IRequest-Method 'Virgil.SDK.Keys.Http.IRequest.Method')
- [IResponse](#T-Virgil-SDK-Keys-Http-IResponse 'Virgil.SDK.Keys.Http.IResponse')
  - [Body](#P-Virgil-SDK-Keys-Http-IResponse-Body 'Virgil.SDK.Keys.Http.IResponse.Body')
  - [Headers](#P-Virgil-SDK-Keys-Http-IResponse-Headers 'Virgil.SDK.Keys.Http.IResponse.Headers')
  - [StatusCode](#P-Virgil-SDK-Keys-Http-IResponse-StatusCode 'Virgil.SDK.Keys.Http.IResponse.StatusCode')
- [IServiceKeyCache](#T-Virgil-SDK-Keys-Clients-IServiceKeyCache 'Virgil.SDK.Keys.Clients.IServiceKeyCache')
  - [GetServiceKey(servicePublicKeyId)](#M-Virgil-SDK-Keys-Clients-IServiceKeyCache-GetServiceKey-System-Guid- 'Virgil.SDK.Keys.Clients.IServiceKeyCache.GetServiceKey(System.Guid)')
- [IVirgilCardsClient](#T-Virgil-SDK-Keys-Clients-IVirgilCardsClient 'Virgil.SDK.Keys.Clients.IVirgilCardsClient')
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Create(System.String,Virgil.SDK.Keys.TransferObject.IdentityType,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Create(System.String,Virgil.SDK.Keys.TransferObject.IdentityType,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Create(Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Create(Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Search(value,type,relations,includeUnconfirmed)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Search(System.String,System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Trust(System.Guid,System.String,System.Guid,System.Byte[],System.String)')
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.IVirgilCardsClient.Untrust(System.Guid,System.Guid,System.Byte[],System.String)')
- [IVirgilService](#T-Virgil-SDK-Keys-Clients-IVirgilService 'Virgil.SDK.Keys.Clients.IVirgilService')
- [Localization](#T-Virgil-SDK-Keys-Localization 'Virgil.SDK.Keys.Localization')
  - [Culture](#P-Virgil-SDK-Keys-Localization-Culture 'Virgil.SDK.Keys.Localization.Culture')
  - [ExceptionDomainValueDomainIdentityIsInvalid](#P-Virgil-SDK-Keys-Localization-ExceptionDomainValueDomainIdentityIsInvalid 'Virgil.SDK.Keys.Localization.ExceptionDomainValueDomainIdentityIsInvalid')
  - [ExceptionPublicKeyNotFound](#P-Virgil-SDK-Keys-Localization-ExceptionPublicKeyNotFound 'Virgil.SDK.Keys.Localization.ExceptionPublicKeyNotFound')
  - [ExceptionStringCanNotBeEmpty](#P-Virgil-SDK-Keys-Localization-ExceptionStringCanNotBeEmpty 'Virgil.SDK.Keys.Localization.ExceptionStringCanNotBeEmpty')
  - [ExceptionUserDataAlreadyExists](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataAlreadyExists 'Virgil.SDK.Keys.Localization.ExceptionUserDataAlreadyExists')
  - [ExceptionUserDataClassSpecifiedIsInvalid](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataClassSpecifiedIsInvalid 'Virgil.SDK.Keys.Localization.ExceptionUserDataClassSpecifiedIsInvalid')
  - [ExceptionUserDataConfirmationEntityNotFound](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationEntityNotFound 'Virgil.SDK.Keys.Localization.ExceptionUserDataConfirmationEntityNotFound')
  - [ExceptionUserDataConfirmationTokenInvalid](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationTokenInvalid 'Virgil.SDK.Keys.Localization.ExceptionUserDataConfirmationTokenInvalid')
  - [ExceptionUserDataIntegrityConstraintViolation](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataIntegrityConstraintViolation 'Virgil.SDK.Keys.Localization.ExceptionUserDataIntegrityConstraintViolation')
  - [ExceptionUserDataIsNotConfirmedYet](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataIsNotConfirmedYet 'Virgil.SDK.Keys.Localization.ExceptionUserDataIsNotConfirmedYet')
  - [ExceptionUserDataNotFound](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataNotFound 'Virgil.SDK.Keys.Localization.ExceptionUserDataNotFound')
  - [ExceptionUserDataValueIsRequired](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataValueIsRequired 'Virgil.SDK.Keys.Localization.ExceptionUserDataValueIsRequired')
  - [ExceptionUserDataWasAlreadyConfirmed](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataWasAlreadyConfirmed 'Virgil.SDK.Keys.Localization.ExceptionUserDataWasAlreadyConfirmed')
  - [ExceptionUserIdHadBeenConfirmed](#P-Virgil-SDK-Keys-Localization-ExceptionUserIdHadBeenConfirmed 'Virgil.SDK.Keys.Localization.ExceptionUserIdHadBeenConfirmed')
  - [ExceptionUserInfoDataValidationFailed](#P-Virgil-SDK-Keys-Localization-ExceptionUserInfoDataValidationFailed 'Virgil.SDK.Keys.Localization.ExceptionUserInfoDataValidationFailed')
  - [ResourceManager](#P-Virgil-SDK-Keys-Localization-ResourceManager 'Virgil.SDK.Keys.Localization.ResourceManager')
- [PrivateKeysClient](#T-Virgil-SDK-Keys-Clients-PrivateKeysClient 'Virgil.SDK.Keys.Clients.PrivateKeysClient')
  - [#ctor(connection,cache)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.#ctor(Virgil.SDK.Keys.Http.IConnection,Virgil.SDK.Keys.Clients.IServiceKeyCache)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-System-String,System-String- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.#ctor(System.String,System.String)')
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.Destroy(System.Guid,System.Byte[],System.String)')
  - [Get(virgilCardId,token)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.Get(System.Guid,Virgil.SDK.Keys.TransferObject.IndentityTokenDto)')
  - [Get(virgilCardId,token,responsePassword)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.Get(System.Guid,Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.String)')
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.PrivateKeysClient.Stash(System.Guid,System.Byte[],System.String)')
- [PrivateKeysConnection](#T-Virgil-SDK-Keys-Http-PrivateKeysConnection 'Virgil.SDK.Keys.Http.PrivateKeysConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Keys-Http-PrivateKeysConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Keys.Http.PrivateKeysConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Keys-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Keys.Http.PrivateKeysConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [PublicKeysClient](#T-Virgil-SDK-Keys-Clients-PublicKeysClient 'Virgil.SDK.Keys.Clients.PublicKeysClient')
  - [#ctor(connection)](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Virgil.SDK.Keys.Clients.PublicKeysClient.#ctor(Virgil.SDK.Keys.Http.IConnection)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-System-String,System-String- 'Virgil.SDK.Keys.Clients.PublicKeysClient.#ctor(System.String,System.String)')
  - [Get(publicKeyId)](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-Get-System-Guid- 'Virgil.SDK.Keys.Clients.PublicKeysClient.Get(System.Guid)')
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Virgil.SDK.Keys.Clients.PublicKeysClient.GetExtended(System.Guid,System.Guid,System.Byte[])')
- [PublicServicesConnection](#T-Virgil-SDK-Keys-Http-PublicServicesConnection 'Virgil.SDK.Keys.Http.PublicServicesConnection')
  - [#ctor(accessToken,baseAddress)](#M-Virgil-SDK-Keys-Http-PublicServicesConnection-#ctor-System-String,System-Uri- 'Virgil.SDK.Keys.Http.PublicServicesConnection.#ctor(System.String,System.Uri)')
  - [ExceptionHandler(message)](#M-Virgil-SDK-Keys-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Virgil.SDK.Keys.Http.PublicServicesConnection.ExceptionHandler(System.Net.Http.HttpResponseMessage)')
- [ServiceKeyCache](#T-Virgil-SDK-Keys-Clients-ServiceKeyCache 'Virgil.SDK.Keys.Clients.ServiceKeyCache')
  - [#ctor(publicKeysClient)](#M-Virgil-SDK-Keys-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Keys-Clients-IPublicKeysClient- 'Virgil.SDK.Keys.Clients.ServiceKeyCache.#ctor(Virgil.SDK.Keys.Clients.IPublicKeysClient)')
- [VirgilCardsClient](#T-Virgil-SDK-Keys-Clients-VirgilCardsClient 'Virgil.SDK.Keys.Clients.VirgilCardsClient')
  - [#ctor(connection)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.#ctor(Virgil.SDK.Keys.Http.IConnection)')
  - [#ctor(accessToken,baseUri)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-System-String,System-String- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.#ctor(System.String,System.String)')
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Create(System.String,Virgil.SDK.Keys.TransferObject.IdentityType,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Create(System.String,Virgil.SDK.Keys.TransferObject.IdentityType,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Create(Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.Guid,System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Create(Virgil.SDK.Keys.TransferObject.IndentityTokenDto,System.Byte[],System.Byte[],System.String,System.Collections.Generic.IDictionary{System.Guid,System.String},System.Collections.Generic.IDictionary{System.String,System.String})')
  - [Search(identityValue,identityType,relations,includeUnconfirmed)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Search(System.String,System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType},System.Collections.Generic.IEnumerable{System.Guid},System.Nullable{System.Boolean})')
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Trust(System.Guid,System.String,System.Guid,System.Byte[],System.String)')
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Virgil.SDK.Keys.Clients.VirgilCardsClient.Untrust(System.Guid,System.Guid,System.Byte[],System.String)')
- [VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-Int32,System-String- 'Virgil.SDK.Keys.Exceptions.VirgilException.#ctor(System.Int32,System.String)')
  - [#ctor(message)](#M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-String- 'Virgil.SDK.Keys.Exceptions.VirgilException.#ctor(System.String)')
  - [ErrorCode](#P-Virgil-SDK-Keys-Exceptions-VirgilException-ErrorCode 'Virgil.SDK.Keys.Exceptions.VirgilException.ErrorCode')
- [VirgilPrivateKeysException](#T-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException 'Virgil.SDK.Keys.Exceptions.VirgilPrivateKeysException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException-#ctor-System-Int32,System-String- 'Virgil.SDK.Keys.Exceptions.VirgilPrivateKeysException.#ctor(System.Int32,System.String)')
- [VirgilPublicKeysException](#T-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException 'Virgil.SDK.Keys.Exceptions.VirgilPublicKeysException')
  - [#ctor(errorCode,errorMessage)](#M-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException-#ctor-System-Int32,System-String- 'Virgil.SDK.Keys.Exceptions.VirgilPublicKeysException.#ctor(System.Int32,System.String)')

<a name='assembly'></a>
# Virgil.SDK.Keys [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-Virgil-SDK-Keys-Infrastructure-BytesExtensions'></a>
## BytesExtensions [#](#T-Virgil-SDK-Keys-Infrastructure-BytesExtensions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Infrastructure

<a name='M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding-'></a>
### GetBytes(source,encoding) `method` [#](#M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetBytes-System-String,System-Text-Encoding- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the byte representation of string in specified encoding.

##### Returns

Byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default |

<a name='M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding-'></a>
### GetString(source,encoding) `method` [#](#M-Virgil-SDK-Keys-Infrastructure-BytesExtensions-GetString-System-Byte[],System-Text-Encoding- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the string of byte array representation.

##### Returns

String representation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default |

<a name='T-Virgil-SDK-Keys-Domain-Cards'></a>
## Cards [#](#T-Virgil-SDK-Keys-Domain-Cards 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Domain

##### Summary

Domain entity that represents a list of recipients Virgil Cards.

##### See Also

- [Virgil.SDK.Keys.Domain.RecipientCard](#T-Virgil-SDK-Keys-Domain-RecipientCard 'Virgil.SDK.Keys.Domain.RecipientCard')

<a name='M-Virgil-SDK-Keys-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Keys-Domain-RecipientCard}-'></a>
### #ctor(collection) `constructor` [#](#M-Virgil-SDK-Keys-Domain-Cards-#ctor-System-Collections-Generic-IEnumerable{Virgil-SDK-Keys-Domain-RecipientCard}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [Cards](#T-Virgil-SDK-Keys-Domain-Cards 'Virgil.SDK.Keys.Domain.Cards') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{Virgil.SDK.Keys.Domain.RecipientCard}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Virgil.SDK.Keys.Domain.RecipientCard}') | The collection. |

<a name='P-Virgil-SDK-Keys-Domain-Cards-Count'></a>
### Count `property` [#](#P-Virgil-SDK-Keys-Domain-Cards-Count 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the number of elements in the collection.

<a name='M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-Byte[]-'></a>
### Encrypt(data) `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the specified data for all recipient cards in the collection.

##### Returns

Encrypted array of bytes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |

<a name='M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-String-'></a>
### Encrypt(text) `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-Encrypt-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts the specified text for all recipient cards in the collection.

##### Returns

Encrypted text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |

<a name='M-Virgil-SDK-Keys-Domain-Cards-GetEnumerator'></a>
### GetEnumerator() `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-GetEnumerator 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns an enumerator that iterates through the collection of Virgil Cards.

##### Returns

An enumerator that can be used to iterate through the collection.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Keys-Domain-Cards-PrepareSearch-System-String-'></a>
### PrepareSearch(identity) `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-PrepareSearch-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes search builder with fluent interface.

##### Returns

The instance of [SearchBuilder](#T-Virgil-SDK-Keys-Domain-SearchBuilder 'Virgil.SDK.Keys.Domain.SearchBuilder')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents an identity. |

<a name='M-Virgil-SDK-Keys-Domain-Cards-Search-Virgil-SDK-Keys-Domain-SearchBuilder-'></a>
### Search(builder) `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-Search-Virgil-SDK-Keys-Domain-SearchBuilder- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the specified builder.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| builder | [Virgil.SDK.Keys.Domain.SearchBuilder](#T-Virgil-SDK-Keys-Domain-SearchBuilder 'Virgil.SDK.Keys.Domain.SearchBuilder') | The builder. |

<a name='M-Virgil-SDK-Keys-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(value,type,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards with specified value, and additional parameters.

##### Returns

The collection of found cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents identity value. |
| type | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The type of identity. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | The a list of relations. |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Indicates wherever unconfirmed cards should be included to search. |

<a name='M-Virgil-SDK-Keys-Domain-Cards-System#Collections#IEnumerable#GetEnumerator'></a>
### System#Collections#IEnumerable#GetEnumerator() `method` [#](#M-Virgil-SDK-Keys-Domain-Cards-System#Collections#IEnumerable#GetEnumerator 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns an enumerator that iterates through a collection of recipient Cards.

##### Returns

An [IEnumerator](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.IEnumerator 'System.Collections.IEnumerator') object that can be used to iterate through the collection.

##### Parameters

This method has no parameters.

<a name='T-Virgil-SDK-Keys-Domain-ConfirmOptions'></a>
## ConfirmOptions [#](#T-Virgil-SDK-Keys-Domain-ConfirmOptions 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Domain

##### Summary



<a name='T-Virgil-SDK-Keys-Http-ConnectionBase'></a>
## ConnectionBase [#](#T-Virgil-SDK-Keys-Http-ConnectionBase 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

##### Summary



<a name='M-Virgil-SDK-Keys-Http-ConnectionBase-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Keys-Http-ConnectionBase-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ConnectionBase](#T-Virgil-SDK-Keys-Http-ConnectionBase 'Virgil.SDK.Keys.Http.ConnectionBase') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application token. |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='F-Virgil-SDK-Keys-Http-ConnectionBase-AccessTokenHeaderName'></a>
### AccessTokenHeaderName `constants` [#](#F-Virgil-SDK-Keys-Http-ConnectionBase-AccessTokenHeaderName 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The access token header name

<a name='P-Virgil-SDK-Keys-Http-ConnectionBase-AccessToken'></a>
### AccessToken `property` [#](#P-Virgil-SDK-Keys-Http-ConnectionBase-AccessToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Access Token

<a name='P-Virgil-SDK-Keys-Http-ConnectionBase-BaseAddress'></a>
### BaseAddress `property` [#](#P-Virgil-SDK-Keys-Http-ConnectionBase-BaseAddress 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Base address for the connection.

<a name='M-Virgil-SDK-Keys-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Keys-Http-ConnectionBase-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='M-Virgil-SDK-Keys-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Keys-Http-IRequest-'></a>
### GetNativeRequest(request) `method` [#](#M-Virgil-SDK-Keys-Http-ConnectionBase-GetNativeRequest-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Produces native HTTP request.

##### Returns

HttpRequestMessage

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#T-Virgil-SDK-Keys-Http-IRequest 'Virgil.SDK.Keys.Http.IRequest') | The request. |

<a name='M-Virgil-SDK-Keys-Http-ConnectionBase-Send-Virgil-SDK-Keys-Http-IRequest-'></a>
### Send(request) `method` [#](#M-Virgil-SDK-Keys-Http-ConnectionBase-Send-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends an HTTP request to the API.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#T-Virgil-SDK-Keys-Http-IRequest 'Virgil.SDK.Keys.Http.IRequest') | The HTTP request details. |

<a name='T-Virgil-SDK-Keys-Clients-EndpointClient'></a>
## EndpointClient [#](#T-Virgil-SDK-Keys-Clients-EndpointClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides a base implementation of HTTP client for the Virgil Security services.

<a name='M-Virgil-SDK-Keys-Clients-EndpointClient-#ctor-Virgil-SDK-Keys-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Keys-Clients-EndpointClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [EndpointClient](#T-Virgil-SDK-Keys-Clients-EndpointClient 'Virgil.SDK.Keys.Clients.EndpointClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Keys-Clients-EndpointClient-Send-Virgil-SDK-Keys-Http-IRequest-'></a>
### Send() `method` [#](#M-Virgil-SDK-Keys-Clients-EndpointClient-Send-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP request.

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Keys-Clients-EndpointClient-Send``1-Virgil-SDK-Keys-Http-IRequest-'></a>
### Send\`\`1() `method` [#](#M-Virgil-SDK-Keys-Clients-EndpointClient-Send``1-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP POST request. Attempts to map the response body to an object of type `TResult`

##### Parameters

This method has no parameters.

<a name='M-Virgil-SDK-Keys-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Keys-Http-IResponse,System-Byte[]-'></a>
### VerifyResponse(nativeResponse,publicKey) `method` [#](#M-Virgil-SDK-Keys-Clients-EndpointClient-VerifyResponse-Virgil-SDK-Keys-Http-IResponse,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the HTTP response with specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nativeResponse | [Virgil.SDK.Keys.Http.IResponse](#T-Virgil-SDK-Keys-Http-IResponse 'Virgil.SDK.Keys.Http.IResponse') | An instance of HTTP response. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A public key to be used for verification. |

<a name='T-Virgil-SDK-Keys-Helpers-Ensure'></a>
## Ensure [#](#T-Virgil-SDK-Keys-Helpers-Ensure 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Helpers

##### Summary

Ensure input parameters

<a name='M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNull-System-Object,System-String-'></a>
### ArgumentNotNull(value,name) `method` [#](#M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNull-System-Object,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks an argument to ensure it isn't null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The argument value to check |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument |

<a name='M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String-'></a>
### ArgumentNotNullOrEmptyString(value,name) `method` [#](#M-Virgil-SDK-Keys-Helpers-Ensure-ArgumentNotNullOrEmptyString-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks a string argument to ensure it isn't null or empty.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument value to check |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument |

<a name='T-Virgil-SDK-Keys-Http-IConnection'></a>
## IConnection [#](#T-Virgil-SDK-Keys-Http-IConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for making HTTP requests against URI endpoints.

<a name='P-Virgil-SDK-Keys-Http-IConnection-BaseAddress'></a>
### BaseAddress `property` [#](#P-Virgil-SDK-Keys-Http-IConnection-BaseAddress 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Base address for the connection.

<a name='M-Virgil-SDK-Keys-Http-IConnection-Send-Virgil-SDK-Keys-Http-IRequest-'></a>
### Send(request) `method` [#](#M-Virgil-SDK-Keys-Http-IConnection-Send-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends an HTTP request to the API.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#T-Virgil-SDK-Keys-Http-IRequest 'Virgil.SDK.Keys.Http.IRequest') | The HTTP request details. |

<a name='T-Virgil-SDK-Keys-Clients-IdentityClient'></a>
## IdentityClient [#](#T-Virgil-SDK-Keys-Clients-IdentityClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods for validating and authorization a different types of identities.

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityClient](#T-Virgil-SDK-Keys-Clients-IdentityClient 'Virgil.SDK.Keys.Clients.IdentityClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Keys.Clients.IServiceKeyCache](#T-Virgil-SDK-Keys-Clients-IServiceKeyCache 'Virgil.SDK.Keys.Clients.IServiceKeyCache') | The cache. |

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityClient](#T-Virgil-SDK-Keys-Clients-IdentityClient 'Virgil.SDK.Keys.Clients.IdentityClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String-'></a>
### IsValid(type,value,validationToken) `method` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns true if validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The validation token. |

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-'></a>
### IsValid(token) `method` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns true if validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The identity token DTO that represent Identity and it's type. |

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-Send-Virgil-SDK-Keys-Http-IRequest-'></a>
### Send(request) `method` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-Send-Virgil-SDK-Keys-Http-IRequest- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Performs an asynchronous HTTP request.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#T-Virgil-SDK-Keys-Http-IRequest 'Virgil.SDK.Keys.Http.IRequest') | The instance of request to send. |

<a name='M-Virgil-SDK-Keys-Clients-IdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType-'></a>
### Verify(identityValue,type) `method` [#](#M-Virgil-SDK-Keys-Clients-IdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Returns

An instance of [IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Keys-Clients-IdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Keys.Clients.IdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

<a name='T-Virgil-SDK-Keys-Exceptions-IdentityServiceException'></a>
## IdentityServiceException [#](#T-Virgil-SDK-Keys-Exceptions-IdentityServiceException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary



##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Keys-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Keys-Exceptions-IdentityServiceException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [IdentityServiceException](#T-Virgil-SDK-Keys-Exceptions-IdentityServiceException 'Virgil.SDK.Keys.Exceptions.IdentityServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Keys-Clients-IIdentityClient'></a>
## IIdentityClient [#](#T-Virgil-SDK-Keys-Clients-IIdentityClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Interface that specifies communication with Virgil Security Identity service.

<a name='M-Virgil-SDK-Keys-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32-'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` [#](#M-Virgil-SDK-Keys-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Confirms the identity using confirmation code, that has been generated to confirm an identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| confirmationCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The count to live. |

<a name='M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String-'></a>
### IsValid(type,value,validationToken) `method` [#](#M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IdentityType,System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks whether the validation token is valid for specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value that represents validation token for Virgil Identity Service. |

<a name='M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-'></a>
### IsValid(token) `method` [#](#M-Virgil-SDK-Keys-Clients-IIdentityClient-IsValid-Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Checks whether the validation token is valid for specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The identity token DTO that represents validation token and identity information. |

<a name='M-Virgil-SDK-Keys-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType-'></a>
### Verify(identityValue,type) `method` [#](#M-Virgil-SDK-Keys-Clients-IIdentityClient-Verify-System-String,Virgil-SDK-Keys-TransferObject-IdentityType- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Sends the request for identity verification, that's will be processed depending of specified type.

##### Returns

An instance of [IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An unique string that represents identity. |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |

##### Remarks

Use method [Confirm](#M-Virgil-SDK-Keys-Clients-IIdentityClient-Confirm-System-Guid,System-String,System-Int32,System-Int32- 'Virgil.SDK.Keys.Clients.IIdentityClient.Confirm(System.Guid,System.String,System.Int32,System.Int32)') to confirm and get the indentity token.

<a name='T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto'></a>
## IndentityTokenDto [#](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.TransferObject

##### Summary



<a name='P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Type'></a>
### Type `property` [#](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Type 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the type.

<a name='P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-ValidationToken'></a>
### ValidationToken `property` [#](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-ValidationToken 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the validation token.

<a name='P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Value'></a>
### Value `property` [#](#P-Virgil-SDK-Keys-TransferObject-IndentityTokenDto-Value 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets or sets the value.

<a name='T-Virgil-SDK-Keys-Clients-IPrivateKeysClient'></a>
## IPrivateKeysClient [#](#T-Virgil-SDK-Keys-Clients-IPrivateKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

<a name='M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String-'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Deletes the private key from service by specified card ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto-'></a>
### Get(virgilCardId,token) `method` [#](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') |  |

##### Remarks

Random password will be generated to encrypt server response

<a name='M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String-'></a>
### Get(virgilCardId,token,responsePassword) `method` [#](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') |  |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String-'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-IPrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Uploads private key to private key store.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Keys-Clients-IPublicKeysClient'></a>
## IPublicKeysClient [#](#T-Virgil-SDK-Keys-Clients-IPublicKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

<a name='M-Virgil-SDK-Keys-Clients-IPublicKeysClient-Get-System-Guid-'></a>
### Get(publicKeyId) `method` [#](#M-Virgil-SDK-Keys-Clients-IPublicKeysClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier.

##### Returns

Public key dto

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='M-Virgil-SDK-Keys-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]-'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` [#](#M-Virgil-SDK-Keys-Clients-IPublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='T-Virgil-SDK-Keys-Http-IRequest'></a>
## IRequest [#](#T-Virgil-SDK-Keys-Http-IRequest 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

<a name='P-Virgil-SDK-Keys-Http-IRequest-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Keys-Http-IRequest-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the requests body.

<a name='P-Virgil-SDK-Keys-Http-IRequest-Endpoint'></a>
### Endpoint `property` [#](#P-Virgil-SDK-Keys-Http-IRequest-Endpoint 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the endpoint. Does not include server base address

<a name='P-Virgil-SDK-Keys-Http-IRequest-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Keys-Http-IRequest-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the http headers.

<a name='P-Virgil-SDK-Keys-Http-IRequest-Method'></a>
### Method `property` [#](#P-Virgil-SDK-Keys-Http-IRequest-Method 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the request method.

<a name='T-Virgil-SDK-Keys-Http-IResponse'></a>
## IResponse [#](#T-Virgil-SDK-Keys-Http-IResponse 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

Represents a generic HTTP response

<a name='P-Virgil-SDK-Keys-Http-IResponse-Body'></a>
### Body `property` [#](#P-Virgil-SDK-Keys-Http-IResponse-Body 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Raw response body.

<a name='P-Virgil-SDK-Keys-Http-IResponse-Headers'></a>
### Headers `property` [#](#P-Virgil-SDK-Keys-Http-IResponse-Headers 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Information about the API.

<a name='P-Virgil-SDK-Keys-Http-IResponse-StatusCode'></a>
### StatusCode `property` [#](#P-Virgil-SDK-Keys-Http-IResponse-StatusCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

The response status code.

<a name='T-Virgil-SDK-Keys-Clients-IServiceKeyCache'></a>
## IServiceKeyCache [#](#T-Virgil-SDK-Keys-Clients-IServiceKeyCache 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides cached value of known public key for channel encryption

<a name='M-Virgil-SDK-Keys-Clients-IServiceKeyCache-GetServiceKey-System-Guid-'></a>
### GetServiceKey(servicePublicKeyId) `method` [#](#M-Virgil-SDK-Keys-Clients-IServiceKeyCache-GetServiceKey-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the service's public key by specified identifier.

##### Returns

An instance of [PublicKeyDto](#T-Virgil-SDK-Keys-TransferObject-PublicKeyDto 'Virgil.SDK.Keys.TransferObject.PublicKeyDto'), that represents Public Key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The service's public key identifier. |

<a name='T-Virgil-SDK-Keys-Clients-IVirgilCardsClient'></a>
## IVirgilCardsClient [#](#T-Virgil-SDK-Keys-Clients-IVirgilCardsClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.IVirgilService](#T-Virgil-SDK-Keys-Clients-IVirgilService 'Virgil.SDK.Keys.Clients.IVirgilService')

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identity. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of virgil card. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The token. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with confirmed identity and specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(value,type,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. Required. |
| type | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The type of identifier. Optional. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between Virgil cards. Optional |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil cards will be included in output. Optional |

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String-'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String-'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-IVirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='T-Virgil-SDK-Keys-Clients-IVirgilService'></a>
## IVirgilService [#](#T-Virgil-SDK-Keys-Clients-IVirgilService 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Interface that specifies the Virgil Security service.

<a name='T-Virgil-SDK-Keys-Localization'></a>
## Localization [#](#T-Virgil-SDK-Keys-Localization 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys

##### Summary

A strongly-typed resource class, for looking up localized strings, etc.

<a name='P-Virgil-SDK-Keys-Localization-Culture'></a>
### Culture `property` [#](#P-Virgil-SDK-Keys-Localization-Culture 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionDomainValueDomainIdentityIsInvalid'></a>
### ExceptionDomainValueDomainIdentityIsInvalid `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionDomainValueDomainIdentityIsInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to Domain value specified for the domain identity is invalid.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionPublicKeyNotFound'></a>
### ExceptionPublicKeyNotFound `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionPublicKeyNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to Public Key is not found.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionStringCanNotBeEmpty'></a>
### ExceptionStringCanNotBeEmpty `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionStringCanNotBeEmpty 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to String can not be empty.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataAlreadyExists'></a>
### ExceptionUserDataAlreadyExists `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataAlreadyExists 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User data with same fields is already exists..

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataClassSpecifiedIsInvalid'></a>
### ExceptionUserDataClassSpecifiedIsInvalid `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataClassSpecifiedIsInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data class specified is invalid.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationEntityNotFound'></a>
### ExceptionUserDataConfirmationEntityNotFound `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationEntityNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data confirmation entity not found.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationTokenInvalid'></a>
### ExceptionUserDataConfirmationTokenInvalid `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataConfirmationTokenInvalid 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data confirmation token invalid.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataIntegrityConstraintViolation'></a>
### ExceptionUserDataIntegrityConstraintViolation `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataIntegrityConstraintViolation 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data integrity constraint violation.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataIsNotConfirmedYet'></a>
### ExceptionUserDataIsNotConfirmedYet `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataIsNotConfirmedYet 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to The user data is not confirmed yet.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataNotFound'></a>
### ExceptionUserDataNotFound `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataNotFound 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User data is not found.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataValueIsRequired'></a>
### ExceptionUserDataValueIsRequired `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataValueIsRequired 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to The user data value is required.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserDataWasAlreadyConfirmed'></a>
### ExceptionUserDataWasAlreadyConfirmed `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserDataWasAlreadyConfirmed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User Data was already confirmed and does not need further confirmation.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserIdHadBeenConfirmed'></a>
### ExceptionUserIdHadBeenConfirmed `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserIdHadBeenConfirmed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to This user id had been confirmed earlier.

<a name='P-Virgil-SDK-Keys-Localization-ExceptionUserInfoDataValidationFailed'></a>
### ExceptionUserInfoDataValidationFailed `property` [#](#P-Virgil-SDK-Keys-Localization-ExceptionUserInfoDataValidationFailed 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Looks up a localized string similar to User info data validation failed.

<a name='P-Virgil-SDK-Keys-Localization-ResourceManager'></a>
### ResourceManager `property` [#](#P-Virgil-SDK-Keys-Localization-ResourceManager 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Returns the cached ResourceManager instance used by this class.

<a name='T-Virgil-SDK-Keys-Clients-PrivateKeysClient'></a>
## PrivateKeysClient [#](#T-Virgil-SDK-Keys-Clients-PrivateKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Private Keys resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.EndpointClient](#T-Virgil-SDK-Keys-Clients-EndpointClient 'Virgil.SDK.Keys.Clients.EndpointClient')
- [Virgil.SDK.Keys.Clients.IPrivateKeysClient](#T-Virgil-SDK-Keys-Clients-IPrivateKeysClient 'Virgil.SDK.Keys.Clients.IPrivateKeysClient')

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache-'></a>
### #ctor(connection,cache) `constructor` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection,Virgil-SDK-Keys-Clients-IServiceKeyCache- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysClient](#T-Virgil-SDK-Keys-Clients-PrivateKeysClient 'Virgil.SDK.Keys.Clients.PrivateKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection') | The connection. |
| cache | [Virgil.SDK.Keys.Clients.IServiceKeyCache](#T-Virgil-SDK-Keys-Clients-IServiceKeyCache 'Virgil.SDK.Keys.Clients.IServiceKeyCache') | The known key provider. |

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysClient](#T-Virgil-SDK-Keys-Clients-PrivateKeysClient 'Virgil.SDK.Keys.Clients.PrivateKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String-'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Destroy-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Deletes the private key from service by specified card ID.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto-'></a>
### Get(virgilCardId,token) `method` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') |  |

##### Remarks

Random password will be generated to encrypt server response

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String-'></a>
### Get(virgilCardId,token,responsePassword) `method` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Get-System-Guid,Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Downloads private part of key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') |  |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String-'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-PrivateKeysClient-Stash-System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Uploads private key to private key store.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='T-Virgil-SDK-Keys-Http-PrivateKeysConnection'></a>
## PrivateKeysConnection [#](#T-Virgil-SDK-Keys-Http-PrivateKeysConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for making HTTP requests against URI endpoints for public keys service.

##### See Also

- [Virgil.SDK.Keys.Http.ConnectionBase](#T-Virgil-SDK-Keys-Http-ConnectionBase 'Virgil.SDK.Keys.Http.ConnectionBase')
- [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection')

<a name='M-Virgil-SDK-Keys-Http-PrivateKeysConnection-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Keys-Http-PrivateKeysConnection-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PrivateKeysConnection](#T-Virgil-SDK-Keys-Http-PrivateKeysConnection 'Virgil.SDK.Keys.Http.PrivateKeysConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Keys-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Keys-Http-PrivateKeysConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles private keys service exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Keys-Clients-PublicKeysClient'></a>
## PublicKeysClient [#](#T-Virgil-SDK-Keys-Clients-PublicKeysClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Public Keys resource endpoints.

<a name='M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicKeysClient](#T-Virgil-SDK-Keys-Clients-PublicKeysClient 'Virgil.SDK.Keys.Clients.PublicKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicKeysClient](#T-Virgil-SDK-Keys-Clients-PublicKeysClient 'Virgil.SDK.Keys.Clients.PublicKeysClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Keys-Clients-PublicKeysClient-Get-System-Guid-'></a>
### Get(publicKeyId) `method` [#](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-Get-System-Guid- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the specified public key by it identifier.

##### Returns

Public key dto

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='M-Virgil-SDK-Keys-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]-'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` [#](#M-Virgil-SDK-Keys-Clients-PublicKeysClient-GetExtended-System-Guid,System-Guid,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='T-Virgil-SDK-Keys-Http-PublicServicesConnection'></a>
## PublicServicesConnection [#](#T-Virgil-SDK-Keys-Http-PublicServicesConnection 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for making HTTP requests against URI endpoints for public api services.

##### See Also

- [Virgil.SDK.Keys.Http.ConnectionBase](#T-Virgil-SDK-Keys-Http-ConnectionBase 'Virgil.SDK.Keys.Http.ConnectionBase')
- [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection')

<a name='M-Virgil-SDK-Keys-Http-PublicServicesConnection-#ctor-System-String,System-Uri-'></a>
### #ctor(accessToken,baseAddress) `constructor` [#](#M-Virgil-SDK-Keys-Http-PublicServicesConnection-#ctor-System-String,System-Uri- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [PublicServicesConnection](#T-Virgil-SDK-Keys-Http-PublicServicesConnection 'Virgil.SDK.Keys.Http.PublicServicesConnection') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='M-Virgil-SDK-Keys-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage-'></a>
### ExceptionHandler(message) `method` [#](#M-Virgil-SDK-Keys-Http-PublicServicesConnection-ExceptionHandler-System-Net-Http-HttpResponseMessage- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Handles public keys service exception resposnses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='T-Virgil-SDK-Keys-Clients-ServiceKeyCache'></a>
## ServiceKeyCache [#](#T-Virgil-SDK-Keys-Clients-ServiceKeyCache 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides cached value of known public key for channel ecnryption

##### See Also

- [Virgil.SDK.Keys.Clients.IServiceKeyCache](#T-Virgil-SDK-Keys-Clients-IServiceKeyCache 'Virgil.SDK.Keys.Clients.IServiceKeyCache')

<a name='M-Virgil-SDK-Keys-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Keys-Clients-IPublicKeysClient-'></a>
### #ctor(publicKeysClient) `constructor` [#](#M-Virgil-SDK-Keys-Clients-ServiceKeyCache-#ctor-Virgil-SDK-Keys-Clients-IPublicKeysClient- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [ServiceKeyCache](#T-Virgil-SDK-Keys-Clients-ServiceKeyCache 'Virgil.SDK.Keys.Clients.ServiceKeyCache') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeysClient | [Virgil.SDK.Keys.Clients.IPublicKeysClient](#T-Virgil-SDK-Keys-Clients-IPublicKeysClient 'Virgil.SDK.Keys.Clients.IPublicKeysClient') | The public keys client. |

<a name='T-Virgil-SDK-Keys-Clients-VirgilCardsClient'></a>
## VirgilCardsClient [#](#T-Virgil-SDK-Keys-Clients-VirgilCardsClient 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with Virgil Card resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.EndpointClient](#T-Virgil-SDK-Keys-Clients-EndpointClient 'Virgil.SDK.Keys.Clients.EndpointClient')
- [Virgil.SDK.Keys.Clients.IVirgilCardsClient](#T-Virgil-SDK-Keys-Clients-IVirgilCardsClient 'Virgil.SDK.Keys.Clients.IVirgilCardsClient')

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Keys-Http-IConnection-'></a>
### #ctor(connection) `constructor` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-Virgil-SDK-Keys-Http-IConnection- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilCardsClient](#T-Virgil-SDK-Keys-Clients-VirgilCardsClient 'Virgil.SDK.Keys.Clients.VirgilCardsClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#T-Virgil-SDK-Keys-Http-IConnection 'Virgil.SDK.Keys.Http.IConnection') | The connection. |

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-System-String,System-String-'></a>
### #ctor(accessToken,baseUri) `constructor` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-#ctor-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilCardsClient](#T-Virgil-SDK-Keys-Clients-VirgilCardsClient 'Virgil.SDK.Keys.Clients.VirgilCardsClient') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-System-String,Virgil-SDK-Keys-TransferObject-IdentityType,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the value of identity. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The type of identity. |
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

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Guid,System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card attached to known public key with confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The token DTO object that contains validation token from Identity information. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}-'></a>
### Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Create-Virgil-SDK-Keys-TransferObject-IndentityTokenDto,System-Byte[],System-Byte[],System-String,System-Collections-Generic-IDictionary{System-Guid,System-String},System-Collections-Generic-IDictionary{System-String,System-String}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates a new Virgil Card with confirmed identity and specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#T-Virgil-SDK-Keys-TransferObject-IndentityTokenDto 'Virgil.SDK.Keys.TransferObject.IndentityTokenDto') | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transfered over network |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of hashes of card that need to trust. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}-'></a>
### Search(identityValue,identityType,relations,includeUnconfirmed) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Search-System-String,System-Nullable{Virgil-SDK-Keys-TransferObject-IdentityType},System-Collections-Generic-IEnumerable{System-Guid},System-Nullable{System-Boolean}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Searches the cards by specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value of identifier. |
| identityType | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The type of identifier. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between Virgil cards. Optional |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil cards will be included in output. Optional |

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String-'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Trust-System-Guid,System-String,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

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

<a name='M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String-'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-SDK-Keys-Clients-VirgilCardsClient-Untrust-System-Guid,System-Guid,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

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

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='T-Virgil-SDK-Keys-Exceptions-VirgilException'></a>
## VirgilException [#](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Base exception class for all Virgil Services operations

##### See Also

- [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception')

<a name='M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-String-'></a>
### #ctor(message) `constructor` [#](#M-Virgil-SDK-Keys-Exceptions-VirgilException-#ctor-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='P-Virgil-SDK-Keys-Exceptions-VirgilException-ErrorCode'></a>
### ErrorCode `property` [#](#P-Virgil-SDK-Keys-Exceptions-VirgilException-ErrorCode 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Gets the error code.

<a name='T-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException'></a>
## VirgilPrivateKeysException [#](#T-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Private keys service exception

##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilPrivateKeysException](#T-Virgil-SDK-Keys-Exceptions-VirgilPrivateKeysException 'Virgil.SDK.Keys.Exceptions.VirgilPrivateKeysException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='T-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException'></a>
## VirgilPublicKeysException [#](#T-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Public keys service exception

##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#T-Virgil-SDK-Keys-Exceptions-VirgilException 'Virgil.SDK.Keys.Exceptions.VirgilException')

<a name='M-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException-#ctor-System-Int32,System-String-'></a>
### #ctor(errorCode,errorMessage) `constructor` [#](#M-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException-#ctor-System-Int32,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Initializes a new instance of the [VirgilPublicKeysException](#T-Virgil-SDK-Keys-Exceptions-VirgilPublicKeysException 'Virgil.SDK.Keys.Exceptions.VirgilPublicKeysException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |
