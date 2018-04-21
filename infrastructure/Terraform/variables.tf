// General Variables
variable "version" {
  type        = "string"
  description = "Release version."
}

// Azure Variables
variable "rg_name" {
  type        = "string"
  description = "Name of resource group containing AKS instance."
}

variable "region" {
  type        = "string"
  description = "Region to deploy AKS."
  default     = "East US"
}

// AKS Variables
variable "cluster_version" {
  type        = "string"
  description = "Version for Kubernetes cluster."
}

variable "cluster_name" {
  type        = "string"
  description = "Name for Kubernetes cluster."
}

variable "dns_name_prefix" {
  type        = "string"
  description = "Sets the domain name prefix for the cluster."
}

variable "service_principal_client_id" {
  type        = "string"
  description = "The client id of the azure service principal used by Kubernetes to interact with Azure APIs."
}

variable "service_principal_client_secret" {
  type        = "string"
  description = "The client secret of the azure service principal used by Kubernetes to interact with Azure APIs."
}

variable "admin_username" {
  description = "Administrative username for the VMs"
  default     = "azureuser"
}

variable "admin_ssh_publickey" {
  type        = "string"
  description = "SSH public key in PEM format for Kubernetes Clusters. The key should include three parts, for example 'ssh-rsa AAAAB... email'"
}

variable "master_count" {
  type        = "string"
  default     = "1"
  description = "The number of Kubernetes masters for the cluster. Allowed values are 1, 3, and 5. The default value is 1."
}

variable "cluster_env_namespace_name" {
  default = "poc"
}

// VM Variables

variable "agent_count" {
  type        = "string"
  description = "Number of worker VMs to initially create"
  default     = "1"
}

variable "agent_vm_size" {
  type        = "string"
  default     = "Standard_D2_v2"
  description = "The size of the virtual machine used for the Kubernetes linux agents in the cluster."
}
