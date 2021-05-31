using System;
using System.Collections.Generic;
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

        private Label Label1 { get; set; }
        private Label Label2 { get; set; }
        private Label Label3 { get; set; }
        private Label Label4 { get; set; }
        private Label Label5 { get; set; }
        private Label Label6 { get; set; }

        private Label LblTitulo1 { get; set; }
        public Label LblTitulo2 { get; set; }

        private Panel PanelDash { get; set; }


        public void CargarDatos(Users user, BunifuPieChart chart1, BunifuPieChart chart2,
             Panel panelDash)
        {
            PanelDash = panelDash;
            PieChart1 = chart1;
            PieChart2 = chart2;
            ChartCanvas1 = panelDash.Controls.Find("ChartCanvas1", true)[0] as BunifuChartCanvas;
            ChartCanvas2 = panelDash.Controls.Find("ChartCanvas2", true)[0] as BunifuChartCanvas;
            Label1 = panelDash.Controls.Find("Lbl1", true)[0] as Label;
            Label2 = panelDash.Controls.Find("Lbl2", true)[0] as Label;
            Label3 = panelDash.Controls.Find("Lbl3", true)[0] as Label;
            Label4 = panelDash.Controls.Find("Lbl4", true)[0] as Label;
            Label5 = panelDash.Controls.Find("Lbl5", true)[0] as Label;
            Label6 = panelDash.Controls.Find("Lbl6", true)[0] as Label;

            LblTitulo1 = panelDash.Controls.Find("LblTitulo1", true)[0] as Label;
            LblTitulo2 = panelDash.Controls.Find("LblTitulo2", true)[0] as Label;



            int? prCC = 0;
            List<string> labels = new List<string>();
            PieChart1.Data.Clear();
            PieChart2.Data.Clear();


            var prByDepartments1 = new ufnGetReqGroupByCost_Result().GetList(1);
            var prByDepartments2 = new ufnGetReqGroupByCost_Result().GetList(2);
            var prTodas = prByDepartments2.Sum(c => c.Nro);

            foreach (var item in prByDepartments2)
            {
                PieChart1.Data.Add(Convert.ToDouble((item.Nro * 100) / prTodas));
                labels.Add(item.Description);
                if (item.CostID == user.CostID)
                {
                    prCC += item.Nro;
                }
            }
            PieChart1.TargetCanvas = ChartCanvas1;
            ChartCanvas1.Labels = labels.ToArray();

            var prByStatus = new ufnGetOrderGroupByStatus_Result().GetList();
            var total2 = prByStatus.Sum(c => c.Nro);
            labels.Clear();
            foreach (var item in prByStatus)
            {
                PieChart2.Data.Add(Convert.ToDouble((item.Nro * 100) / total2));
                labels.Add(item.Description);
            }

            ChartCanvas2.Labels = labels.ToArray();
            PieChart2.TargetCanvas = ChartCanvas2;

            FormatearControles();


            foreach (Control item in PanelDash.Controls)
            {
                if (item is Label)
                {
                    item.AutoSize = true;
                    item.BackColor = Color.FromArgb(37, 37, 38);
                    item.ForeColor = Color.FromArgb(154, 196, 85);
                    item.Font = new Font("Calibri", 11.25f, FontStyle.Bold);
                }
            }
            LblTitulo1.Font = new Font("Tahoma", 8, FontStyle.Regular);
            LblTitulo1.Text = "PR";
            LblTitulo1.ForeColor = Color.White;
            LblTitulo2.Font = new Font("Tahoma", 8, FontStyle.Regular);
            LblTitulo2.ForeColor = Color.White;
            LblTitulo2.Text = "PO";
            //int actives = 0;
            //for (int i = 0; i < Grid.Rows.Count; i++)
            //{
            //    var current = (DataRow)Grid.Rows[i].Tag;
            //    if (Convert.ToInt32(current["StatusID"]) == 2)
            //    {
            //        actives++;
            //    }
            //}

            //! Labels Pr
            Label1.Text = $"All :     {prTodas + prByDepartments2.Count}"; // total
            Label2.Text = $"Actives : {prTodas}"; // Mias
            Label3.Text = $"{ user.CostID} :    {prCC}";

            //! Labels Po
            Label4.Text = $"All :     {total2}"; // total
            Label5.Text = $"Actives : {prByStatus.Count(c => c.StatusID == 2)}"; // Mias
            Label6.Text = $"{ user.CostID} :    {0}";
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

        private void FormatearControles()
        {

            ChartCanvas1.XAxesGridLines = false;
            ChartCanvas1.YAxesGridLines = false;
            ChartCanvas1.ShowXAxis = false;
            ChartCanvas1.ShowYAxis = false;
            ChartCanvas1.LegendDisplay = false; // Las barras de colores que representan cada item.
            //ChartCanvas1.Title = "eeeeeeeeee"; // El título hace que el cículo se haga más pequeño.

            ChartCanvas1.Size = new Size(205, 103);
            ChartCanvas1.BackColor = Color.FromArgb(37, 37, 38);

            ChartCanvas2.XAxesGridLines = false;
            ChartCanvas2.YAxesGridLines = false;
            ChartCanvas2.ShowXAxis = false;
            ChartCanvas2.ShowYAxis = false;
            ChartCanvas2.LegendDisplay = false;

            ChartCanvas2.Size = new Size(205, 103);
            ChartCanvas2.BackColor = Color.FromArgb(37, 37, 38);
            // FORECOLOR

            PieChart1.BackgroundColor = GetNext(154, 256);
            PieChart1.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };

            PieChart2.BackgroundColor = GetNext(154, 256); ;
            PieChart2.BorderColor = new List<Color>() { Color.FromArgb(45, 45, 48) };





        }

    }
}
