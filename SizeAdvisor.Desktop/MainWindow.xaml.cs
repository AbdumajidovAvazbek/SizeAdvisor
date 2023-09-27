using System;
using System.Windows;
using SizeAdvisor.Service.Dtos;
using SizeAdvisor.Service.Services;
using SizeAdvisor.Service.Interfaces;
using System.Windows.Documents;
using System.Collections.Generic;
using SizeAdvisor.Desktop.Companents;

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
                mainTapControl.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void MenuBtnClick(object sender, RoutedEventArgs e)
        {
            mainTapControl.SelectedIndex = 0;
        }
    }
}
