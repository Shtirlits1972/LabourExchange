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
    public partial class FamilyStatusPage : Page
    {
        List<FamilyStatus> FamilyStatusList = new List<FamilyStatus>();
        public FamilyStatusPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FamilyStatusList = FamilyStatusCrud.GetAll();
            gridMain.ItemsSource = FamilyStatusList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    FamilyStatus model = (FamilyStatus)gridMain.SelectedItem;
                    FamilyStatusCrud.Del(model.Id);
                    FamilyStatusList.RemoveAt(gridMain.SelectedIndex);
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
            FamilyStatusEdit FamilyStatusEdit = new FamilyStatusEdit();
            FamilyStatusEdit.IsEdit = false;
            FamilyStatusEdit.ShowDialog();

            FamilyStatus model = FamilyStatusEdit.model;

            if (model != null)
            {
                FamilyStatusList.Add(model);
                gridMain.ItemsSource = FamilyStatusList;
                gridMain.Items.Refresh();
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                FamilyStatusEdit edit = new FamilyStatusEdit();
                edit.IsEdit = true;
                FamilyStatus model = (FamilyStatus)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < FamilyStatusList.Count; i++)
                {
                    FamilyStatus item = FamilyStatusList[i];

                    if (model.Id == item.Id)
                    {
                        FamilyStatusList[i] = model;
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
