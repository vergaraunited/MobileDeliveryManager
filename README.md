# MobileDeliveryManager
United Window Mobile Delivery Manager API - Server API serving all Clients the Delivery SQL Server data.

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
`nnuget pack`
#### Push into the Artifact (Azure/DevOps)
`nuget.exe push -Source "UMDNuget" -ApiKey az UMDGeneral.1.1.0.nupkg`

## Docker

#### Build
docker build -t mpbiledeliverymanager .

#### Run
docker run -d --name mobiledeliverymanager --mount source=logs,target=/app/UnitedMobileDelivery  mobiledeliverymanager

#### Interactive shell into mobiledeliverymanager container
winpty docker exec -it 03f8ba004e11 cmd

#### MSSQL
** docker exec -it <container_id|container_name> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P <your_password>

#### Log Volume for persisting and exposing the logs outside of the container and on localhost's filesystem
##### In order access log files and not issue docker commands to enter the interactive shell within the running container, volumes (and mounts) offer the ability to expose and persist across restarts and rebuilds on the localhost's file system.
`docker volume create logs`
`docker volume ls`
`docker volume inspect logs`
