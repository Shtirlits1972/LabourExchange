using LabourExchange.CRUD;
using LabourExchange.Model;
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
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    public partial class BenefitEdit : Window
    {
        public bool IsEdit = false;
        public Benefit model = new Benefit();
        public BenefitEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.Name = txtName.Text;

            int intVal = 0;
            if (int.TryParse(txtVal.Text.Replace("_", "").Trim(), out intVal))
            {
                model.Val = intVal;
            }

            model.Data_Vyplaty = (DateTime)picData_Vyplaty.SelectedDate;
            model.Data_Postanovki = (DateTime)picData_Postanovki.SelectedDate;

            if (IsEdit)
            {
                BenefitCrud.Edit(model);
            }
            else
            {
                model = BenefitCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtName.Text = model.Name;
                txtVal.Text = model.Val.ToString();

                picData_Vyplaty.SelectedDate = model.Data_Vyplaty;
                picData_Vyplaty.DisplayDate = model.Data_Vyplaty;

                picData_Postanovki.SelectedDate = model.Data_Postanovki;
                picData_Postanovki.DisplayDate = model.Data_Postanovki;
            }
            else
            {
                picData_Vyplaty.SelectedDate = DateTime.Now; 
                picData_Vyplaty.DisplayDate = DateTime.Now;

                picData_Postanovki.SelectedDate = DateTime.Now;
                picData_Postanovki.DisplayDate = DateTime.Now;
            }
        }
    }
}
