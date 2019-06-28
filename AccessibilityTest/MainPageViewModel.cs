using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AccessibilityTest
{
    public class MainPageViewModel:INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            LabelHint = "Placeholder Text!";
        }

        public string LabelHint
        {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
