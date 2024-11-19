using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace BikbulatovLibrary
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Employee currentEmployee = new Employee();
        public List<string> UniquePosts { get; set; }
        public AddEditPage(Employee SelectedEmployee)
        {
            InitializeComponent();

            var EmployeeWork = BikbulatovLibraryEntities.GetContext().Library.Select(p => p.LibraryName).ToList();

            foreach (var item in EmployeeWork)
            {
                EmpWorkBox.Items.Add(item);
            }

            var uniquePosts = BikbulatovLibraryEntities.GetContext().Employee
                                  .Select(e => e.Post)
                                  .Distinct()
                                  .ToList();

            foreach (var post in uniquePosts)
            {
                EmpPostBox.Items.Add(post);
            }

            if (SelectedEmployee != null)
            {
                currentEmployee = SelectedEmployee;
                EmpWorkBox.SelectedIndex = currentEmployee.LibraryID - 1;
                EmpPostBox.SelectedItem = currentEmployee.Post;
            }
            else
            {
                EmpWorkBox.SelectedIndex = 0;
                if (uniquePosts.Any())
                {
                    EmpPostBox.SelectedIndex = 0;
                }
            }

            DataContext = this;
            DataContext = currentEmployee;
        }

        private void SNILSTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string currentText = textBox.Text;

            // Разрешаем ввод только цифр
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            // Автоматически добавляем дефисы и пробелы в нужные позиции
            if (currentText.Length == 3 || currentText.Length == 7)
            {
                textBox.Text += "-";
                textBox.CaretIndex = textBox.Text.Length;
            }
            else if (currentText.Length == 11)
            {
                textBox.Text += " ";
                textBox.CaretIndex = textBox.Text.Length;
            }

            // Перемещаем курсор в конец
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void DateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string currentText = textBox.Text;

            // Разрешаем ввод только цифр
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            // Автоматически добавляем дефисы в нужные позиции
            if (currentText.Length == 4 || currentText.Length == 7)
            {
                textBox.Text += "-";
                textBox.CaretIndex = textBox.Text.Length;
            }

            // Перемещаем курсор в конец
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void PassportTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем ввод только цифр
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true; // Блокируем ввод, если символ не цифра
            }
        }

        private void INNTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем ввод только цифр
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true; // Блокируем ввод, если символ не цифра
            }
        }




        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                currentEmployee.EmployeePhoto = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeeSurname))
                errors.AppendLine("Укажите фамилию сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.Post))
                errors.AppendLine("Укажите должность сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.AdmissionYear.ToString()) || currentEmployee.AdmissionYear < 2008 || currentEmployee.AdmissionYear > DateTime.Today.Year)
                errors.AppendLine("Укажите год поступления на работу сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.Salary.ToString()) || currentEmployee.Salary <= 11999 || currentEmployee.Salary > 150000)
                errors.AppendLine("Укажите заработную плату сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeePassport?.ToString()) || currentEmployee.EmployeePassport?.Length != 10)
                errors.AppendLine("Укажите паспортные данные сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeeINN?.ToString()) || currentEmployee.EmployeeINN?.Length != 12)
                errors.AppendLine("Укажите ИНН сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeeINN?.ToString()))
                errors.AppendLine("Укажите СНИЛС сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeeBirthday?.ToString()))
                errors.AppendLine("Укажите дату рождения сотрудника!");
            if (string.IsNullOrWhiteSpace(currentEmployee.EmployeeAddress?.ToString()))
                errors.AppendLine("Укажите адрес сотрудника!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            currentEmployee.LibraryID = EmpWorkBox.SelectedIndex + 1;

            if (currentEmployee.EmployeeID == 0)
            {
                BikbulatovLibraryEntities.GetContext().Employee.Add(currentEmployee);
            }

            try
            {
                BikbulatovLibraryEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
