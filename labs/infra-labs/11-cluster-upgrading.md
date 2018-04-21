# Upgrade an Azure Kubernetes Service (AKS) cluster

## Determine upgrade

Use the `az aks get-upgrades` command to check which Kubernetes releases are available for upgrade.

```
az aks get-upgrades --name aks-AZBootCamp2018k8s-arm --resource-group AZBootCamp2018k8s --output table

Name     ResourceGroup      MasterVersion    NodePoolVersion    Upgrades
-------  -----------------  ---------------  -----------------  -------------------
default  AZBootCamp2018k8s  1.8.10           1.8.10             1.9.1, 1.9.2, 1.9.6
```

## Upgrade AKS cluster

*Note: When upgrading an AKS cluster, Kubernetes minor versions cannot be skipped. For example, upgrade 1.7 > 1.9 is not allowed.

az aks upgrade --name aks-AZBootCamp2018k8s-arm --resource-group AZBootCamp2018k8s --kubernetes-version 1.9.1


## Validate upgrade

```
az aks show --name aks-AZBootCamp2018k8s-arm --resource-group AZBootCamp2018k8s --output table
Name                       Location    ResourceGroup      KubernetesVersion    ProvisioningState    Fqdn
-------------------------  ----------  -----------------  -------------------  -------------------  ------------------------------------------------
aks-AZBootCamp2018k8s-arm  eastus      AZBootCamp2018k8s  1.9.1                Succeeded            lraazbc2018armmgmt-9ac55307.hcp.eastus.azmk8s.io
```


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)