using Microsoft.EntityFrameworkCore;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using test2.Models;

namespace test2.Controls
{
    /// <summary>
    /// Логика взаимодействия для tableControl.xaml
    /// </summary>
    public partial class tableControl : UserControl
    {
        public testContext _context;
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public tableControl()
        {
            InitializeComponent();



            _context = Session.Context;

           _context.Products.Include(e => e.IdCategoryNavigation)
                .Include(e => e.IdManufacturerNavigation)
                .Include(e => e.IdSupplierNavigation)
                .Include(e => e.IdUnitNavigation).Load();

            _context.Categories.Load();

            Products = _context.Products.Local.ToObservableCollection();

            Categories = _context.Categories.Local.ToObservableCollection();

            MainList.ItemsSource = Products;

            DataContext = this;
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox.Text.Length > 0) 
            {
                Products = Products.Where(p => p.Name.Contains(textBox.Text) || p.Description.Contains(textBox.Text)).ToObservableCollection();
            }
        }
    }
}
