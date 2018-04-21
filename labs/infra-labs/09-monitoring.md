# Add Monitoring to an Azure Kubernetes Service Cluster

Monitoring your Kubernetes cluster and containers is critical, especially when running a production cluster, at scale, with multiple applications.

In this lab, we will configure monitoring of your AKS cluster using the Containers solution for Log Analytics.

https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-monitor

Sample ARM and Terraform templates are provided.

* ARM - [aks.json](../../infrastructure/ARM/aks.json)
* Terraform - [monitoring.tf](../../infrastructure/Terraform/monitoring.tf)

## Configure OMS workspace

https://docs.microsoft.com/en-us/azure/log-analytics/

The Log analytics Workspace ID and Key are needed for configuring the solution agent on the Kubernetes nodes.

## Configure OMS AKS Solution

Install the `Container Monitoring Solution` for OMS via Portal or ARM.

```
"variables": {
    "omsWorkspaceName": "lra-azbootcamp2018"
    "solutionName": "[concat('Containers(', variables('omsWorkspaceName'), ')')]"
},
"resources": [
{
    "apiVersion": "2015-11-01-preview",
    "location": "[resourceGroup().location]",
    "name": "[variables('solutionName')]",
    "type": "Microsoft.OperationsManagement/solutions",
    "id": "[concat('/subscriptions/', subscription().subscriptionId, '/resourceGroups/', resourceGroup().name, '/providers/Microsoft.OperationsManagement/solutions/', variables('solutionName'))]",
    "dependsOn": [
        "[concat('Microsoft.OperationalInsights/workspaces/', variables('omsWorkspaceName'))]"
    ],
    "properties": {
        "workspaceResourceId": "[resourceId('Microsoft.OperationalInsights/workspaces/', variables('omsWorkspaceName'))]"
    },
    "plan": {
        "name": "[variables('solutionName')]",
        "publisher": "Microsoft",
        "product": "OMSGallery/Containers",
        "promotionCode": ""
    }
}]
```

## Create Kubernetes secret

Store the OMS settings in a Kubernetes secret. This value will be used by the Daemonset yaml.

```
kubectl create secret generic omsagent-secret --from-literal=WSID=WORKSPACE_ID --from-literal=KEY=WORKSPACE_KEY

secret "omsagent-secret" created
```

## Configure monitoring agents

Create the [oms-agent.yaml](../../infrastructure/OMS/oms-agent.yaml)

```
kubectl create -f oms-agent.yaml

daemonset "omsagent" created
```

## Troubleshooting and Logs

* Review pod configuration - `kubectl describe pod omsagent-qchth`
* Debug services - https://kubernetes.io/docs/tasks/debug-application-cluster/debug-service/
* Review logs using "kubectl logs" command - `kubectl logs omsagent-qchth -p -c`
* Find and view the cluster logs
    * api-server
    * kube-scheduler
    * controller-manager
    * kubelet (agent)
* Run command on pod `kubectl exec omsagent-qchth ls /`
* Display content of file on pod `kubectl exec omsagent-qchth cat /etc/resolv.conf`


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)