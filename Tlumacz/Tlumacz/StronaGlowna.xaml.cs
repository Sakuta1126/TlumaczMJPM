using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        async Task<string> RozpocznijTlumaczenie(string jezykWpisany, string jezykDocelowy,string tekst)
        {
            try
            {
                using(HttpClient klient = new HttpClient())
                {
                    HttpResponseMessage zapytanie = await klient.GetAsync($"https://translation.googleapis.com/language/translate/v2?key={KluczApi}&source={jezykWpisany}&target={jezykDocelowy}&q={Uri.EscapeDataString(tekst)}");
                    if (zapytanie.IsSuccessStatusCode)
                    {
                        OdpowiedzTlumaczaGoogle odpowiedz = JsonConvert.DeserializeObject<OdpowiedzTlumaczaGoogle>(await zapytanie.Content.ReadAsStringAsync());
                        return odpowiedz?.Dane?.Tlumaczenia?[0]?.PrzetlumaczonyTekst;
                    }
                    else
                    {
                        return "Błąd w czasie tłumaczenia.";
                    }
                }
            }
            catch
            {
                return "Błąd w czasie tłumaczenia.";
            }
        }
    }
}