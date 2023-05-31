using LabourExchange.CRUD;
using LabourExchange.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    
    public partial class AnketaPage2 : Page
    {
        List<Anketa> AnketaList = AnketaCrud.GetAll();
        public AnketaPage2()
        {
            InitializeComponent();
            mainGrid.ItemsSource = AnketaList;  
            mainGrid.Rebind();  
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            AnketaEdit AnketaEdit = new AnketaEdit();
            AnketaEdit.IsEdit = false;
            AnketaEdit.ShowDialog();

            try
            {
                Anketa model = AnketaEdit.model;

                if (model != null)
                {
                    AnketaList.Add(model);
                    mainGrid.ItemsSource = AnketaList;
                    mainGrid.Rebind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                AnketaEdit edit = new AnketaEdit();
                edit.IsEdit = true;
                Anketa model = (Anketa)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();


                try
                {
                    model = edit.model;

                    if (model != null)
                    {
                        for (int i = 0; i < AnketaList.Count; i++)
                        {
                            Anketa item = AnketaList[i];

                            if (model.Id == item.Id)
                            {
                                AnketaList[i] = model;
                                break;
                            }
                        }
                        mainGrid.Rebind();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }

        private void bReport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "CSV file (*.CSV)|*.CSV";

            saveFileDialog.Title = "Отчёт.CSV";
            saveFileDialog.FileName = "Отчёт.CSV";

            if (saveFileDialog.ShowDialog() == true)
            {
                string strReport = $"Фамилия;Имя;Отчество;Паспорт;Стаж;Email;Телефон\r\n";
                for (int i = 0; i < mainGrid.Items.Count; i++)
                {
                    Anketa tmp = (Anketa)mainGrid.Items[i];
                    strReport += $"{tmp.Fam};{tmp.Name};{tmp.Otch};{tmp.Pasport};{tmp.KolYear};{tmp.Email};{tmp.Telephone}\r\n";
                }

                File.WriteAllText(saveFileDialog.FileName, strReport, Encoding.UTF8);
                System.Windows.MessageBox.Show("Готово!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Anketa model = (Anketa)mainGrid.SelectedItem;

                    AnketaCrud.CheckResult res = AnketaCrud.CanDelete(model.Id);

                    if(res.flag)
                    {
                        AnketaCrud.Del(model.Id);

                        AnketaList.Remove(model);
                        mainGrid.ItemsSource = AnketaList;
                        mainGrid.Rebind();
                    }
                    else
                    {
                        MessageBox.Show(res.Reason, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }

                }
            }
            else
            {
                MessageBox.Show("Выберите строку!");
            }
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in mainGrid.Columns)
            {
                column.ClearFilters();
            }
            mainGrid.FilterDescriptors.ResumeNotifications();
        }
    }
}
