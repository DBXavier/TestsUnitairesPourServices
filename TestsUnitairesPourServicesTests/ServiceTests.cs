using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestsUnitairesPourServices.Data;
using TestsUnitairesPourServices.Models;

namespace TestsUnitairesPourServicesTests
{
    [TestClass]
    public class ServiceTests
    {
        private ApplicationDBContext _db;

        private const int CHAT_AVEC_MAISON_ID = 1;
        private const int CHAT_SANS_MAISON_ID = 2;

        [TestInitialize]
        public void Init()
        {
            string dbname = "CatsServiceTests" + Guid.NewGuid().ToString();

            DbContextOptions<ApplicationDBContext> options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .UseLazyLoadingProxies(true)
                .Options;

            _db = new ApplicationDBContext(options);

            House House1 = new();
            Cat cat1 = new()
            {
                Age = 12,
                House = House1,
                Name = "Toto",
                Id = 1
            };
            Cat cat2 = new()
            {
                Age = 555,
                Name = "NoToto",
                Id = 2
            };

            _db.AddRange([House1,cat1, cat2]);
            _db.SaveChangesAsync();
            
        }

        [TestCleanup]
        public void Dispose()
        {
            _db.Dispose();
        }

        [TestMethod]
        public void Test1()
        {

        }
    }
}
