using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using TurismoApp.Models;

namespace TurismoApp.ViewModels
{
    public  class GuestViewModel: INotifyPropertyChanged
    {

        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();
        public City City { get; set; }

        public string Error { get; set; } = "";



        private void ChangeView(string view)
        {
            if (view == "create")
            {
                City = new City(); //Aqui se guardaran los datos capturados al agregar
                vistaAlumno = new AgregarAlumnoView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaAlumno);
            }
            else if (vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }

        }


        private void AdToFavorites()
        {
            if (City != null)
            {
                Error = "";

                if (string.IsNullOrWhiteSpace(City.Name))
                {
                    Error = "Escriba el número de control";
                }

                if (string.IsNullOrWhiteSpace(City.Type))
                {
                    Error = "Escriba el nombre del alumno";
                }

                if (string.IsNullOrWhiteSpace(Error)) //agregar
                {
                    Cities.Add(City);


                   
                    Save();

                }

                PropertyChange();

            }
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


        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
