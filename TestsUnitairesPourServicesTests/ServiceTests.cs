using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestsUnitairesPourServices.Data;
using TestsUnitairesPourServices.Models;
using TestsUnitairesPourServices.Services;
using TestsUnitairesPourServices.Services;

namespace TestsUnitairesPourServicesTests
{
    [TestClass]
    public class ServiceTests
    {
        private ApplicationDBContext _db;
        private CatsService _catsService;
        private CatsService _catsService;

        private const int CHAT_AVEC_MAISON_ID = 1;
        private const int CHAT_SANS_MAISON_ID = 2;
        private const int MAISON_SOURCE_ID = 1;
        private const int MAISON_DESTINATION_ID = 2;
        private const int ID_INCONNU = 999;
        private const int MAISON_SOURCE_ID = 1;
        private const int MAISON_DESTINATION_ID = 2;
        private const int ID_INCONNU = 999;

        [TestInitialize]
        public void Init()
        {
            string dbname = "CatsServiceTests" + Guid.NewGuid().ToString();

            DbContextOptions<ApplicationDBContext> options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .UseLazyLoadingProxies(true)
                .Options;

            _db = new ApplicationDBContext(options);

            House House1 = new() { Id = MAISON_SOURCE_ID };
            House House2 = new() { Id = MAISON_DESTINATION_ID };

            Cat catAvecMaison = new()
            House House1 = new() { Id = MAISON_SOURCE_ID };
            House House2 = new() { Id = MAISON_DESTINATION_ID };

            Cat catAvecMaison = new()
            {
                Age = 12,
                House = House1,
                Name = "Toto",
                Id = CHAT_AVEC_MAISON_ID
                Id = CHAT_AVEC_MAISON_ID
            };
            Cat catSansMaison = new()
            Cat catSansMaison = new()
            {
                Age = 555,
                Name = "NoToto",
                Id = CHAT_SANS_MAISON_ID
                Id = CHAT_SANS_MAISON_ID
            };

            _db.AddRange([House1, catAvecMaison, catSansMaison]);
            _db.AddRange([House1, catAvecMaison, catSansMaison]);
            _db.SaveChangesAsync();
            
        }

        [TestCleanup]
        public void Dispose()
        {
            _db.Dispose();

        }

        [TestMethod]
        public async Task MoveNotNull()
        {
            Init();
            House house1 = await _db.House.Where(x => x.Id == MAISON_SOURCE_ID).SingleOrDefaultAsync();
            House house2 = await _db.House.Where(x => x.Id == MAISON_SOURCE_ID).SingleOrDefaultAsync();

            var result = _catsService.Move(CHAT_AVEC_MAISON_ID, house1, house2);

            Assert.IsNotNull(result);

            Dispose();
        }

        [TestMethod]
        public async Task Move_UnknownCatId_ReturnsNull()
        {
            Init();
            House house1 = await _db.House.Where(x => x.Id == MAISON_SOURCE_ID).SingleOrDefaultAsync();
            House house2 = await _db.House.Where(x => x.Id == MAISON_SOURCE_ID).SingleOrDefaultAsync();

            var result = _catsService.Move(ID_INCONNU, house1, house2);

            Assert.IsNull(result);

            Dispose();
        }
    }
}
