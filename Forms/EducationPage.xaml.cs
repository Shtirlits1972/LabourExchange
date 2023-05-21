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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    public partial class EducationPage : Page
    {
        List<Education> ObrazovanieList = new List<Education>();
        public EducationPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObrazovanieList = EducationCrud.GetAll();
            gridMain.ItemsSource = ObrazovanieList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Education model = (Education)gridMain.SelectedItem;
                    EducationCrud.Del(model.Id);
                    ObrazovanieList.RemoveAt(gridMain.SelectedIndex);
                    gridMain.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку!");
            }
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            EducationEdit educationEdit = new EducationEdit();
            educationEdit.IsEdit = false;
            educationEdit.ShowDialog();

            Education model = educationEdit.model;

            if(model == null)
            {
                return;
            }
            else
            {
                ObrazovanieList.Add(model);
                gridMain.Items.Refresh();
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                EducationEdit edit = new EducationEdit();
                edit.IsEdit = true;
                Education model = (Education)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < ObrazovanieList.Count; i++)
                {
                    Education item = ObrazovanieList[i];

                    if (model.Id == item.Id)
                    {
                        ObrazovanieList[i] = model;
                        break;
                    }
                }
                gridMain.Items.Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
    }
}
