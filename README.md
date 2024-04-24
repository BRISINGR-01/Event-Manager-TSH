# TSH Event Manager

My second-semester project at Fontys. It was inspired from my stay at TSH (The Social Hub) and my work there as a part of the event committee. I even had the chance to interview the event organizer on how to accomodate the app to the needs of the company. You can check out more details in the **Documents** folder.

## Goal
An event management system designed specifically for TSH. Its main aim is to combine different systems into one – creating/joining events, sharing photos, scanning/storing QR codes, events promotion, and a means communication about changes. It will provide security but keep an easy-to-use interface with well-crafted design which fits the company. Among shared functionality there will be role-specific features for event-managers and students as well as mixed ones for students on the event committee. The roles are defined as such: students will be able to interact with the events and other features as “clients” and event-organizers will act as “admins” by creating and managing the content on the platform. Event committee participants will have common restrictions with other students and partial access to a few features reserved to the organizers. The design is going to be responsive and the web application – downloadable. That way it will be accessible from all devices, unlike the desktop application which will serve as administration panel mainly for accounts and branches management. All employees, students, events, and other content will be branch-specific and accessible only within members of the same branch. Only Administrators will have access to all information and privileges to access the administration panel.

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

#### Clean up

`az group delete --name fontys-group`
