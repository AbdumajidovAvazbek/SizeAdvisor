using System;
using System.Windows;
using System.Windows.Controls;
using SizeAdvisor.Service.Dtos;

namespace SizeAdvisor.Desktop.Companents
{
    public partial class SizeRoom : UserControl
    {
        public SizeRoom()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                decimal height = decimal.Parse(mainWindow.Balanlik.Text);
                decimal weight = decimal.Parse(mainWindow.Ogirlik.Text);
             
                string[] sizes = CalculateSizes(height, weight);
                futbolkaSize.Text = sizes[0].ToString();
                shimSize.Text = sizes[1].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string[] CalculateSizes(decimal height, decimal weight)
        {
            string[] futbolkaRazmerlari = { "XS", "S", "M", "L", "XL", "XXL", "XXXL", "4XL" };
            string[] shimRazmerlari = { "S", "M", "L", "XL", "XXL", "XXXL", "XXXL", "5XL" };
            string[] sizes = new string[2];

            if (height > 135 && weight > 25 && height <= 140 && weight <= 30)
            {
                sizes[0] = futbolkaRazmerlari[0];
                sizes[1] = shimRazmerlari[0];
            }
            else if (height > 140 && weight > 30 && height <= 145 && weight <= 35)
            {
                sizes[0] = futbolkaRazmerlari[1];
                sizes[1] = shimRazmerlari[1];
            }
            else if (height > 145 && weight > 35 && height <= 150 && weight <= 45)
            {
                sizes[0] = futbolkaRazmerlari[2];
                sizes[1] = shimRazmerlari[2];
            }
            else if (height > 150 && weight > 45 && height <= 155 && weight <= 55)
            {
                sizes[0] = futbolkaRazmerlari[3];
                sizes[1] = shimRazmerlari[3];
            }
            else if (height > 155 && weight > 55 || height <= 165 && weight <= 60)
            {
                sizes[0] = futbolkaRazmerlari[4];
                sizes[1] = shimRazmerlari[4];
            }
            else if (height > 165 && weight > 80 || height <= 175 && weight <= 100)
            {
                sizes[0] = futbolkaRazmerlari[5];
                sizes[1] = shimRazmerlari[5];
            }
            else if (height > 175 && height <= 180 && weight > 100 && weight <= 120)
            {
                sizes[0] = futbolkaRazmerlari[6];
                sizes[1] = shimRazmerlari[6];
            }
            else if (height > 190 || weight > 100 || height <= 200  ||weight <= 120)
            {
                sizes[0] = futbolkaRazmerlari[7];
                sizes[1] = shimRazmerlari[7];
            }
            else
            {
                MessageBox.Show("Bu bo'y va vazn bilan faqatgini kiyimni tiktirishingiz mumkun xalos");
            }

            return sizes;
        }

    }
}
