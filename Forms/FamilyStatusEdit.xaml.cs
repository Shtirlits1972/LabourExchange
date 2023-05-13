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
    public partial class FamilyStatusEdit : Window
    {
        public bool IsEdit = false;
        public FamilyStatus model = new FamilyStatus();
        public FamilyStatusEdit()
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
            if (IsEdit)
            {
                FamilyStatusCrud.Edit(model);
            }
            else
            {
                model = FamilyStatusCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtName.Text = model.Name;
            }
        }
    }
}
