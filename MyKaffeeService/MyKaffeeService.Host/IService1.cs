using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyKaffeeService.Host
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        IEnumerable<Kaffee> GetKaffeeListe();
    }

    public class Kaffee
    {
        public string Sorte { get; set; }

        public string Hersteller { get; set; }

        public bool GanzeBohnen { get; set; }

    }
}
