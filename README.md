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

Hash Password Example:

    ICryptoService cryptoService = new PBKDF2();

    //New User
    string password = "password";
	
    //save this salt to the database
    string salt = cryptoService.GenerateSalt();

    //save this hash to the database
    string hashedPassword = cryptoService.Compute(password);
	            
    //validate user
    //compare the password (this should be true since we are rehashing the same password and using the same generated salt)
    bool isPasswordValid = cryptoService.Compute(password, salt) == hashedPassword;

Generate Random Password Example:

    //generate uppercase passwords only
    string password = RandomPassword.Generate(PasswordGroup.Uppercase);
     
    //generate both upper case and lower passwords only
    string password = RandomPassword.Generate(PasswordGroup.Uppercase, PasswordGroup.Lowercase);

    //generate 10 character uppercase passwords only
    string password = RandomPassword.Generate(10, PasswordGroup.Uppercase);


## Necessary prerequisites

.NET 4


##Changelog

  **0.2.0.0 - September 16, 2012** - Generate Random password feature added.
  
  **0.3.0.0 - March 14, 2013** - Generate Salt feature added.
