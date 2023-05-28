# WebAPI.Microservice.Sample
Web API Microservice that is a simple CRUD app only but demonstrates some best practices like CQRS, Mediator, Repository patterns...

## Docker command:

Build docker image:
```bash
docker build -t web-api-sample .
```

Run a container from the built image:
```bash
docker run -p 80:80 web-api-sample
```
Now you can check the api documentation of the project visiting following URL
http://localhost:80/swagger
