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
    /// Interaction logic for BiletsFrame.xaml
    /// </summary>
    public partial class BiletsFrame : Page
    {
        public BiletsFrame()
        {
            InitializeComponent();
            FillBiletComboBox();
            FillThemeBox();

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
    }
}
