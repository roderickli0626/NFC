using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NFC.DAO
{
    public class PlaceAccessDAO : BasicDAO
    {
        public PlaceAccessDAO() { }
        public List<PlaceAccess> FindAll()
        {
            Table<PlaceAccess> table = GetContext().PlaceAccesses;
            return table.ToList();
        }
        public PlaceAccess FindByID(int id)
        {
            return GetContext().PlaceAccesses.Where(g => g.Id == id).FirstOrDefault();
        }
        public List<PlaceAccess> FindByUserID(int userId)
        {
            return GetContext().PlaceAccesses.Where(g => g.UserID == userId).ToList();
        }
        public PlaceAccess FindByUserAndPlace(int userId, int placeID)
        {
            return GetContext().PlaceAccesses.Where(g => g.UserID == userId && g.PlaceID == placeID).FirstOrDefault();
        }
        public bool Insert(PlaceAccess place)
        {
            GetContext().PlaceAccesses.InsertOnSubmit(place);
            GetContext().SubmitChanges();
            return true;
        }

        public bool Update(PlaceAccess place)
        {
            GetContext().SubmitChanges();
            GetContext().Refresh(RefreshMode.OverwriteCurrentValues, place);
            return true;
        }
        public bool Delete(int id)
        {
            PlaceAccess place = GetContext().PlaceAccesses.SingleOrDefault(u => u.Id == id);
            GetContext().PlaceAccesses.DeleteOnSubmit(place);
            GetContext().SubmitChanges();
            return true;
        }
    }
}