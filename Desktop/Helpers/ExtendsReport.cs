using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using PurchaseData.DataModel;

using PurchaseDesktop.Interfaces;

namespace PurchaseDesktop.Helpers
{
    public static class ExtendsReport
    {
        public static List<OrderStatus> LoadOrderStatus(this IPerfilActions input)
        {
            using (PurchaseManagerContext contextDB = new PurchaseManagerContext())
            {
                return contextDB.OrderStatus.ToList();
            }
        }

        public static void LoadAccounts(this IPerfilActions input)
        {
            using (PurchaseManagerContext contextDB = new PurchaseManagerContext())
            {
                //   input.AccountsCharts = contextDB.AccountsCharts.ToList();
            }
        }

        public static DataTable ToDataTable<T>(this IPerfilActions input, List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //DataColumn column = new DataColumn
            //{
            //    DataType = System.Type.GetType("System.Int32"),
            //    AutoIncrement = true,
            //    AutoIncrementSeed = 1000,
            //    AutoIncrementStep = 10
            //};

            //dataTable.Columns.Add(column);



            foreach (PropertyInfo prop in Props)
            {


                if (prop.PropertyType == typeof(DateTime))
                {
                    dataTable.Columns.Add(prop.Name, typeof(DateTime));
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }


            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }




    }

}
