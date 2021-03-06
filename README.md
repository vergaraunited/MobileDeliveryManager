# MobileDeliveryManager
United Window Mobile Delivery Manager API - Server API serving all Clients the Delivery SQL Server data.
### MobileDeliveryMangerAPI
#### WebSocket API IPad Delivery, Winform Manifest Generator, A/R Notifications.

## NuGet Package References
#### UMDNuGet - Azure Artifact Repository
##### nuget.config file
```xml
<configuration>
  <packageSources>
    <clear />
    <add key="UMDNuget" value="https://pkgs.dev.azure.com/unitedwindowmfg/1e4fcdac-b7c9-4478-823a-109475434848/_packaging/UMDNuget/nuget/v3/index.json" />
  </packageSources>
</configuration>
```

Package Name            | Version   | Description
--------------------    | -------   | -----------
UMDGeneral              | 1.1.0     | Mobile Delivery General Code
MobileDeliveryServer    | 1.3.0     | Mobile Delivery Server base code for all servers
MobilDeliveryClient     | 1.1.0     | Mobile Delivery Client base code for all clients
MobileDeliveryLogger    | 1.0.0     | Mobile Delivery Logger base code for all components
MobileDeliverySettings  | 1.0.0     | Mobile Delivery Settings base code for all components

    
## Configuration
#### Configuration is built into the docker image based on the settings in the app.config

```xml
<appSettings>
    <add key="LogPath" value="C:\app\logs\" />
    <add key="LogLevel" value="Info" />
    <add key="Url" value="localhost" />
    <add key="Port" value="81" />
    <add key="WinsysUrl" value="localhost" />
    <add key="WinsysPort" value="8181" />
    <add key="WinsysSrcFilePath" value="\\Fs01\vol1\Winsys32\DATA" />
    <!-- If left empty WinsysDestFilePath defaults to Environment.GetFolderPath(Environment.SpecialFolder.Desktop)-->
    <add key="WinsysDstFilePath" value="" />
    <add key="ClientSettingsProvider.ServiceUri" value="" />
</appSettings>`
```


## NuGet

#### Initialize the project
`nuget restore`

#### Don't need to run spec, only once to generate the nuspec file which is already checked into git
`nuget spec`

#### Creates the nupkg - don't checkin nupkg file, (.ignore git)
`nuget pack`
`nuget pack -IncludeReferencedProjects -Build -Properties Configuration=Release`

#### Push Artifact to Repository (Azure/DevOps)
`find -name *.nupkg | xargs -i nuget push {} -Source "UMDNuget" -ApiKey az`


## Docker

#### Build
`docker build -t mobiledeliverymanager .`

#### Run
`docker run -d -p 81:81 -p 8181:8181 -p 1433:1433 --name mobiledeliverymanager --mount source=logs,destination=c:/app/logs  mobiledeliverymanager`
`docker run -d -p 81:81 -p 8181:8181 -p 1433:1433 --name mobiledeliverymgrcore --mount source=logs,destination=c:/app/logs mobiledeliverymgrcore`

#### Interactive shell into mobiledeliverymanager container
`winpty docker exec -it 03f8ba004e11 cmd`

#### MSSQL
** `docker exec -it <container_id|container_name> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P <your_password>`

#### Log Volume for persisting and exposing the logs outside of the container and on localhost's filesystem
##### In order access log files and not issue docker commands to enter the interactive shell within the running container, volumes (and mounts) offer the ability to expose and persist across restarts and rebuilds on the localhost's file system.
`docker service create 
    --mount 'type=volume,src=log,dst=/app/log,ro=0,volume-driver=local,volume-opt=type=nfs,volume-opt=device=<nfs-server>:<nfs-path>,"volume-opt=o=addr=<nfs-address>,vers=4,soft,timeo=180,bg,tcp,rw"' 
    --name myservice 
    <IMAGE>`


`docker volume create logs`
`docker volume ls`
`docker volume inspect logs`
