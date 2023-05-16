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

    public partial class SignUp : Page
    {

        
        public SignUp()
        {
            InitializeComponent();
            

        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
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
                            MessageBox.Show("Пользователь с таким логином уже существует");
                            userValid = false;
                            break;
                        }
                        else
                        {
                            userValid = true;
                        }
                    }
                    if (userValid)
                    {
                        int maxIdUser = (from us in db.users select us.idUser).Max();
                        users user = new users();

                        user.login = login.Text;
                        user.password = password;
                        user.idUser = maxIdUser + 1;
                        db.users.Add(user);
                        db.SaveChanges();

                        MainWindow.userID = user.idUser;
                        if (login.Text == "admin")
                        {
                            NavigationService.Navigate(new AdminPanel());
                        }
                        else
                        {
                            NavigationService.Navigate(new BiletsFrame());
                        }
                    }

                }
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }
    }
}
