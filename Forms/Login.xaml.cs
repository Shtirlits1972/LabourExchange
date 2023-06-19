using LabourExchange.CRUD;
using LabourExchange.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    public partial class Login : Window
    {
        public bool IsRegister = false;
        public bool IsClosedByUser = false;

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;
        public Login()
        {
            try
            {
                InitializeComponent();
                this.SourceInitialized += new EventHandler(Window1_SourceInitialized);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Window1_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            IntPtr windowHandle = helper.Handle; //Get the handle of this window
            IntPtr hmenu = GetSystemMenu(windowHandle, 0);
            int cnt = GetMenuItemCount(hmenu);
            //remove the button
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            //remove the extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(windowHandle); //Redraw the menu bar
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = txtLogin.Text;
            string strPassword = txtPassword.Text;

          Ut.currentUser = UsersCrud.Login(strLogin, strPassword);

            if(Ut.currentUser == null)
            {
                MessageBox.Show("Неправильный логин и(или) пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop); 
                return;
            }
            else
            {
                Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            IsRegister = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsClosedByUser = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(e.Cancel == false)
            {
                IsClosedByUser = true;
            }
        }
    }
}
