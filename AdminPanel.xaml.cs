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

namespace testLikeQuest
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Hidden;
            addQuestionPanel.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Hidden;
            CategoryPanel.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.userID = null;
            NavigationService.Navigate(new MainFrame());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Visible;
            addQuestionPanel.Visibility = Visibility.Hidden;
            CategoryPanel.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;
        }
    }
}
