using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Testing.Repository
{
    internal class PortataRepositoryTest
    {
        private PortataRepository _repository = new PortataRepository(TestUtils.ctx);

        private Portata[] portateTest = {
            new Portata()
            {
                Nome = "Lasagne",
                Prezzo = 8.5M,
                Tipo = TipoPortata.Primo
            },
            new Portata()
            {
                Prezzo = 6M,
                Tipo = TipoPortata.Secondo
            },
            new Portata()
            {
                Nome = "Tiramisù",
                Prezzo = 10.501M,
                Tipo = TipoPortata.Dolce
            }
        };

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            foreach (Portata portata in portateTest) portata.Id = 0;
            await TestUtils.ClearRepositoryAsync(PortataConfiguration.TableName);
        }

        [Test]
        public async Task ShouldAddCourse()
        {
            await _repository.AggiungiAsync(portateTest[0]);
            await _repository.SaveAsync();

            Assert.Pass();
        }

        [Test]
        public async Task ShouldRetrieveCourse()
        {
            await _repository.AggiungiAsync(portateTest[0]);
            await _repository.SaveAsync();

            Assert.That(await _repository.OttieniAsync(portateTest[0].Id), Is.EqualTo(portateTest[0]));
        }

        [Test]
        public async Task ShouldNotAddCourse()
        {
            await _repository.AggiungiAsync(portateTest[1]);
            Assert.ThrowsAsync<DbUpdateException>(_repository.SaveAsync);
        }

        [Test]
        public async Task ShouldTruncatePrice()
        {
            var prezzoOrig = portateTest[2].Prezzo;
            await _repository.AggiungiAsync(portateTest[2]);
            await _repository.SaveAsync();

            TestUtils.ctx.Entry(portateTest[2]).State = EntityState.Detached;
            var portataSalvata = await _repository.OttieniAsync(portateTest[2].Id);

            Assert.That(portataSalvata.Prezzo, Is.Not.EqualTo(prezzoOrig));
        }

        [Test]
        public async Task ShouldUpdateCourse()
        {
            var entries = TestUtils.ctx.ChangeTracker.Entries<Portata>();
            await _repository.AggiungiAsync(portateTest[0]);
            await _repository.SaveAsync();

            portateTest[0].Nome = "Pasta al forno";
            _repository.Modifica(portateTest[0]);
            await _repository.SaveAsync();

            var portataModificata = await _repository.OttieniAsync(portateTest[0].Id);

            Assert.That(portataModificata.Nome, Is.EqualTo("Pasta al forno"));
        }

        [Test]
        public async Task ShouldDeleteCourse()
        {
            await _repository.AggiungiAsync(portateTest[0]);
            await _repository.SaveAsync();

            await _repository.EliminaAsync(portateTest[0].Id);
            await _repository.SaveAsync();

            Assert.That(await _repository.OttieniAsync(portateTest[0].Id), Is.Null);
        }
    }
}
