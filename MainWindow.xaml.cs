using Newtonsoft.Json;
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
using System.Reflection;

namespace Homework_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void getBreedButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://dog.ceo/api/breeds/list/all";
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    Result r = JsonConvert.DeserializeObject<Result>(content);
                    DogBreedList list = r.Message;

                    Type t = list.GetType();
                    PropertyInfo[] properties = t.GetProperties();

                    // QUESTION 2 (LIST SUB BREEDS)
                    foreach (PropertyInfo pi in properties)
                    {
                        if (pi.Name != null)
                        {
                            breedListBox.Items.Add(pi.Name);

                            List<string> pvalues = (List<string>)pi.GetValue(list);
                            if (pvalues.Count > 0)
                            {
                                foreach (string v in pvalues)
                                {
                                    subbreedListBox.Items.Add(v);
                                }
                            }
                        }
                    }
                }
            }
        }


        // QUESTION 3 (3 PHOTOS OF BREED)
        private void breedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string breed = breedListBox.Name;
                string uri = $"https://dog.ceo/api/breed/images/random/3";
                Task<HttpResponseMessage> rm = client.GetAsync(uri);
                HttpResponseMessage response = rm.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> ts = response.Content.ReadAsStringAsync();
                    string content = ts.Result;
                    Result3 r = JsonConvert.DeserializeObject<Result3>(content);

                    BitmapImage pic1 = new BitmapImage();

                    pic1.BeginInit();
                    pic1.UriSource = new Uri(r.Message[0]);
                    pic1.EndInit();
                    dogImage1.Source = pic1;

                    BitmapImage pic2 = new BitmapImage();
                    pic2.BeginInit();
                    pic2.UriSource = new Uri(r.Message[1]);
                    pic2.EndInit();
                    dogImage2.Source = pic2;

                    BitmapImage pic3 = new BitmapImage();
                    pic3.BeginInit();
                    pic3.UriSource = new Uri(r.Message[2]);
                    pic3.EndInit();
                    dogImage3.Source = pic3;


                }


            }
        }

        // QUESTION 4 (3 PHOTOS OF SUB BREED)
        private void subbreedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string breed = subbreedListBox.Name;
                string uri = $"https://dog.ceo/api/breed/images/random/3";
                Task<HttpResponseMessage> rm = client.GetAsync(uri);
                HttpResponseMessage response = rm.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> ts = response.Content.ReadAsStringAsync();
                    string content = ts.Result;
                    Result3 r = JsonConvert.DeserializeObject<Result3>(content);

                    BitmapImage pic1 = new BitmapImage();

                    pic1.BeginInit();
                    pic1.UriSource = new Uri(r.Message[0]);
                    pic1.EndInit();
                    dogImage1.Source = pic1;

                    BitmapImage pic2 = new BitmapImage();
                    pic2.BeginInit();
                    pic2.UriSource = new Uri(r.Message[1]);
                    pic2.EndInit();
                    dogImage2.Source = pic2;

                    BitmapImage pic3 = new BitmapImage();
                    pic3.BeginInit();
                    pic3.UriSource = new Uri(r.Message[2]);
                    pic3.EndInit();
                    dogImage3.Source = pic3;


                }
            }
        }
    }
}
