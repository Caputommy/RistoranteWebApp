using Microsoft.EntityFrameworkCore;

namespace Unicam.Ristorante.Testing
{
    [TestFixture]
    public class UserRepositoryTest
    {

        private UtenteRepository _repository = new UtenteRepository(TestsUtil.ctx);

        private Utente[] utentiTest = {
            new Utente()
            {
                Email = "marco.caputo@gmail.com",
                Nome = "Marco",
                Cognome = "Caputo",
                Password = "password123",
                Ruolo = Ruolo.Cliente
            },
            new Utente()
            {
                Email = "marco.caputo@hotmail.it"
            }
        };

        [TearDown]
        public async Task TearDownAsync()
        {
            foreach (Utente utente in utentiTest)
            {
                TestsUtil.ctx.Entry(utente).State = EntityState.Detached;
                utente.Id = 0;
            }
            await TestsUtil.ClearRepositoryAsync(UtenteConfiguration.TableName);
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
            var test = utentiTest[0].Email;
            await _repository.AggiungiAsync(utentiTest[0]);
            await _repository.SaveAsync();

            Assert.That(await _repository.OttieniAsync(utentiTest[0].Id), Is.EqualTo(utentiTest[0]));
        }

        [Test]
        public async Task ShouldNotAddUser()
        {
            await _repository.AggiungiAsync(utentiTest[1]);
            Assert.ThrowsAsync<DbUpdateException>(_repository.SaveAsync);
        }
    }
}