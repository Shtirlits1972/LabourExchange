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
    public partial class BezWorkPage : Page
    {
        List<BezWork> BezWorkList = new List<BezWork>();
        public BezWorkPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BezWorkList = BezWorkCrud.GetAll();
            gridMain.ItemsSource = BezWorkList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    BezWork model = (BezWork)gridMain.SelectedItem;
                    BezWorkCrud.Del(model.Id);
                    BezWorkList.RemoveAt(gridMain.SelectedIndex);
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
            BezWorkEdit BezWorkEdit = new BezWorkEdit();
            BezWorkEdit.IsEdit = false;
            BezWorkEdit.ShowDialog();

            try
            {
                BezWork model = BezWorkEdit.model;
                if (model != null)
                {
                    BezWorkList.Add(model);
                    gridMain.ItemsSource = BezWorkList;
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
                BezWorkEdit edit = new BezWorkEdit();
                edit.IsEdit = true;
                BezWork model = (BezWork)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                try
                {
                    model = edit.model;

                    if (model != null)
                    {
                        for (int i = 0; i < BezWorkList.Count; i++)
                        {
                            BezWork item = BezWorkList[i];

                            if (model.Id == item.Id)
                            {
                                BezWorkList[i] = model;
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
