using LabourExchange.CRUD;
using LabourExchange.Model;
using Microsoft.Win32;
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
using System.IO;

namespace LabourExchange.Forms
{
    public partial class AnketaPage : Page
    {
        List<Anketa> AnketaList = new List<Anketa>();
        public AnketaPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AnketaList = AnketaCrud.GetAll();
            gridMain.ItemsSource = AnketaList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Anketa model = (Anketa)gridMain.SelectedItem;
                    AnketaCrud.Del(model.Id);
                    AnketaList.RemoveAt(gridMain.SelectedIndex);
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
            AnketaEdit AnketaEdit = new AnketaEdit();
            AnketaEdit.IsEdit = false;
            AnketaEdit.ShowDialog();

            try
            {
                Anketa model = AnketaEdit.model;

                if (model != null)
                {
                    AnketaList.Add(model);
                    gridMain.ItemsSource = AnketaList;
                    gridMain.Items.Refresh();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                AnketaEdit edit = new AnketaEdit();
                edit.IsEdit = true;
                Anketa model = (Anketa)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();


                try
                {
                    model = edit.model;

                    if(model != null)
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
                        gridMain.Items.Refresh();
                    }
                }
                catch(Exception ex)
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
                for(int i=0;i< gridMain.Items.Count; i++)
                {
                    Anketa tmp = (Anketa)gridMain.Items[i];
                    strReport += $"{tmp.Fam};{tmp.Name};{tmp.Otch};{tmp.Pasport};{tmp.KolYear};{tmp.Email};{tmp.Telephone}\r\n"; 
                }

                File.WriteAllText(saveFileDialog.FileName, strReport,Encoding.UTF8);
                MessageBox.Show("Готово!","Внимание", MessageBoxButton.OK, MessageBoxImage.Information );
            }
              
        }
    }
}
