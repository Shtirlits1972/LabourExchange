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
    public partial class WorkScedulePage : Page
    {
        List<WorkScedule> WorkSceduleList = new List<WorkScedule>();
        public WorkScedulePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WorkSceduleList = WorkSceduleCrud.GetAll();
            gridMain.ItemsSource = WorkSceduleList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    WorkScedule model = (WorkScedule)gridMain.SelectedItem;
                    WorkSceduleCrud.Del(model.Id);
                    WorkSceduleList.RemoveAt(gridMain.SelectedIndex);
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
            try
            {
                WorkSceduleEdit WorkSceduleEdit = new WorkSceduleEdit();
                WorkSceduleEdit.IsEdit = false;
                WorkSceduleEdit.ShowDialog();

                WorkScedule model = WorkSceduleEdit.model;
                WorkSceduleList.Add(model);
                gridMain.ItemsSource = WorkSceduleList;
                gridMain.Items.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridMain.SelectedItem != null)
                {
                    WorkSceduleEdit edit = new WorkSceduleEdit();
                    edit.IsEdit = true;
                    WorkScedule model = (WorkScedule)gridMain.SelectedItem;

                    edit.model = model;
                    edit.ShowDialog();

                    model = edit.model;

                    if (edit.model != null)
                    {
                        for (int i = 0; i < WorkSceduleList.Count; i++)
                        {
                            WorkScedule item = WorkSceduleList[i];

                            if (model.Id == item.Id)
                            {
                                WorkSceduleList[i] = model;
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
            catch (Exception ex)
            {

            }
        }
    }
}
