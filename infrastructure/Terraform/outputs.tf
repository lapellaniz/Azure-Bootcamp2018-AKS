output "get_credentials_command" {
  value = "az aks get-credentials --name=\"${azurerm_kubernetes_cluster.KubeTerraform.name}\" --resource-group=\"${azurerm_resource_group.KubeTerraform.name}\""
}

output "masterFQDN" {
  value = "az aks show --name=\"${azurerm_kubernetes_cluster.KubeTerraform.name}\" --resource-group=\"${azurerm_resource_group.KubeTerraform.name}\" --query fqdn"
}

output "master_fqdn" {
  value = "${azurerm_kubernetes_cluster.KubeTerraform.fqdn}"
}

output "ssh_command_master0" {
  value = "ssh ${var.admin_username}@${azurerm_kubernetes_cluster.KubeTerraform.fqdn} -A -p 22"
}
