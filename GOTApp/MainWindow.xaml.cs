using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gecko;
using Newtonsoft.Json;

namespace GOTApp
{
    public partial class MainWindow : Window
    {
        private string url = "https://api.got.show/api/book/characters";
        public MainWindow()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");

            using (WebClient webClient = new WebClient())
            {
                byte[] byteArray = webClient.DownloadData(url);
                string json = Encoding.UTF8.GetString(byteArray);
                List<Datum> rootObject = JsonConvert.DeserializeObject<List<Datum>>(json);
                foreach (var character in rootObject)
                {
                    characterListBox.Items.Add(character);
                }
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            GeckoWebBrowser browser = new GeckoWebBrowser();
            host.Child = browser;
            webWindowGrid.Children.Add(host);
            browser.Navigate("http://viewers-guide.hbo.com/game-of-thrones/season-6/episode-10/map/location/77/bay-of-dragons");
        }

        private void CharacterListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            var selectedItem = characterListBox.SelectedItem as Datum;
            if (selectedItem.Image != null)
            {
                Uri uri = new Uri((characterListBox.SelectedItem as Datum).Image);
                BitmapImage bitmapImage = new BitmapImage(uri);
                infoWindow.characterImage.Source = bitmapImage;
            }
            infoWindow.nameLabel.Content += selectedItem.Name;
            infoWindow.houseLabel.Content += selectedItem.House;
            infoWindow.genderLabel.Content += selectedItem.Gender;
            infoWindow.actorLabel.Content += selectedItem.Actor;
            infoWindow.Show();
        }

        private void SearcherTextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < characterListBox.Items.Count; i++)
            {
                if (!(characterListBox.Items[i] as Datum).Name.Contains((sender as TextBox).Text))
                {
                    characterListBox.Items.RemoveAt(i);
                }
            }
        }
    }
}