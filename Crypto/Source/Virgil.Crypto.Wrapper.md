<a name='contents'></a>
# Contents [#](#contents 'Go To Here')

- [CryptoHelper](#T-Virgil-Crypto-Wrapper-CryptoHelper 'Virgil.Crypto.Wrapper.CryptoHelper')
  - [Decrypt(cipherTextBase64,recipientId,privateKey,privateKeyPassword)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Decrypt(System.String,System.String,System.Byte[],System.String)')
  - [Decrypt(cipherTextBase64,password)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Decrypt(System.String,System.String)')
  - [Decrypt(cipherData,recipientId,privateKey,privateKeyPassword)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Decrypt(System.Byte[],System.String,System.Byte[],System.String)')
  - [Decrypt(cipherData,password)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Decrypt(System.Byte[],System.String)')
  - [Encrypt(text,recipientId,recipientPublicKey)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Virgil.Crypto.Wrapper.CryptoHelper.Encrypt(System.String,System.String,System.Byte[])')
  - [Encrypt(text,recipients)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Virgil.Crypto.Wrapper.CryptoHelper.Encrypt(System.String,System.Collections.Generic.IDictionary{System.String,System.Byte[]})')
  - [Encrypt(text,password)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Encrypt(System.String,System.String)')
  - [Encrypt(data,recipients)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Virgil.Crypto.Wrapper.CryptoHelper.Encrypt(System.Byte[],System.Collections.Generic.IDictionary{System.String,System.Byte[]})')
  - [Encrypt(data,password)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Encrypt(System.Byte[],System.String)')
  - [GenerateKeyPair(password)](#M-Virgil-Crypto-Wrapper-CryptoHelper-GenerateKeyPair-System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.GenerateKeyPair(System.String)')
  - [Sign(text,privateKey,privateKeyPassword)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-String,System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Sign(System.String,System.Byte[],System.String)')
  - [Sign(data,privateKey,privateKeyPassword)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String- 'Virgil.Crypto.Wrapper.CryptoHelper.Sign(System.Byte[],System.Byte[],System.String)')
  - [Verify(text,signBase64,publicKey)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-String,System-String,System-Byte[]- 'Virgil.Crypto.Wrapper.CryptoHelper.Verify(System.String,System.String,System.Byte[])')
  - [Verify(data,signData,publicKey)](#M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Virgil.Crypto.Wrapper.CryptoHelper.Verify(System.Byte[],System.Byte[],System.Byte[])')

<a name='assembly'></a>
# Virgil.Crypto.Wrapper [#](#assembly 'Go To Here') [=](#contents 'Back To Contents')

<a name='T-Virgil-Crypto-Wrapper-CryptoHelper'></a>
## CryptoHelper [#](#T-Virgil-Crypto-Wrapper-CryptoHelper 'Go To Here') [=](#contents 'Back To Contents')

##### Namespace

Virgil.Crypto.Wrapper

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String-'></a>
### Decrypt(cipherTextBase64,recipientId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts encrypted text for recipient with private key.

##### Returns

Decrypted text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherTextBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The encrypted text |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The recipient's unique identifier |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Private Key's password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String-'></a>
### Decrypt(cipherTextBase64,password) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts encrypted text with password.

##### Returns

Decrypted text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherTextBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Encrypted text in Base64 format |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Encrypted text password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String-'></a>
### Decrypt(cipherData,recipientId,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts cipher data for recipient ID with private key.

##### Returns

Decrypted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Encrypted data |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The recipient's unique identifier |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The Private Key |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Private Key's password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String-'></a>
### Decrypt(cipherData,password) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Decrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Decrypts encrypted data with password.

##### Returns

Decrypted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cipherData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Encrypted data |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Encrypted text password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]-'></a>
### Encrypt(text,recipientId,recipientPublicKey) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text for recipient's public key.

##### Returns

Encrypted text in Base64 format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted |
| recipientId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The recipient's unique identifier. |
| recipientPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The recipient's public key data. |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}-'></a>
### Encrypt(text,recipients) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text for multiple recipient.

##### Returns

Encrypted text in Base64 format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted |
| recipients | [System.Collections.Generic.IDictionary{System.String,System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.Byte[]}') | Dictionary of recipients public keys |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String-'></a>
### Encrypt(text,password) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-String,System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text with password.

##### Returns

Encrypted text in Base64 format

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text to be encrypted |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}-'></a>
### Encrypt(data,recipients) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-Collections-Generic-IDictionary{System-String,System-Byte[]}- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text for multiple recipient.

##### Returns

Encrypted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted |
| recipients | [System.Collections.Generic.IDictionary{System.String,System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.Byte[]}') | Dictionary of recipients public keys |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-String-'></a>
### Encrypt(data,password) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Encrypt-System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Encrypts text with password.

##### Returns

Encrypted data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The data to be encrypted |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-GenerateKeyPair-System-String-'></a>
### GenerateKeyPair(password) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-GenerateKeyPair-System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Generates public/private key pair.

##### Returns

Instance of [VirgilKeyPair](#T-Virgil-Crypto-VirgilKeyPair 'Virgil.Crypto.VirgilKeyPair')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-String,System-Byte[],System-String-'></a>
### Sign(text,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-String,System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates digital signature for passing data using private key.

##### Returns

The digital signature data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Data to be signed. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The Private Key |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String-'></a>
### Sign(data,privateKey,privateKeyPassword) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Sign-System-Byte[],System-Byte[],System-String- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Creates digital signature for passing data using private key.

##### Returns

The digital signature data

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Data to be signed. |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The Private Key |
| privateKeyPassword | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The private key password |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-String,System-String,System-Byte[]-'></a>
### Verify(text,signBase64,publicKey) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-String,System-String,System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Virifies the original text digital signature for owner's public key.

##### Returns

True, if digital signature is valid, otherwise False.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The original text |
| signBase64 | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Digital signature in Base64 format |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key |

<a name='M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]-'></a>
### Verify(data,signData,publicKey) `method` [#](#M-Virgil-Crypto-Wrapper-CryptoHelper-Verify-System-Byte[],System-Byte[],System-Byte[]- 'Go To Here') [=](#contents 'Back To Contents')

##### Summary

Virifies the original data digital signature for owner's public key.

##### Returns

True, if digital signature is valid, otherwise False.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The original data |
| signData | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Digital signature data |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key |
