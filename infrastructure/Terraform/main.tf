resource "azurerm_resource_group" "KubeTerraform" {
  name     = "${var.rg_name}"
  location = "${var.region}"

  tags {
    Purpose     = "AZ Bootcamp 2018"
    Environment = "POC"
  }
}

resource "azurerm_kubernetes_cluster" "KubeTerraform" {
  name                = "aks-${azurerm_resource_group.KubeTerraform.name}"
  location            = "${azurerm_resource_group.KubeTerraform.location}"
  resource_group_name = "${azurerm_resource_group.KubeTerraform.name}"
  kubernetes_version  = "${var.cluster_version}"
  dns_prefix          = "${var.dns_name_prefix}"
  depends_on          = ["azurerm_resource_group.KubeTerraform"]

  linux_profile {
    admin_username = "${var.admin_username}"

    ssh_key {
      key_data = "${var.admin_ssh_publickey}"
    }
  }

  agent_pool_profile {
    name    = "default"
    count   = "${var.agent_count}"
    vm_size = "${var.agent_vm_size}"
    os_type = "Linux"
  }

  service_principal {
    client_id     = "${var.service_principal_client_id}"
    client_secret = "${var.service_principal_client_secret}"
  }

  tags {
    Purpose     = "AZ Bootcamp 2018"
    Environment = "POC"
  }
}

resource "kubernetes_namespace" "KubeTerraform" {
  depends_on = ["azurerm_kubernetes_cluster.KubeTerraform"]

  metadata {
    name = "${var.cluster_env_namespace_name}"
  }
}

resource "azurerm_storage_account" "test" {
  name                     = "sacr${replace(lower(azurerm_resource_group.KubeTerraform.name), "-", "")}"
  resource_group_name      = "${azurerm_resource_group.KubeTerraform.name}"
  location                 = "${azurerm_resource_group.KubeTerraform.location}"
  account_tier             = "Standard"
  account_replication_type = "GRS"
}

resource "azurerm_container_registry" "test" {
  name                = "acr${replace(azurerm_resource_group.KubeTerraform.name, "-", "")}"
  resource_group_name = "${azurerm_resource_group.KubeTerraform.name}"
  location            = "${azurerm_resource_group.KubeTerraform.location}"
  admin_enabled       = true
  sku                 = "Classic"
  storage_account_id  = "${azurerm_storage_account.test.id}"
  depends_on          = ["azurerm_storage_account.test"]
}

#
#resource "kubernetes_service" "KubeTerraform" {
#  depends_on = ["azurerm_kubernetes_cluster.KubeTerraform"]
#
#  metadata {
#    name      = "my-healthysvc"
#    namespace = "${var.cluster_env_namespace_name}"
#  }
#
#  spec {
#    selector {
#      app = "${kubernetes_pod.KubeTerraform.metadata.0.labels.app}"
#    }
#
#    session_affinity = "ClientIP"
#
#    port {
#      protocol    = "TCP"
#      port        = 8080
#      target_port = 8080
#    }
#
#    type = "LoadBalancer"
#  }
#}

