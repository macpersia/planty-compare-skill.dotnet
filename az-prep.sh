az vm create --resource-group planty-resource-group --name my-docker-vm --image UbuntuLTS --admin-username azureuser --generate-ssh-key --custom-data az-vm-custom-data.txt

az vm open-port --port 8080 --resource-group planty-resource-group --name my-docker-vm

