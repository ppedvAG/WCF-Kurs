using PizzaService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PizzaService.Host
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class PService : IPizzaService
    {
        public IEnumerable<Pizza> GetPizzaListe()
        {
            yield return new Pizza() { Name = "Margherita", Preis = 8.90m };
            yield return new Pizza() { Name = "Salami", Preis = 9.50m };
            yield return new Pizza() { Name = "Schinken", Preis = 12.90m };
            yield return new Pizza() { Name = "Hawai", Preis = 5000m };
        }


        static List<Bestellung> Bestellungen { get; set; } = new List<Bestellung>();

        public void SendBestellung(Bestellung bestellung)
        {
            if (bestellung.Pizzen == null || bestellung.Pizzen.Count == 0)
                throw new FaultException<BestellError>(new BestellError()
                { Bestellung = bestellung, Fehlernummer = 7 },
                "Keine Pizzas in der Bestellung");

            Bestellungen.Add(bestellung);

            Console.WriteLine($"Bestellung am {bestellung.Bestelldatum} von {bestellung.Besteller}");
            foreach (var p in bestellung.Pizzen)
            {
                Console.WriteLine($"\t{p.Name} {p.Preis:c}");
            }
        }

        public IEnumerable<Bestellung> GetBestellungen()
        {
            return Bestellungen;
        }
    }
}
