using Microsoft.EntityFrameworkCore;
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
using test2.Models;

namespace test2.Controls
{
    /// <summary>
    /// Логика взаимодействия для ResgisrationControl.xaml
    /// </summary>
    public partial class ResgisrationControl : UserControl
    {
        public testContext _context;
        public ResgisrationControl()
        {

            InitializeComponent();

            _context = (Session.Context != null) ? Session.Context : new testContext();

            LoginButton.Click += Login;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Session.CurrentUser = _context.Users.FirstOrDefault(u => (u.Login == LoginText.Text && u.Password == PasswordText.Text));

            GustButton.Content = "1";

            Session.Window.MainContent.Content = new tableControl();

            if (Session.CurrentUser != null) {
                Session.Window.MainContent = new tableControl();
                    }

        }

        //private void GustButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Session.CurrentUser = _context.Users.FirstOrDefault(u => u.IdRole == 1)
        //}
    }
}
