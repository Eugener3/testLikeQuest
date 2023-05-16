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
    public partial class BiletsFrame : Page
    {

        private string chooseVar;
        private string answer;
        private string filename;
        private List<CustomObject> questionsList;
        private int key = 0;
        private int idQuestion;

        public class CustomObject 
        {
            private static int keyCounter = 0;

            public int Key { get; }
            public int IdQuestion { get; set; }
            public string Title { get; set; }
            public string Var1 { get; set; }
            public string Var2 { get; set; }
            public string Var3 { get; set; }
            public string Var4 { get; set; }
            public string Answer { get; set; }
            public string ImgUrl { get; set; }

            public CustomObject()
            {
                Key = ++keyCounter;
            }

            public static CustomObject GetObjectByKey(int key)
            {
                // Здесь вам нужно обращаться к вашей базе данных или коллекции объектов,
                // чтобы найти объект с указанным ключом и вернуть его
                // В этом примере я просто создаю новый объект для иллюстрации

                CustomObject obj = new CustomObject();
                obj.IdQuestion = key;
                obj.Title = "Object with Key " + key;
                obj.Var1 = "Var1";
                obj.Var2 = "Var2";
                obj.Var3 = "Var3";
                obj.Var4 = "Var4";
                obj.Answer = "Answer";
                obj.ImgUrl = "ImgUrl";

                return obj;
            }
        }

        public BiletsFrame()
        {
            InitializeComponent();
            questionsList = new List<CustomObject>();
            FillQuestions();
            FillBiletComboBox();
            FillThemeBox();
            DisplayNextQuestion();
        }
        private void FillThemeBox()
        {
 
            tlqEntities db = new tlqEntities();

            using (db)
            {
                foreach (categories c in db.categories)
                {

                    themeBox.Items.Add(c.nameCategory);
                }
            }
        }


        private void DisplayNextQuestion()
        {
            if (questionsList.Count > 0 && key < questionsList.Count)
            {
                CustomObject obj = questionsList[key];
                idQuestion = obj.IdQuestion;
                title.Content = obj.Title;
                var1.Content = obj.Var1;
                var2.Content = obj.Var2;
                var3.Content = obj.Var3;
                if (obj.Var4.Length > 5)
                {
                    var4.Content = obj.Var4;
                }
                if (obj.ImgUrl.Length > 5)
                {

                    string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
                    filename = obj.ImgUrl;
                    string final = sourcePath + filename;

                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.UriSource = new Uri(final, UriKind.RelativeOrAbsolute);
                    imageSource.EndInit();

                    img.Source = imageSource;
                }
                answer = obj.Answer;
                key++;
            }
            else
            {
                key = 1;
                CustomObject obj = questionsList[key];
                idQuestion = obj.IdQuestion;
                title.Content = obj.Title;
                var1.Content = obj.Var1;
                var2.Content = obj.Var2;
                var3.Content = obj.Var3;
                if (obj.Var4.Length > 5)
                {
                    var4.Content = obj.Var4;
                }
                if (obj.ImgUrl.Length > 5)
                {

                    string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
                    filename = obj.ImgUrl;
                    string final = sourcePath + filename;

                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.UriSource = new Uri(final, UriKind.RelativeOrAbsolute);
                    imageSource.EndInit();

                    img.Source = imageSource;
                }
                answer = obj.Answer;
                key++;
            }
        }

        private void FillQuestions() 
        {



            tlqEntities db = new tlqEntities();

            using (db)
            {
                
                foreach (questions question in db.questions)
                {
                    CustomObject quest = new CustomObject();
                    quest.IdQuestion = question.idQuestion;
                    quest.Title = question.title;
                    quest.Var1 = question.var1;
                    quest.Var2 = question.var2;
                    quest.Var3 = question.var3;
                    if (question.var4 != null)
                    {
                        quest.Var4 = question.var4;
                    }
                    else
                    {
                        quest.Var4 = "lal";
                    }
                    quest.Answer = question.answer;
                    if (question.imgUrl != null)
                    {
                        quest.ImgUrl = question.imgUrl;
                    }
                    else
                    {
                        quest.ImgUrl = "lul";
                    }
                    questionsList.Add(quest);
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

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.userID = null;
            NavigationService.Navigate(new MainFrame());
        }

        private void themeBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void biletBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void var1_Click(object sender, RoutedEventArgs e)
        {
            chooseVar = "var1";
        }

        private void var2_Click(object sender, RoutedEventArgs e)
        {
            chooseVar = "var2";
        }

        private void var3_Click(object sender, RoutedEventArgs e)
        {
            chooseVar = "var3";
        }

        private void var4_Click(object sender, RoutedEventArgs e)
        {
            chooseVar = "var4";
        }

        private void AnsButton_Click(object sender, RoutedEventArgs e)
        {

            tlqEntities db = new tlqEntities();

            var wrongAnswers = db.wrongAnswers;
            var rightAnswers = db.rightAnswers;

            
            using (db)
            {

                if (chooseVar == answer)
                {
                    rightAnswers rightAnswer = new rightAnswers();

                    int maxIdRightAnswer;

                    try
                    {
                        maxIdRightAnswer = (from rb in db.rightAnswers select rb.idRightAnswer).Max();
                    }
                    catch (Exception)
                    {
                        maxIdRightAnswer = 1;
                    }


                    rightAnswer.idRightAnswer = maxIdRightAnswer;
                    rightAnswer.idUser = Convert.ToInt32(MainWindow.userID);
                    rightAnswer.idQuestion = Convert.ToInt32(idQuestion);

                    db.rightAnswers.Add(rightAnswer);
                    db.SaveChanges();

                    MessageBox.Show("Вы ответили правильно");
                }
                else
                {
                    wrongAnswers wrongAnswer = new wrongAnswers();

                    int maxIdWrongAnswer;

                    try
                    {
                        maxIdWrongAnswer = (from wb in db.wrongAnswers select wb.idWrongAnswer).Max();
                    }
                    catch (Exception)
                    {
                        maxIdWrongAnswer = 1;
                    }


                    wrongAnswer.idWrongAnswer = maxIdWrongAnswer;
                    wrongAnswer.idUser = Convert.ToInt32(MainWindow.userID);
                    wrongAnswer.idQuestion = Convert.ToInt32(idQuestion);

                    db.wrongAnswers.Add(wrongAnswer);
                    db.SaveChanges();

                    MessageBox.Show("Вы ответили не правильно");
                }
            }

            img.Source = null;
            DisplayNextQuestion();
            chooseVar = "";
            filename = "";
        }

        private void WrongButton_Click(object sender, RoutedEventArgs e)
        {
            if (questionsList.Count > 0 && key < questionsList.Count)
            {
                CustomObject obj = questionsList[key];
                idQuestion = obj.IdQuestion;
                title.Content = obj.Title;
                var1.Content = obj.Var1;
                var2.Content = obj.Var2;
                var3.Content = obj.Var3;
                if (obj.Var4.Length > 5)
                {
                    var4.Content = obj.Var4;
                }
                if (obj.ImgUrl.Length > 5)
                {

                    string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
                    filename = obj.ImgUrl;
                    string final = sourcePath + filename;

                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.UriSource = new Uri(final, UriKind.RelativeOrAbsolute);
                    imageSource.EndInit();

                    img.Source = imageSource;
                }
                answer = obj.Answer;
                key++;
            }
            else
            {
                key = 1;
                CustomObject obj = questionsList[key];
                idQuestion = obj.IdQuestion;
                title.Content = obj.Title;
                var1.Content = obj.Var1;
                var2.Content = obj.Var2;
                var3.Content = obj.Var3;
                if (obj.Var4.Length > 5)
                {
                    var4.Content = obj.Var4;
                }
                if (obj.ImgUrl.Length > 5)
                {

                    string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
                    filename = obj.ImgUrl;
                    string final = sourcePath + filename;

                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.UriSource = new Uri(final, UriKind.RelativeOrAbsolute);
                    imageSource.EndInit();

                    img.Source = imageSource;
                }
                answer = obj.Answer;
                key++;
            }
        }

        private void FillWrongQuestions()
        {

            tlqEntities db = new tlqEntities();

            using (db)
            {

                foreach (questions question in db.questions)
                {
                    CustomObject quest = new CustomObject();
                    quest.IdQuestion = question.idQuestion;
                    quest.Title = question.title;
                    quest.Var1 = question.var1;
                    quest.Var2 = question.var2;
                    quest.Var3 = question.var3;
                    if (question.var4 != null)
                    {
                        quest.Var4 = question.var4;
                    }
                    else
                    {
                        quest.Var4 = "lal";
                    }
                    quest.Answer = question.answer;
                    if (question.imgUrl != null)
                    {
                        quest.ImgUrl = question.imgUrl;
                    }
                    else
                    {
                        quest.ImgUrl = "lul";
                    }
                    questionsList.Add(quest);
                }
            }

        }
    }
}
