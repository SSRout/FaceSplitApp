## Human Face Detection and Crop

It will take one photo and detect human faces and split each human faces in different image files.

### Tech Used:

  - .Net 5
  - Sql Server
  - Opencv4
  - Docker
  - Rabbitmq
  - MassTransit


### Project Structure

In this applicatoin we will use microservice architecture to communicate between modules,
Having 4 modules these are FaceApi, OrderApi, SharedLib and MvcWebClient.

```
	Facedetect-sln
	├── API	
	│   ├── FacesAPI
	│   ├── OrdersAPI
	├── SharedLib(class library)
	├── MvcWebclient
	├── Test
```

##### Docker Commamnds:

```
#To run Sqlserver In docker 
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@$$w0rd"  --name Ordersmssql -p 1445:1443 mcr.microsoft.com/mssql/server:latest
```

Project And Referenced files:

| Project  | References/Plugin |
| ------ | ------ |
| FacesAPI | OpenCvSharp4.Windows |
| OrdersAPI |MassTransit |
| OrdersAPI |MassTransit.Extensions.DependencyInjection |
| OrdersAPI |MassTransit.RabbitMQ |
| OrdersAPI |Microsoft.EntityFrameworkCore |
| OrdersAPI |Microsoft.EntityFrameworkCore.SqlServer |
| OrdersAPI |Microsoft.EntityFrameworkCore.Tools |
| OrdersAPI |MessagingQueue.csproj|
| FaceWebMNC | Microsoft.VisualStudio.Web.CodeGeneration.Design |
| FaceWebMNC | MassTransit |
| FaceWebMNC | MassTransit.Extensions.DependencyInjection |
| FaceWebMNC | MassTransit.RabbitMQ |
| FaceWebMNC | Refit |

###### Thanks To F. Frank Ozz
**✔️🍺 Happy Coding 👍😊**