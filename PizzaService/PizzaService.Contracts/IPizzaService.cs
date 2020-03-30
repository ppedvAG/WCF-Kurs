using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace PizzaService.Contracts
{
    [ServiceContract]
    public interface IPizzaService
    {
        [OperationContract]
        IEnumerable<Pizza> GetPizzaListe();

        [OperationContract]
        IEnumerable<Bestellung> GetBestellungen();

        [OperationContract]
        [FaultContract(typeof(BestellError))]
        void SendBestellung(Bestellung bestellung);
    }


    public class BestellError
    {
        public Bestellung Bestellung { get; set; }
        public int Fehlernummer { get; set; }
    }

    [DataContract]
    public class Pizza
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember(Name = "Pice")]
        public decimal Preis { get; set; }
    }

    [DataContract(Name = "Order")]
    public class Bestellung
    {

        [DataMember(Name = "Orderer")]
        public string Besteller { get; set; }
        [DataMember(Name = "OrderDate")]
        public DateTime Bestelldatum { get; set; }
        [DataMember(Name = "Pizzas")]
        public List<Pizza> Pizzen { get; set; } = new List<Pizza>();
    }
}
