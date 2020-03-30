using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyKaffeeService.Host
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public IEnumerable<Kaffee> GetKaffeeListe()
        {
            yield return new Kaffee() { Hersteller = "Tschibo", Sorte = "Leckere Brühe", GanzeBohnen = false };
            yield return new Kaffee() { Hersteller = "Lavazza", Sorte = "Schwarzes Öl", GanzeBohnen = true};
            yield return new Kaffee() { Hersteller = "Schümli", Sorte = "Dunkler Käse", GanzeBohnen = true};
        }
    }
}
