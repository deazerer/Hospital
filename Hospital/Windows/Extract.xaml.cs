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
using System.Windows.Shapes;

namespace Hospital.Windows
{
    /// <summary>
    /// Логика взаимодействия для Extract.xaml
    /// </summary>
    public partial class Extract : Window
    {
        public Extract()
        {
            InitializeComponent();
            using(HospitalEntities hospital = new HospitalEntities())
            {
                extractGrid.ItemsSource = hospital.Extracts.ToList();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
