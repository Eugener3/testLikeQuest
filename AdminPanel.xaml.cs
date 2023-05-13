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
            FillComboBox();
            FillSelComboBox();
        }


        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Hidden;
            addQuestionPanel.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void FillComboBox()
        {
            categoryBox.Items.Clear();

            tlqEntities db = new tlqEntities();

            using (db)
            {
                foreach (categories c in db.categories)
                {

                    categoryBox.Items.Add(c.nameCategory); 
                }
            }
        }

        private void FillSelComboBox()
        {
            SelcategoryBox.Items.Clear();

            tlqEntities db = new tlqEntities();

            using (db)
            {
                foreach (categories c in db.categories)
                {

                    SelcategoryBox.Items.Add(c.nameCategory);
                }
            }
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Hidden;
            CategoryPanel.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {

            tlqEntities db = new tlqEntities();

            using (db)
            {
                categories category = new categories();
                if (categoryBox.Text != null)
                {
                    int maxIdCategory = (from us in db.categories select us.idCategory).Max();

                    category.nameCategory = categoryBox.Text;
                    category.idCategory = maxIdCategory + 1;

                    db.categories.Add(category);
                    db.SaveChanges();
                    MessageBox.Show("Категория успешно создана");
                    categoryBox.Text = null;
                    FillComboBox();
                }
            }
        }
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            tlqEntities db = new tlqEntities();

            using (db)
            {
                // Разобраться с синтактисом удаления категории

                MessageBox.Show("Категория успешно удалена");
                categoryBox.Text = null;
                FillComboBox();
            }

                
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
