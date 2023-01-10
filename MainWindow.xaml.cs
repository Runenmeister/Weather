using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json; 

namespace WeatherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apikey = "ea537718225d6e700202fd6fab04de2a";
        private string requesturl = "https://api.openweathermap.org/data/2.5/weather";


        public MainWindow()
        {
            InitializeComponent();
            UpdateData("Hamburg");

        }

        public void UpdateData(string city)
        {
            WeatherMapResponse result = GetweatherData(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "Cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "Rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "Snow.png";
            }
            backgroundImage.ImageSource = new BitmapImage(new Uri("images/" + finalImage, UriKind.Relative));
            lableTemperatur.Content = result.main.temp.ToString("F1") + "°C";
            info1.Content = result.main.humidity + " % Luftfeuchtigkeit";
            info2.Content = result.main.pressure + " Millibar Luftdruck";
            info3.Content = result.weather[0].main;
            info4.Content = result.weather[0].description;
            info5.Content = result.wind.speed + " km/h Windgeschwindigkeit";
            cr.Content = "Created by Runenmeister";
        }
        public WeatherMapResponse GetweatherData(string city)
        {
            HttpClient httpClient = new HttpClient();
            var finalUri = requesturl + "?q=" + city + "&appid=" + apikey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;

            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);
            return weatherMapResponse;
            //Console.WriteLine(weatherMapResponse);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = Query.Text;
            UpdateData(query);
        }
    }
}
