using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NFC.DAO
{
    public class PlaceDAO : BasicDAO
    {
        public PlaceDAO() { }
        public List<Place> FindAll()
        {
            Table<Place> table = GetContext().Places;
            return table.ToList();
        }
        public Place FindByID(int id)
        {
            return GetContext().Places.Where(g => g.Id == id).FirstOrDefault();
        }
        public bool Insert(Place place)
        {
            GetContext().Places.InsertOnSubmit(place);
            GetContext().SubmitChanges();
            return true;
        }

        public bool Update(Place place)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, place);
            return true;
        }
        public bool Delete(int id)
        {
            Place place = GetContext().Places.SingleOrDefault(u => u.Id == id);
            GetContext().Places.DeleteOnSubmit(place);
            GetContext().SubmitChanges();
            return true;
        }
    }
}