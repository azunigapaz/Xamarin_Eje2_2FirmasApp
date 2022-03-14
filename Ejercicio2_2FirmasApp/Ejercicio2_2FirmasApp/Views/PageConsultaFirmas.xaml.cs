using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Ejercicio2_2FirmasApp.Models;

namespace Ejercicio2_2FirmasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageConsultaFirmas : ContentPage
    {
        FirmasAppModel firmasModel = new FirmasAppModel();
        public PageConsultaFirmas()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var listaFirmas = await App.FirmasAppDB.ListaFirmas();
            ObservableCollection<FirmasAppModel> observableCollectionFirmas = new ObservableCollection<FirmasAppModel>();
            listViewConsultaFirmas.ItemsSource = observableCollectionFirmas;
            foreach (FirmasAppModel FirmaImagen in listaFirmas)
            {
                observableCollectionFirmas.Add(FirmaImagen);
            }
        }

        private async void listViewConsultaFirmas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                FirmasAppModel modelItemLista = (FirmasAppModel)e.Item;

                var messageAlert = await DisplayAlert("Opción", "Seleccione una opción", "Editar Firma", "Eliminar Firma");

                if (messageAlert)
                {                    
                    Models.FirmasAppModel item = (Models.FirmasAppModel)e.Item;
                    var newpage = new Views.PageActualizarFirma();
                    newpage.BindingContext = item;
                    await Navigation.PushAsync(newpage);
                }
                else
                {
                    var resultadoDelete = await App.FirmasAppDB.EliminarFirma(modelItemLista);

                    if (resultadoDelete != 0)
                    {
                        await DisplayAlert("Aviso", "Firma eliminado !", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "Ha ocurrido un error !", "Ok");
                    }

                    obtenerListaFirmas();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.Message, "Ok");
            }
        }


        private async void obtenerListaFirmas()
        {
            var listaFirmas = await App.FirmasAppDB.ListaFirmas();
            //Creamos un colleccion observable para que los cambios que se realizan en el modelo se reflejen de manera automatica en el View
            ObservableCollection<FirmasAppModel> observableCollectionFotos = new ObservableCollection<FirmasAppModel>();
            listViewConsultaFirmas.ItemsSource = observableCollectionFotos;
            foreach (FirmasAppModel FirmaImagen in listaFirmas)
            {
                observableCollectionFotos.Add(FirmaImagen);
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}