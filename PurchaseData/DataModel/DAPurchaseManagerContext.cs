using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace PurchaseData.DataModel
{
    public partial class PurchaseManagerEntities
    {
        //public PurchaseManagerEntities(bool indicator) : base("name=PurchaseManagerEntities")
        //{
        //    this.Configuration.LazyLoadingEnabled = indicator;
        //}
        public override int SaveChanges()
        {
            int res = 0;
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                res = base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                SqlException sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    return sqlException.Number;
                }
                if (ex is DbUpdateConcurrencyException)
                {
                    return 999;
                }
            }

            return res;
        }
    }

    //public class MyClass : PurchaseManagerEntities
    //{
    //    Configuration.LazyLoadingEnabled = false;
    //}
}
