#Step1 Creating Resource Group
az group create --name DemoResourceGroup --location eastus
#vm creation command
az vm create --resource-group 
DemoResourceGroup --name DemoVM --image
Win2022Datacenter --admin-username
azureuser --admin-password "Password@123"
#Open RDP PORT
az vm open-port --resource-group
DemoResourceGroup --name DemoVM --port
3389
# message
Write-Host "Azure VM deployment script completed successfully."  