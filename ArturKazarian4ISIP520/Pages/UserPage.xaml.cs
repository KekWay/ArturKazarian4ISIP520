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
    public partial class UserPage : Window // Объявление класса UserPage, который наследует от класса Window.
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1(); // Создание экземпляра класса контекста базы данных.

        public UserPage() // Конструктор класса UserPage.
        {
            InitializeComponent(); // Вызов метода InitializeComponent() для инициализации компонентов окна.
            LoadEvents(); // Вызов метода LoadEvents() для загрузки списка мероприятий при инициализации окна пользователя.
        }

        private void LoadEvents() // Метод для загрузки списка мероприятий в список ListBox.
        {
            lstEvents.ItemsSource = db.Event.ToList(); // Установка списка мероприятий в качестве источника данных для элемента управления lstEvents.
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Получаем текст из searchTextBox
            string searchText = SearchTextBox.Text.ToLower();



            // Фильтруем данные в DataGrid в соответствии с запросом searchText
            var filteredData = db.Event
                .Where(s => s.Name.ToLower().Contains(searchText) ||
                            s.Days.ToString().Contains(searchText) ||
                            s.City.Name.ToLower().Contains(searchText))
                .ToList();
            // Обновляем отображаемые данные в DataGrid
            lstEvents.ItemsSource = filteredData;
        }


      
    }
}
