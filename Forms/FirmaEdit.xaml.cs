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
    public partial class FirmaEdit : Window
    {
        public bool IsEdit = false;
        public Firma model = new Firma();
        public FirmaEdit()
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

            bool isFree = FirmaCrud.NameIsFree(txtName.Text);

            if (!isFree)
            {
                MessageBox.Show("Такое название уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsEdit)
            {
                FirmaCrud.Edit(model);
            }
            else
            {
                model = FirmaCrud.Add(model);
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
