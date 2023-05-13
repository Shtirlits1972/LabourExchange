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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LabourExchange.Forms
{
    public partial class VacancyEdit : Window
    {
        public bool IsEdit = false;
        public Vacancy model = new Vacancy();

        public VacancyEdit()
        {
            InitializeComponent();
            comboSex.ItemsSource = Ut.SexArr;
            comboFirma.ItemsSource = FirmaCrud.GetAll();
            comboEducation.ItemsSource = EducationCrud.GetAll();
            comboPosition.ItemsSource = PositionCrud.GetAll();
            comboWorkScedule.ItemsSource = WorkSceduleCrud.GetAll();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.UsloviyWorkOplata = txtUsloviyWorkOplata.Text;
            model.Trebovan = txtTrebovan.Text;
            model.Priznak = (bool)checkPriznak.IsChecked;

            model.FirmaId = ((Firma)comboFirma.SelectedItem).Id;
            model.FirmaName = ((Firma)comboFirma.SelectedItem).Name;

            model.EducationId = ((Education)comboEducation.SelectedItem).Id;
            model.EducationName = ((Education)comboEducation.SelectedItem).Name;

            model.PositionId = ((Position)comboPosition.SelectedItem).Id;
            model.PositionName = ((Position)comboPosition.SelectedItem).Name;

            model.WorkSceduleId = ((WorkScedule)comboWorkScedule.SelectedItem).Id;
            model.WorkSceduleName = ((WorkScedule) comboWorkScedule.SelectedItem).Name;

            model.Sex = ((string)comboSex.SelectedItem);

            if (IsEdit)
            {
                VacancyCrud.Edit(model);
            }
            else
            {
                model = VacancyCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboSex.SelectedIndex = 0;
            comboFirma.SelectedIndex = 0;
            comboEducation.SelectedIndex = 0;
            comboPosition.SelectedIndex = 0;
            comboWorkScedule.SelectedIndex = 0;
            checkPriznak.IsChecked = false;

            if (IsEdit)
            {
                txtUsloviyWorkOplata.Text = model.UsloviyWorkOplata;
                txtTrebovan.Text = model.Trebovan;
                checkPriznak.IsChecked = model.Priznak;

                #region sex
                for (int i = 0; i < comboSex.Items.Count; i++)
                {
                    string sex = (string)comboSex.Items[i];
                    if (sex == model.Sex)
                    {
                        comboSex.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region Firma
                for (int i = 0; i < comboFirma.Items.Count; i++)
                {
                    Firma tmp = (Firma)comboFirma.Items[i];
                    if (tmp.Id == model.FirmaId)
                    {
                        comboFirma.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region Education
                for (int i = 0; i < comboEducation.Items.Count; i++)
                {
                    Education tmp = (Education)comboEducation.Items[i];
                    if (tmp.Id == model.EducationId)
                    {
                        comboEducation.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region Position
                for (int i = 0; i < comboPosition.Items.Count; i++)
                {
                    Position tmp = (Position)comboPosition.Items[i];
                    if (tmp.Id == model.PositionId)
                    {
                        comboPosition.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region WorkScedule
                for (int i = 0; i < comboWorkScedule.Items.Count; i++)
                {
                    WorkScedule tmp = (WorkScedule)comboWorkScedule.Items[i];
                    if (tmp.Id == model.WorkSceduleId)
                    {
                        comboWorkScedule.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
