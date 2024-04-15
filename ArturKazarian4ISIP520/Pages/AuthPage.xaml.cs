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
using System.Windows.Shapes;
using ArturKazarian4ISIP520.DB;

namespace ArturKazarian4ISIP520.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Window
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1();
        public AuthPage()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "btnLogin".
        {
            string username = txtUsername.Text; // Получение логина из текстового поля.
            string password = txtPassword.Password; // Получение пароля из поля для ввода пароля.

            Users users = db.Users.FirstOrDefault(u => u.Login == username && u.Password == password); // Поиск пользователя в базе данных по введенным логину и паролю.

            if (users != null) // Если пользователь найден.
            {
                if (users.RoleID == 1) // Если у пользователя роль "admin".
                {
                    AdminPage adminPage = new AdminPage(); // Создание экземпляра страницы администратора.
                    adminPage.Show(); // Отображение страницы администратора.
                    Close(); // Закрытие текущего окна.
                }
                if (users.RoleID == 2) // Если у пользователя другая роль (не "admin").
                {
                    UserPage userWindow = new UserPage(); // Создание экземпляра страницы пользователя.
                    userWindow.Show(); // Отображение страницы пользователя.
                    Close(); // Закрытие текущего окна.
                }
            }
            else // Если пользователь не найден.
            {
                MessageBox.Show("Неверно введен логин или пароль."); // Вывод сообщения об ошибке.
            }
        }

    }
}
