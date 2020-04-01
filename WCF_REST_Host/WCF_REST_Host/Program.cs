using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCF_REST_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF REST Host ***");

            var host = new ServiceHost(typeof(EisService));

            var web = new WebHttpBinding();

            var ep = host.AddServiceEndpoint(typeof(IEisService), web, "http://localhost:1");
            ep.EndpointBehaviors.Add(new WebHttpBehavior() { AutomaticFormatSelectionEnabled = true });

            host.Open();
            Console.WriteLine("RESTful Service gestartet");
            Console.ReadLine();
            host.Close();

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }

    [ServiceContract]
    public interface IEisService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Eis")]
        IEnumerable<Eis> GetEisListe();

        [OperationContract]
        [WebGet(UriTemplate = "Eis?suche={suche}")]
        IEnumerable<Eis> SuchEis(string suche);

        [OperationContract]
        [WebGet(UriTemplate = "EisById?id={id}")]
        Eis GetById(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Eis")]
        void AddEis(Eis eis);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Eis")]
        void DeleteEis(Eis eis);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Eis")]
        void UpdateEis(Eis eis);
    }

    public class EisService : IEisService
    {
        static List<Eis> db = new List<Eis>();

        static EisService()
        {
            db.Add(new Eis() { Id = 1, Name = "Erdbeere", IstMilcheis = false });
            db.Add(new Eis() { Id = 2, Name = "Schoko", IstMilcheis = true });
            db.Add(new Eis() { Id = 3, Name = "Fischstäbchen", IstMilcheis = false });
        }

        public void AddEis(Eis eis)
        {
            db.Add(eis);
        }

        public void DeleteEis(Eis eis)
        {
            var old = GetById(eis.Id);
            if (old != null)
                db.Remove(old);
        }

        public Eis GetById(int id)
        {
            return db.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Eis> GetEisListe()
        {
            return db;
        }

        public IEnumerable<Eis> SuchEis(string suche)
        {
            return db.Where(x => x.Name.Contains(suche));
        }

        public void UpdateEis(Eis eis)
        {
            var old = GetById(eis.Id);
            DeleteEis(old);
            AddEis(eis);
        }
    }
}
