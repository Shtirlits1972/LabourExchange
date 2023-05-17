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
using System.Net.Mail;
using System.Net;
using System.Xml;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;

namespace LabourExchange.Forms
{
    public partial class EmploymentPage : Page
    {
        List<Vacancy> listVacancy = new List<Vacancy>();
        Vacancy vacancy = new Vacancy();
        int intCurrentVacation = 0;
        public EmploymentPage()
        {
            InitializeComponent();
            comboAnketa.ItemsSource = BezWorkCrud.GetAll();
            comboAnketa.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnFindVacation_Click(object sender, RoutedEventArgs e)
        {
            FindVacancy();
        }

        private void FindVacancy()
        {
            intCurrentVacation = 0;
            BezWork worker = (BezWork)comboAnketa.SelectedItem;
            listVacancy = VacancyCrud.GetAll(worker.EducationId, worker.PositionId, worker.AnketaId);

            if (listVacancy.Count > 0)
            {
                lblNumber.Content = 1;
                vacancy = listVacancy[0];
                SetvacancyAttr(vacancy);
            }
            else
            {
                lblNumber.Content = 0;
            }

            lblAllNumber.Content = listVacancy.Count;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if ((intCurrentVacation + 1) < listVacancy.Count)
            {
                intCurrentVacation++;
                vacancy = listVacancy[intCurrentVacation];
                SetvacancyAttr(vacancy);
            }
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if ((intCurrentVacation - 1) > -1)
            {
                intCurrentVacation--;
                vacancy = listVacancy[intCurrentVacation];
                SetvacancyAttr(vacancy);
            }
        }
        private void SetvacancyAttr(Vacancy vac)
        {
            FirmName.Content = vac.FirmaName;
            Dolzh.Content = vac.PositionName;
            Zhilish.Content = vac.EducationName;
            Usloviya.Content = vac.UsloviyWorkOplata;
            Trebovanie.Content = vac.Trebovan;

            if(listVacancy.Count == 0)
            {
                lblNumber.Content = "0";
            }
            else
            {
                lblNumber.Content = intCurrentVacation+1;
            }
            lblAllNumber.Content = listVacancy.Count;
        }

        private void btnOffer_Click(object sender, RoutedEventArgs e)
        {
            BezWork bezWork = (BezWork)comboAnketa.SelectedItem;
            bezWork.Arhiv = true;
            BezWorkCrud.Edit(bezWork);

            vacancy.Priznak = false;
            VacancyCrud.Edit(vacancy);

            VacancyCrud.AddAnketaVacancyLink(vacancy.Id, bezWork.AnketaId);

            FindVacancy();

            string fileName = "Направление на работу " + bezWork.AnketaName + " " + DateTime.Now.ToShortDateString();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "XLS file (*.XLS)|*.XLS";

            saveFileDialog.Title = fileName;
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                string filname = saveFileDialog.FileName;
                // Создаем новый файл Excel
                var file = new FileInfo(filname);
                if (file.Exists)
                {
                    file.Delete(); // Удаляем существующий файл
                }
                using (var package = new ExcelPackage(file))
                {
                    // Добавляем лист в документ
                    var xlWorkSheet = package.Workbook.Worksheets.Add("Направление на работу");

                    int index = 2;

                    xlWorkSheet.Cells[1, 1].Value = "Направление на работу от " + DateTime.Today.ToString("dd.MM.yyyy");
                    xlWorkSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    xlWorkSheet.Cells[1, 1, 1, 2].Merge = true;

                    xlWorkSheet.Cells[index, 1].Value = "Клиент";
                    xlWorkSheet.Cells[index, 2].Value = bezWork.AnketaName;
                    index++;
                    xlWorkSheet.Cells[index, 1].Value = "Фирма";
                    xlWorkSheet.Cells[index, 2].Value = vacancy.FirmaName;
                    index++;
                    xlWorkSheet.Cells[index, 1].Value = "Должность";
                    xlWorkSheet.Cells[index, 2].Value = vacancy.PositionName;
                    index++;
                    xlWorkSheet.Cells[index, 1].Value = "Образование";
                    xlWorkSheet.Cells[index, 2].Value = vacancy.EducationName;
                    index++;
                    xlWorkSheet.Cells[index, 1].Value = "Условия работы";
                    xlWorkSheet.Cells[index, 2].Value = vacancy.UsloviyWorkOplata;
                    index++;
                    xlWorkSheet.Cells[index, 1].Value = "Требования";
                    xlWorkSheet.Cells[index, 2].Value = vacancy.Trebovan;

                    xlWorkSheet.Column(1).Width = 30;
                    xlWorkSheet.Column(2).Style.Font.Bold = true;
                    xlWorkSheet.Column(2).Width = 30;

                    //границы таблицы
                    ExcelRange range = xlWorkSheet.Cells[2, 1, index, 2];
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    package.Save();

                }

                MessageBoxResult result = System.Windows.MessageBox.Show("Отчет сформирован! \r\n Хотите отправить почту?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    string recipient = bezWork.Email;
                    string subject = "Трудоустройство";
                    string body = "Трудоустройство в " + vacancy.FirmaName;
                    string attachmentPath = filname;

                    Ut.SendEmailWithAttachment(recipient, subject, body, attachmentPath);
                }

                try
                {
                    System.Diagnostics.Process.Start(filname);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void comboAnketa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BezWork bezWork = (BezWork)comboAnketa.SelectedItem;

            lblProfession.Content = bezWork.Professional;
            lblStag.Content = bezWork.Stag;
        }
    }
}
