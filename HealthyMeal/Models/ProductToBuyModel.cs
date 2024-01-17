using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HealthyMeal.Models
{
    public class ProductToBuyModel : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public int UnitsId { get; set; }
        public string UnitsName { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        private bool _isBought;
        public bool IsBought 
        { 
            get => _isBought; 
            set
            {
                _isBought = value;
                NotifyPropertyChanged(nameof(IsBought));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
