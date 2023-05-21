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
    public partial class PositionPage : Page
    {
        List<Position> PositionList = new List<Position>();
        public PositionPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PositionList = PositionCrud.GetAll();
            gridMain.ItemsSource = PositionList;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Position model = (Position)gridMain.SelectedItem;
                    PositionCrud.Del(model.Id);
                    PositionList.RemoveAt(gridMain.SelectedIndex);
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
            PositionEdit PositionEdit = new PositionEdit();
            PositionEdit.IsEdit = false;
            PositionEdit.ShowDialog();

            Position model = PositionEdit.model;

            if(model != null)
            {
                PositionList.Add(model);
                gridMain.ItemsSource = PositionList;
                gridMain.Items.Refresh();
            }
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            if (gridMain.SelectedItem != null)
            {
                PositionEdit edit = new PositionEdit();
                edit.IsEdit = true;
                Position model = (Position)gridMain.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < PositionList.Count; i++)
                {
                    Position item = PositionList[i];

                    if (model.Id == item.Id)
                    {
                        PositionList[i] = model;
                        break;
                    }
                }
                gridMain.Items.Refresh();
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
    }
}
