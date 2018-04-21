# Azure Kubernetes Service (AKS) Deployment using Terraform
Terraform provides an experience to safely and predictably make changes to infrastructure.

In this lab Terraform will be used to create Azure Kubernetes Service Cluster (AKS).

![logo](img/terraform_Logo.png =150x)

## Define resources in HCL

Terraform configuration consits of files used to represent resource definitions, variables, outputs, and inputs. These files are based on Hashicorp Configuration Language ([HCL](https://github.com/hashicorp/hcl)).

1. Main file with resource definitions
    * [main.tf](../../infrastructure/Terraform/main.tf)
2. Variable input
    * [terraform.tfvars](../../infrastructure/Terraform/terraform.tfvars)
3. Variable definition
    * [variables.tf](../../infrastructure/Terraform/variables.tf)
4. Output declarations
    * [outputs.tf](../../infrastructure/Terraform/outputs.tf)

## Provision AKS cluster with Terraform

### Initialization

Run `init` command to initialze the working directory. Terraform configuration files must exist.

```
terraform init
```

### Plan

Run `plan` command to see what changes are going to be planned. A plan can be created during a **CI build**.

```
terraform plan -out myaksplan.tfplan
```

### Apply

Run `apply` to deploy changes required to reach the desired state of the configuration specified in the plan.

```
terraform apply myaksplan.tfplan
```

### Outputs

```
azurerm_kubernetes_cluster.KubeTerraform: Creation complete after 13m7s (ID: /subscriptions/028e15e9-7083-48fb-a6f9-...nagedClusters/aks-azbootcamp2018k8s-tf)

Apply complete! Resources: 2 added, 0 changed, 0 destroyed.

Outputs:

get_credentials_command = az acs kubernetes get-credentials --name="aks-azbootcamp2018k8s-tf" --resource-group="AZBootCamp2018k8s"
masterFQDN = az aks show --name="aks-azbootcamp2018k8s-tf" --resource-group="AZBootCamp2018k8s" --query fqdn
master_fqdn = lraazbc2018tf-11f90250.hcp.eastus.azmk8s.io
ssh_command_master0 = ssh lraadminuser@lraazbc2018tf-11f90250.hcp.eastus.azmk8s.io -A -p 22
```


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)