using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Bunifu.Charts.WinForms;
using Bunifu.Charts.WinForms.ChartTypes;

using PurchaseData.DataModel;

namespace PurchaseDesktop.Helpers
{
    public partial class HFunctions
    {
        private BunifuPieChart PieChart1 { get; set; }
        private BunifuPieChart PieChart2 { get; set; }
        private BunifuChartCanvas ChartCanvas1 { get; set; }
        private BunifuChartCanvas ChartCanvas2 { get; set; }

        public Label Label1 { get; set; }
        private Label Label2 { get; set; }
        private Label Label3 { get; set; }

        public Users User { get; set; }


        public List<ufnGetReqGroupByCost_Result> ReqGroupByCost_Results { get; set; }
        public List<ufnGetOrderGroupByStatus_Result> OrderGroupByStatus_Results { get; set; }


        public void CargarDatos(BunifuPieChart chart1, BunifuPieChart chart2,
             BunifuChartCanvas chartCanvas1, BunifuChartCanvas chartCanvas2, Label label1, Label label2, Label label3)
        {

            PieChart1 = chart1;
            PieChart2 = chart2;
            ChartCanvas1 = chartCanvas1;
            ChartCanvas2 = chartCanvas2;
            Label1 = label1;
            Label2 = label2;
            Label3 = label3;

            int? mias = 0;

            List<string> labels = new List<string>();
            PieChart1.Data.Clear();
            PieChart2.Data.Clear();
            var prByDepartments = ReqGroupByCost_Results;
            var total1 = prByDepartments.Sum(c => c.Nro);

            foreach (var item in prByDepartments)
            {
                PieChart1.Data.Add(Convert.ToDouble((item.Nro * 100) / total1));
                labels.Add(item.Description);
                if (item.CostID == User.CostID)
                {
                    mias += item.Nro;
                }
            }
            PieChart1.TargetCanvas = ChartCanvas1;
            ChartCanvas1.Labels = labels.ToArray();

            var prByStatus = OrderGroupByStatus_Results;
            var total2 = prByStatus.Sum(c => c.Nro);
            labels.Clear();
            foreach (var item in prByStatus)
            {
                PieChart2.Data.Add(Convert.ToDouble((item.Nro * 100) / total2));
                labels.Add(item.Description);
            }

            ChartCanvas2.Labels = labels.ToArray();
            PieChart2.TargetCanvas = ChartCanvas2;

            ChartCanvas1.XAxesGridLines = false;
            ChartCanvas1.YAxesGridLines = false;
            ChartCanvas1.ShowXAxis = false;
            ChartCanvas1.ShowYAxis = false;
            ChartCanvas1.LegendDisplay = false;

            ChartCanvas1.Size = new Size(205, 103);
            ChartCanvas1.BackColor = Color.FromArgb(37, 37, 38);

            ChartCanvas2.XAxesGridLines = false;
            ChartCanvas2.YAxesGridLines = false;
            ChartCanvas2.ShowXAxis = false;
            ChartCanvas2.ShowYAxis = false;
            ChartCanvas2.LegendDisplay = false;

            ChartCanvas2.Size = new Size(205, 103);
            ChartCanvas2.BackColor = Color.FromArgb(37, 37, 38);

            PieChart1.BackgroundColor = GetNext(154, 256);
            PieChart1.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };

            PieChart2.BackgroundColor = GetNext(154, 256); ;
            PieChart2.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };

            int actives = 0;
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                var current = (DataRow)Grid.Rows[i].Tag;
                if (Convert.ToInt32(current["StatusID"]) == 2)
                {
                    actives++;
                }
            }

            //! Labels
            Label1.Text = $"All :    {total1}"; // total
            Label2.Text = $"Me :     {actives}"; // Mias
            Label3.Text = $"{User.CostID} :    {mias}";



        }
        private List<Color> GetNext(int red, int max)
        {
            List<Color> bgColors = new List<Color>();
            var rnd = new Random();
            int MaxColor = max;
            var lastColor = Color.FromArgb(red, rnd.Next(MaxColor), rnd.Next(MaxColor));

            for (int i = 0; i < 100; i++)
            {
                Color nextColor = Color.FromArgb(
               (rnd.Next(MaxColor) + lastColor.R) / 2,
               (rnd.Next(MaxColor) + lastColor.G) / 2,
               (rnd.Next(MaxColor) + lastColor.B) / 2
               );
                bgColors.Add(nextColor);
            }
            return bgColors;
        }
    }
}
