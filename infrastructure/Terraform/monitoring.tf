resource "azurerm_log_analytics_workspace" "KubeTerraform" {
  name                = "oms-${azurerm_resource_group.KubeTerraform.name}"
  location            = "${azurerm_resource_group.KubeTerraform.location}"
  resource_group_name = "${azurerm_resource_group.KubeTerraform.name}"
  sku                 = "Free"
  depends_on          = ["azurerm_resource_group.KubeTerraform"]

  tags {
    Purpose     = "AZ Bootcamp 2018"
    Environment = "POC"
  }
}

resource "azurerm_log_analytics_solution" "KubeTerraform" {
  solution_name         = "Containers"
  location              = "${azurerm_resource_group.KubeTerraform.location}"
  resource_group_name   = "${azurerm_resource_group.KubeTerraform.name}"
  workspace_resource_id = "${azurerm_log_analytics_workspace.KubeTerraform.id}"
  workspace_name        = "${azurerm_log_analytics_workspace.KubeTerraform.name}"
  depends_on            = ["azurerm_resource_group.KubeTerraform", "azurerm_log_analytics_workspace.KubeTerraform"]

  plan {
    publisher = "Microsoft"
    product   = "OMSGallery/Containers"
  }
}
