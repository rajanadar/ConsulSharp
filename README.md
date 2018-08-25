ConsulSharp
==========

A cross-platform .NET Library for HashiCorp's Consul - A Secret Management System.

**ConsulSharp Latest release: [1.2.200](https://www.nuget.org/packages/ConsulSharp) [Install-Package ConsulSharp -Version 1.2.200]**

**ConsulSharp Latest Documentation:** Inline Below and also at: http://rajanadar.github.io/ConsulSharp/

**ConsulSharp Gitter Lobby:** [Gitter Lobby](https://gitter.im/rajanadar-ConsulSharp/Lobby)

**Report Issues/Feedback:** [Create a ConsulSharp GitHub issue](https://github.com/rajanadar/ConsulSharp/issues/new)

[![Join the chat at https://gitter.im/rajanadar-ConsulSharp/Lobby](https://badges.gitter.im/rajanadar-ConsulSharp/Lobby.svg)](https://gitter.im/rajanadar-ConsulSharp/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)	
[![License](https://img.shields.io/:license-apache%202.0-brightgreen.svg)](http://www.apache.org/licenses/LICENSE-2.0.html)	
[![Build status](https://ci.appveyor.com/api/projects/status/aldh4a6n2t7hthdv?svg=true)](https://ci.appveyor.com/project/rajanadar/ConsulSharp) [![Join the chat at https://gitter.im/rajanadar-ConsulSharp/Lobby](https://badges.gitter.im/rajanadar-ConsulSharp/Lobby.svg)](https://gitter.im/rajanadar-ConsulSharp/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### What is ConsulSharp?	

* ConsulSharp is a .NET Standard 1.3 (and .NET 4.5) cross-platform C# Library that can be used in any .NET application to interact with Hashicorp's Consul.	
* Consul is a service mesh solution providing a full featured control plane with service discovery, configuration, and segmentation functionality.

ConsulSharp is designed ground up, to give a structured user experience across the various Consul capabilities.
Also, the Intellisense on IConsulClient class should help. I have tried to add a lot of documentation.

### Give me a quick snippet for use!

 * Add a Nuget reference to ConsulSharp as follows ```Install-Package ConsulSharp -Version <latest_version>```
 * Instantiate a IConsulClient as follows:

 ```cs	
IConsulClient consulClient = new ConsulClient(consulUriWithPort, consulToken);
var managementTokenId = await consulClient.V1.ACL.BootstrapAsync();
```

### Gist of the features

 * ConsulSharp supports every API supported by Consul. 
 * These capabilities are bucketed into:
   - ACL
   - Agent
   - Catalog
   - Connect
   - Coordinates
   - Events
   - Health
   - KV Store
   - Operator
   - Prepared Queries
   - Sessions
   - Snapshots
   - Status
   - Transactions
 * Abundant intellisense.
 * Provides hooks into http-clients to set custom proxy settings etc.

### ConsulSharp - Supported .NET Platforms

ConsulSharp is built on **.NET Standard 1.3** & **.NET Framework 4.5**. This makes it highly compatible and cross-platform.

The following platforms are supported due to that.

 * .NET Core 1.0 and above including .NET Core 2.0
 * .NET Framework 4.5 and above
 * Mono 4.6 and above
 * Xamarin.iOS 10.0 and above
 * Xamarin Mac 3.0 and above
 * Xamarin.Android 7.0 and above
 * UWP 10.0 and above
 
 Source: https://github.com/dotnet/standard/blob/master/docs/versions.md

### What is the deal with the Versioning of ConsulSharp?

* This library is written for Hashicorp's Consul Service
* Hence this library makes it easier for its consumers to relate to the Consul service version it supports.
* Hence a ConsulSharp version of 1.2.x denotes that this library will support the Consul 1.2.x Service Apis for sure.
* Tomorrow when Consul Service gets upgraded to 1.4.x, this library will be modified accordingly and versioned as 1.4.x

### Can I use it in my PowerShell Automation?

* Absolutely. ConsulSharp is a .NET Library. 
* This means, apart from using it in your C#, VB.NET, J#.NET and any .NET application, you can use it in PowerShell automation as well.
* Load up the DLL in your PowerShell code and execute the methods. PowerShell can totally work with .NET Dlls.

### All the methods are async. How do I use them synchronously?

* The methods are async as the defacto implementation. The recommended usage.
* However, there are innumerable scenarios where you would continue to want to use it synchronously.
* For all those cases, there are various options available to you.
* There is a lot of discussion around the right usage, avoiding deadlocks etc.
* This library allows you to set the 'continueAsyncTasksOnCapturedContext' option when you initialize the client.
* It is an optional parameter and defaults to 'false'
* Setting it to false, allows you to access the .Result property of the task with reduced/zero deadlock issues.
* There are other ways as well to invoke it synchronously, and  I leave it to the users of the library. (Task.Run etc.) 
* But please note that as much as possible, use it in an async manner. 

### In Conclusion

* If the above documentation doesn't help you, feel free to create an issue or email me. https://github.com/rajanadar/ConsulSharp/issues/new

### Happy Coding folks!
