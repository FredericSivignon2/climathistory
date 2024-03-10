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
