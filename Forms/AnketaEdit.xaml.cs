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

    public partial class AnketaEdit : Window
    {
        public bool IsEdit = false;
        public Anketa model = new Anketa();

        public AnketaEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.Birthday = (DateTime)picBirthday.SelectedDate;
            model.Birthday = (DateTime)picBirthday.DisplayDate;

            model.Fam = txtFam.Text;
            model.Name = txtName.Text;

            model.Otch = txtOtch.Text;
            model.Pasport = txtPasport.Text;

            int intKolYear = 0;
            if (int.TryParse(txtKolYear.Text.Replace("_", "").Trim(), out intKolYear))
            {
                model.KolYear = intKolYear;
            }

            model.Email = txtEmail.Text;
            model.Telephone = txtTelephone.Text;
            model.UserId = ((Users)comboUsers.SelectedItem).Id;

            if(!Ut.IsMoreThen16((DateTime)picBirthday.SelectedDate))
            {
                MessageBox.Show("Соискателю не может быть менее 16 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (IsEdit)
                {
                    AnketaCrud.Edit(model);
                }
                else
                {
                    model = AnketaCrud.Add(model);
                }
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtFam.Text = model.Fam;
                txtName.Text = model.Name;
                txtOtch.Text = model.Otch;
                txtPasport.Text = model.Pasport;
                txtKolYear.Text = model.KolYear.ToString();
                txtEmail.Text = model.Email;
                txtTelephone.Text = model.Telephone;

                picBirthday.SelectedDate = model.Birthday;
                picBirthday.DisplayDate = model.Birthday;

                List<Users> users = new List<Users>();
                Users usr = UsersCrud.GetOne(model.UserId);
                users.Add(usr);

                comboUsers.ItemsSource = users;
                comboUsers.SelectedIndex = 0;
                comboUsers.IsEnabled = false;
            }
            else
            {
                DateTime dateTime = new DateTime(2020, 1, 1);

                picBirthday.SelectedDate = dateTime;
                picBirthday.DisplayDate = dateTime;


                List<Users> users = UsersCrud.GetUsersWithoutAnkets();

                if (users == null && users.Count == 0)
                {
                    MessageBox.Show("Нет новых пользователей без анкеты", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    comboUsers.ItemsSource = users;
                    comboUsers.SelectedIndex = 0;
                }
            }
        }
    }
}