# Lab Environment
This lab demonstrates how to deploy Azure Container Services - Kubernetes (AKS) cluster using Azure Resource Manager Templates and Hashicorp Terraform. Visual Studio Teams Systems is used as the CI/CD platform and manage the infrastructure source.

## Tools

1. Azure Resource Manager Templates (ARM)
2. Azure CLI
3. Hashicorp Terraform
4. Kubernetes CLI (kubectl)
5. SSH Generator - git bash / PuTTYgen / OpenSSH
6. Visual Studio Code
7. Visual Studio Team Systems (VSTS)
8. Chocolatey - Windows package manager

## Requirements

1. Azure subscription
    * Owner or Contributor role
    * Azure Active Directory permissions (create service principal)
3. VSTS account

## Setup Environment

1. Setup Azure CLI:
    * Download latest [tool](https://docs.microsoft.com/en-us/cli/azure/authenticate-azure-cli?view=azure-cli-latest).
    * Run ```az login``` and follow the instructions. 
    * You can also use [Cloud Shell](https://azure.microsoft.com/en-us/features/cloud-shell/).
2. Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl):
    * Using [Chocolatey](https://chocolatey.org/), install with: ```choco install kubernetes-cli```
    * Using Azure CLI, install with: `az aks intall-cli`
    * Run ```kubectl version``` to verify the installation.
3. Install git:
    * Download latest [tool](https://gitforwindows.org/).
    * Include Windows Explorer integration for git bash.
    * Use Git from the Windows Command Prompt.
4. Setup Visual Studio Code:
    * Login in to VSTS
    * Create a repository and add *infrastructure* folder from demo source.

---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)