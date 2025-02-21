using Microsoft.AspNetCore.Identity;


namespace EventFinder.Data
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
            Image = "/img/tech-dev-talks.webp",
            Description = "Bilgi teknolojileri, yazılım, bilişim kariyeri düşünen öğrenciler ve yeni mezunlar sektörün önde gelen şirketlerini tanıma fırsatı buluyor.",
            EventCategory = EventCategory.Meetup,
            Address = new Address()
            {
                Street = "Teknopark Caddesi",
                City = "Ankara",
                State = "Ankara"
            }
        },
        new Event()
        {
            Title = "AI & Machine Learning Conference",
            Image = "/img/ai-ml-conference.webp",
            Description = "Makine öğrenmesi ve yapay zeka alanındaki son gelişmelerin konuşulacağı bir konferans.",
            EventCategory = EventCategory.Conference,
            Address = new Address()
            {
                Street = "Taksim Meydanı",
                City = "İstanbul",
                State = "İstanbul"
            }
        },
        new Event()
        {
            Title = "Cyber Security Workshop",
            Image = "/img/cyber-security-workshop.webp",
            Description = "Siber güvenlik uzmanlarıyla birlikte gerçek zamanlı saldırılara karşı korunma yöntemlerini öğrenin.",
            EventCategory = EventCategory.Workshop,
            Address = new Address()
            {
                Street = "Bilgi Üniversitesi Kampüsü",
                City = "İstanbul",
                State = "İstanbul"
            }
        },
        new Event()
        {
            Title = "Blockchain & Web3 Summit",
            Image = "/img/blockchain-summit.webp",
            Description = "Blockchain teknolojileri, DeFi ve Web3 ekosistemine dair derinlemesine sunumlar.",
            EventCategory = EventCategory.Summit,
            Address = new Address()
            {
                Street = "Cumhuriyet Caddesi",
                City = "İzmir",
                State = "İzmir"
            }
        },
        new Event()
        {
            Title = "Cloud Computing Bootcamp",
            Image = "/img/cloud-computing-bootcamp.webp",
            Description = "AWS, Azure ve Google Cloud gibi popüler platformlarda bulut bilişim eğitimi.",
            EventCategory = EventCategory.Bootcamp,
            Address = new Address()
            {
                Street = "ODTÜ Teknokent",
                City = "Ankara",
                State = "Ankara"
            }
        },
        new Event()
        {
            Title = "Open Source Contribution Day",
            Image = "/img/open-source-day.webp",
            Description = "Açık kaynak projelerine katkıda bulunmak isteyen geliştiriciler için özel bir etkinlik.",
            EventCategory = EventCategory.Meetup,
            Address = new Address()
            {
                Street = "Koç Üniversitesi",
                City = "İstanbul",
                State = "İstanbul"
            }
        },

        new Event()
        {
            Title = "VR & AR Hackathon",
            Image = "/img/vr-ar-hackathon.webp",
            Description = "Artırılmış ve sanal gerçeklik projeleri geliştirerek yarışma şansı yakalayın.",
            EventCategory = EventCategory.Hackathon,
            Address = new Address()
            {
                Street = "BTK Teknoloji Kampüsü",
                City = "Ankara",
                State = "Ankara"
            }
        },
        new Event()
        {
            Title = "Gaming & Game Development Conference",
            Image = "/img/gaming-conference.webp",
            Description = "Oyun geliştirme dünyasında Unity, Unreal Engine ve yapay zeka destekli oyunlar üzerine konuşmalar.",
            EventCategory = EventCategory.Conference,
            Address = new Address()
            {
                Street = "GameX Expo Center",
                City = "İstanbul",
                State = "İstanbul"
            }
        },
        new Event()
        {
            Title = "Women in Tech Meetup",
            Image = "/img/women-in-tech.webp",
            Description = "Kadın teknoloji liderleri ve geliştiricilerle bir araya gelip deneyim paylaşımı yapın.",
            EventCategory = EventCategory.Meetup,
            Address = new Address()
            {
                Street = "Kadıköy Teknoloji Merkezi",
                City = "İstanbul",
                State = "İstanbul"
            }
        }
    });

                    context.SaveChanges();
                }


            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "esergiser@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "esraSrg",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}






