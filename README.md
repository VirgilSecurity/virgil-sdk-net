# Quickstart
This documentation will help you get started using Virgil Security services together with Crypto library for .NET platform.

- [Introduction](#introduction)
- [Build prerequisite](#build-prerequisite)
- [Build](#build)
- [Examples](#examples)
    - [General statements](#general-statements)
    - [Example 1: Generate keys](#example-1)
    - [Example 2: Register user on the PKI service](#example-2)
    - [Example 3: Get user's public key from the PKI service](#example-3)
    - [Example 4: Encrypt data](#example-4)
    - [Example 5: Decrypt data](#example-5)
    - [Example 6: Sign data](#example-6)
    - [Example 7: Verify data](#example-7)
- [License](#license)
- [Contacts](#contacts)

## Introduction

This branch focuses on the C# library implementation and covers next topics:

  * build prerequisite;
  * build;
  * usage exmaples.

Common library description can be found [here](https://github.com/VirgilSecurity/virgil).

## Build prerequisite

1. [Visual Studio 2013+](https://www.visualstudio.com/).
1. [.Net 4.5+](https://www.microsoft.com/en-US/download/details.aspx?id=30653).


## Build

Just build downloaded solution in Visual Studio. [Virgil.Net](https://www.nuget.org/packages/Virgil.Net/) library will be downloaded from the official [NuGet](https://www.nuget.org/) server during package restore.


## Examples

This section describes common case library usage scenarios, like

  * encrypt data for user identified by email, phone, etc;
  * sign data with own private key;
  * verify data received via email, file sharing service, etc;
  * decrypt data if verification successful.

### General statements

1. Examples MUST be run from their directory.
1. All results are stored in the same directory.

### <a name="example-1"></a> Example 1: Generate keys

*Input*:

*Output*: Public Key and Private Key

``` {.c#}

	namespace Virgil.Samples
	{
	    class GenerateKeys
	    {
	        public static void Run()
	        {
	            Console.WriteLine("Generate keys with with password: 'password'");
	            var virgilKeyPair = new VirgilKeyPair(Encoding.UTF8.GetBytes("password"));
	
	            Console.WriteLine("Store public key: public.key ...");
	            using (var fileStream = File.Create("public.key"))
	            {
	                byte[] publicKey = virgilKeyPair.PublicKey();
	                fileStream.Write(publicKey, 0, publicKey.Length);
	            }
	
	            Console.WriteLine("Store private key: private.key ...");
	            using (var fileStream = File.Create("private.key"))
	            {
	                byte[] privateKey = virgilKeyPair.PrivateKey();
	                fileStream.Write(privateKey, 0, privateKey.Length);
	            }
	        }
	    }
	}

```

using System;
using System.IO;
using System.Text;


### <a name="example-2"></a> Example 2: Register user on the PKI service

*Input*: User ID

*Output*: Virgil Public Key

``` {.c#}
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Text;
	using Newtonsoft.Json;
	
	namespace Virgil.Samples
	{
	    class Program
	    {
	        public const string UserIdType = "email";
	        public const string UserId = "cak0339631@haqed.com";
	
	        public static VirgilCertificate CreateUser(byte[] publicKey, string userIdType, string userId)
	        {
	            var certificate = new
	            {
	                public_key = publicKey,
	                user_data = new[]
	                {
	                    new
	                    {
	                        @class = "user_id",
	                        type = userIdType,
	                        value = userId
	                    }
	                }
	            };
	
	            var httpClient = new HttpClient();
	
	            const string uri = "https://pki.virgilsecurity.com/objects/public-key";
	            var json = JsonConvert.SerializeObject(certificate);
	            var content = new StringContent(json, Encoding.UTF8, "application/json");
	
	            var responseMessage = httpClient.PostAsync(uri, content).Result;
	            var reresponseText = responseMessage.Content.ReadAsStringAsync().Result;
	            dynamic response = JsonConvert.DeserializeObject(reresponseText);
	
	            string accountId = response.id.account_id;
	            string publicKeyId = response.id.public_key_id;
	
	            var virgilPublicKey = new VirgilCertificate(publicKey);
	            virgilPublicKey.Id().SetAccountId(Encoding.UTF8.GetBytes(accountId));
	            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));
	
	            return virgilPublicKey;
	        }
	
	        static void Main()
	        {
	            Console.WriteLine("Prepare input file: public.key...");
	            using (var inFile = File.OpenRead("public.key"))
	            {
	                Console.WriteLine("Prepare output file: virgil_public.key...");
	                using (var outFile = File.Create("virgil_public.key"))
	                {
	                    var publicKey = new byte[inFile.Length];
	                    inFile.Read(publicKey, 0, (int)inFile.Length);
	
	                    Console.WriteLine("Create user ({0}) account on the Virgil PKI service...", Program.UserId);
	                    VirgilCertificate virgilPublicKey = Program.CreateUser(publicKey, Program.UserIdType, Program.UserId);
	
	                    Console.WriteLine("Store virgil public key to the output file...");
	
	                    byte[] virgilPublickKeyBytes = virgilPublicKey.ToAsn1();
	                    outFile.Write(virgilPublickKeyBytes, 0, virgilPublickKeyBytes.Length);
	                }
	            }
	        }
	    }
	}

```

### <a name="example-3"></a> Example 3: Get user's public key from the PKI service

*Input*: User ID

*Output*: Virgil Public Key

``` {.c#}

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net.Http;
	using System.Text;
	using Newtonsoft.Json;
	
	namespace Virgil.Samples
	{
	    class Program
	    {
	        public const string UserIdType = "email";
	        public const string UserId = "cak0339631@haqed.com";
	        
	        public static VirgilCertificate GetPkiPublicKey(string userIdType, string userId)
	        {
	            const string uri = "https://pki.virgilsecurity.com/objects/account/actions/search";
	            var httpClient = new HttpClient();
	            var payload = new Dictionary<string,string> {{userIdType, userId}};
	            string json = JsonConvert.SerializeObject(payload, Formatting.None);
	            var content = new StringContent(json, Encoding.UTF8, "application/json");
	            var responseMessage = httpClient.PostAsync(uri, content).Result;
	
	            string responseText = responseMessage.Content.ReadAsStringAsync().Result;
	
	            dynamic response = JsonConvert.DeserializeObject(responseText);
	
	            dynamic publicKeyObject = response[0].public_keys[0];
	            string publicKeyId = publicKeyObject.id.public_key_id;
	            string publicKey = publicKeyObject.public_key;
	
	            var virgilPublicKey = new VirgilCertificate(VirgilBase64.Decode(publicKey));
	            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));
	
	            return virgilPublicKey;
	        }
	
	        static void Main()
	        {
	            Console.WriteLine("Get user ({0}) information from the Virgil PKI service...", Program.UserId);
	            var virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.UserId);
	
	            Console.WriteLine("Prepare output file: virgil_public.key...");
	            using (var outFile = File.Create("virgil_public.key"))
	            {
	                Console.WriteLine("Store virgil public key to the output file...");
	
	                byte[] virgilPublickKeyBytes = virgilPublicKey.ToAsn1();
	                outFile.Write(virgilPublickKeyBytes, 0, virgilPublickKeyBytes.Length);
	            }
	        }
	    }
	}

```

### <a name="example-4"></a> Example 4: Encrypt data

*Input*: User ID, Data

*Output*: Encrypted data

``` {.c#}

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Text;
	using Newtonsoft.Json;
	
	namespace Virgil.Samples
	{
	    class StreamSink : VirgilDataSink
	    {
	        private readonly Stream stream;
	
	        public StreamSink(Stream target)
	        {
	            this.stream = target;
	        }
	
	        public override bool IsGood()
	        {
	            return this.stream.CanWrite;
	        }
	
	        public override void Write(byte[] data)
	        {
	            this.stream.Write(data, 0, data.Length);
	        }
	    }
	
	    class StreamSource : VirgilDataSource
	    {
	        private readonly Stream stream;
	        private readonly byte[] buffer;
	
	        public StreamSource(Stream source)
	        {
	            this.stream = source;
	            this.buffer = new byte[1024];
	        }
	
	        public override bool HasData()
	        {
	            return this.stream.CanRead && this.stream.Position < this.stream.Length;
	        }
	
	        public override byte[] Read()
	        {
	            var bytesRead = this.stream.Read(buffer, 0, buffer.Length);
	
	            if (bytesRead == buffer.Length)
	            {
	                return buffer;
	            }
	
	            var arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);
	            return arraySegment.ToArray();
	        }
	    }
	
	    class Program
	    {
	        const string UserIdType = "email";
	        const string UserId = "cak0339631@haqed.com";
	        
	        static VirgilCertificate GetPkiPublicKey(string userIdType, string userId)
	        {
	            const string uri = "https://pki.virgilsecurity.com/objects/account/actions/search";
	            var httpClient = new HttpClient();
	            var payload = new Dictionary<string,string> {{userIdType, userId}};
	            string json = JsonConvert.SerializeObject(payload, Formatting.None);
	            var content = new StringContent(json, Encoding.UTF8, "application/json");
	            var responseMessage = httpClient.PostAsync(uri, content).Result;
	
	            string responseText = responseMessage.Content.ReadAsStringAsync().Result;
	
	            dynamic response = JsonConvert.DeserializeObject(responseText);
	
	            dynamic publicKeyObject = response[0].public_keys[0];
	            string publicKeyId = publicKeyObject.id.public_key_id;
	            string publicKey = publicKeyObject.public_key;
	
	            var virgilPublicKey = new VirgilCertificate(VirgilBase64.Decode(publicKey));
	            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));
	
	            return virgilPublicKey;
	        }
	
	        static void Main()
	        {
	            Console.WriteLine("Prepare input file: test.txt...");
	
	            using (var input = File.OpenRead("test.txt"))
	            {
	                Console.WriteLine("Prepare output file: test.txt.enc...");
	
	                using (var output = File.Create("test.txt.enc"))
	                {
	                    Console.WriteLine("Initialize cipher...");
	                    var virgilStreamCipher = new VirgilStreamCipher();
	
	                    Console.WriteLine("Get recipient (" + Program.UserId + ") information from the Virgil PKI service...");
	                    var virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.UserId);
	
	                    Console.WriteLine("Add recipient...");
	                    virgilStreamCipher.AddKeyRecipient(virgilPublicKey.Id().CertificateId(),
	                        virgilPublicKey.PublicKey());
	
	                    Console.WriteLine("Encrypt and store results...");
	
	                    var source = new StreamSource(input);
	                    var sink = new StreamSink(output);
	
	                    virgilStreamCipher.Encrypt(source, sink, true);
	
	                    Console.WriteLine("Encrypted data is successfully stored in the output file...");
	                }
	            }
	        }
	    }
	}


```

### <a name="example-5"></a> Example 5: Decrypt data

*Input*: Encrypted data, Virgil Public Key, Private Key, Private Key password

*Output*: Decrypted data

``` {.c#}

	using System;
	using System.IO;
	using System.Linq;
	using System.Text;
	
	namespace Virgil.Samples
	{
	    class StreamSink : VirgilDataSink
	    {
	        private readonly Stream stream;
	
	        public StreamSink(Stream target)
	        {
	            this.stream = target;
	        }
	
	        public override bool IsGood()
	        {
	            return this.stream.CanWrite;
	        }
	
	        public override void Write(byte[] data)
	        {
	            this.stream.Write(data, 0, data.Length);
	        }
	    }
	
	    class StreamSource : VirgilDataSource
	    {
	        private readonly Stream stream;
	        private readonly byte[] buffer;
	
	        public StreamSource(Stream source)
	        {
	            this.stream = source;
	            this.buffer = new byte[1024];
	        }
	
	        public override bool HasData()
	        {
	            return this.stream.CanRead && this.stream.Position < this.stream.Length;
	        }
	
	        public override byte[] Read()
	        {
	            var bytesRead = this.stream.Read(buffer, 0, buffer.Length);
	
	            if (bytesRead == buffer.Length)
	            {
	                return buffer;
	            }
	
	            var arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);
	            return arraySegment.ToArray();
	        }
	    }
	
	    class Program
	    {
	        public const string UserIdType = "email";
	        public const string UserId = "cak0339631@haqed.com";
	        public const string SignerId = "cak0339631@haqed.com";
	
	        static void Main()
	        {
	            Console.WriteLine("Prepare input file: test.txt.enc...");
	
	            using (var input = File.OpenRead("test.txt.enc"))
	            {
	                Console.WriteLine("Prepare output file: decrypted_test.txt...");
	
	                using (var output = File.Create("decrypted_test.txt"))
	                {
	                    Console.WriteLine("Initialize cipher...");
	                    var virgilStreamCipher = new VirgilStreamCipher();
	
	                    Console.WriteLine("Read virgil public key...");
	                    var publicKeyBytes = File.ReadAllBytes("virgil_public.key");
	                    var virgilPublicKey = new VirgilCertificate();
	                    virgilPublicKey.FromAsn1(publicKeyBytes);
	
	                    Console.WriteLine("Read private key...");
	
	                    var privateKey = File.ReadAllBytes("private.key");
	
	                    Console.WriteLine("Decrypt...");
	
	                    var source = new StreamSource(input);
	                    var sink = new StreamSink(output);
	                    byte[] password = Encoding.UTF8.GetBytes("password");
	
	                    virgilStreamCipher.DecryptWithKey(source, sink, virgilPublicKey.Id().CertificateId(),
	                        privateKey, password);
	
	                    Console.WriteLine("Decrypted data is successfully stored in the output file...");
	                }
	            }
	        }
	    }
	}


```

### <a name="example-6"></a> Example 6: Sign data

*Input*: Data, Virgil Public Key, Private Key

*Output*: Virgil Sign

``` {.c#}
	
	using System;
	using System.IO;
	using System.Linq;
	using System.Text;
	
	namespace Virgil.Samples
	{
	    class StreamSink : VirgilDataSink
	    {
	        private readonly Stream stream;
	
	        public StreamSink(Stream target)
	        {
	            this.stream = target;
	        }
	
	        public override bool IsGood()
	        {
	            return this.stream.CanWrite;
	        }
	
	        public override void Write(byte[] data)
	        {
	            this.stream.Write(data, 0, data.Length);
	        }
	    }
	
	    class StreamSource : VirgilDataSource
	    {
	        private readonly Stream stream;
	        private readonly byte[] buffer;
	
	        public StreamSource(Stream source)
	        {
	            this.stream = source;
	            this.buffer = new byte[1024];
	        }
	
	        public override bool HasData()
	        {
	            return this.stream.CanRead && this.stream.Position < this.stream.Length;
	        }
	
	        public override byte[] Read()
	        {
	            var bytesRead = this.stream.Read(buffer, 0, buffer.Length);
	
	            if (bytesRead == buffer.Length)
	            {
	                return buffer;
	            }
	
	            var arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);
	            return arraySegment.ToArray();
	        }
	    }
	
	    class Program
	    {
	        public const string UserIdType = "email";
	        public const string UserId = "cak0339631@haqed.com";
	        public const string SignerId = "cak0339631@haqed.com";
	
	        private static void Main()
	        {
	            Console.WriteLine("Prepare input file: test.txt...");
	
	            using (var input = File.OpenRead("test.txt"))
	            {
	                Console.WriteLine("Prepare output file: test.txt.sign...");
	
	                using (var output = File.Create("test.txt.sign"))
	                {
	                    Console.WriteLine("Read virgil public key...");
	                    var publicKeyBytes = File.ReadAllBytes("virgil_public.key");
	                    var virgilPublicKey = new VirgilCertificate();
	                    virgilPublicKey.FromAsn1(publicKeyBytes);
	
	                    Console.WriteLine("Read private key...");
	
	                    var privateKey = File.ReadAllBytes("private.key");
	
	                    Console.WriteLine("Initialize signer...");
	
	                    var signer = new VirgilStreamSigner();
	
	                    byte[] password = Encoding.UTF8.GetBytes("password");
	
	                    Console.WriteLine("Sign data...");
	
	                    var source = new StreamSource(input);
	
	                    VirgilSign sign = signer.Sign(source, virgilPublicKey.Id().CertificateId(), privateKey, password);
	
	                    Console.WriteLine("Save sign...");
	                    var asn1Sign = sign.ToAsn1();
	                    output.Write(asn1Sign, 0, asn1Sign.Length);
	
	                    Console.WriteLine("Sign is successfully stored in the output file.");
	                }
	            }
	        }
	    }
	}


```

### <a name="example-7"></a> Example 7: Verify data

*Input*: Data, Sign, Virgil Public Key

*Output*: Verification result

``` {.c#}

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Text;
	using Newtonsoft.Json;
	
	namespace Virgil.Samples
	{
	    class StreamSink : VirgilDataSink
	    {
	        private readonly Stream stream;
	
	        public StreamSink(Stream target)
	        {
	            this.stream = target;
	        }
	
	        public override bool IsGood()
	        {
	            return this.stream.CanWrite;
	        }
	
	        public override void Write(byte[] data)
	        {
	            this.stream.Write(data, 0, data.Length);
	        }
	    }
	
	    class StreamSource : VirgilDataSource
	    {
	        private readonly Stream stream;
	        private readonly byte[] buffer;
	
	        public StreamSource(Stream source)
	        {
	            this.stream = source;
	            this.buffer = new byte[1024];
	        }
	
	        public override bool HasData()
	        {
	            return this.stream.CanRead && this.stream.Position < this.stream.Length;
	        }
	
	        public override byte[] Read()
	        {
	            var bytesRead = this.stream.Read(buffer, 0, buffer.Length);
	
	            if (bytesRead == buffer.Length)
	            {
	                return buffer;
	            }
	
	            var arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);
	            return arraySegment.ToArray();
	        }
	    }
	
	    class Program
	    {
	        public const string UserIdType = "email";
	        public const string UserId = "cak0339631@haqed.com";
	        public const string SignerId = "cak0339631@haqed.com";
	        
	        static VirgilCertificate GetPkiPublicKey(string userIdType, string userId)
	        {
	            const string uri = "https://pki.virgilsecurity.com/objects/account/actions/search";
	            var httpClient = new HttpClient();
	            var payload = new Dictionary<string,string> {{userIdType, userId}};
	            string json = JsonConvert.SerializeObject(payload, Formatting.None);
	            var content = new StringContent(json, Encoding.UTF8, "application/json");
	            var responseMessage = httpClient.PostAsync(uri, content).Result;
	
	            string responseText = responseMessage.Content.ReadAsStringAsync().Result;
	
	            dynamic response = JsonConvert.DeserializeObject(responseText);
	
	            dynamic publicKeyObject = response[0].public_keys[0];
	            string publicKeyId = publicKeyObject.id.public_key_id;
	            string publicKey = publicKeyObject.public_key;
	
	            var virgilPublicKey = new VirgilCertificate(VirgilBase64.Decode(publicKey));
	            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));
	
	            return virgilPublicKey;
	        }
	
	        private static void Main()
	        {
	            Console.WriteLine("Prepare input file: test.txt...");
	
	            using (var input = File.OpenRead("test.txt"))
	            {
	                Console.WriteLine("Read virgil sign...");
	
	                using (var signStream = File.OpenRead("test.txt.sign"))
	                {
	                    var signBytes = new byte[signStream.Length];
	                    signStream.Read(signBytes, 0, signBytes.Length);
	
	                    var virgilSign = new VirgilSign();
	                    virgilSign.FromAsn1(signBytes);
	
	                    Console.WriteLine("Get signer (" + Program.SignerId + ") information from the Virgil PKI service...");
	
	                    VirgilCertificate virgilPublicKey = Program.GetPkiPublicKey(Program.UserIdType, Program.SignerId);
	
	                    Console.WriteLine("Initialize verifier...");
	
	                    var signer = new VirgilStreamSigner();
	
	                    Console.WriteLine("Verify data...");
	                    var dataSource = new StreamSource(input);
	                    bool verified = signer.Verify(dataSource, virgilSign, virgilPublicKey.PublicKey());
	
	                    Console.WriteLine("Data is " + (verified ? "" : "not ") + "verified!");
	                }
	            }
	        }
	    }
	}


```

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>
