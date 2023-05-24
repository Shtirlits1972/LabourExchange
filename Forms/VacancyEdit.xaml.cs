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
            bool IsReady = true;
            string Error = string.Empty;

            model.UsloviyWorkOplata = txtUsloviyWorkOplata.Text;
            model.Trebovan = txtTrebovan.Text;
            model.Priznak = (bool)checkPriznak.IsChecked;

            int intQty = 0;
            int.TryParse(txtQty.Text, out intQty);
            model.qty = intQty;

            if (comboFirma.SelectedItem == null)
            {
                IsReady = false;
                Error += "Выберите фирму \r\n";
            }

            if (comboEducation.SelectedItem == null)
            {
                IsReady = false;
                Error += "Выберите образование \r\n";
            }

            if (comboPosition.SelectedItem == null)
            {
                IsReady = false;
                Error += "Выберите должность \r\n";
            }

            //if (comboSex.SelectedItem == null)
            //{
            //    IsReady = false;
            //    Error += "Выберите пол \r\n";
            //}

            if (comboWorkScedule.SelectedItem == null)
            {
                IsReady = false;
                Error += "Выберите график \r\n";
            }

            if (IsReady)
            {
                model.FirmaId = ((Firma)comboFirma.SelectedItem).Id;
                model.FirmaName = ((Firma)comboFirma.SelectedItem).Name;

                model.EducationId = ((Education)comboEducation.SelectedItem).Id;
                model.EducationName = ((Education)comboEducation.SelectedItem).Name;

                model.PositionId = ((Position)comboPosition.SelectedItem).Id;
                model.PositionName = ((Position)comboPosition.SelectedItem).Name;

                model.WorkSceduleId = ((WorkScedule)comboWorkScedule.SelectedItem).Id;
                model.WorkSceduleName = ((WorkScedule)comboWorkScedule.SelectedItem).Name;

                if(comboSex.SelectedItem != null)
                {
                    model.Sex = ((string)comboSex.SelectedItem);
                }
                else
                {
                    model.Sex = string.Empty; ;
                }

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
            else
            {
                MessageBox.Show(Error, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkPriznak.IsChecked = false;

            if (IsEdit)
            {
                txtUsloviyWorkOplata.Text = model.UsloviyWorkOplata;
                txtTrebovan.Text = model.Trebovan;
                checkPriznak.IsChecked = model.Priznak;
                txtQty.Text = model.qty.ToString();

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

        private void btnAddEducation_Click(object sender, RoutedEventArgs e)
        {
            EducationEdit educationEdit = new EducationEdit();
            educationEdit.IsEdit = false;
            educationEdit.ShowDialog();

            Education model = educationEdit.model;

            if (model != null)
            {
                comboEducation.ItemsSource = EducationCrud.GetAll().OrderBy(x => x.Id).ToList();
                comboEducation.SelectedIndex = comboEducation.Items.Count - 1;
            }
        }

        private void btnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionEdit PositionEdit = new PositionEdit();
            PositionEdit.IsEdit = false;
            PositionEdit.ShowDialog();

            Position model = PositionEdit.model;

            if (model != null)
            {
                comboPosition.ItemsSource = PositionCrud.GetAll().OrderBy(x => x.Id).ToList();
                comboPosition.SelectedIndex = comboPosition.Items.Count - 1;
            }
        }

        private void btnAddFirm_Click(object sender, RoutedEventArgs e)
        {
            FirmaEdit FirmaEdit = new FirmaEdit();
            FirmaEdit.IsEdit = false;
            FirmaEdit.ShowDialog();

            Firma model = FirmaEdit.model;

            if (model != null)
            {
                comboFirma.ItemsSource = FirmaCrud.GetAll().OrderBy(x => x.Id).ToList();
                comboFirma.SelectedIndex = comboFirma.Items.Count - 1;
            }
        }

        private void btnAddWorkScedule_Click(object sender, RoutedEventArgs e)
        {
            WorkSceduleEdit WorkSceduleEdit = new WorkSceduleEdit();
            WorkSceduleEdit.IsEdit = false;
            WorkSceduleEdit.ShowDialog();

            WorkScedule model = WorkSceduleEdit.model;

            if (model != null)
            {
                comboWorkScedule.ItemsSource = WorkSceduleCrud.GetAll().OrderBy(x => x.Id).ToList();
                comboWorkScedule.SelectedIndex = comboWorkScedule.Items.Count - 1;
            }
        }
    }
}
