# EventFinder

 Web application for discovering and managing technology events. It leverages IP geo-location to filter events based on user location and uses Cloudinary for cloud-based image storage. The project uses the Repository Pattern with Entity Framework Core, ensuring a clean and maintainable data access layer.

## 🌐 Live Domain
 [https://eventfinder.runasp.net](https://eventfinder.runasp.net)  
 
## Key Features
| Feature | Description |
|---------|-------------|
| 📍 Smart Discovery | Automatic location detection using IP geolocation |
| ☁️ Cloud Storage | Image management via Cloudinary integration |
| 🛡️ Secure Access | Role-based authentication with ASP.NET Identity |
| ⚙️ Clean Architecture | Repository pattern with EF Core data access |

## Technologies Used

**Core Stack**  
- ASP.NET MVC 8  
- C# 
- Microsoft SQL Server 2022  

**Data Access**  
- Entity Framework Core   
- Repository Pattern  
- Dependency Injection  

**Services & Integration**  
- ASP.NET Core Identity  
- [IP Geolocation API](https://ipinfo.io)  
- [Cloudinary](https://cloudinary.com) Media Management  

**Infrastructure**  
- [MonsterASP Hosting](https://www.monsterasp.net)
-  HTTPS/SSL Encryption

  ## Getting Started
### Prerequisites
- .NET 8 SDK
- SQL Server 2022
- Cloudinary account 
- IPinfo API token

### Installation

1. **Clone repository**
   ```bash
   git clone https://github.com/EsraSrg/EventFinder.git
   cd EventFinder
   ```

   2.**Restore NuGet Packages:**

   ```bash
   dotnet restore
   ```

   3.**Configure Settings:**

- Update your database connection string in appsettings.json and configure Cloudinary and IPInfo settings.
  
   4.**Run the database migrations:**

  ```bash
  dotnet ef database update
  ```
  5.**Run the App:**

  ```bash
  dotnet run
  ```
