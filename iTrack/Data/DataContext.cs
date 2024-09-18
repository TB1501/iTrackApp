using Bogus;
using iTrack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static iTrack.Models.Client;
using static System.Net.WebRequestMethods;

namespace iTrack.Data;

public class DataContext:IdentityDbContext
{
    public DbSet<Client> Clients { get; set; }
    public DataContext(DbContextOptions options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>().HasData(GetClients());
    }

    private List<Client> GetClients()
    {
        var clients = new List<Client>();

        var faker = new Faker("en"); // Bogus library to generate fake data

        for (int i = 1; i < 10; i++)
        {
            var client = new Client
            {
                Id = i,
                Name = faker.Name.FullName(),
				Email = faker.Internet.Email(),
                ImgUrl= $"https://api.dicebear.com/9.x/pixel-art/svg?seed={i}",
                Enrolment = faker.Date.Past(),
                ServicePrice = faker.Finance.Amount(25, 100),
                Status = GetRandomStatus(),
                Debt = faker.Finance.Amount(0, 100)
            };

            clients.Add(client);
        }

        return clients;
       
    }



    private ClientStatus GetRandomStatus()
    {
        var radnom = new Random();
        var types = Enum.GetValues(typeof(ClientStatus));

        return (ClientStatus)types.GetValue(radnom.Next(types.Length));
    }

 
}
