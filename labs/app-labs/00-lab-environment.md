# Lab Environment
This lab demonstrates how to develop and deploy a .NET Core console application into Azure Container Services - Kubernetes (AKS) cluster using VSTS.

## Tools

1. Azure Resource Manager Templates (ARM)
2. Azure CLI
3. Docker for Windows
4. GitVersion
5. Kubernetes CLI (kubectl)
6. PowerShell
7. Visual Studio Code
8. Visual Studio Team Systems (VSTS)

## Requirements

1. Azure subscription
2. AKS Cluster
3. VSTS account
    4. ARM Service Endpoint
    5. AKS Service Endpoint

## Setup Environment

1. Setup Azure CLI:
    * Download latest [tool](https://docs.microsoft.com/en-us/cli/azure/authenticate-azure-cli?view=azure-cli-latest).
    * Run ```az login``` and follow the instructions. 
    * You can also use [Cloud Shell](https://azure.microsoft.com/en-us/features/cloud-shell/).
2. Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl):    
    * Using Azure CLI, install with: `az aks intall-cli`
    * Run ```kubectl version``` to verify the installation.
3. Install git:
    * Download latest [tool](https://gitforwindows.org/).
    * Include Windows Explorer integration for git bash.
    * Use Git from the Windows Command Prompt.
4. Setup Visual Studio Code:
    * Login in to VSTS
    * Create a repository and add *infrastructure* folder from demo source.
5. Install GitVertsion CommandLine:
    * Run `Install-Package GitVersion.CommandLine` from PowerShell

---
[lab](00-lab-environment.md) > [setup](01-setup.md) > [docker](02-docker.md) > [cicd](03-cicd.md) > [cofig](04-configuration.md) > [logging](05-logging.md) > [readiness](06-readiness.md)
