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
using Word = Microsoft.Office.Interop.Word;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void extractBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Extract extract = new Windows.Extract();
            extract.Show();
            this.Hide();
        }

        private void medicalCardBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.MedicalCard medicalCard = new Windows.MedicalCard();
            medicalCard.Show();
            this.Hide();
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки medicalCardBtn
        /// Экспорт таблицы MedicalCard в PDF-файл
        /// </summary>
        private void medicalCardPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            using (HospitalEntities ent = new HospitalEntities())
            {
                List<Hospital.MedicalCard> medicalCards = ent.MedicalCards.OrderBy(x => x.id).ToList();
                var app = new Word.Application();
                Word.Document doc = app.Documents.Add();
                Word.Paragraph paragraph = doc.Paragraphs.Add();
                Word.Range range = paragraph.Range;
                range.Text = "Медицинские карты";
                paragraph.set_Style("Заголовок 1");
                range.InsertParagraphAfter();
                Word.Paragraph tableParagraph = doc.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table medTable = doc.Tables.Add(tableRange, medicalCards.Count() + 1, 8);
                medTable.Borders.InsideLineStyle = medTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                medTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                Word.Range cellRange;
                cellRange = medTable.Cell(1, 1).Range;
                cellRange.Text = "Порядковый номер";
                cellRange = medTable.Cell(1, 2).Range;
                cellRange.Text = "ФИО";
                cellRange = medTable.Cell(1, 3).Range;
                cellRange.Text = "Пол";
                cellRange = medTable.Cell(1, 4).Range;
                cellRange.Text = "Возраст";
                cellRange = medTable.Cell(1, 5).Range;
                cellRange.Text = "Диагноз";
                cellRange = medTable.Cell(1, 6).Range;
                cellRange.Text = "Рост";
                cellRange = medTable.Cell(1, 7).Range;
                cellRange.Text = "Цвет волос";
                cellRange = medTable.Cell(1, 8).Range;
                cellRange.Text = "Отличительные черты";
                medTable.Rows[1].Range.Bold = 1;
                medTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int i = 1;
                foreach (var currentMedicalCard in medicalCards)
                {
                    cellRange = medTable.Cell(i + 1, 1).Range;
                    cellRange.Text = currentMedicalCard.id.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 2).Range;
                    cellRange.Text = currentMedicalCard.fullName.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 3).Range;
                    cellRange.Text = currentMedicalCard.gender.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 4).Range;
                    cellRange.Text = currentMedicalCard.age.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 5).Range;
                    cellRange.Text = currentMedicalCard.diagnosis.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 6).Range;
                    cellRange.Text = currentMedicalCard.height.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 7).Range;
                    cellRange.Text = currentMedicalCard.hairColor.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = medTable.Cell(i + 1, 8).Range;
                    cellRange.Text = currentMedicalCard.signs.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    i++;
                }
                app.Visible = true;
                doc.SaveAs2(@"C:\Users\Ильвир\Desktop\MedicalCards.pdf", Word.WdExportFormat.wdExportFormatPDF);
            }
        }
    }
}
