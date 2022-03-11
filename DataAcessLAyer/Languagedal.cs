using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLAyer
{
    public class Languagedal
    {
        ProgramingDbEntities db = new ProgramingDbEntities();

        public IEnumerable<Language> GetAll()
        {
            return db.Language;
        }

        public Language GetById(int id)
        {
            return db.Language.Find(id);
        }
        public Language Create(Language language)
        {
            db.Language.Add(language);
            db.SaveChanges();
            return language;
        }
        public Language UpdateLanguage(int id ,Language language)
        {
          db.Entry(language).State =System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return language;
            
        }
        public void DeleteLanguage(int id)
        {
            var silinen = db.Language.Find(id);
            db.Language.Remove(silinen);
            db.SaveChanges();
        }
        public bool IsThereAnyLanguage(int id)
        {
            return db.Language.Any(x => x.ID == id);
        }
    }
}
