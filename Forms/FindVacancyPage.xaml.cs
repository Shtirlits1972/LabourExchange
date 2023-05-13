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
    public partial class FindVacancyPage : Page
    {
        public FindVacancyPage()
        {
            InitializeComponent();

            Education education = new Education {Id=0, Name = "Все" };
            List<Education> listEducation = new List<Education>();
            listEducation.Add(education);
            listEducation.AddRange(EducationCrud.GetAll());
            comboEducation.ItemsSource = listEducation;
            comboEducation.SelectedIndex = 0;

            List<string> listSex = new List<string>();
            listSex.Add("Все");
            listSex.AddRange(Ut.SexArr.Select(s => s.ToString()));
            comboSex.ItemsSource = listSex;
            comboSex.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gridMain.ItemsSource = VacancyCrud.GetAll();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int EducationId = ((Education)comboEducation.SelectedItem).Id;
            string Sex = comboSex.SelectedItem.ToString();
            gridMain.ItemsSource = VacancyCrud.GetAll(EducationId, Sex);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            comboEducation.SelectedIndex = 0;
            comboSex.SelectedIndex = 0;
            gridMain.ItemsSource = VacancyCrud.GetAll();
        }
    }
}
