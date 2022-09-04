# microservices-commandservice

Les Jackson - .NET Microservices course (https://youtu.be/DgVjEo3OGBI)

### Command Service

This service is a .NET 6 Web API which implements GET and POST operations for the command resource. 
* uses an in-memory database. 
* has a background service which subscribes to the message queue and processes the messages published by the Platform service; when a new platform is added to the Platform service, a message is published to the queue and the new platform is also added to the Command service's database.
* uses gRPC to load all the existing platforms from the Platform service.
<br/>
With the message queue and the gRPC communication, the platform resources are always kept in sync between the two services.

### Kubernetes architecture

![Kubernetes architecture](https://user-images.githubusercontent.com/62215591/188326182-557c9b75-7c5c-4a4e-b837-f39f42a95c01.png)
