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

namespace LabourExchange.Forms
{
    public partial class BenefitEdit : Window
    {
        public bool IsEdit = false;
        public Benefit model = new Benefit();
        public BenefitEdit()
        {
            InitializeComponent();
            comboAnketa.ItemsSource = AnketaCrud.GetAll();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(comboAnketa.SelectedItem == null)
            {
                MessageBox.Show("Выберите анкету", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            model.Deskr = txtDeskr.Text;

            int intVal = 0;
            if (int.TryParse(txtVal.Text.Replace("_", "").Trim(), out intVal))
            {
                model.Val = intVal;
            }

            model.Data_Vyplaty = (DateTime)picData_Vyplaty.SelectedDate;
            model.Data_Postanovki = (DateTime)picData_Postanovki.SelectedDate;

            model.AnketaId = ((Anketa)comboAnketa.SelectedItem).Id;
            model.AnketaName = ((Anketa)comboAnketa.SelectedItem).ToString();

            if (IsEdit)
            {
                BenefitCrud.Edit(model);
            }
            else
            {
                model = BenefitCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtDeskr.Text = model.Deskr;
                txtVal.Text = model.Val.ToString();

                picData_Vyplaty.SelectedDate = model.Data_Vyplaty;
                picData_Vyplaty.DisplayDate = model.Data_Vyplaty;

                picData_Postanovki.SelectedDate = model.Data_Postanovki;
                picData_Postanovki.DisplayDate = model.Data_Postanovki;

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
            }
            else
            {
                picData_Vyplaty.SelectedDate = DateTime.Now;
                picData_Vyplaty.DisplayDate = DateTime.Now;

                picData_Postanovki.SelectedDate = DateTime.Now;
                picData_Postanovki.DisplayDate = DateTime.Now;
            }
        }

        private void comboAnketa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anketa anketa = (Anketa)comboAnketa.SelectedItem;
            int intValue = 0;

            DateTime dateTime = DateTime.Now;
            if (dateTime.AddYears(-64) > anketa.Birthday)
            {
                intValue = 5500;
            }
            else
            {
                if(anketa.KolYear <= 3)
                {
                    intValue = 1500;
                }
                else if(anketa.KolYear >=3 && anketa.KolYear <=6)
                {
                    intValue = 3000;
                }


                else if (anketa.KolYear >= 6 && anketa.KolYear <= 8)
                {
                    intValue = 5000;
                }
                else if (anketa.KolYear >= 8 && anketa.KolYear <= 11)
                {
                    intValue = 7000;
                }
                else if (anketa.KolYear >= 11 && anketa.KolYear <= 15)
                {
                    intValue = 9000;
                }
                else if (anketa.KolYear >= 16)
                {
                    intValue = 12000;
                }
            }
            
            txtVal.Text = intValue.ToString();

        }
    }
}
