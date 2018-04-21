# Authenticate with Azure Container Registry from Azure Container Service

Two approaches:

1) Use the Service Principal used to create AKS and grant that access to ACR
2) Create a new Service Principal and scope it to accessing ACR only. This will be deployed as an AKS secret and then must be specified in the yaml

## Create ACR

```
az acr create --resource-group AZBootCamp2018k8s --name acrAZBootCamp2018k8s --sku Basic
```

## Get AKS Client ID
```
az aks show --resource-group AZBootCamp2018k8s --name aks-AZBootCamp2018k8s-arm --query "servicePrincipalProfile.clientId" --output tsv
```

## Get ACR Resource ID

```
az acr show --name acrAZBootCamp2018k8s --resource-group AZBootCamp2018k8s --query "id" --output tsv

Output:
/subscriptions/028e15e9-7083-48fb-a6f9-5d83e846cc9b/resourceGroups/AZBootCamp2018k8s/providers/Microsoft.ContainerRegistry/registries/acrAZBootCamp2018k8s
```

## Grant AKS access to ACR

```
az role assignment create --assignee ffc5c0a1-2ac7-4a65-9b20-add840f34250 --role Reader --scope /subscriptions/028e15e9-7083-48fb-a6f9-5d83e846cc9b/resourceGroups/AZBootCamp2018k8s/providers/Microsoft.ContainerRegistry/registries/acrAZBootCamp2018k8s
```