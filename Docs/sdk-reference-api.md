# SDK Reference API 

- [BytesExtensions](#bytesextensions)
  - [GetBytes(source,encoding)](#getbytes(source,encoding))
  - [GetString(source,encoding)](#getstring(source,encoding))
- [Cards](#cards)
  - [#ctor(collection)](##ctor(collection))
  - [Count](#count)
  - [Encrypt(data)](#encrypt(data))
  - [Encrypt(text)](#encrypt(text))
  - [GetEnumerator()](#getenumerator())
  - [PrepareSearch(identity)](#preparesearch(identity))
  - [Search(builder)](#search(builder))
  - [Search(value,type,relations,includeUnconfirmed)](#search(value,type,relations,includeUnconfirmed))
- [ConfirmOptions](#confirmoptions)
- [ConnectionBase](#connectionbase)
  - [#ctor(accessToken,baseAddress)](##ctor(accessToken,baseAddress))
  - [AccessTokenHeaderName](#accesstokenheadername)
  - [AccessToken](#accesstoken)
  - [BaseAddress](#baseaddress)
  - [ExceptionHandler(message)](#exceptionhandler(message))
  - [GetNativeRequest(request)](#getnativerequest(request))
  - [Send(request)](#send(request)connectionbase)
- [EndpointClient](#endpointclient)
  - [#ctor(connection)](##ctor(connection))
  - [Send()](#send())
  - [VerifyResponse(nativeResponse,publicKey)](#verifyresponse(nativeresponse,publickey))
- [Ensure](#ensure)
  - [ArgumentNotNull(value,name)](#argumentnotnull(value,name))
  - [ArgumentNotNullOrEmptyString(value,name)](#argumentnotnulloremptystring(value,name))
- [IConnection](#iconnection)
  - [BaseAddress](#baseaddress)
  - [Send(request)](#send(request)iconnection)
- [IdentityClient](#identityclient)
  - [#ctor(connection,cache)](##ctor(connection,cache))
  - [#ctor(accessToken,baseUri)](##ctor(accesstoken,baseuri))
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#confirm(actionid,confirmationcode,timetolive,counttolive))
  - [IsValid(type,value,validationToken)](#isvalid(type,value,validationtoken))
  - [IsValid(token)](#isvalid(token))
  - [Send(request)](#send(request)identityclient)
  - [Verify(identityValue,type)](#verify(identityvalue,type))
- [IdentityServiceException](#identityserviceexception)
  - [#ctor(errorCode,errorMessage)](##ctor(errorcode,errormessage))
- [IIdentityClient](#iidentityclient)
  - [Confirm(actionId,confirmationCode,timeToLive,countToLive)](#confirm(actionid,confirmationcode,timetolive,counttolive)iidentity)
  - [IsValid(type,value,validationToken)](#isvalid(type,value,validationtoken)iidentity)
  - [IsValid(token)](#isvalid(token)iidentity)
  - [Verify(identityValue,type)](#verify(identityvalue,type)iidentity)
- [IndentityTokenDto](#indentitytokendto)
  - [Type](#type)
  - [ValidationToken](#validationtoken)
  - [Value](#value)
- [IPrivateKeysClient](#iprivatekeysclient)
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#destroy(virgilcardid,privatekey,privatekeypassword))
  - [Get(virgilCardId,token)](#get(virgilcardid,token))
  - [Get(virgilCardId,token,responsePassword)](#get(virgilcardid,token,responsepassword))
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#stash(virgilcardid,privatekey,privatekeypassword))
- [IPublicKeysClient](#ipublickeysclient)
  - [Get(publicKeyId)](#get(publickeyid))
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#getextended(publickeyid,virgilcardid,privatekey))
- [IRequest](#irequest)
  - [Body](#bodyirequest)
  - [Endpoint](#endpoint)
  - [Headers](#headersirequest)
  - [Method](#method)
- [IResponse](#iresponse)
  - [Body](#bodyiresponse)
  - [Headers](#headersiresponse)
  - [StatusCode](#statuscode)
- [IServiceKeyCache](#iservicekeycache)
  - [GetServiceKey(servicePublicKeyId)](#getservicekey(servicepublickeyid))
- [IVirgilCardsClient](#ivirgilcardsclient)
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#create(identityvalue,identitytype,publickeyid,privatekey,privatekeypassword,cardshash,customdata))
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#create(identityvalue,identitytype,publickey,privatekey,privatekeypassword,cardshash,customdata))
  - [Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData)](#create(token,publickeyid,privatekey,privatekeypassword,cardshash,customdata))
  - [Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#create(token,publickey,privatekey,privatekeypassword,cardshash,customdata))
  - [Search(value,type,relations,includeUnconfirmed)](#search(value,type,relations,includeunconfirmed))
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#trust(trustedcardid,trustedcardhash,ownercardid,privatekey,privatekeypassword))
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#untrust(trustedcardid,ownercardid,privatekey,privatekeypassword))
- [IVirgilService](#ivirgilservice)
- [Localization](#localization)
  - [Culture](#culture)
  - [ExceptionDomainValueDomainIdentityIsInvalid](#exceptiondomainvaluedomainidentityisinvalid)
  - [ExceptionPublicKeyNotFound](#exceptionpublickeynotfound)
  - [ExceptionStringCanNotBeEmpty](#exceptionstringcannotbeempty)
  - [ExceptionUserDataAlreadyExists](#exceptionuserdataalreadyexists)
  - [ExceptionUserDataClassSpecifiedIsInvalid](#exceptionuserdataclassspecifiedisinvalid)
  - [ExceptionUserDataConfirmationEntityNotFound](#exceptionuserdataconfirmationentitynotfound)
  - [ExceptionUserDataConfirmationTokenInvalid](#exceptionuserdataconfirmationtokeninvalid)
  - [ExceptionUserDataIntegrityConstraintViolation](#exceptionuserdataintegrityconstraintviolation)
  - [ExceptionUserDataIsNotConfirmedYet](#exceptionuserdataisnotconfirmedyet)
  - [ExceptionUserDataNotFound](#exceptionuserdatanotfound)
  - [ExceptionUserDataValueIsRequired](#exceptionuserdatavalueisrequired)
  - [ExceptionUserDataWasAlreadyConfirmed](#exceptionuserdatawasalreadyconfirmed)
  - [ExceptionUserIdHadBeenConfirmed](#exceptionuseridhadbeenconfirmed)
  - [ExceptionUserInfoDataValidationFailed](#exceptionuserinfodatavalidationfailed)
  - [ResourceManager](#resourcemanager)
- [PrivateKeysClient](#privatekeysclient)
  - [#ctor(connection,cache)](##ctor(connection,cache))
  - [#ctor(accessToken,baseUri)](##ctor(accesstoken,baseuri))
  - [Destroy(virgilCardId,privateKey,privateKeyPassword)](#destroy(virgilcardid,privatekey,privatekeypassword))
  - [Get(virgilCardId,token)](#get(virgilcardid,token))
  - [Get(virgilCardId,token,responsePassword)](#get(virgilcardid,token,responsepassword))
  - [Stash(virgilCardId,privateKey,privateKeyPassword)](#stash(virgilcardid,privatekey,privatekeypassword))
- [PrivateKeysConnection](#privatekeysconnection)
  - [#ctor(accessToken,baseAddress)](##ctor(accesstoken,baseaddress))
  - [ExceptionHandler(message)](#exceptionhandler(message))
- [PublicKeysClient](#publickeysclient)
  - [#ctor(connection)](##ctor(connection))
  - [#ctor(accessToken,baseUri)](##ctor(accesstoken,baseuri))
  - [Get(publicKeyId)](#get(publickeyid))
  - [GetExtended(publicKeyId,virgilCardId,privateKey)](#getextended(publickeyid,virgilcardid,privatekey))
- [PublicServicesConnection](#publicservicesconnection)
  - [#ctor(accessToken,baseAddress)](##ctor(accessToken,baseAddress))
  - [ExceptionHandler(message)](#exceptionhandler(message))
- [ServiceKeyCache](#servicekeycache)
  - [#ctor(publicKeysClient)](##ctor(publicKeysClient))
- [VirgilCardsClient](#virgilcardsclient)
  - [#ctor(connection)](##ctor(connection))
  - [#ctor(accessToken,baseUri)](##ctor(accesstoken,baseuri))
  - [Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#create(identityvalue,identityеype,publickeyid,privateKey,privatekeypassword,cardshashes,customdata))
  - [Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData)](#create(identityvalue,identitytype,publickey,privatekey,privatekeypassword,cardshash,customdata))
  - [Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData)](#create(identitytoken,publickeyid,privatekey,privatekeypassword,cardshashes,customdata))
  - [Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData)](#create(identitytoken,publickey,privatekey,privatekeypassword,cardshashes,customdata))
  - [Search(identityValue,identityType,relations,includeUnconfirmed)](#search(identityvalue,identitytype,relations,includeunconfirmed))
  - [Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword)](#trust(trustedcardid,trustedcardhash,ownercardid,privatekey,privatekeypassword))
  - [Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword)](#untrust(trustedcardid,ownercardid,privatekey,privatekeypassword))
- [VirgilException](#virgilexception)
  - [#ctor(errorCode,errorMessage)](##ctor(errorcode,errormessage))
  - [#ctor(message)](##ctor(message))
  - [ErrorCode](#errorcode)
- [VirgilPrivateKeysException](#virgilprivatekeysexception)
  - [#ctor(errorCode,errorMessage)](##ctor(errorcode,errormessage))
- [VirgilPublicKeysException](#virgilpublickeysexception)
  - [#ctor(errorCode,errorMessage)](##ctor(errorCode,errorMessage))


# Virgil.SDK.Keys 

<a name='bytesextensions'></a>
## BytesExtensions 

##### Namespace

Virgil.SDK.Keys.Infrastructure

<a name='getbytes(source,encoding)'></a>
### GetBytes(source,encoding) `method` 

##### Summary

Gets the byte representation of a string in the specified encoding.

##### Returns

Byte array.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default. |

<a name='getstring(source,encoding)'></a>
### GetString(source,encoding) `method` 

##### Summary

Gets a string in the byte array representation.

##### Returns

String representation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The source. |
| encoding | [System.Text.Encoding](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Text.Encoding 'System.Text.Encoding') | The encoding. Optional. UTF8 is used by default. |

<a name='cards'></a>
## Cards 

##### Namespace

Virgil.SDK.Keys.Domain

##### Summary

Domain entity that represents a list of the Virgil Card recipients.

##### See Also

- [Virgil.SDK.Keys.Domain.RecipientCard](#T-Virgil-SDK-Keys-Domain-RecipientCard 'Virgil.SDK.Keys.Domain.RecipientCard')

<a name='#ctor(collection)'></a>
### #ctor(collection) `constructor` 

##### Summary

Initializes a new instance of the [Cards](#cards) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{Virgil.SDK.Keys.Domain.RecipientCard}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{Virgil.SDK.Keys.Domain.RecipientCard}') | The collection. |

<a name='count'></a>
### Count `property` 

##### Summary

Gets the number of elements in the collection.

<a name='encrypt(data)'></a>
### Encrypt(data) `method` 

##### Summary

Encrypts the specified data for all the recipient Virgil Cards in the collection.

##### Returns

Encrypted array of bytes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |

<a name='encrypt(text)'></a>
### Encrypt(text) `method` 

##### Summary

Encrypts the specified text for all the recipient Virgil Cards in the collection.

##### Returns

Encrypted text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |

<a name='getenumerator()'></a>
### GetEnumerator() `method`

##### Summary

Returns an enumerator that iterates through the collection of Virgil Cards.

##### Returns

An enumerator that can be used to iterate through the collection.

##### Parameters

This method has no parameters.

<a name='preparesearch(identity)'></a>
### PrepareSearch(identity) `method` 

##### Summary

Initializes a search builder with a fluent interface.

##### Returns

The instance of [SearchBuilder](#T-Virgil-SDK-Keys-Domain-SearchBuilder 'Virgil.SDK.Keys.Domain.SearchBuilder')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents an identity. |

<a name='search(builder)'></a>
### Search(builder) `method` 

##### Summary

Searches the specified builder.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| builder | [Virgil.SDK.Keys.Domain.SearchBuilder](#T-Virgil-SDK-Keys-Domain-SearchBuilder 'Virgil.SDK.Keys.Domain.SearchBuilder') | The builder. |

<a name='search(value,type,relations,includeUnconfirmed)'></a>
### Search(value,type,relations,includeUnconfirmed) `method` 

##### Summary

Searches for the Virgil Cards with a specified value and additional parameters.

##### Returns

The collection of found Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents an identity value. |
| type | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The type of identity. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | A list of relations. |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Indicates whether unconfirmed cards should be included to search. |

<a name='confirmoptions'></a>
## ConfirmOptions 

##### Namespace

Virgil.SDK.Keys.Domain

##### Summary

!!!

<a name='connectionbase'></a>
## ConnectionBase 

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

!!!

<a name='#ctor(accessToken,baseAddress)'></a>
### #ctor(accessToken,baseAddress) `constructor` 

##### Summary

Initializes a new instance of the [ConnectionBase](#connectionbase) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The application token. |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='accesstokenheadername'></a>
### AccessTokenHeaderName `constants` 

##### Summary

An access token header name.

<a name='accesstoken'></a>
### AccessToken `property` 

##### Summary

Access Token

<a name='baseaddress'></a>
### BaseAddress `property` 

##### Summary

Base address for the connection.

<a name='exceptionhandler(message)'></a>
### ExceptionHandler(message) `method` 

##### Summary

Handles the exception responses.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The HTTP response message. |

<a name='getnativerequest(request)'></a>
### GetNativeRequest(request) `method` 

##### Summary

Produces a native HTTP request.

##### Returns

HttpRequestMessage

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#irequest) | The request. |

<a name='send(request)connectionbase'></a>
### Send(request) `method` 

##### Summary

Sends an HTTP request to the API.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#irequest) | The HTTP request details. |

<a name='endpointclient'></a>
## EndpointClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides a base implementation of an HTTP client for the Virgil Security services.

<a name='#ctor(connection)'></a>
### #ctor(connection) `constructor` 

##### Summary

Initializes a new instance of the [EndpointClient](#endpointclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#iconnection) | The connection. |

<a name='send()'></a>
### Send() `method` 

##### Summary

Performs an asynchronous HTTP request. Attempts to map the response body to an object of `TResult` type.

##### Parameters

This method has no parameters.

<a name='verifyresponse(nativeresponse,publickey)'></a>
### VerifyResponse(nativeResponse,publicKey) `method` 

##### Summary

Verifies the HTTP response with a specified public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nativeResponse | [Virgil.SDK.Keys.Http.IResponse](#iresponse) | An instance of HTTP response. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A public key to be used for verification. |

<a name='ensure'></a>
## Ensure 

##### Namespace

Virgil.SDK.Keys.Helpers

##### Summary

Ensures the input parameters.

<a name='argumentnotnull(value,name)'></a>
### ArgumentNotNull(value,name) `method` 

##### Summary

Checks an argument to ensure it isn't null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The argument value to check. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument name. |

<a name='argumentnotnulloremptystring(value,name)'></a>
### ArgumentNotNullOrEmptyString(value,name) `method` 

##### Summary

Checks a string argument to ensure it isn't null or empty.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument value to check. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument. |

<a name='iconnection'></a>
## IConnection 

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for creating HTTP requests against URI endpoints.

<a name='baseaddress'></a>
### BaseAddress `property` 

##### Summary

Base address for the connection.

<a name='send(request)iconnection'></a>
### Send(request) `method` 

##### Summary

Sends an HTTP request to the API.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#irequest) | The HTTP request details. |

<a name='identityclient'></a>
## IdentityClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods for validation and authorization of different identity types.

<a name='#ctor(connection,cache)'></a>
### #ctor(connection,cache) `constructor` 

##### Summary

Initializes a new instance of the [IdentityClient](#identityclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#iconnection) | The connection. |
| cache | [Virgil.SDK.Keys.Clients.IServiceKeyCache](#iservicekeycache) | The cache. |

<a name='#ctor(accesstoken,baseuri)'></a>
### #ctor(accessToken,baseUri) `constructor` 

##### Summary

Initializes a new instance of the [IdentityClient](#identityclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='confirm(actionid,confirmationcode,timetolive,counttolive)'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` 

##### Summary

Confirms an identity using the confirmation code that has been generated to confirm the identity.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| confirmationCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Count to live. |

<a name='isvalid(type,value,validationtoken)'></a>
### IsValid(type,value,validationToken) `method` 

##### Summary

Returns true if the validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The validation token. |

<a name='isvalid(token)'></a>
### IsValid(token) `method` 

##### Summary

Returns true if the validation token is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The identity token DTO that represents the identity and its type. |

<a name='send(request)identityclient'></a>
### Send(request) `method` 

##### Summary

Performs an asynchronous HTTP request.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Virgil.SDK.Keys.Http.IRequest](#irequest) | The instance of a request to send. |

<a name='verify(identityvalue,type)'></a>
### Verify(identityValue,type) `method` 

##### Summary

Sends the request for identity verification that will be processed depending on the specified type.

##### Returns

An instance of [IndentityTokenDto](#indentitytokendto) response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A unique string that represents the identity. |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |

##### Remarks

Use method [Confirm](#confirm(actionid,confirmationcode,timetolive,counttolive)) to confirm and get the indentity token.

<a name='identityserviceexception'></a>
## IdentityServiceException 

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

!!!

##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#virgilexception)

<a name='#ctor(errorcode,errormessage)'></a>
### #ctor(errorCode,errorMessage) `constructor` 

##### Summary

Initializes a new instance of the [IdentityServiceException](#T-Virgil-SDK-Keys-Exceptions-IdentityServiceException 'Virgil.SDK.Keys.Exceptions.IdentityServiceException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='iidentityclient'></a>
## IIdentityClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Interface that specifies communication with Virgil Identity Service.

<a name='confirm(actionid,confirmationcode,timetolive,counttolive)iidentity'></a>
### Confirm(actionId,confirmationCode,timeToLive,countToLive) `method` 

##### Summary

Confirms an identity using a confirmation code that has been generated to confirm the identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| actionId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The action identifier. |
| confirmationCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The confirmation code. |
| timeToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Time to live. |
| countToLive | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Сount to live. |

<a name='isvalid(type,value,validationtoken)iidentity'></a>
### IsValid(type,value,validationToken) `method` 

##### Summary

Checks whether the validation token is valid for the specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| validationToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value that represents a validation token for Virgil Identity Service. |

<a name='isvalid(token)iidentity'></a>
### IsValid(token) `method` 

##### Summary

Checks whether the validation token is valid for the specified identity.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The identity token DTO that represents a validation token and identity information. |

<a name='verify(identityvalue,type)iidentity'></a>
### Verify(identityValue,type) `method` 

##### Summary

Sends the request for an identity verification that will be processed depending on the specified type.

##### Returns

An instance of [IndentityTokenDto](#indentitytokendto) response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A unique string that represents an identity. |
| type | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |

##### Remarks

Use method [Confirm](#confirm(actionid,confirmationcode,timetolive,counttolive)) to confirm and get the indentity token.

<a name='identitytokendto'></a>
## IndentityTokenDto 

##### Namespace

Virgil.SDK.Keys.TransferObject

##### Summary

!!!

<a name='type'></a>
### Type `property` 

##### Summary

Gets or sets the type.

<a name='validationtoken'></a>
### ValidationToken `property` 

##### Summary

Gets or sets the validation token.

<a name='value'></a>
### Value `property` 

##### Summary

Gets or sets the value.

<a name='iprivatekeysclient'></a>
## IPrivateKeysClient

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Private Keys resource endpoints.

<a name='destroy(virgilcardid,privatekey,privatekeypassword)'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Deletes the private key from the Private Keys Service by the specified Virgil Card ID.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='get(virgilcardid,token)'></a>
### Get(virgilCardId,token) `method` 

##### Summary

Downloads the private part of the key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | !!! |

##### Remarks

Random password will be generated to encrypt the server response.

<a name='get(virgilcardid,token,responsepassword)'></a>
### Get(virgilCardId,token,responsePassword) `method` 

##### Summary

Downloads the private part of the key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | !!! |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | !!! |

<a name='stash(virgilcardid,privatekey,privatekeypassword)'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Uploads the private key to the private key storage.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='ipublickeysclient'></a>
## IPublicKeysClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Public Keys resource endpoints.

<a name='get(publickeyid)'></a>
### Get(publicKeyId) `method` 

##### Summary

Gets the specified public key by its identifier.

##### Returns

Public key DTO.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='getextended(publickeyid,virgilcardid,privatekey)'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` 

##### Summary

Gets the specified public key by its identifier with an extended data.

##### Returns

Extended public key DTO response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |

<a name='irequest'></a>
## IRequest 

##### Namespace

Virgil.SDK.Keys.Http

<a name='bodyirequest'></a>
### Body `property` 

##### Summary

Gets the request body.

<a name='endpoint'></a>
### Endpoint `property` 

##### Summary

Gets the endpoint. Does not include the server base address.

<a name='headersirequest'></a>
### Headers `property` 

##### Summary

Gets the http headers.

<a name='method'></a>
### Method `property` 

##### Summary

Gets the request method.

<a name='iresponse'></a>
## IResponse 

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

Represents a generic HTTP response.

<a name='bodyiresponse'></a>
### Body `property` 

##### Summary

Raw response body.

<a name='headersiresponse'></a>
### Headers `property` 

##### Summary

Information about the API.

<a name='statuscode'></a>
### StatusCode `property` 

##### Summary

The response status code.

<a name='iservicekeycache'></a>
## IServiceKeyCache 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides a cached value of a known public key for the channel encryption.

<a name='getservicekey(servicepublickeyid)'></a>
### GetServiceKey(servicePublicKeyId) `method` 

##### Summary

Gets the service's public key by the specified identifier.

##### Returns

An instance of [PublicKeyDto](#T-Virgil-SDK-Keys-TransferObject-PublicKeyDto 'Virgil.SDK.Keys.TransferObject.PublicKeyDto') that represents the public key.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| servicePublicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The service's public key identifier. |

<a name='ivirgilcardsclient'></a>
## IVirgilCardsClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Public Keys resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.IVirgilService](#ivirgilservice)

<a name='create(identityvalue,identitytype,publickeyid,privatekey,privatekeypassword,cardshash,customdata)'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` 

##### Summary

Creates a new Virgil Card attached to a known public key with an unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents an identity value. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='create(identityvalue,identitytype,publickey,privatekey,privatekeypassword,cardshash,customdata)'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` 

##### Summary

Creates a new Virgil Card with an unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identity value. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type of a Virgil Card. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Remarks

This card will not be searchable by default.

<a name='create(token,publickeyid,privatekey,privatekeypassword,cardshash,customdata)'></a>
### Create(token,publicKeyId,privateKey,privateKeyPassword,cardsHash,customData) `method` 

##### Summary

Creates a new Virgil Card attached to a known public key with a confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The token. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce sign. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='create(token,publickey,privatekey,privatekeypassword,cardshash,customdata)'></a>
### Create(token,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` 

##### Summary

Creates a new Virgil Card with a confirmed identity and a specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='search(value,type,relations,includeunconfirmed)'></a>
### Search(value,type,relations,includeUnconfirmed) `method` 

##### Summary

Searches for Virgil Cards by a specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identifier value. Required. |
| type | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The identifier type. Optional. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between the Virgil Cards. Optional |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil Cards will be included into the output. Optional |

<a name='trust(trustedcardid,trustedcardhash,ownercardid,privatekey,privatekeypassword)'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Trusts the specified Virgil Card by signing its Hash attribute.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusted Virgil Card. |
| trustedCardHash | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trusted Virgil Card Hash value. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The signer of the Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signer's private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='untrust(trustedcardid,ownercardid,privatekey,privatekeypassword)'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Stops trusting the specified Virgil Card by deleting the sign digest.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusted Virgil Card. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The owner of Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='ivirgilservice'></a>
## IVirgilService 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Interface that specifies the Virgil Security service.

<a name='localization'></a>
## Localization 

##### Namespace

Virgil.SDK.Keys

##### Summary

A strongly-typed resource class for looking up the localized strings, etc.

<a name='culture'></a>
### Culture `property` 

##### Summary

Overrides the current thread's CurrentUICulture property for all the resource lookups using this strongly typed resource class.

<a name='exceptiondomainvaluedomainidentityisinvalid'></a>
### ExceptionDomainValueDomainIdentityIsInvalid `property` 

##### Summary (???)

Looks up a localized string similar to Domain value specified for the domain identity is invalid.

<a name='exceptionpublickeynotfound'></a>
### ExceptionPublicKeyNotFound `property` 

##### Summary (???)

Looks up a localized string similar to Public Key is not found.

<a name='exceptionstringcannotbeempty'></a>
### ExceptionStringCanNotBeEmpty `property`

##### Summary (???)

Looks up a localized string similar to String can not be empty.

<a name='exceptionuserdataalreadyexists'></a>
### ExceptionUserDataAlreadyExists `property` 

##### Summary (???)

Looks up a localized string similar to User data with the same fields is already exists.

<a name='exceptionuserdataclassspecifiedisinvalid'></a>
### ExceptionUserDataClassSpecifiedIsInvalid `property` 

##### Summary (???)

Looks up a localized string similar to the User Data class specified is invalid.

<a name='exceptionuserdataconfirmationentitynotfound'></a>
### ExceptionUserDataConfirmationEntityNotFound `property` 

##### Summary (???)

Looks up a localized string similar to the User Data confirmation entity not found.

<a name='exceptionuserdataconfirmationtokeninvalid'></a>
### ExceptionUserDataConfirmationTokenInvalid `property` 

##### Summary (???)

Looks up a localized string similar to the User Data confirmation token invalid.

<a name='exceptionuserdataintegrityconstraintviolation'></a>
### ExceptionUserDataIntegrityConstraintViolation `property` 

##### Summary (???)

Looks up a localized string similar to the User Data integrity constraint violation.

<a name='exceptionuserdataisnotconfirmedyet'></a>
### ExceptionUserDataIsNotConfirmedYet `property` 

##### Summary (???)

Looks up a localized string similar to the user data is not confirmed yet.

<a name='exceptionuserdatanotfound'></a>
### ExceptionUserDataNotFound `property` 

##### Summary (???)

Looks up a localized string similar to the User data is not found.

<a name='exceptionuserdatavalueisrequired'></a>
### ExceptionUserDataValueIsRequired `property` 

##### Summary

Looks up a localized string similar to the user data value is required.

<a name='exceptionuserdatawasalreadyconfirmed'></a>
### ExceptionUserDataWasAlreadyConfirmed `property` 

##### Summary (???)

Looks up a localized string similar to the User Data was already confirmed and does not need further confirmation.

<a name='exceptionuseridhadbeenconfirmed'></a>
### ExceptionUserIdHadBeenConfirmed `property` 

##### Summary (???)
 
Looks up a localized string similar to this user id had been confirmed earlier.

<a name='exceptionuserinfodatavalidationfailed'></a>
### ExceptionUserInfoDataValidationFailed `property` 

##### Summary (???)

Looks up a localized string similar to the User info data validation failed.

<a name='resourcemanager'></a>
### ResourceManager `property` 

##### Summary

Returns the cached ResourceManager instance used by this class.

<a name='privatekeysclient'></a>
## PrivateKeysClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Private Keys resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.EndpointClient](#endpointclient)
- [Virgil.SDK.Keys.Clients.IPrivateKeysClient](#iprivatekeysclient)

<a name='#ctor(connection,cache)'></a>
### #ctor(connection,cache) `constructor` 

##### Summary

Initializes a new instance of the [PrivateKeysClient](#privatekeysclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#iconnection) | The connection. |
| cache | [Virgil.SDK.Keys.Clients.IServiceKeyCache](#iservicekeycache) | The known key provider. |

<a name='#ctor(accesstoken,baseuri)'></a>
### #ctor(accessToken,baseUri) `constructor` 

##### Summary

Initializes a new instance of the [PrivateKeysClient](#privatekeysclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='destroy(virgilcardid,privatekey,privatekeypassword)'></a>
### Destroy(virgilCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Deletes the private key from the service by a specified card ID.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='get(virgilcardid,token)'></a>
### Get(virgilCardId,token) `method` 

##### Summary

Downloads the private part of the key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | !!! |

##### Remarks

Random password will be generated to encrypt the server response.

<a name='get(virgilcardid,token,responsepassword)'></a>
### Get(virgilCardId,token,responsePassword) `method` 

##### Summary

Downloads the private part of the key by its public id.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| token | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | !!! |
| responsePassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | !!! |

<a name='stash(virgilcardid,privatekey,privatekeypassword)'></a>
### Stash(virgilCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Uploads the private key to the Private Key storage.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key value. Private key is used to produce the signature. It is not transfered over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

<a name='privatekeysconnection'></a>
## PrivateKeysConnection 

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for making HTTP requests against URI endpoints for the Public Keys Service.

##### See Also

- [Virgil.SDK.Keys.Http.ConnectionBase](#connectionbase)
- [Virgil.SDK.Keys.Http.IConnection](#iconnection)

<a name='#ctor(accesstoken,baseaddress)'></a>
### #ctor(accessToken,baseAddress) `constructor` 

##### Summary

Initializes a new instance of the [PrivateKeysConnection](#privatekeysconnection) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token. |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='exceptionhandler(message)'></a>
### ExceptionHandler(message) `method` 

##### Summary

Handles the Private Keys service exception resposnses.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='publickeysclient'></a>
## PublicKeysClient 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Public Keys resource endpoints.

<a name='#ctor(connection)'></a>
### #ctor(connection) `constructor` 

##### Summary

Initializes a new instance of the [PublicKeysClient](#publickeysclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#iconnection) | The connection. |

<a name='#ctor(accesstoken,baseuri)'></a>
### #ctor(accessToken,baseUri) `constructor` 

##### Summary

Initializes a new instance of the [PublicKeysClient](#publickeysclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='get(publickeyid)'></a>
### Get(publicKeyId) `method` 

##### Summary

Gets the specified public key by its identifier.

##### Returns

Public key DTO.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |

<a name='getextended(publickeyid,virgilcardid,privatekey)'></a>
### GetExtended(publicKeyId,virgilCardId,privateKey) `method` 

##### Summary

Gets the specified public key by its identifier with an extended data.

##### Returns

Extended public key DTO response.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| virgilCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |

<a name='publicservicesconnection'></a>
## PublicServicesConnection

##### Namespace

Virgil.SDK.Keys.Http

##### Summary

A connection for creating HTTP requests against the URI endpoints for the public API services.

##### See Also

- [Virgil.SDK.Keys.Http.ConnectionBase](#connectionbase')
- [Virgil.SDK.Keys.Http.IConnection](#iconnection)

<a name='#ctor(accessToken,baseAddress)'></a>
### #ctor(accessToken,baseAddress) `constructor` 

##### Summary

Initializes a new instance of the [PublicServicesConnection](#publicservicesconnection) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Application token. |
| baseAddress | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | The base address. |

<a name='exceptionhandler(message)'></a>
### ExceptionHandler(message) `method`

##### Summary

Handles the Public Keys Service exception resposnses.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The http response message. |

<a name='servicekeycache'></a>
## ServiceKeyCache 

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides cached value of a known public key for the channel ecnryption.

##### See Also

- [Virgil.SDK.Keys.Clients.IServiceKeyCache](#iservicekeycache)

<a name='#ctor(publicKeysClient)'></a>
### #ctor(publicKeysClient) `constructor`

##### Summary

Initializes a new instance of the [ServiceKeyCache](#servicekeycache) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKeysClient | [Virgil.SDK.Keys.Clients.IPublicKeysClient](#T-Virgil-SDK-Keys-Clients-IPublicKeysClient 'Virgil.SDK.Keys.Clients.IPublicKeysClient') | The public keys client. |

<a name='virgilcardsclient'></a>
## VirgilCardsClient

##### Namespace

Virgil.SDK.Keys.Clients

##### Summary

Provides common methods to interact with the Virgil Card resource endpoints.

##### See Also

- [Virgil.SDK.Keys.Clients.EndpointClient](#endpointclient)
- [Virgil.SDK.Keys.Clients.IVirgilCardsClient](#ivirgilcardsclient)

<a name='#ctor(connection)'></a>
### #ctor(connection) `constructor` 

##### Summary

Initializes a new instance of the [VirgilCardsClient](#virgilcardsclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connection | [Virgil.SDK.Keys.Http.IConnection](#iconnection) | The connection. |

<a name='#ctor(accesstoken,baseuri)'></a>
### #ctor(accessToken,baseUri) `constructor`

##### Summary

Initializes a new instance of the [VirgilCardsClient](#virgilcardsclient) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| accessToken | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The access token. |
| baseUri | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base URI. |

<a name='create(identityvalue,identityеype,publickeyid,privateKey,privatekeypassword,cardshashes,customdata)'></a>
### Create(identityValue,identityType,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method`

##### Summary

Creates a new Virgil Card attached to a known public key with an unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the identity value. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The collection of custom user information. |

##### Remarks

This card will not be searchable by default.

<a name='create(identityvalue,identitytype,publickey,privatekey,privatekeypassword,cardshash,customdata)'></a>
### Create(identityValue,identityType,publicKey,privateKey,privateKeyPassword,cardsHash,customData) `method` 

##### Summary

Creates a new Virgil Card with an unconfirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string that represents the identity value. |
| identityType | [Virgil.SDK.Keys.TransferObject.IdentityType](#T-Virgil-SDK-Keys-TransferObject-IdentityType 'Virgil.SDK.Keys.TransferObject.IdentityType') | The identity type. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHash | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') | !!! |

##### Remarks

This card will not be searchable by default.

<a name='create(identitytoken,publickeyid,privatekey,privatekeypassword,cardshashes,customdata)'></a>
### Create(identityToken,publicKeyId,privateKey,privateKeyPassword,cardsHashes,customData) `method`

##### Summary

Creates a new Virgil Card attached to a known public key with a confirmed identity.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The token DTO object that contains a validation token from the Identity information. |
| publicKeyId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The public key identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') | !!! |

<a name='create(identitytoken,publickey,privatekey,privatekeypassword,cardshashes,customdata)'></a>
### Create(identityToken,publicKey,privateKey,privateKeyPassword,cardsHashes,customData) `method` 

##### Summary

Creates a new Virgil Card with a confirmed identity and a specified public key.

##### Returns

An instance of [VirgilCardDto](#T-Virgil-SDK-Keys-TransferObject-VirgilCardDto 'Virgil.SDK.Keys.TransferObject.VirgilCardDto').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityToken | [Virgil.SDK.Keys.TransferObject.IndentityTokenDto](#indentitytokendto) | The token. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |
| cardsHashes | [System.Collections.Generic.IDictionary{System.Guid,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.Guid,System.String}') | The collection of the Virgil Card hashes that should be trusted. |
| customData | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | The custom data. |

<a name='search(identityvalue,identitytype,relations,includeunconfirmed)'></a>
### Search(identityValue,identityType,relations,includeUnconfirmed) `method` 

##### Summary

Searches for Virgil Cards by a specified criteria.

##### Returns

The collection of Virgil Cards.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identityValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The identifier value. |
| identityType | [System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{Virgil.SDK.Keys.TransferObject.IdentityType}') | The identifier type. |
| relations | [System.Collections.Generic.IEnumerable{System.Guid}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Guid}') | Relations between the Virgil Cards. Optional. |
| includeUnconfirmed | [System.Nullable{System.Boolean}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Boolean}') | Unconfirmed Virgil Cards will be included into the output. Optional. |

<a name='trust(trustedcardid,trustedcardhash,ownercardid,privatekey,privatekeypassword)'></a>
### Trust(trustedCardId,trustedCardHash,ownerCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Trusts the specified Virgil Card by signing its Hash attribute.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusted Virgil Card. |
| trustedCardHash | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trusted Virgil Card Hash value. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The Virgil Card signer identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key signer. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') | !!! |

<a name='untrust(trustedcardid,ownercardid,privatekey,privatekeypassword)'></a>
### Untrust(trustedCardId,ownerCardId,privateKey,privateKeyPassword) `method` 

##### Summary

Stops trusting the specified Virgil Card by deleting the sign digest.

##### Returns

!!!

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| trustedCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The trusted Virgil Card. |
| ownerCardId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') | The owner of the Virgil Card identifier. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key. Private key is used to produce the signature. It is not transferred over the network. |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') | !!! |

<a name='virgilexception'></a>
## VirgilException 

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Base exception class for all the Virgil Services operations.

##### See Also

- [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception')

<a name='#ctor(errorcode,errormessage)'></a>
### #ctor(errorCode,errorMessage) `constructor`

##### Summary

Initializes a new instance of the [VirgilException](#virgilexception) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='#ctor(message)'></a>
### #ctor(message) `constructor` 

##### Summary

Initializes a new instance of the [VirgilException](#virgilexception) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='errorcode'></a>
### ErrorCode `property` 

##### Summary

Gets the error code.

<a name='virgilprivatekeysexception'></a>
## VirgilPrivateKeysException 

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Private Keys Service exceptions.

##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#virgilexception)

<a name='#ctor(errorcode,errormessage)'></a>
### #ctor(errorCode,errorMessage) `constructor` 

##### Summary

Initializes a new instance of the [VirgilPrivateKeysException](#virgilprivatekeysexception) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |

<a name='virgilpublickeysexception'></a>
## VirgilPublicKeysException 

##### Namespace

Virgil.SDK.Keys.Exceptions

##### Summary

Public Keys Service exceptions.

##### See Also

- [Virgil.SDK.Keys.Exceptions.VirgilException](#virgilexception)

<a name='#ctor(errorCode,errorMessage)'></a>
### #ctor(errorCode,errorMessage) `constructor` 

##### Summary

Initializes a new instance of the [VirgilPublicKeysException](#virgilpublickeysexception) class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The error code. |
| errorMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message. |
