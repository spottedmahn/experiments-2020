
using Xamarin.Forms;

namespace StyleInheritance.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();

            BindingContext = new[] { "a", "b", "c" };
        }
    }
}
