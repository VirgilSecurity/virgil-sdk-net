# Virgil Security C# library

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

1. [CMake](http://www.cmake.org/).
1. [Git](http://git-scm.com/).
1. [Python](http://python.org/).
1. [Python YAML](http://pyyaml.org/).
1. C/C++ compiler:
    [gcc](https://gcc.gnu.org/),
    [clang](http://clang.llvm.org/),
    [MinGW](http://www.mingw.org/),
    [Microsoft Visual Studio](http://www.visualstudio.com/), or other.
1. [libcurl](http://curl.haxx.se/libcurl/).

## Build

1. Run one of the folowing scripts:

    * build.sh - on the Unix-like OS;
    * build.bat - on the Windows OS [coming soon].

1. Inspect folder `origin_lib` that contains built library.

1. Inspect folder `examples_bin` that contains built examples.

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
COMING SOON!
```

### <a name="example-2"></a> Example 2: Register user on the PKI service

*Input*: User ID

*Output*: Virgil Public Key

``` {.c#}
COMING SOON!
```

### <a name="example-3"></a> Example 3: Get user's public key from the PKI service

*Input*: User ID

*Output*: Virgil Public Key

``` {.c#}
COMING SOON!
```

### <a name="example-4"></a> Example 4: Encrypt data

*Input*: User ID, Data

*Output*: Encrypted data

``` {.c#}
COMING SOON!
```

### <a name="example-5"></a> Example 5: Decrypt data

*Input*: Encrypted data, Virgil Public Key, Private Key, Private Key password

*Output*: Decrypted data

``` {.c#}
COMING SOON!
```

### <a name="example-6"></a> Example 6: Sign data

*Input*: Data, Virgil Public Key, Private Key

*Output*: Virgil Sign

``` {.c#}
COMING SOON!
```

### <a name="example-7"></a> Example 7: Verify data

*Input*: Data, Sign, Virgil Public Key

*Output*: Verification result

``` {.c#}
COMING SOON!
```

## License
BSD 3-Clause. See [LICENSE](https://github.com/VirgilSecurity/virgil/blob/master/LICENSE) for details.

## Contacts
Email: <support@virgilsecurity.com>
