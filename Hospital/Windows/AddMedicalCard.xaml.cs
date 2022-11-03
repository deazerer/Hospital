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
    /// Логика взаимодействия для AddMedicalCard.xaml
    /// </summary>
    public partial class AddMedicalCard : Window
    {
        MedicalCard _medicalCardWindow;
        Hospital.MedicalCard _medicalCard;
        public AddMedicalCard(MedicalCard medicalCardWindow, Hospital.MedicalCard medicalCard = null)
        {
            InitializeComponent();
            _medicalCardWindow = medicalCardWindow;
            _medicalCard = medicalCard;
            if (_medicalCard != null)
            {
                fullNameTB.Text = _medicalCard.fullName;
                genderTB.Text = _medicalCard.gender;
                ageTB.Text = _medicalCard.age.ToString();
                diagnosisTB.Text = _medicalCard.diagnosis;
                heightTB.Text = _medicalCard.height.ToString();
                hairColorTB.Text = _medicalCard.hairColor;
                signsTB.Text = _medicalCard.signs;
                AddBtn.Content = "Изменить";
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using(HospitalEntities hospitalEntities = new HospitalEntities())
            {
                try
                {
                    if(_medicalCard == null)
                    {
                        hospitalEntities.MedicalCards.Add(new Hospital.MedicalCard
                        {
                            fullName = fullNameTB.Text,
                            gender = genderTB.Text,
                            age = Convert.ToInt32(ageTB.Text),
                            diagnosis = diagnosisTB.Text,
                            height = Convert.ToInt32(heightTB.Text),
                            hairColor = hairColorTB.Text,
                            signs = signsTB.Text
                        });
                     }
                    else
                    {
                        hospitalEntities.MedicalCards.Attach(_medicalCard);
                        _medicalCard.fullName = fullNameTB.Text;
                        _medicalCard.gender = genderTB.Text;
                        _medicalCard.age = Convert.ToInt32(ageTB.Text);
                        _medicalCard.diagnosis = diagnosisTB.Text;
                        _medicalCard.height = Convert.ToInt32(heightTB.Text);
                        _medicalCard.hairColor = hairColorTB.Text;
                        _medicalCard.signs = signsTB.Text;
                    }
                    hospitalEntities.SaveChanges();
                    MessageBox.Show("Успешно выполнено!");
                    _medicalCardWindow.DGRefresh();
                    _medicalCardWindow.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Некорректно заполненные данные!");
                }
            }
        }
    }
}
