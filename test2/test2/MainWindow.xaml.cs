using System.Text;
using test2.Controls;
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


namespace test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User CurrentUser;

        public MainWindow()
        {
            InitializeComponent();

            Session.Window = this;

            this.MainContent.Content = new ResgisrationControl();
        }
    }
}