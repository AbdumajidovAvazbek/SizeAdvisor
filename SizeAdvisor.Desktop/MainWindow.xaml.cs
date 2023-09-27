using System;
using System.Windows;
using SizeAdvisor.Service.Dtos;
using SizeAdvisor.Service.Services;
using SizeAdvisor.Service.Interfaces;
using System.Windows.Documents;
using System.Collections.Generic;

namespace SizeAdvisor.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly IUserService userService = new UserService();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OkClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ForCheckedDto checkedDto = new ForCheckedDto()
                {
                    email = emailniKirtish.Text,
                    password = passwordniKiritish.Password
                };
                bool available = await userService.IsThereAsync(checkedDto);

                if (available)
                {
                    mainTapControl.SelectedIndex = 4;
                }
                else
                {
                    MessageBox.Show("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            mainTapControl.SelectedIndex = 2;
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            mainTapControl.SelectedIndex = 1;
        }

        public async void QoshishClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (password.Password == againPassword.Password)
                {
                    UserForCreationDto user = new UserForCreationDto()
                    {
                        Email = emailOlish.Text,
                        FirstName = isimOlish.Text,
                        LastName = familyaOlish.Text,
                        Password = password.Password
                    };
                    await userService.CreateAsync(user);
                    mainTapControl.SelectedIndex = 4;
                }
                else
                    MessageBox.Show("Passwordni togri kiriting iltimos");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public  void RazmerniOlish(object sender, RoutedEventArgs e)
        {
            try
            {
                UserSizeDto userSize = new UserSizeDto()
                {
                    Height = decimal.Parse(Ogirlik.Text),
                    Weight = decimal.Parse(Balanlik.Text),
                };
                string[] razmerlar = { "XS", "S", "M", "L", "XL", "XXL", "XXXL", "4XL" };

                string razmer = string.Empty;

                if (userSize.Height > 135 || (userSize.Height <= 140 && userSize.Weight > 25) || (userSize.Height <= 140 && userSize.Weight <= 30))
                    razmer = $"Futbolka : {razmerlar[0]} \n Shim : S ";
                else if (userSize.Height > 140 || (userSize.Height <= 145 && userSize.Weight > 30) || (userSize.Height <= 145 && userSize.Weight <= 35))
                    razmer = $"Futbolka : {razmerlar[1]} \n Shim : M ";
                else if  (userSize.Height > 145 || (userSize.Height <= 150 && userSize.Weight > 35) || (userSize.Height <= 150 && userSize.Weight <= 45))
                    razmer = $"Futbolka : {razmerlar[2]} \n Shim : L ";
                else if (userSize.Height > 150 || (userSize.Height <= 155 && userSize.Weight > 45) || (userSize.Height <= 155 && userSize.Weight <= 55))
                    razmer = $"Futbolka : {razmerlar[3]} \n Shim : XL ";
                else if (userSize.Height > 155 || (userSize.Height <= 165 && userSize.Weight > 55) || (userSize.Height <= 165 && userSize.Weight <= 60))
                    razmer = $"Futbolka : {razmerlar[4]} \n Shim : XXL ";
                else if (userSize.Height > 165 || (userSize.Height <= 170 && userSize.Weight > 60) || (userSize.Height <= 170 && userSize.Weight <= 80))
                    razmer = $"Futbolka : {razmerlar[5]} \n Shim : XXXL ";
                else if (userSize.Height > 170 || (userSize.Height <= 175 && userSize.Weight > 80) || (userSize.Height <= 175 && userSize.Weight <= 100))
                    razmer = $"Futbolka : {razmerlar[6]} \n Shim : XXXL ";
                else if (userSize.Height > 175 || (userSize.Height <= 185 && userSize.Weight > 100) || (userSize.Height <= 185 && userSize.Weight <= 120))
                    razmer = $"Futbolka : {razmerlar[7]} \n Shim : XXXL ";
                else
                    MessageBox.Show("Bu bo'y va vazn bilan faqatgini kiyimni tiktirishingiz mumkun xalos");
                mainTapControl.SelectedIndex = 3;

                MessageBox.Show(razmer);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
