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
    public partial class AddPage : Window // Объявление класса AddPage, который наследует от класса Window.
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1(); // Создание экземпляра класса контекста базы данных.

        public AddPage() // Конструктор класса AddPage.
        {
            InitializeComponent(); // Вызов метода InitializeComponent() для инициализации компонентов окна.
            cmbFormOfEvent.ItemsSource = db.City.Select(g => g.Name).ToList(); // Вывод данных из таблицы FormOfEvents в ComboBox
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки "btnAdd".
        {
            // Проверяем заполненность всех необходимых полей
            if (string.IsNullOrWhiteSpace(txtVenue.Text) ||
                dpDate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtNumberOfVisitors.Text) ||
                cmbFormOfEvent.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return; // Прекращаем выполнение метода, так как не все поля заполнены
            }

            // Проверяем, является ли количество посетителей числом
            if (!int.TryParse(txtNumberOfVisitors.Text, out int numberOfVisitors))
            {
                MessageBox.Show("Пожалуйста, введите корректное число посетителей.");
                return; // Прекращаем выполнение метода, так как количество посетителей не является числом
            }

            // Проверяем, выбрано ли значение из ComboBox
            if (cmbFormOfEvent.SelectedItem != null)
            {
                // Получаем название формы мероприятия из ComboBox
                string namecity = cmbFormOfEvent.SelectedItem.ToString();

                // Находим соответствующий объект FormOfEvents по имени
                var namecity1 = db.City.FirstOrDefault(f => f.Name == namecity);

                if (namecity1 != null)
                {
                    // Создаем новое мероприятие и заполняем его данными
                    Event newEvent = new Event
                    {
                        Name = txtVenue.Text,
                        Date = (DateTime)dpDate.SelectedDate,
                        Days = int.Parse(txtNumberOfVisitors.Text),
                        CityId = namecity1.Id
                    };

                    // Добавляем новое мероприятие в базу данных
                    db.Event.Add(newEvent);
                    db.SaveChanges(); // Сохраняем изменения в базе данных
                    MessageBox.Show("Мероприятие добавлено успешно.");
                    Close(); // Закрываем окно
                }
                else
                {
                    MessageBox.Show("Выбранная форма мероприятия не найдена.");
                }
            }
            else
            {
                MessageBox.Show("Выберите форму мероприятия из списка.");
            }
        }

        private void cmbFormOfEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
