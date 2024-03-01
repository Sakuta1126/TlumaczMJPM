using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tlumacz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StronaGlowna : ContentPage
    {
        public StronaGlowna()
        {
            InitializeComponent();
        }

        private void WprowadzaonyTekst_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}