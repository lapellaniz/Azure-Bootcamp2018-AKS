## Topics Presented

1) Logging using .NET Core
2) Configuration mangement using .NET Core appsettings.json
3) Mounting volumes for k8s config-map and secrets
4) Docker
5) self-hosting background worker service
6) Handling signals in order to gracefully shutdown application
7) K8s deployment configuration

## Running Startup

```
var host = await new ServiceHostBuilder()
                .UseConfiguration(args)
                .UseStartup<Startup>().BuildAsync();
await host.RunAsync();
```

## Docker

* [docker-compose.yml](docker-compose.yml)
* [Dockerfile](src/ImageProcessor.Service/Dockerfile)


## AKS Configuration
[kube-deploy.yml](deploy/kube/kube-deploy.yml)

## Build Scripts

* [Transform configuration file](build/scripts/mergesettings.ps1)
* [Set Docker version variable](build/scripts/setsemvervariable.sh)
    * [package.json](build/scripts/package.json)
    * [index.js](build/scripts/index.js)