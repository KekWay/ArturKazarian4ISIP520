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
    /// Логика взаимодействия для Parcipiants.xaml
    /// </summary>
    public partial class Parcipiants : Window
    {
        private Kazarian4ISIP520_EventsEntities1 db = new Kazarian4ISIP520_EventsEntities1();
        public Parcipiants()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents() // Метод для загрузки списка мероприятий в список ListBox.
        {
            Players.ItemsSource = db.Parcipiants.ToList(); // Установка списка мероприятий в качестве источника данных для элемента управления lstEvents.
        }
    }
}
