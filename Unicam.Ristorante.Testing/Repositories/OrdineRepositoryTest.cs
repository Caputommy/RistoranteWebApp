using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Testing.Repository
{
    internal class OrdineRepositoryTest
    {
        private OrdineRepository _ordineRepository = new OrdineRepository(TestUtils.ctx);
        private UtenteRepository _utenteRepository = new UtenteRepository(TestUtils.ctx);

        
        private static Indirizzo[] indirizziTest =
        {
            new Indirizzo()
            {
                Via = "Via Roma",
                NumeroCivico = "1",
                Citta = "Macerata",
                CAP = "62100"
            },
            new Indirizzo() {
                Via = "Via Roma",
                NumeroCivico = "2",
                Citta = "Macerata",
                CAP = "62100"
            },
            new Indirizzo()
            {
                Via = "Via Roma",
                NumeroCivico = "1",
                Citta = "Macerata",
                CAP = "621000000"
            },
        };

        private static Utente[] utentiTest =
        {
            new Utente()
            {
                Email = "mario.rossi@gmail.com",
                Password = "Password123",
                Ruolo = Ruolo.Cliente
            },
            new Utente()
            {
                Email = "luigi.bianchi@gmail.com",
                Password = "Password123",
                Ruolo = Ruolo.Cliente
            }
        };

        private static Ordine[] ordiniTest = {
            new Ordine()
            {
                Data = DateTime.Now,
                IndirizzoConsegna = indirizziTest[0],
                Utente = utentiTest[0]
            },
            new Ordine()
            {
                Data = DateTime.Now,
                IndirizzoConsegna = indirizziTest[2],
                Utente = utentiTest[1]
            },
        };

        
        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            foreach (Ordine ordine in ordiniTest) ordine.Numero = 0;
            foreach (Utente utente in utentiTest) utente.Id = 0;
            foreach (Indirizzo indirizzo in indirizziTest) indirizzo.Id = 0;
            await TestUtils.ClearRepositoryAsync(VoceOrdineConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(OrdineConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(IndirizzoConfiguration.TableName);
            await TestUtils.ClearRepositoryAsync(UtenteConfiguration.TableName);
        }

        [Test]
        public async Task ShouldAddOrder()
        {
            await _ordineRepository.AggiungiAsync(ordiniTest[0]);
            await _ordineRepository.SaveAsync();

            Assert.Pass();
        }
        
        [Test]
        public async Task ShouldRetrieveOrder()
        {
            await _ordineRepository.AggiungiAsync(ordiniTest[0]);
            await _ordineRepository.SaveAsync();

            Assert.That(await _ordineRepository.OttieniAsync(ordiniTest[0].Numero), Is.EqualTo(ordiniTest[0]));
        }

        [Test]
        public async Task ShouldNotAddOrder()
        {
            await _ordineRepository.AggiungiAsync(ordiniTest[1]);
            Assert.ThrowsAsync<DbUpdateException>(_ordineRepository.SaveAsync);
        }

        [Test]
        public async Task ShouldUpdateOrder()
        {
            await _ordineRepository.AggiungiAsync(ordiniTest[0]);
            await _ordineRepository.SaveAsync();

            ordiniTest[0].IndirizzoConsegna = indirizziTest[1];
            _ordineRepository.Modifica(ordiniTest[0]);
            await _ordineRepository.SaveAsync();

            var ordineModificato = await _ordineRepository.OttieniAsync(ordiniTest[0].Numero);

            Assert.That(ordineModificato.IndirizzoConsegna.NumeroCivico, Is.EqualTo("2"));
        }

        [Test]
        public async Task ShouldDeleteOrder()
        {
            await _ordineRepository.AggiungiAsync(ordiniTest[0]);
            await _ordineRepository.SaveAsync();

            await _ordineRepository.EliminaAsync(ordiniTest[0].Numero);
            await _ordineRepository.SaveAsync();

            Assert.That(await _ordineRepository.OttieniAsync(ordiniTest[0].Numero), Is.Null);
        }
    }
}
