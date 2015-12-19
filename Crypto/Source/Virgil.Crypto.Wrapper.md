# Reference API Crypto Library

# Methods [#](#contents 'Go To Here')

- [CryptoHelper](#T-Virgil-Crypto-CryptoHelper 'Virgil.Crypto.CryptoHelper')
  - [Decrypt Text With Key](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.String,System.String,System.Byte[],System.String)')
  - [Decrypt Data With Key](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.Byte[],System.String,System.Byte[],System.String)')
  - [Decrypt Text With Password](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.String,System.String)')
  - [Decrypt Data With Password](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.Byte[],System.String)')
  - [Encrypt Text With Key](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.String,System.Byte[])')
  - [Encrypt Text for Multiple Recipients](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.Collections.Generic.IDictionary{System.String,System.Byte[]})')
  - [Encrypt Data for Multiple Recipients](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Virgil.Crypto.CryptoHelper.Encrypt(System.Byte[],System.Collections.Generic.IDictionary{System.String,System.Byte[]})')
  - [Encrypt Text With Password](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.String)')
  - [Encrypt Data With Password](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Encrypt(System.Byte[],System.String)')
  - [Generate Public/Private Key Pair](#M-Virgil-Crypto-CryptoHelper-GenerateKeyPair-System-String- 'Virgil.Crypto.CryptoHelper.GenerateKeyPair(System.String)')
  - [Sign Text](#M-Virgil-Crypto-CryptoHelper-Sign-System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Sign(System.String,System.Byte[],System.String)')
  - [Sign Data](#M-Virgil-Crypto-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Sign(System.Byte[],System.Byte[],System.String)')
  - [Verify Text](#M-Virgil-Crypto-CryptoHelper-Verify-System-String,System-String,System-Byte[]- 'Virgil.Crypto.CryptoHelper.Verify(System.String,System.String,System.Byte[])')
  - [Verify Data](#M-Virgil-Crypto-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Virgil.Crypto.CryptoHelper.Verify(System.Byte[],System.Byte[],System.Byte[])')

<a name='T-Virgil-Crypto-CryptoHelper'></a>
## CryptoHelper [#](#T-Virgil-Crypto-CryptoHelper 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.Crypto

##### Summary

Performs cryptographic operations like encryption and decryption using the Virgil Security Crypto Library.

<a name='M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String-'></a>
### Decrypt(cipherTextBase64,recipientId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts data that was previously encrypted with `Public Key`.

##### Returns

The decrypted data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherTextBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to decrypt in Base64 format. |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The unique recipient ID, that identifies a Public Key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key`'s password. |

##### Remarks

This method decrypts a data that is ecrypted using the [Encrypt](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.String,System.Byte[])') method.

<a name='M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String-'></a>
### Decrypt(cipherData,recipientId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts data that was previously encrypted with `Public Key`.

##### Returns

The decrypted data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to decrypt. |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The unique recipient ID, that identifies a Public Key. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key`'s password |

##### Remarks

This method decrypts a data that is ecrypted using the [Encrypt](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.String,System.Byte[])') method.

<a name='M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String-'></a>
### Decrypt(cipherTextBase64,password) `method` [#](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts data that was previously encrypted with specified password.

##### Returns

The decrypted text.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherTextBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to decrypt in Base64 format. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that was used to encrypt specified data. |

##### Remarks

This method decrypts a data that is ecrypted using the [Encrypt](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String- 'Virgil.Crypto.CryptoHelper.Encrypt(System.String,System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String-'></a>
### Decrypt(cipherData,password) `method` [#](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts data that was previously encrypted with specified password.

##### Returns

The decrypted data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to decrypt. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that was used to encrypt specified data. |

##### Remarks

This method decrypts a data that is ecrypted using the [Encrypt](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Encrypt(System.Byte[],System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]-'></a>
### Encrypt(text,recipientId,recipientPublicKey) `method` [#](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text for the specified recipient's public key.

##### Returns

The encrypted text in Base64 format.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The unique recipient ID, that identifies a Public Key |
| recipientPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Public Key` |

##### Remarks

This method encrypts a data that is decrypted using the [Decrypt](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.String,System.String,System.Byte[],System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}-'></a>
### Encrypt(text,recipients) `method` [#](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text for the specified dictionary of recipients.

##### Returns

The encrypted text in Base64 format.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |
| recipients | [System.Collections.Generic.IDictionary{System.String,System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.Byte[]}') | The dictionary of recipients Public Keys |

##### Remarks

This method encrypts a data that is decrypted using the [Decrypt](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.String,System.String,System.Byte[],System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}-'></a>
### Encrypt(data,recipients) `method` [#](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts data for the specified dictionary of recipients.

##### Returns

The encrypted data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |
| recipients | [System.Collections.Generic.IDictionary{System.String,System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.Byte[]}') | The dictionary of recipients Public Keys |

##### Remarks

This method encrypts a data that is decrypted using the [Decrypt](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.Byte[],System.String,System.Byte[],System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String-'></a>
### Encrypt(text,password) `method` [#](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text with the specified password.

##### Returns

The encrypted text in Base64 format.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that uses to encrypt specified data. |

##### Remarks

This method encrypts a text that is decrypted using the [Decrypt](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-String,System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.String,System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-String-'></a>
### Encrypt(data,password) `method` [#](#M-Virgil-Crypto-CryptoHelper-Encrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts data with the specified password.

##### Returns

The encrypted data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted. |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password that uses to encrypt specified data. |

##### Remarks

This method encrypts a data that is decrypted using the [Decrypt](#M-Virgil-Crypto-CryptoHelper-Decrypt-System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Decrypt(System.Byte[],System.String)') method.

<a name='M-Virgil-Crypto-CryptoHelper-GenerateKeyPair-System-String-'></a>
### GenerateKeyPair(password) `method` [#](#M-Virgil-Crypto-CryptoHelper-GenerateKeyPair-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates a random Public/Private key pair to be used for encryption and decryption.

##### Returns

The generated instance of [VirgilKeyPair](#T-Virgil-Crypto-VirgilKeyPair 'Virgil.Crypto.VirgilKeyPair') key pair.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key` password |

<a name='M-Virgil-Crypto-CryptoHelper-Sign-System-String,System-Byte[],System-String-'></a>
### Sign(text,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-CryptoHelper-Sign-System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Computes the hash value of the specified string and signs the resulting hash value.

##### Returns

The digital signature in Base64 format for the specified data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The input text for which to compute the hash. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key` password |

##### Remarks

This method creates a digital signature that is verified using the [Verify](#M-Virgil-Crypto-CryptoHelper-Verify-System-String,System-String,System-Byte[]- 'Virgil.Crypto.CryptoHelper.Verify(System.String,System.String,System.Byte[])') method.

<a name='M-Virgil-Crypto-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String-'></a>
### Sign(data,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Computes the hash value of the specified byte array and signs the resulting hash value.

##### Returns

The digital signature for the specified data.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The input data for which to compute the hash. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Private Key` |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The `Private Key` password |

##### Remarks

This method creates a digital signature that is verified using the [Verify](#M-Virgil-Crypto-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Virgil.Crypto.CryptoHelper.Verify(System.Byte[],System.Byte[],System.Byte[])') method.

<a name='M-Virgil-Crypto-CryptoHelper-Verify-System-String,System-String,System-Byte[]-'></a>
### Verify(text,signBase64,publicKey) `method` [#](#M-Virgil-Crypto-CryptoHelper-Verify-System-String,System-String,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the specified signature by comparing it to the signature computed for the specified text.

##### Returns

`true` if the signature verifies as valid; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text that was signed. |
| signBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The signature text in Base64 format to be verified. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Public Key` |

##### Remarks

This method verifies the digital signature produced by [Sign](#M-Virgil-Crypto-CryptoHelper-Sign-System-String,System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Sign(System.String,System.Byte[],System.String)').

<a name='M-Virgil-Crypto-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]-'></a>
### Verify(data,signData,publicKey) `method` [#](#M-Virgil-Crypto-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Verifies the specified signature by comparing it to the signature computed for the specified data.

##### Returns

`true` if the signature verifies as valid; otherwise, `false`.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data that was signed. |
| signData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signature data to be verified. |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A byte array that represents a `Public Key` |

##### Remarks

This method verifies the digital signature produced by [Sign](#M-Virgil-Crypto-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String- 'Virgil.Crypto.CryptoHelper.Sign(System.Byte[],System.Byte[],System.String)').
