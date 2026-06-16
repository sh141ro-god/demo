using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.Utility;
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
using test.Models;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для regControl.xaml
    /// </summary>
    public partial class regControl : UserControl
    {
        private readonly AppDbContext _context = new AppDbContext();
        public List<Models.Condition> Conditions { get; set; }   // для combobox грида (если вернёшь)
        public RelayCommand SaveCommand { get; set; }

        public regControl()
        {
            InitializeComponent();

            SaveCommand = new RelayCommand(Save);
            SaveButton.Command = SaveCommand;

            Conditions = _context.Conditions.ToList();

            _context.Employees
                .Include(e => e.IdJobNavigation)
                .Load();

            DataContext = this;
        }

        private void Save()
        {
            var window = Window.GetWindow(this) as MainWindow;
            window.Content = new tableControl();
        }

        private void Print(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
