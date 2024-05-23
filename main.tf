terraform {
	required_provieders {
	     azurerm = {
		     source = "hashicorp/azurerm"
		     version = "2.96.0"
		 }
	}
}

provider "azurerm" {
     features {

	 }
}

resource "azurerm_resource_group" "lt_rg" {
	name = "lifetrackerrg"
	location = "Egypt"
}