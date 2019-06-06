#az vm create --resource-group planty-resource-group --name vm-income-estimate --size Standard_B2s --image Canonical:UbuntuServer:18.04-LTS:latest --admin-username azureuser --generate-ssh-key --custom-data az-vm-custom-data.txt --storage-sku Standard_LRS --os-disk-size-gb 10
az vm create --resource-group planty-resource-group --name vm-income-estimate --size Standard_B2s --image Canonical:UbuntuServer:18.04-LTS:latest --admin-username azureuser --generate-ssh-key --custom-data az-vm-custom-data.txt --storage-sku Standard_LRS

az vm open-port --port 8080 --resource-group planty-resource-group --name vm-income-estimate

