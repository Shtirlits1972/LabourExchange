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

    public partial class WorkSceduleEdit : Window
    {
        public bool IsEdit = false;
        public WorkScedule model = new WorkScedule();
        public WorkSceduleEdit()
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
          //  int count = WorkSceduleCrud.NameIsFree(txtName.Text);
            int intDuration = 0;

            if(int.TryParse(txtDuration.Text.Replace("_","").Trim(), out intDuration))
            {
                model.duration = intDuration;
            }

            bool flag = WorkSceduleCrud.CheckFreeName(model);

            if (IsEdit)
            {
                if (!flag)
                {
                    MessageBox.Show("Такое название уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(flag)
                {
                    WorkSceduleCrud.Edit(model);
                }
            }
            else
            {
                if (!flag)
                {
                    MessageBox.Show("Такое название уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(flag)
                {
                    model = WorkSceduleCrud.Add(model);
                }
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtName.Text = model.Name;
                txtDuration.Text = model.duration.ToString();
            }
        }
    }
}
