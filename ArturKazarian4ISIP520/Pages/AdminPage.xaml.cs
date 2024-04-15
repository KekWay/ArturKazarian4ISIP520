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
    public partial class AdminPage : Window // Объявление класса AdminPage, который наследует от класса Window.
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1(); // Создание экземпляра класса контекста базы данных.

        public AdminPage() // Конструктор класса AdminPage.
        {
            InitializeComponent(); // Вызов метода InitializeComponent() для инициализации компонентов окна.
            LoadEvents(); // Вызов метода LoadEvents() для загрузки списка мероприятий при инициализации окна.
        }

        private void LoadEvents() // Метод для загрузки списка мероприятий в список ListBox.
        {
            lstEvents.ItemsSource = db.Event.ToList(); // Установка списка мероприятий в качестве источника данных для элемента управления lstEvents.
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Добавить мероприятие".
        {
            AddPage addEventWindow = new AddPage(); // Создание экземпляра окна для добавления нового мероприятия.
            addEventWindow.ShowDialog(); // Отображение окна для добавления мероприятия в режиме диалога.
            LoadEvents(); // После закрытия окна обновляем список мероприятий.
        }

       

        private void btnEditEvent_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Редактировать мероприятие".
        {
            Event selectedEvent = (Event)lstEvents.SelectedItem; // Получение выбранного мероприятия из списка.
            if (selectedEvent != null) // Проверка, что мероприятие выбрано.
            {
                EditPage editEventWindow = new EditPage(selectedEvent); // Создание экземпляра окна для редактирования выбранного мероприятия.
                editEventWindow.ShowDialog(); // Отображение окна для редактирования мероприятия в режиме диалога.
                LoadEvents(); // После закрытия окна обновляем список мероприятий.
            }
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Удалить мероприятие".
        {
            Event selectedEventToDel = (Event)lstEvents.SelectedItem; // Получение выбранного мероприятия из списка.
            if (selectedEventToDel != null) // Проверка, что мероприятие выбрано.
            {
                db.Event.Remove(selectedEventToDel); // Удаление выбранного мероприятия из базы данных.
                db.SaveChanges(); // Сохранение изменений в базе данных.
                LoadEvents(); // После удаления обновляем список мероприятий.
            }
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

        private void Playerss_Click(object sender, RoutedEventArgs e)
        {
            Parcipiants parcipiants = new Parcipiants();
            parcipiants.Show();
        }
    }
}
