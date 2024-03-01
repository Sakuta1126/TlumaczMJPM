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
        async Task<string> RozpocznijTlumaczenie(string tekst, string jezykWpisany, string jezykDocelowy)
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
        private async Task<string> PrzetlumaczTekst(string tekst, string jezykWpisany, string jezykDocelowy)
        {
            jezykWpisany = (jezykWpisany == "Polski") ? "pl" : ((jezykWpisany == "Angielski") ? "en" : ((jezykWpisany == "Niemiecki") ? "de" : jezykWpisany));
            jezykDocelowy = (jezykDocelowy == "Polski") ? "pl" : ((jezykDocelowy == "Angielski") ? "en" : ((jezykDocelowy == "Niemiecki") ? "de" : jezykDocelowy));
            return (jezykDocelowy == jezykWpisany) ? "Wybierz 2 inne języki." : await RozpocznijTlumaczenie(tekst, jezykWpisany, jezykDocelowy);
        }
        private async void WprowadzonoTekst(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(JezykZrodlowy.SelectedItem as string) || string.IsNullOrEmpty(JezykDocelowy.SelectedItem as string))
            {
                PrzetlumaczonyTekst.Text = "Wybierz język.";
            }
            else
            {
                PrzetlumaczonyTekst.Text = await PrzetlumaczTekst(WprowadzanyTekst.Text, JezykZrodlowy.SelectedItem as string, JezykDocelowy.SelectedItem as string);
            }
        }

        public async void ZmienionoJezyk(object sender, TextChangedEventArgs e)
        {
            if(JezykZrodlowy.SelectedIndex != null && JezykDocelowy.SelectedIndex)
            {
                int zaznaczony = JezykZrodlowy.SelectedIndex;
                JezykDocelowy.SelectedIndex = JezykZrodlowy.SelectedIndex;
                JezykZrodlowy.SelectedIndex = zaznaczony;

                PrzetlumaczonyTekst.Text = await PrzetlumaczTekst(WprowadzanyTekst.Text, JezykZrodlowy.SelectedItem as string, JezykDocelowy.SelectedItem as string);
            }
            else
            {
                PrzetlumaczonyTekst.Text = "Błąd przy wyborze języków.";
            }
        }

    }
}