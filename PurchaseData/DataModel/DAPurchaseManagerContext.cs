using System.Data.Entity;

namespace PurchaseData.DataModel
{
    public partial class PurchaseManagerEntities
    {
        public void SaveChanges(DbContextTransaction transactions)
        {
            try
            {
                base.SaveChanges();
            }
            catch (System.Exception)
            {
                transactions.Rollback();

            }
            //try
            //{
            //    //Thread.Sleep(wait);    

            //}
            //catch (Exception e)
            //{
            //    var ee = e.Message;
            //    //foreach (var eve in e.EntityValidationErrors)
            //    //{
            //    //    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //    //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //    //    foreach (var ve in eve.ValidationErrors)
            //    //    {
            //    //        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //    //            ve.PropertyName, ve.ErrorMessage);
            //    //    }
            //    //}
            //    //throw;
            //}
        }
    }
}
