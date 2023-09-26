using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using SizeAdvisor.Service.Dtos;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using SizeAdvisor.Service.Services;
using SizeAdvisor.Service.Interfaces;
using SizeAdvisor.Domain.Entities;

namespace SizeAdvisor.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
                    password = passwordniKiritish.Text,
                };
                bool avialibe = await userService.IsThereAsync(checkedDto);
               
                  mainTapControl.SelectedIndex = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            mainTapControl.SelectedIndex = 2;
        }
        private async void SignUpClick(object sender, RoutedEventArgs e)
        {
            mainTapControl.SelectedIndex = 1;
        }
        public async void QoshishClick(object sender, RoutedEventArgs e)
        {
            try
            {
                UserForCreationDto user = new UserForCreationDto()
                {
                    Email = emailOlish.Text,
                    FirstName = isimOlish.Text,
                    LastName = familyaOlish.Text,
                    Password = password.Text,
                };
                await userService.CreateAsync(user);
                mainTapControl.SelectedIndex = 4;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void RazmerniOlish(object sender, RoutedEventArgs e)
        {
            try
            {
                UserSizeDto userSize = new UserSizeDto()
                {
                    Height = decimal.Parse(Ogirlik.Text),
                    Weight = decimal.Parse(Balanlik.Text),
                };
                mainTapControl.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


             

        }
    }
}
