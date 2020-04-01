using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloASP.Models
{
    public class EisDataService 
    {
        static List<Eis> db = new List<Eis>();

        static EisDataService()
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
