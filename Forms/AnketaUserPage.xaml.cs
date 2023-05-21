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
    public partial class AnketaUserPage : Page
    {
        Anketa model = null;
        public AnketaUserPage()
        {
            InitializeComponent();
            picBirthday.SelectedDate = new DateTime(2000, 1, 1);
            picBirthday.DisplayDate = new DateTime(2000, 1, 1);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            model = AnketaCrud.GetByUserId(Ut.currentUser.Id);

            if (model == null)
            {
                model = new Anketa { Id = 0, UserId = Ut.currentUser.Id };
                return;
            }
            else if (model.Id > 0)
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
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!Ut.IsMoreThen16((DateTime)picBirthday.SelectedDate))
            {
                MessageBox.Show("Соискателю не может быть менее 16 лет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                model.Fam = txtFam.Text;
                model.Name = txtName.Text;

                model.Otch = txtOtch.Text;
                model.Pasport = txtPasport.Text.Replace("_", "").Trim();

                int intKolYear = 0;
                if (int.TryParse(txtKolYear.Text.Replace("_", "").Trim(), out intKolYear))
                {
                    model.KolYear = intKolYear;
                }

                model.Email = txtEmail.Text;
                model.Telephone = txtTelephone.Text;

                model.Birthday = (DateTime)picBirthday.SelectedDate;

                if (model.Id > 0)
                {
                    #region checkData
                    bool userId = AnketaCrud.checkUserIdEdit(model.UserId, model.Id);
                    if (userId)
                    {
                        MessageBox.Show("Анкета с таким пользователем уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool passport = AnketaCrud.checkPassportEdit(model.Pasport.Trim(), model.Id);
                    if (passport)
                    {
                        MessageBox.Show("Анкета с таким паспортом уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool phone = AnketaCrud.checkTelephoneEdit(model.Telephone.Trim(), model.Id);
                    if (phone)
                    {
                        MessageBox.Show("Анкета с таким телефоном уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    #endregion

                    AnketaCrud.Edit(model);
                }
                else
                {
                    #region checkData
                    bool userId = AnketaCrud.checkUserId(model.UserId);
                    if (userId)
                    {
                        MessageBox.Show("Анкета с таким пользователем уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool passport = AnketaCrud.checkPassport(model.Pasport.Trim());
                    if (passport)
                    {
                        MessageBox.Show("Анкета с таким паспортом уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool phone = AnketaCrud.checkTelephone(model.Telephone.Trim());
                    if (phone)
                    {
                        MessageBox.Show("Анкета с таким телефоном уже существует! \r\nДанные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    #endregion

                    model = AnketaCrud.Add(model);
                }
                MessageBox.Show("Внимание", "Данные сохранены", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
