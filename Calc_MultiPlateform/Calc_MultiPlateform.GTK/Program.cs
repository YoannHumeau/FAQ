using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace Calc_MultiPlateform.GTK
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();

            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Calc multiplateform");
            window.Show();

            Gtk.Application.Run();
        }
    }
}
