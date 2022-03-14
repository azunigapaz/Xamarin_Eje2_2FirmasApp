using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ejercicio2_2FirmasApp.Controllers;
using System.IO;

namespace Ejercicio2_2FirmasApp
{
    public partial class App : Application
    {
        static FirmasAppDB basedatos;

        public static FirmasAppDB FirmasAppDB
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new FirmasAppDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirmasAppDB.db3"));
                }
                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage =  new NavigationPage( new Views.PageAddFirma());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
