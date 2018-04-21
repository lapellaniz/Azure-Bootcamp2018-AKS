# Working with Azure Kubernetes Service Cluster Scaling

## Scake Kubernetes Cluster

1. Check to see number of current nodes running.

```
kubectl get nodes
NAME                        STATUS    ROLES     AGE       VERSION
aks-agentpools-67251657-0   Ready     agent     1d        v1.8.10
aks-agentpools-67251657-1   Ready     agent     1d        v1.8.10
```

2. Scale out AKS cluster to accomodate the demand.

```
az aks scale -g AZBootCamp2018k8s -n aks-AZBootCamp2018k8s-arm --node-count 3
```


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)