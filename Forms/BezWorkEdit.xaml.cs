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
    public partial class BezWorkEdit : Window
    {
        public bool IsEdit = false;
        public BezWork model = new BezWork();
        public BezWorkEdit()
        {
            InitializeComponent();
            comboAnketa.ItemsSource = AnketaCrud.GetAll();
            comboAnketa.SelectedIndex = 0;

            comboEducation.ItemsSource = EducationCrud.GetAll();
            comboEducation.SelectedIndex = 0;

            comboPosition.ItemsSource = PositionCrud.GetAll();
            comboPosition.SelectedIndex = 0;

            comboFamilyStatus.ItemsSource = FamilyStatusCrud.GetAll();
            comboFamilyStatus.SelectedIndex = 0;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.AnketaId = ((Anketa)comboAnketa.SelectedItem).Id;
            model.AnketaName = ((Anketa)comboAnketa.SelectedItem).Name;

            model.EducationId = ((Education)comboEducation.SelectedItem).Id;
            model.EducationName = ((Education)comboEducation.SelectedItem).Name;

            model.PositionId = ((Position)comboPosition.SelectedItem).Id;
            model.PositionName = ((Position)comboPosition.SelectedItem).Name;

            model.Professional = txtProfessiona.Text;
            model.MestoWork = txtMestoWork.Text;
            model.PrichinaUvoln = txtPrichinaUvoln.Text;

            model.FamilyStatusId = ((FamilyStatus)comboFamilyStatus.SelectedItem).Id;
            model.FamilyStatusName = ((FamilyStatus)comboFamilyStatus.SelectedItem).Name;

            model.KontaktKoord = txtKontaktKoord.Text;
            model.Trebov_K_Work = txtTrebov_K_Work.Text;

            model.Arhiv = checkArhiv.IsChecked.Value;

            if (IsEdit)
            {
                BezWorkCrud.Edit(model);
            }
            else
            {
                model = BezWorkCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                #region Anketa
                for (int i = 0; i < comboAnketa.Items.Count; i++)
                {
                    Anketa tmp = (Anketa)comboAnketa.Items[i];
                    if (tmp.Id == model.AnketaId)
                    {
                        comboAnketa.SelectedIndex = i;
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

                txtProfessiona.Text = model.Professional;
                txtMestoWork.Text = model.MestoWork;
                txtPrichinaUvoln.Text = model.PrichinaUvoln;

                #region FamilyStatus
                for (int i = 0; i < comboFamilyStatus.Items.Count; i++)
                {
                    FamilyStatus tmp = (FamilyStatus)comboFamilyStatus.Items[i];
                    if (tmp.Id == model.FamilyStatusId)
                    {
                        comboFamilyStatus.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                txtKontaktKoord.Text = model.KontaktKoord;
                txtTrebov_K_Work.Text = model.Trebov_K_Work;

                checkArhiv.IsChecked = model.Arhiv;
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
        private void btnFamilyStatus_Click(object sender, RoutedEventArgs e)
        {
            FamilyStatusEdit FamilyStatusEdit = new FamilyStatusEdit();
            FamilyStatusEdit.IsEdit = false;
            FamilyStatusEdit.ShowDialog();

            FamilyStatus model = FamilyStatusEdit.model;

            if (model != null)
            {
                comboFamilyStatus.ItemsSource = FamilyStatusCrud.GetAll().OrderBy(x => x.Id).ToList();
                comboFamilyStatus.SelectedIndex = comboFamilyStatus.Items.Count - 1;
            }
        }
    }
}
