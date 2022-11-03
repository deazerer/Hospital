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
    /// Логика взаимодействия для MedicalCard.xaml
    /// </summary>
    public partial class MedicalCard : Window
    {
        public MedicalCard()
        {
            InitializeComponent();
            DGRefresh();
        }
        public void DGRefresh()
        {
            using(HospitalEntities hosp = new HospitalEntities())
            {
                medCardsGrid.ItemsSource = hosp.MedicalCards.ToList();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalCard addMedicalCard = new AddMedicalCard(this, (sender as Button).DataContext as Hospital.MedicalCard);
            addMedicalCard.Show();
            this.Hide();
        }

        private void DltBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Вы уверены что хотите удалить данную запись?", "Удаление записи",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Hospital.MedicalCard medicalCard = (sender as Button).DataContext as Hospital.MedicalCard;
                    using (HospitalEntities context = new HospitalEntities())
                    {
                        context.Entry(medicalCard).State = System.Data.Entity.EntityState.Deleted;
                        context.MedicalCards.Remove(medicalCard);
                        context.SaveChanges();
                        DGRefresh();
                        MessageBox.Show("Запись удалена!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись!");
            }
            
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMedicalCard addMedicalCard = new AddMedicalCard(this, (sender as Button).DataContext as Hospital.MedicalCard);
            addMedicalCard.Show();
            this.Hide();        
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }
    }
}
