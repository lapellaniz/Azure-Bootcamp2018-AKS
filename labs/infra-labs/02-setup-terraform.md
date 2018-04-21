# Setup Hashicorp Terraform on Local Machine
Terrafrom needs to be installed locally. It is used via the `terraform` command.

![logo](img/terraform_Logo.png =150x)

## Install Terraform
1. Install Terraform using the [binary package](https://www.terraform.io/downloads.html) for Windows (or your choice of OS).

2. Unzip the package and copy to another folder such as ```C:\Terraform```.

3. Configure the ```terraform``` binary in your *PATH* variables.

see https://www.terraform.io/intro/getting-started/install.html

## Verify the Installation

Execute ```terraform``` from the command line and confirm installation.

## Configuration

1. Configure the Azure Provider. If using Azure CLI, run ```az login```.

2. Set your context to the subsription you plan to use.

    ```
    az account set --subscription="SUBSCRIPTION_ID"
    ```

If using a service principal, update [infrastructure/Terraform/provider.tf](../../infrastructure/Terraform/provider.tf).

## Initialization
The first command to run is ```terraform init```. This will install the necessary plugins and providers.

see https://www.terraform.io/intro/getting-started/build.html


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)