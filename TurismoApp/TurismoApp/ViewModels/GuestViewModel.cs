using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TurismoApp.Models;

namespace TurismoApp.ViewModels
{
    public  class GuestViewModel: INotifyPropertyChanged
    {




        public Guest Guest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
