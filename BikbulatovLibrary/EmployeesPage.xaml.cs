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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            var currentEmployees = BikbulatovLibraryEntities.GetContext().Employee.ToList();
            EmployeeListView.ItemsSource = currentEmployees;
            filterCBox.SelectedIndex = 0;
            rbuttonUp.IsChecked = true;
        }

        private void UpdateEmployee()
        {
            var currentEmployee = BikbulatovLibraryEntities.GetContext().Employee.ToList();
            currentEmployee = currentEmployee.Where(p => (p.EmployeeSurname.ToLower().Contains(searchTBox.Text.ToLower()))).ToList();
            int EmployeeCount = currentEmployee.Count;
            if (rbuttonDown.IsChecked.Value)
            {
                currentEmployee = currentEmployee.OrderByDescending(p => p.Salary).ToList();
            }
            if (rbuttonUp.IsChecked.Value)
            {
                currentEmployee = currentEmployee.OrderBy(p => p.Salary).ToList();
            }


            if (filterCBox.SelectedIndex == 0)
            {
                currentEmployee = currentEmployee.Where(p => (p.AdmissionYear >= 1 && p.AdmissionYear <= 3000)).ToList();
            }
            if (filterCBox.SelectedIndex == 1)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Архивариус")).ToList();
            }
            if (filterCBox.SelectedIndex == 2)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Ассистент библиотекаря")).ToList();
            }
            if (filterCBox.SelectedIndex == 3)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Библиотекарь")).ToList();
            }
            if (filterCBox.SelectedIndex == 4)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Библиотечный ассистент")).ToList();
            }
            if (filterCBox.SelectedIndex == 5)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Исследователь")).ToList();
            }
            if (filterCBox.SelectedIndex == 6)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Куратор выставок")).ToList();
            }
            if (filterCBox.SelectedIndex == 7)
            {
                currentEmployee = currentEmployee.Where(p => (p.Post == "Научный сотрудник")).ToList();
            }

            TBCount.Text = currentEmployee.Count.ToString();
            TBALLRecords.Text = EmployeeCount.ToString();

            EmployeeListView.ItemsSource = currentEmployee;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Employee));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void searchTBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }

        private void filterCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateEmployee();
        }

        private void rbuttonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }

        private void rbuttonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateEmployee();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAgent = (sender as Button).DataContext as Employee;
            var currentAgentDefence = BikbulatovLibraryEntities.GetContext().FundReplenishment.ToList();

            currentAgentDefence = currentAgentDefence.Where(p => p.EmployeeID == currentAgent.EmployeeID).ToList();
            if (currentAgentDefence.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, т.к. сотрудник совершал пополнение фонда");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        BikbulatovLibraryEntities.GetContext().Employee.Remove(currentAgent);
                        BikbulatovLibraryEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BikbulatovLibraryEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                EmployeeListView.ItemsSource = BikbulatovLibraryEntities.GetContext().Employee.ToList();
            }
        }
    }
}
