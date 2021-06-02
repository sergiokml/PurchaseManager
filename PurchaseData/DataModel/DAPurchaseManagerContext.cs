using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace PurchaseData.DataModel
{
    public partial class PurchaseManagerEntities
    {
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
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    return sqlException.Number;
                }
            }
            return res;
        }
    }
}
