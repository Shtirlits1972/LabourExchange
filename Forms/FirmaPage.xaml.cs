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

    public partial class NextPage : Page
    {
        List<Firma> FirmaList = new List<Firma>();
        public NextPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FirmaList = FirmaCrud.GetAll();
            gridMain.ItemsSource = FirmaList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Firma model = (Firma)gridMain.SelectedItem;
                    FirmaCrud.Del(model.Id);
                    FirmaList.RemoveAt(gridMain.SelectedIndex);
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
            FirmaEdit FirmaEdit = new FirmaEdit();
            FirmaEdit.IsEdit = false;
            FirmaEdit.ShowDialog();

            try
            {
                Firma model = FirmaEdit.model;
                if (model != null)
                {
                    FirmaList.Add(model);
                    gridMain.ItemsSource = FirmaList;
                    gridMain.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                FirmaEdit edit = new FirmaEdit();
                edit.IsEdit = true;
                Firma model = (Firma)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                try
                {
                    model = edit.model;

                    if (model != null)
                    {
                        for (int i = 0; i < FirmaList.Count; i++)
                        {
                            Firma item = FirmaList[i];

                            if (model.Id == item.Id)
                            {
                                FirmaList[i] = model;
                                break;
                            }
                        }
                        gridMain.Items.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }

    }
}
