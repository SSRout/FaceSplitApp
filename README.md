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
Having 5 modules these are FaceApi, OrderApi, SharedLib, CustomerNotification and MvcWebClient.

```
	Facedetect-sln
	├── API	
	│   ├── FacesAPI
	│   ├── OrdersAPI
	├── SharedLib
	│   ├──MessagingQueue(class library)
	├── Webclient
	│   ├── FaceWEbMvc
	├──CustomerNotification
	│	├── EmailService(class library)
	│	├── NotificationService(ConsoleApp)
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
| EmailService | NETCore.MailKit |
| NotificationService |MassTransit |
| NotificationService |MassTransit.Extensions.DependencyInjection |
| NotificationService |MassTransit.RabbitMQ |
| NotificationService |Microsoft.Extensions.Configuration.Abstractions|
| NotificationService |Microsoft.Extensions.Hosting|
| NotificationService |System.Drawing.Common|
| NotificationService |MessagingQueue.csproj|
| NotificationService |EmailService.csproj|

###### Thanks To F. Frank Ozz
**✔️🍺 Happy Coding 👍😊**