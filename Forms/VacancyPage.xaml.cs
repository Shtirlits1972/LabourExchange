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
    public partial class VacancyPage : Page
    {
        List<Vacancy> VacancyList = new List<Vacancy>();
        public VacancyPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VacancyList = VacancyCrud.GetAll();
            gridMain.ItemsSource = VacancyList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Vacancy model = (Vacancy)gridMain.SelectedItem;
                    VacancyCrud.Del(model.Id);
                    VacancyList.RemoveAt(gridMain.SelectedIndex);
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
            VacancyEdit VacancyEdit = new VacancyEdit();
            VacancyEdit.IsEdit = false;
            VacancyEdit.ShowDialog();

            try
            {
                Vacancy model = VacancyEdit.model;

                if (model != null)
                {
                    VacancyList.Add(model);
                    gridMain.ItemsSource = VacancyList;
                    gridMain.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                VacancyEdit edit = new VacancyEdit();
                edit.IsEdit = true;
                Vacancy model = (Vacancy)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                if(model != null)
                {
                    for (int i = 0; i < VacancyList.Count; i++)
                    {
                        Vacancy item = VacancyList[i];

                        if (model.Id == item.Id)
                        {
                            VacancyList[i] = model;
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
