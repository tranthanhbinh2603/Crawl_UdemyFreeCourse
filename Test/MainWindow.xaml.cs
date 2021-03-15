using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Test
{
    class Item : INotifyPropertyChanged
    {
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _Captions;
        public string Captions {
            get
            {
                return _Captions;
            }
            set
            {
                _Captions = value;
                OnPropertyChanged("Captions");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        ObservableCollection<Item> items;
        public MainWindow()
        {            
            InitializeComponent();
            items = new ObservableCollection<Item>();
            items.Add(new Item() { Name = "Bình", Captions = "Tôi đây" });
            items.Add(new Item() { Name = "Ánh", Captions = "Tôi đây" });
            items.Add(new Item() { Name = "Thương", Captions = "Tôi đây" });
            lvUsers.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            items[1].Captions = "Địt mẹ nó";
        }
    }
}
