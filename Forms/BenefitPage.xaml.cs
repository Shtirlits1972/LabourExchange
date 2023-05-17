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
    public partial class BenefitPage : Page
    {
        List<Benefit> BenefitList = new List<Benefit>();
        public BenefitPage()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BenefitList = BenefitCrud.GetAll();
            gridMain.ItemsSource = BenefitList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Benefit model = (Benefit)gridMain.SelectedItem;
                    BenefitCrud.Del(model.Id);
                    BenefitList.RemoveAt(gridMain.SelectedIndex);
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
            BenefitEdit BenefitEdit = new BenefitEdit();
            BenefitEdit.IsEdit = false;
            BenefitEdit.ShowDialog();

            Benefit model = BenefitEdit.model;

            if (model != null)
            {
                BenefitList.Add(model);
                gridMain.ItemsSource = BenefitList;
                gridMain.Items.Refresh();
            }
        }
        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                BenefitEdit edit = new BenefitEdit();
                edit.IsEdit = true;
                Benefit model = (Benefit)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                if (model != null)
                {
                    model = edit.model;

                    for (int i = 0; i < BenefitList.Count; i++)
                    {
                        Benefit item = BenefitList[i];

                        if (model.Id == item.Id)
                        {
                            BenefitList[i] = model;
                            break;
                        }
                    }
                    gridMain.Items.Refresh();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
    }
}
