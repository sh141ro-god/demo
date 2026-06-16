using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using test2.Models;

namespace test2
{
    public static class Session
    {
        public static User CurrentUser;
        public static testContext Context = new testContext();

        public static MainWindow Window { get; set; }
    }
}
