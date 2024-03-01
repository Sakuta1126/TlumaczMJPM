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
    public partial class ArkuszStyli : ResourceDictionary
    {
        public ArkuszStyli()
        {
            InitializeComponent();
        }
    }
}