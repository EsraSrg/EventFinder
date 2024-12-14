using Microsoft.AspNetCore.Identity;


namespace RunGroopWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Events.Any())
                {
                    context.Events.AddRange(new List<Event>()
                    {
                        new Event()
                        {
                            Title = "Tech&Dev Talks",
                            Image = "/img/tech&dev talks.webp",
                            Description = "Bilgi teknolojileri, yazılım, bilişim kariyeri düşünen öğrenciler ve yeni mezunlar sektörün önde gelen şirketlerini tanıma ve merak ettikleri soruların cevaplarını şirketlerden duyma fırsatına ulaşıyor.",
                            EventCategory = EventCategory.Meetup,
                            Address = new Address()
                            {
                                Street = "street",
                                City = "Ankara",
                                State = "state"
                            }
                         },
                        new Event()
                        {
                            Title = "Tech&Dev Talks",
                            Image = "/img/tech&dev talks.webp",
                            Description = "Bilgi teknolojileri, yazılım, bilişim kariyeri düşünen öğrenciler ve yeni mezunlar sektörün önde gelen şirketlerini tanıma ve merak ettikleri soruların cevaplarını şirketlerden duyma fırsatına ulaşıyor.",
                            EventCategory = EventCategory.Meetup,
                            Address = new Address()
                            {
                                Street = "street",
                                City = "Ankara",
                                State = "state"
                            }
                         },
                         new Event()
                        {
                            Title = "Tech&Dev Talks",
                            Image = "/img/tech&dev talks.webp",
                            Description = "Bilgi teknolojileri, yazılım, bilişim kariyeri düşünen öğrenciler ve yeni mezunlar sektörün önde gelen şirketlerini tanıma ve merak ettikleri soruların cevaplarını şirketlerden duyma fırsatına ulaşıyor.",
                            EventCategory = EventCategory.Meetup,
                            Address = new Address()
                            {
                                Street = "street",
                                City = "Ankara",
                                State = "state"
                            }
                         },
                        new Event()
                        {
                            Title = "Tech&Dev Talks",
                            Image = "/img/tech&dev talks.webp",
                            Description = "Bilgi teknolojileri, yazılım, bilişim kariyeri düşünen öğrenciler ve yeni mezunlar sektörün önde gelen şirketlerini tanıma ve merak ettikleri soruların cevaplarını şirketlerden duyma fırsatına ulaşıyor.",
                            EventCategory = EventCategory.Meetup,
                            Address = new Address()
                            {
                                Street = "street",
                                City = "Ankara",
                                State = "state"
                            }
                         }
                    });
                    context.SaveChanges();
                }


            }
        }
    }
}



//     public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
//     {
//         using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//         {
//             //Roles
//             var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//             if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
//                 await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//             if (!await roleManager.RoleExistsAsync(UserRoles.User))
//                 await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

//             //Users
//             var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//             string adminUserEmail = "teddysmithdeveloper@gmail.com";

//             var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
//             if (adminUser == null)
//             {
//                 var newAdminUser = new AppUser()
//                 {
//                     UserName = "teddysmithdev",
//                     Email = adminUserEmail,
//                     EmailConfirmed = true,
//                     Address = new Address()
//                     {
//                         Street = "123 Main St",
//                         City = "Charlotte",
//                         State = "NC"
//                     }
//                 };
//                 await userManager.CreateAsync(newAdminUser, "Coding@1234?");
//                 await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
//             }

//             string appUserEmail = "user@etickets.com";

//             var appUser = await userManager.FindByEmailAsync(appUserEmail);
//             if (appUser == null)
//             {
//                 var newAppUser = new AppUser()
//                 {
//                     UserName = "app-user",
//                     Email = appUserEmail,
//                     EmailConfirmed = true,
//                     Address = new Address()
//                     {
//                         Street = "123 Main St",
//                         City = "Charlotte",
//                         State = "NC"
//                     }
//                 };
//                 await userManager.CreateAsync(newAppUser, "Coding@1234?");
//                 await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
//             }
//         }
//     }
// }
// }