using System.Configuration;
using System.Data;
using System.Windows;

namespace RecuritmentSystem20122791
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public Management Service { get; set; } = new Management();
    }

}
