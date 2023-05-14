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
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Win32;

namespace testLikeQuest
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        private string selectedFilePath;

        public AdminPanel()
        {
            InitializeComponent();
            FillComboBox();
            FillSelComboBox();
            FillBiletComboBox();


            answer.Items.Add("var1");
            answer.Items.Add("var2");
            answer.Items.Add("var3");
            answer.Items.Add("var4");

        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonsPanel.Visibility = Visibility.Hidden;
            addQuestionPanel.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void CreateQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            tlqEntities db = new tlqEntities();
            if (title.Text == "" || var1.Text == "" || var2.Text == "" || var3.Text == "" || answer.Text == "" || SelcategoryBox.Text == "" || biletBox.Text == "")
            {
                MessageBox.Show("Заполните все обязательные поля");
            }
            else {
                using (db)
                {
                    int maxIdQuestion = (from us in db.questions select us.idQuestion).Max();
                    questions question = new questions();

                    question.idQuestion = maxIdQuestion + 1;
                    question.title = title.Text;
                    question.var1 = var1.Text;
                    question.var2 = var2.Text;
                    question.var3 = var3.Text;
                    question.answer = answer.Text;
                    question.nameCategory = SelcategoryBox.Text;
                    question.nameBilet = int.Parse(biletBox.Text);
                    if (var4.Text != "")
                    {
                        question.var4 = var4.Text;
                    }
                    if (selectedFilePath != "")
                    {

                        var fileName = System.IO.Path.GetFileName(selectedFilePath);
                        string destFile = System.IO.Path.Combine(@"images\", fileName);

                        File.Copy(selectedFilePath, destFile, true);

                        question.imgUrl = destFile;
                    }

                    db.questions.Add(question);
                    db.SaveChanges();

                    MessageBox.Show("Вопрос успешно создан");
                }
            }
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
            }
            string fileName = System.IO.Path.GetFileName(selectedFilePath);
            chooseImg.Content = fileName;

        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {

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

        private void FillBiletComboBox()
        {

            tlqEntities db = new tlqEntities();

            using (db)
            {
                int[] biletsBox = new int[20];
                foreach (bilets c in db.bilets)
                {
                    int i = 0;


                    biletsBox[i] = c.nameBilet;
                    i++;
                    Array.Sort(biletsBox);
                    
                }
                for (int i = 0; i < biletsBox.Length; i++)
                {
                    biletBox.Items.Add(biletsBox[i]);
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

                    category.nameCategory = categoryBox.Text;

                    db.categories.Add(category);
                    db.SaveChanges();
                    MessageBox.Show("Категория успешно создана");
                    categoryBox.Text = null;
                    FillComboBox();
                    FillSelComboBox();
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
