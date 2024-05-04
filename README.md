# climathistory

## Docker

### Build images

To build the APIs, execute this command where the corresponding Dockerfile is:

```
docker build --pull -t weatherapi .
```


### Start images

If you don't use the compose.yaml file and want to start the image independently:

```
docker run --rm -it -p 4000:4000 -e ASPNETCORE_URLS=https://localhost:4000 -e ASPNETCORE_ENVIRONMENT=Development -v %APPDATA%\microsoft\UserSecrets:/root/.microsoft/usersecrets -v %USERPROFILE%\.aspnet\https:/root/.aspnet/https/ weatherapi
```


### Start Docker compose

In the directory where compose.yaml is:

```
docker-compose up
```

### Push an image to docker hub

```
docker login
docker tag <image id> fredericsivignon/climathistory:<version>
docker push fredericsivignon/climathistory:<version>

```

<image id> is the ID of the image to push (you can copy it from Docker Desktop)

`<version>` could be any label like "version1.0". If not specified, the "lastest" label for the tag is set.
For example, right now, it's something like "api_v0.1" for API, "front_v0.1" for front.


### Synology stuffs

SSH:
ssh Frederic@magellanstore


Error when running docker-compose up:

Frederic@MagellanStore:/volume1/docker$ docker-compose up
WARN[0000] The "APPDATA" variable is not set. Defaulting to a blank string.
WARN[0000] The "USERPROFILE" variable is not set. Defaulting to a blank string.
permission denied while trying to connect to the Docker daemon socket at unix:///var/run/docker.sock: Get "http://%2Fvar%2Frun%2Fdocker.sock/v1.24/containers/json?all=1&filters=%7B%22label%22%3A%7B%22com.docker.compose.project%3Ddocker%22%3Atrue%7D%7D": dial unix /var/run/docker.sock: connect: permission denied

> sudo synogroup --add docker Frederic
> sudo chgrp docker /var/run/docker.sock

It fixes the previous issue. But not solve all errors.

Pour se connecter en root sur DSM:
sudo -i
depuis un commande prompt SSH

#### Directory location of docker images and containers
/volume1/@docker/

'sudo -i' to have access rights


#### Téléchargement d'images (sans passer par un projet)

URL des repos:
https://hub.docker.com/_/postgres
https://hub.docker.com/r/fredericsivignon/climathistory
https://hub.docker.com/r/fredericsivignon/weatherapi

#### Accéder à la console du container en cours d'execution

Pour accéder à la console du container docker créé (et vérifier ce qu'il y a dans /usr/share/nginx/html):
docker exec -it fredericsivignon+-climathistory-1 /bin/sh

climathistory.FamilyDS.net