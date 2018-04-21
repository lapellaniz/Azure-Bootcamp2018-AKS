# Introduction 
Microservice architecture has become a very popular for building highly-available cloud-based applications. Each service can be deployed and maintained independently. Targeting .NET Core allows us to run these microservices as Docker containers in Kubernetes which provides a highly scalable and resilient environment. During this session, we will discuss and demonstrate the creation of a CI/CD pipeline for creating AKS infrastructure and the process of creating Docker images and deploying to the environment.

## Getting Started
Follow the labs to provided in order to walkthrough setting up a cluster and deploying a .NET Core microservice.

* [Infrastructure lab](labs/infra-labs/00-lab-environment.md)
* [Application lab](labs/app-labs/00-lab-environment.md)

## Sample Application
A [.NET Core service](app/ImageProcessor.sln) is provided for the lab in the app directory. 

It's a simple shell that provides the following topics:

1) Logging using .NET Core
2) Configuration mangement using .NET Core appsettings.json
3) Mounting volumes for k8s config-map and secrets
4) Docker
5) self-hosting background worker service
6) Handling signals in order to gracefully shutdown application
7) K8s deployment configuration