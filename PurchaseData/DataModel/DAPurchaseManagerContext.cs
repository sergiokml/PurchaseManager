using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace PurchaseData.DataModel
{
    public partial class PurchaseManagerEntities
    {

        public override int SaveChanges()
        {
            int res = 0;
            //HandleChangeTracking();
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

        public int GuardarCambios(Users user, string evento)
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
        private void HandleChangeTracking()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                switch (entity.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Added:
                        if (entity.Entity.GetType().FullName == "PurchaseData.DataModel.RequisitionHeader")
                        {

                        }
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        break;
                    default:
                        break;
                }

            }
        }


    }

}
