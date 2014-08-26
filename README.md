[![NuGet](http://img.shields.io/nuget/v/SimpleCrypto.svg?style=flat-square)](https://www.nuget.org/packages/SimpleCrypto/)
[![Downloads](http://img.shields.io/nuget/dt/SimpleCrypto.svg?style=flat-square)](https://www.nuget.org/packages/SimpleCrypto/)
[![Build Status](http://img.shields.io/teamcity/codebetter/bt964.svg?style=flat-square)](http://teamcity.codebetter.com/project.html?projectId=project321&guest=1)
[![Code Coverage](http://img.shields.io/teamcity/coverage/bt964.svg?style=flat-square)](http://teamcity.codebetter.com/project.html?projectId=project321&guest=1)


# SimpleCrypto.Net

## NuGet

Visual Studio users can install this directly into their .NET projects by executing the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package SimpleCrypto

## Description

Simple cryptography library that wraps complex hashing algorithms for quick and simple usage. 

## Usage

You may download the source and build the project or install it directly from NuGet.

If building the source, please reference the following file in your .net project:

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
    string hashedPassword2 = cryptoService.Compute(password, salt);
    bool isPasswordValid = cryptoService.Compare(hashedPassword, hashedPassword2);

Generate Random Password Example:

    //generate uppercase passwords only
    string password = RandomPassword.Generate(PasswordGroup.Uppercase);
     
    //generate both upper case and lower passwords only
    string password = RandomPassword.Generate(PasswordGroup.Uppercase, PasswordGroup.Lowercase);

    //generate 10 character uppercase passwords only
    string password = RandomPassword.Generate(10, PasswordGroup.Uppercase);


## Necessary prerequisites

.NET 4

## License

SimpleCrypto.NET is licensed with the Apache License, version 2.0. You can find more information on the license here: http://www.apache.org/licenses/LICENSE-2.0.html

##Changelog

  1. **0.3.0.0 - March 14, 2013** - Generate Salt feature added.
  2. **0.2.0.0 - September 16, 2012** - Generate Random password feature added.
