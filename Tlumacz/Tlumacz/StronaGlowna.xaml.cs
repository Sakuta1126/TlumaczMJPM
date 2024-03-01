using Newtonsoft.Json;
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
        private const string KluczApi = "AIzaSyBEAMc2DxP4MFiXFyRIWUuzkYy_5RXoki4";
        public class OdpowiedzTlumaczaGoogle
        {
            [JsonProperty("data")]
            public DaneTlumaczenia Dane { get; set; }
        }
        public class DaneTlumaczenia
        {
            [JsonProperty("translations")]
            public Tlumaczenie[] Tlumaczenia { get; set; }
        }
        public class Tlumaczenie
        {
            [JsonProperty("translatedText")]
            public string PrzetlumaczonyTekst { get; set; }
        }
    }
}