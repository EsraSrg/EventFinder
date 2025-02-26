# EventFinder

 ASP.NET MVC web application for discovering and managing technology events. It leverages IP geo-location to filter events based on user location and uses Cloudinary for cloud-based image storage. The project uses the Repository Pattern with Entity Framework Core, ensuring a clean and maintainable data access layer.

## Key Features

- **Event Discovery:** Find technology events near you using IP geo-location.
- **Cloud Image Management:** Store and manage event images in the cloud with Cloudinary.
- **Robust Architecture:** Built with the Repository Pattern and EF Core for efficient data handling.
- **User Management:** Supports event creation, editing, and admin-level controls for managing events.

## Technologies Used

- ASP.NET MVC & .NET 8.0
- C#
- Entity Framework Core
- Identity
- Repository Pattern
- IP Geolocation API
- Cloudinary

  ## Getting Started

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/EsraSrg/EventFinder.git
   cd EventFinder
   ````

 2.**Restore NuGet Packages:**

   ```bash
   dotnet restore
   ````
3.**Configure Settings:**

- Update your database connection string in appsettings.json and configure Cloudinary settings.

 4.**Run the App:**
 
 ```bash
dotnet run
```

