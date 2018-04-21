#Azure Kubernetes Service (AKS) Deployment using ARM

Azure Resource Manager (ARM) templates provide additional control over resources.

This lab provides sample templates for creation of managed Kubernetes cluster (AKS).

![logo](img/arm_Logo.png =100x)

## Define resources in Json

ARM configuration consists of two primary pieces. 

1. Main file with resource definitions such as parameters, variables, resources, and outputs.
    * [aks.json](../../infrastructure/ARM/aks.json)
2. Parameter file with environment specificvalues.
    * [aks.parameters.json](../../infrastructure/ARM/aks.parameters.json)

## Provision AKS cluster with Azure CLI

```
az group create --name AZBootCamp2018k8s --location eastus

az group deployment create --resource-group AZBootCamp2018k8s --template-file aks.json --parameters aks.parameters.json
```

Use ```--debug``` flag to inspect deployment issues.


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)