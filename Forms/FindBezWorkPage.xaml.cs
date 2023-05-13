using LabourExchange.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    public partial class FindBezWorkPage : Page
    {
        public FindBezWorkPage()
        {
            InitializeComponent();
            gridMain.ItemsSource = BezWorkCrud.GetAll();

            List<int> listStage = new List<int>();

            for (int i = 1; i < 51; i++)
            {
                listStage.Add(i);
            }
            comboStag.ItemsSource = listStage;
            comboStag.SelectedIndex = 0;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int intStag = (int)comboStag.SelectedValue;

            string professin = txtProfession.Text.Replace("_", "").Trim();
            gridMain.ItemsSource = BezWorkCrud.GetAll(intStag, professin);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            comboStag.SelectedIndex = 0;
            txtProfession.Text = "";
            gridMain.ItemsSource = BezWorkCrud.GetAll();
        }
    }
}
