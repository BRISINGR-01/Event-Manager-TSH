# TSH-2

## Set up

`./hooks/set-up`

## Build

Windows:
`docker build -f .\Web\Dockerfile -t web:latest .`

Linux:
`docker build -f ./Web/Dockerfile -t web:latest .`

## Run

`docker compose up`

### or docker run

`docker run -it -p 5000:80 -p 5001:443 --name TSH web:latest`

## Azure setup

```bash
docker run -it mcr.microsoft.com/azure-cli
az login
az account set --subscription 1c0ba348-e874-4a3d-b8c9-f51d3ec61022
az group create --name fontys-group --location europe
az container create --resource-group fontys-group --name tsh-2-container --image alexpopo`v33/tsh:latest`--dns-name-label tsh-2
az acr create --resource-group fontys-group --name tsh-acr --sku Basic
az acr login --name tsh-acr
az acr show --name tsh-acr --query loginServer --output table
docker tag tsh <acrLoginServer>/tsh:latest
docker push <acrLoginServer>/aci-tutorial-app:v1
az container create --resource-group myResourceGroup --name aci-tutorial-app --image <acrLoginServer>/aci-tutorial-app:v1 --cpu 1 --memory 1 --registry-login-server <acrLoginServer> --registry-username <service-principal-ID> --registry-password <service-principal-password> --ip-address Public --dns-name-label <aciDnsLabel> --ports 80

```

### clean up

`az group delete --name fontys-group`
