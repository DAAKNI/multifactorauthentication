Für das Starten der Anwendung gibt es verschiedene Möglichkeiten. Bei installierten
.NET Core 3.1 SDK und der .NET Core CLI lässt sich der Server starten, in dem man
in einem Terminal in das MultiFactorAuthentication.Web Verzeichnis wechselt und
die Anwendung mit folgendem Befehl startet:
```
dotnet run
```
Alternativ lässt sich die MultiFactorAuthentication.sln mit einer geeignet IDE wie
Visual Studio 11 oder Rider 12 öffnen und aus der IDE heraus ausführen. Am einfachsten
ist es jedoch die Applikation als Docker Container auszuführen. Vorausgesetzt Docker 13
und Compose sind installiert kann die Anwendung aus dem Ordner mit der dockercompose.yml Datei mit
```
docker-compose up
```
gestartet werden. Bei allen Varianten ist die Applikation in einem aktuellen Browser und
der URL http://localhost:5555/ zu erreichen.