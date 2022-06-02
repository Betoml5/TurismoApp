using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TurismoApp.Models;
using TurismoApp.Views;
using Xamarin.Forms;

namespace TurismoApp.ViewModels
{
    public class CityViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();
        public City City { get; set; }

        public string Error { get; set; } = "";

        //Commands
        public ICommand ChangeViewCommand { get; set; }
        public ICommand CreateCityCommand { get; set; }

        public ICommand DetailsCityCommand { get; set; }    

        // Pages
        CreateCityPage createCityPage;
        DetailsCityPage detailsCityPage;

        public CityViewModel()
        {
            Open();
            ChangeViewCommand = new Command<string>(ChangeView);
            CreateCityCommand = new Command(CreateCity);
            DetailsCityCommand = new Command<City>(DetailsCity);
        }



        private void ChangeView(string view)
        {
            if (view == "create")
            {
                City = new City(); //Aqui se guardaran los datos capturados al agregar
                createCityPage = new CreateCityPage() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(createCityPage);
            } 
            else if (view == "home")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }

        }


        private void CreateCity()
        {
            Cities.Add(City);
            Save();
            ChangeView("home");
            Change();
        }


        private void DetailsCity(City City)
        {


            detailsCityPage = new DetailsCityPage()
            {
                BindingContext = City
            };
            Application.Current.MainPage.Navigation.PushAsync(detailsCityPage);
        }



        void Save()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "cities.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Cities));
        }

        void Open()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "cities.json";
            if (File.Exists(file))
            {
                Cities = JsonConvert.DeserializeObject<ObservableCollection<City>>(File.ReadAllText(file));
            }
        }


        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private ImageSource image;

        public ImageSource Image { get => image; set => SetProperty(ref image, value); }
    }
}
