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

        public int index;

        //Commands
        public ICommand ChangeViewCommand { get; set; }
        public ICommand CreateCityCommand { get; set; }
        public ICommand DetailsCityCommand { get; set; }    
        public ICommand DeleteCityCommand { get; set; }
        public ICommand EditCityCommand { get; set; }

        public ICommand ToggleFavoriteCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        // Pages
        CreateCityPage createCityPage;
        DetailsCityPage detailsCityPage;
        EditCityPage editCityPage;

        public CityViewModel()
        {
            Open();
            ChangeViewCommand = new Command<string>(ChangeView);
            CreateCityCommand = new Command(CreateCity);
            DetailsCityCommand = new Command<City>(DetailsCity);
            DeleteCityCommand = new Command<City>(DeleteCity);
            EditCityCommand = new Command<City>(EditCity);
            ToggleFavoriteCommand = new Command<City>(ToggleFavorite);
            SaveChangesCommand = new Command(SaveChanges);
        }

     

        private void ChangeView(string view)
        {
            if (view == "create")
            {
                Error = "";
                City = new City();
                createCityPage = new CreateCityPage() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(createCityPage);
            } 
            else if (view == "home")
            {
                Error = "";
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }

        }


        private void CreateCity()
        {

            Error = "";

            if (string.IsNullOrWhiteSpace(City.Name))
            {
                ShowErrorMessage("Escribe un nombre de ciudad valido");
            }

            if (string.IsNullOrWhiteSpace(City.Type))
            {
                ShowErrorMessage("Ingresa un tipo de ciudad valido");
            }

            if (string.IsNullOrWhiteSpace(City.Description))
            {
                ShowErrorMessage("Ingresa un descripción");
            }

            if (City.AvgPrice == 0)
            {
                ShowErrorMessage("Ingresa un numero valido");
            }

            if (!Uri.TryCreate(City.Image, UriKind.Absolute, out var uri))
            {
                ShowErrorMessage("Ingresa una url valida");
            }

            if (Error == "")
            {
                Cities.Add(City);
                Save();
                ChangeView("home");
                Change();

            }

        }

        public void EditCity(City City)
        {
            if(City != null)
            {
                //clonar
                index = Cities.IndexOf(City);

                this.City = new City
                {
                    Name = City.Name,
                    Image = City.Image,
                    Type = City.Type,
                    Description = City.Description,
                    AvgPrice = City.AvgPrice,

                };

                editCityPage = new EditCityPage()
                {
                    BindingContext = this
                };

                Application.Current.MainPage.Navigation.PushAsync(editCityPage);
            }
        }




        private void DetailsCity(City City)
        {

           
            detailsCityPage = new DetailsCityPage()
            {
                BindingContext = this   
            };
            this.City = City;
            Change();
            Application.Current.MainPage.Navigation.PushAsync(detailsCityPage);
        }

        public void DeleteCity(City City)
        {
            if (City != null)
            {
                Cities.Remove(City);
                Save();
                Application.Current.MainPage.Navigation.PopToRootAsync();

            }
        }

        private void SaveChanges()
        {
            //validar

            Error = "";

            if (string.IsNullOrWhiteSpace(City.Name))
            {
                ShowErrorMessage("Escribe un nombre de ciudad valido");
            }

            if (string.IsNullOrWhiteSpace(City.Type))
            {
                ShowErrorMessage("Ingresa un tipo de ciudad valido");
            }

            if (string.IsNullOrWhiteSpace(City.Description))
            {
                ShowErrorMessage("Ingresa un descripción");
            }

            if (City.AvgPrice == 0)
            {
                ShowErrorMessage("Ingresa un numero valido");
            }
            if (!Uri.TryCreate(City.Image, UriKind.Absolute, out var uri))
            {
                ShowErrorMessage("Ingresa una url valida");
            }

            if (Error == "")
            {
                Cities[index] = City; //Reemplaza el original por el clon
                Save();
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public void ToggleFavorite(City City)
        {
            if (City.isFav)
            {
                City.isFav = false;
            }
            else
            {
                City.isFav = true;
            }

            Save();
            Change();
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        void ShowErrorMessage(string ErrorMsg)
        {
            this.Error += ErrorMsg + "\n";
            Change();
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

       

    }
}
