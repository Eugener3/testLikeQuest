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
using System.Runtime.InteropServices;
using System.Security;

namespace testLikeQuest
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            string password = null;
            IntPtr passwordBSTR = IntPtr.Zero;
            SecureString passwordSecure = passwordBox.SecurePassword;

            passwordBSTR = Marshal.SecureStringToBSTR(passwordSecure);
            password = Marshal.PtrToStringBSTR(passwordBSTR);

            tlqEntities db = new tlqEntities();

            using (db)
            {
                if (login.Text == "" || password == "")
                {
                    MessageBox.Show("Заполните все поля");
                }
                else
                {
                    var users = db.users;
                    bool userValid = false;
                    foreach (users u in users)
                    {
                        if (login.Text == u.login)
                        {
                            userValid = true;
                            if (password == u.password)
                            {
                                MainWindow.userID = u.idUser;
                                if (login.Text == "admin")
                                {
                                    NavigationService.Navigate(new AdminPanel());
                                }
                                else
                                {
                                    NavigationService.Navigate(new BiletsFrame());
                                }
                                break;
                            }
                            else {
                                MessageBox.Show("Пароль введён не верно");
                                break;
                            }
                        }
                        else
                        {
                            userValid = false;
                        }
                    }
                    if (!userValid)
                    {
                        MessageBox.Show("Пользователя с таким логином не существует");
                    }

                }
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }

    }
}
