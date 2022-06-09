using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TurismoApp.Models
{
    public class City : INotifyPropertyChanged
    {

        public string Name { get; set; }
        private bool isFavorite;

        public bool isFav
        {
            get { return isFavorite; }
            set {
                isFavorite = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

            }
        }


        public string Type { get; set; }

        public string Image { get; set; } = "";

        public double AvgPrice { get; set; }

        public string Description { get; set; } = "";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
