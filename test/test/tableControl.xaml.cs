using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using test.Models;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для tableControl.xaml
    /// </summary>
    public partial class tableControl : UserControl
    {
        private readonly AppDbContext _context = new AppDbContext();
        public List<Employee> Employees { get; set; }
        public List<Job> Jobs { get; set; }


        public tableControl()
        {
            InitializeComponent();

            Employees = _context.Employees.ToList();


            _context.Employees.Include(e => e.IdJobNavigation).Load();

            EmployeeList.ItemsSource = Employees;
            Jobs = _context.Jobs.ToList();

            SaveButton.Click += Save;
            SortButton.Click += Sort;

            DataContext = this;

        }

        public void Save(object sender, RoutedEventArgs e)
        {
            _context.SaveChanges();
        }
        public void Sort(object sender, RoutedEventArgs e)
        {
            SortBy("FullName", ListSortDirection.Ascending);
        }

        private void SortBy(string prop, ListSortDirection listSort) 
        {
            var view = CollectionViewSource.GetDefaultView(EmployeeList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(prop, listSort));
        }
    }
}
