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

    public partial class AnketaEdit : Window
    {
        public bool IsEdit = false;
        public Anketa model = new Anketa();

        public AnketaEdit()
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
            model.Fam = txtFam.Text;
            model.Name = txtName.Text;

            model.Otch = txtOtch.Text;
            model.Pasport = txtPasport.Text;

            int intKolYear = 0;
            if (int.TryParse(txtKolYear.Text.Replace("_","").Trim(), out intKolYear))
            {
                model.KolYear = intKolYear;
            }

            model.Email = txtEmail.Text;
            model.Telephone = txtTelephone.Text;

            if (IsEdit)
            {
                AnketaCrud.Edit(model);
            }
            else
            {
                model = AnketaCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtFam.Text = model.Fam;
                txtName.Text = model.Name;
                txtOtch.Text = model.Otch;
                txtPasport.Text = model.Pasport;
                txtKolYear.Text = model.KolYear.ToString();
                txtEmail.Text = model.Email;
                txtTelephone.Text = model.Telephone;

            }
        }
    }
}