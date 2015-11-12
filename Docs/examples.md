#Passwordless Authentication Client Side

##C#/.Net

##Install
Use the NuGet Package Manager (Tools -> Library Package Manager -> Package Manager Console) to install the Virgil.SDK.Keys package, running the command:
```
PM> Install-Package Virgil.Crypto
```

## Implementation

```csharp
var authResponse = await WebClient.Post<AuthResponse>("/auth")
    .Body(new { email = "user-email@example.com" });

var authResult = await WebClient.Post<AuthResult>("/auth")
    .Body(new
    {
        email = "user-email@example.com",
        decrypted_token = VirgilCrypto.Decrypt(authResponse.EncryptedToken, myPrivateKey)
    });
```

#Example Java/Android

##Install
Use the Maven Central and JCenter repositories to install Virgil.SDK for Android
```
compile 'com.virgilsecurity.android:sdk+'
```

##Get Public Key and Encrypt Data

```java
VirgilKeys.search("demo@virgilsecurity.com", new ResponseCallback<PublicKey>() {
    @Override
    public void onResult(@Nullable PublicKey publicKey) {
        final String data = "Encrypt Me, Pleeeeeeease.";
        
        final byte[] cipherData = VirgilCrypto.encrypt(data, publicKey);
        final byte[] sign = VirgilCrypto.sign(cipherData, myPrivateKey);
    }
});
```

#Example Javascript

##Install
Use bower or npm to install Virgil.SDK packages.
```
bower install virgil-browsers
```
```
npm install virgil-browsers
```
##Get Public Key and Encrypt Data

```javascript
VirgilKeys.search('demo@virgilsecurity.com').then(function(publicKey){
    var data = "Encrypt Me, Pleeeeeeease.";
    
    var cipherData = VirgilCrypto.encrypt(data, publicKey);
    var sign = VirgilCrypto.sign(cipherData, myPrivateKey);
});
