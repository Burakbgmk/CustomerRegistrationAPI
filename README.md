# Customer Registration API
- Final API Project of the Link-Bilgisayar Bootcamp

## Project Summary and Purposes
- This project aims to provide functionalities such as keeping customer information alongside with commercial activity data belongs to these customers, authentication and authorization of users who are admin or editor, reports of statistical data about customers and endpoints to reach saved data.
- Project is developed with **C# and .Net Core 6**.
- **PostgreSql** is used as a relational-database.
- **Layered Architecture** is used in the project.
- **Quartz Library** is used for reporting.
- **RabbitMQ** is used as a message broker to apply watermark to images.
- Authorization is done for two different roles; an Admin role has authority for everything and Editor role only for adding, updating, and listing.

## Project Layout
![layers](https://user-images.githubusercontent.com/95534656/179264038-54d1e622-4e8f-419d-a2a2-eb965e5fbae4.JPG) 
- Project has 6 layers as shown in the figure.

### Core Layer
- This layer mainly consist of entities, dto's, and abstractions.
- Entities are inherited from a base entity since they all have Id.
- Services, Repositories and UnitOfWork classes have interfaces in here as an abstraction layer.

### Data Layer
- Generic and Customer Repositories, AppDbContext, and UnitOfWork classes are here.
- This layer is for extracting data from database and serve it to an upper layer.
- Since loading image files and detecting repeated phone numbers is neccessary only for customer entity, a CustomerRepository is generated along with GenericRepository.

### Service Layer
- This layer consist of services and mappers.
- Most of the bussiness actions are performed in this layer.
- Authentication services also in here.
- Since loading image files and detecting repeated phone numbers is neccessary only for customer entity, a CustomerService is generated along with GenericService.

### Report Layer
- Reporting, excel, emailsending, and uploading reports are being done in this layer.
- Reporting done by Quartz.Net.
- Uploading is done by loading reports as one to many relationship entities to database. One's for keeping track of report Id, Many is for reports detail since excel report is in the format of a list.
![countreport](https://user-images.githubusercontent.com/95534656/179284681-5e4c2f76-94bf-4e3e-b498-a32fa57ecaa5.JPG)
![countdetail](https://user-images.githubusercontent.com/95534656/179284665-b89de7e4-f72a-42fb-a67f-98e01474a2c0.JPG)
- These reports can be downloaded as excel files using ReportsController
- Top Five Customers by Activity reports can only be seen by Admin.

### Shared Library
- Reponse Dto's, configurations, background services such as image watermarking, and RabbitMQ publisher and clients are in this section.

### API Layer
- Controllers consists of Customer, Commercial Activity, Authentication, User, and Report.
- ReportController is for downloading past reports according to their dates and Id's.


