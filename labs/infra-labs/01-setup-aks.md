# Initial steps to create AKS cluster

## Generate SSH using Git Bash:

1. Run the following command and follow the prompts:

    ```ssh-keygen -t rsa -b 4096 -C "your_email@example.com" -f .ssh/id_rsa```

* [Here](https://help.github.com/articles/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent/) are instructions using Git Bash.
* [Here](https://www.ssh.com/ssh/putty/windows/puttygen) are instructions using PuTTYgen.

## Create service principal:

*NOTE: For the purpose of this demo, a single SPN is created and used for VSTS and AKS.*

1. Get your subscription:

    ```az account show --query "id"```

2. Create principal:

    ```az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/<subscription-id>"```

## Manage secrets (optional)

Keep your secrets safe, put them in Azure Key Vault

```
$ az keyvault create --name <Key Vault name> --resource-group <Key Vault resource group name>
```


---
[1](00-lab-environment.md) > [2](01-setup-aks.md) > [3](02-setup-terraform.md) > [4](03-create-aks-cluster-cli.md) > [5](04-create-aks-cluster-arm.md) > [6](05-create-aks-cluster-tf.md) > [7](06-cicd.md) > [8](07-kubernetes-ui.md) > [9](08-container-registry.md) > [10](09-monitoring.md) > [11](10-cluster-scaling.md) > [12](11-cluster-upgrading.md) > [13](12-advanced.md)