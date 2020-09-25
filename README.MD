A Prototype for a .NET core application using FIDO2 as second factor for MFA using the [abergs / fido2-net-lib](https://github.com/abergs/fido2-net-lib) NuGet package. This prototype is part of my Bachelorthesis __Multi-Faktor-Authentifizierung in der Remote-Diagnose__.

There are several ways to start the application. The easiest way
is to run the application as a docker container. Assuming Docker 13
and Compose are installed, the application can be started from the folder containing the dockercompose.yml file with
```
docker-compose up
```
Alternatively with installed .NET Core 3.1 SDK and the .NET Core CLI, the server can be started from the MultiFactorAuthentication.Web directory with
```
dotnet run
```
Also the MultiFactorAuthentication.sln can be used with a suitable IDE such as Open Visual Studio 11 or Rider 12 and run it from the IDE.  
With all methods you can access the application in a current browser under [localhost:5555](http://localhost:5555/)