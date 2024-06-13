using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Ristorante.Application.Services;

namespace Unicam.Ristorante.Testing.Services
{
    internal class OrdineServiceTest
    {
        private OrdineService service = new OrdineService(new OrdineRepository(TestUtils.ctx), new IndirizzoRepository(TestUtils.ctx));

        private static Portata[] portateTest = {
            new Portata()
            {
                Nome = "Lasagne",
                Prezzo = 8.5M,
                Tipo = TipoPortata.Primo
            },
            new Portata()
            {
                Nome = "Tagliatelle al Ragù",
                Prezzo = 7.5M,
                Tipo = TipoPortata.Primo
            },
            new Portata()
            {
                Nome = "Arista",
                Prezzo = 10M,
                Tipo = TipoPortata.Secondo
            },
            new Portata()
            {
                Nome = "Salmone",
                Prezzo = 12M,
                Tipo = TipoPortata.Secondo
            },
            new Portata() {
                Nome = "Insalata",
                Prezzo = 3M,
                Tipo = TipoPortata.Contorno
            },
            new Portata()
            {
                Nome = "Tiramisù",
                Prezzo = 4.50M,
                Tipo = TipoPortata.Dolce
            }
        };

        private static Ordine[] ordiniTest = {
            new Ordine()
            {
                Voci = new List<VoceOrdine>()
                {
                    new VoceOrdine(){Portata = portateTest[0], Quantita = 1},
                    new VoceOrdine(){Portata = portateTest[1], Quantita = 1}
                }
            },
            new Ordine()
            {
                Voci = new List<VoceOrdine>()
                {
                    new VoceOrdine(){Portata = portateTest[0], Quantita = 2},
                    new VoceOrdine(){Portata = portateTest[2], Quantita = 1},
                    new VoceOrdine(){Portata = portateTest[4], Quantita = 1},
                    new VoceOrdine(){Portata = portateTest[5], Quantita = 1}
                }
            },
            new Ordine()
            {
                Voci = new List<VoceOrdine>()
                {
                    new VoceOrdine(){Portata = portateTest[0], Quantita = 2},
                    new VoceOrdine(){Portata = portateTest[1], Quantita = 3},
                    new VoceOrdine(){Portata = portateTest[2], Quantita = 2},
                    new VoceOrdine(){Portata = portateTest[3], Quantita = 5},
                    new VoceOrdine(){Portata = portateTest[4], Quantita = 5},
                    new VoceOrdine(){Portata = portateTest[5], Quantita = 3},
                }
            }
        };

        /*
        [Test]
        public void ShouldComputePrice1()
        {
            Assert.That(service.CalcolaPrezzo(ordiniTest[0]), Is.EqualTo(16M));
        }

        [Test]
        public void ShouldComputePrice2()
        {
            Assert.That(service.CalcolaPrezzo(ordiniTest[1]), Is.EqualTo(31.9M));
        }

        [Test]
        public void ShouldComputePrice3()
        {
            Assert.That(service.CalcolaPrezzo(ordiniTest[2]), Is.EqualTo(139.7M));
        }
        */

        //TODO: Fare OrdineControllerTest e integrare questi test qui
    }
}
