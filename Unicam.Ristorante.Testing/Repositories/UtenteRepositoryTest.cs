using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Testing.Repository
{
    [TestFixture]
    public class UtenteRepositoryTest
    {

        private UtenteRepository _repository = new UtenteRepository(TestUtils.ctx);

        private Utente[] utentiTest = {
            new Utente()
            {
                Email = "marco.caputo@gmail.com",
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "Password123",
                Ruolo = Ruolo.Cliente
            },
            new Utente()
            {
                Email = "marco.caputo@hotmail.it"
            },
            new Utente()
            {
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "Password123",
                Ruolo = Ruolo.Cliente
            }
        };

        [TearDown]
        public async Task TearDownAsync()
        {
            TestUtils.DetachAllEntities();
            foreach (Utente utente in utentiTest) utente.Id = 0;
            await TestUtils.ClearRepositoryAsync(UtenteConfiguration.TableName);
        }

        [Test]
        public async Task ShouldAddUser()
        {
            await _repository.AggiungiAsync(utentiTest[0]);
            await _repository.SaveAsync();

            Assert.Pass();
        }

        [Test]
        public async Task ShouldRetrieveUser()
        {
            await _repository.AggiungiAsync(utentiTest[0]);
            await _repository.SaveAsync();

            Assert.That(await _repository.OttieniAsync(utentiTest[0].Id), Is.EqualTo(utentiTest[0]));
        }

        [Test]
        public async Task ShouldNotAddUser1()
        {
            await _repository.AggiungiAsync(utentiTest[1]);
            Assert.ThrowsAsync<DbUpdateException>(_repository.SaveAsync);
        }

        [Test]
        public async Task ShouldNotAddUser2()
        {
            await _repository.AggiungiAsync(utentiTest[2]);
            Assert.ThrowsAsync<DbUpdateException>(_repository.SaveAsync);
        }

        [Test]
        public async Task ShouldUpdateUser()
        {             
            await _repository.AggiungiAsync(utentiTest[0]);
            await _repository.SaveAsync();

            utentiTest[0].Nome = "Mario";
            _repository.Modifica(utentiTest[0]);
            await _repository.SaveAsync();

            var utenteModificato = await _repository.OttieniAsync(utentiTest[0].Id);
        
            Assert.That(utenteModificato.Nome, Is.EqualTo("Mario"));
        }

        [Test]
        public async Task ShouldDeleteUser()
        {
            await _repository.AggiungiAsync(utentiTest[0]);
            await _repository.SaveAsync();

            await _repository.EliminaAsync(utentiTest[0].Id);
            await _repository.SaveAsync();

            Assert.That(await _repository.OttieniAsync(utentiTest[0].Id), Is.Null);
        }
    }
}