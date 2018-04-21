# Azure Kubernetes Service (AKS) Deployment using Azure CLI

## Register Azure CLI to work with AKS

```az provider register -n Microsoft.ContainerService```

## Create a Kubernetes cluster

Note: Must be one of these regions 'eastus,westeurope,centralus,westus2,ukwest,canadacentral,canadaeast'

```az group create --name AZBootCamp2018k8s --location eastus```

```az aks create --resource-group AZBootCamp2018k8s --name aks-AZBootCamp2018k8s-cli --ssh-key-value .ssh/id_rsa.pub```

## Verify provisioning

```
az aks list -o table
Name                       Location    ResourceGroup      KubernetesVersion    ProvisioningState    Fqdn
-------------------------  ----------  -----------------  -------------------  -------------------  -------------------------------------------------
aks-AZBootCamp2018k8s-arm  eastus      AZBootCamp2018k8s  1.8.10               Succeeded            lraazbc2018armmgmt-9ac55307.hcp.eastus.azmk8s.io
```

## Connect to cluster

.kube/config file contains information about the cluster, such as cluster name, master endpoint, users of the cluster, client certificate data, client ket data, certificate authority data and etc.

Run the following command in order to get the credentials to access the managed Kubernetes cluster in Azure.

* If using Azure CLI, run:

    ```az aks get-credentials --resource-group AZBootCamp2018k8s --name aks-AZBootCamp2018k8s-cli```

* If using SCP, run:

    ```scp azureuser@<master endpoint address>:.kube/config /.kube/config```

## Browse Cluster

```
kubectl get nodes
NAME                        STATUS    ROLES     AGE       VERSION
aks-agentpools-67251657-0   Ready     agent     1d        v1.8.10
aks-agentpools-67251657-1   Ready     agent     1d        v1.8.10
```


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)