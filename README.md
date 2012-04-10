# SimpleCrypto.Net

## NuGet

Visual Studio users can install this directly into their .NET projects by executing the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package SimpleCrypto

## Description

Simple cryptography library that wraps complex hashing algorithms for quick and simple usage. 

## Usage

Go to the [downloads page](https://github.com/Mixmasterxp/SimpleCrypto.net/downloads) and download the latest version or utilize the NuGet package.
Unzip the file files and reference the following file in your .net project:

	SimpleCrypto.dll

Sample Source:

    ICryptoService cryptoService = new PBKDF2();

    //New User
    string password = "password";

    //save this hash to the database
    string hashedPassword = cryptoService.Compute(password);

    //save this salt to the database
    string salt = cryptoService.Salt;
            
    //validate user
    //compare the password (this should be true since we are rehashing the same password and using the same generated salt)
    bool isPasswordValid = cryptoService.Compute(password, salt) == hashedPassword;
	
## Necessary prerequisites

.NET 4
