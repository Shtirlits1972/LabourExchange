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

    public partial class RegisterNewAnketaView : Window
    {
        public bool IsReg = false;
        public RegisterNewAnketaView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users { Id = 0,  Login = txtLogin.Text, Password = txtPassword.Text, Role = "user" };
            Ut.currentUser = UsersCrud.Add(users);

            //Anketa anketa = new Anketa { Id = 0, Email = "", Fam = "", Name = "", Otch = "", Pasport = "", Telephone = "", KolYear = 0, UserId = users.Id };
            //anketa = AnketaCrud.Add(anketa);

            IsReg = true;

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private void txtStage_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    int Stag = 0;
        //    bool flag = int.TryParse(txtStage.Text.Trim(), out Stag);

        //    if (!flag)
        //    {
        //        txtStage.Text = "";
        //    }
        //    else
        //    {
        //        if ((Stag < 0) || (Stag > 50))
        //        {
        //            txtStage.Text = "";
        //        }
        //    }
        //}
    }
}
