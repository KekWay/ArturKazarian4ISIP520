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
    public partial class EditPage : Window // Объявление класса EditPage, который наследует от класса Window.
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1(); // Создание экземпляра класса контекста базы данных.
        private Event selectedEvent; // Поле для хранения выбранного мероприятия.

        public EditPage(Event selectedEvent) // Конструктор класса EditPage, принимающий выбранное мероприятие в качестве аргумента.
        {
            InitializeComponent(); // Вызов метода InitializeComponent() для инициализации компонентов окна.
            this.selectedEvent = selectedEvent; // Присвоение значения выбранного мероприятия полю selectedEvent.
            LoadEventData(); // Вызов метода LoadEventData() для загрузки данных о мероприятии в элементы управления.
            cmbFormOfEvent.ItemsSource = db.City.Select(g => g.Name).ToList(); // Вывод данных из таблицы FormOfEvents в ComboBox
        }

        private void LoadEventData() // Метод для загрузки данных о мероприятии в элементы управления.
        {
            txtVenue.Text = selectedEvent.Name; // Заполнение текстового поля места проведения мероприятия.
            dpDate.SelectedDate = selectedEvent.Date; // Заполнение выбранной даты мероприятия.
            txtNumberOfVisitors.Text = selectedEvent.Days.ToString(); // Заполнение текстового поля количества посетителей мероприятия.
            cmbFormOfEvent.SelectedItem = selectedEvent.City.Name; // Заполнение ComboBox выбранного мероприятия.
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "Сохранить".
        {
            try // Начало блока обработки исключений.
            {
                selectedEvent.Name = txtVenue.Text; // Обновление места проведения мероприятия.
                selectedEvent.Date = (DateTime)dpDate.SelectedDate; // Обновление даты мероприятия.
                selectedEvent.Days = int.Parse(txtNumberOfVisitors.Text); // Обновление количества посетителей мероприятия.
                string selectedCityName = cmbFormOfEvent.SelectedItem as string; // Получаем выбранный элемент ComboBox в виде строки
                var citiesname = db.City.FirstOrDefault(g => g.Name == selectedCityName); // Ищем соответствующий объект формы мероприятия в базе данных по имени
                selectedEvent.City = citiesname; // Присваиваем найденный объект формы мероприятия выбранному мероприятию

                db.SaveChanges(); // Сохранение изменений в базе данных.
                MessageBox.Show("Изменения сохранены успешно."); // Вывод сообщения об успешном сохранении изменений.
                Close(); // Закрытие окна.
            }
            catch (Exception ex) // Обработка исключений.
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}"); // Вывод сообщения об ошибке.
            }
        }
    }
}
