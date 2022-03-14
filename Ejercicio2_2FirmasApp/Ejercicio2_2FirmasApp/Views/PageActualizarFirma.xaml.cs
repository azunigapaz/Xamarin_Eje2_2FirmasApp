using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ejercicio2_2FirmasApp.Models;

namespace Ejercicio2_2FirmasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActualizarFirma : ContentPage
    {
        public PageActualizarFirma()
        {
            InitializeComponent();
        }

        byte[] ImageBytes;

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            Stream Image = await FirmaPad.GetImageStreamAsync(SignatureImageFormat.Png);

            try
            {
                //convertimos imagen a base 64
                var image = await FirmaPad.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                ImageBytes = Convert.FromBase64String(base64Val);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }


            var firma = new FirmasAppModel
            {
                FirmaId = Convert.ToInt32(id.Text),
                FirmaNombre = txtNombre.Text,
                FirmaDescripcion = txtDescripcion.Text,
                FirmaImagen = ImageBytes
            };

            if (String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Aviso", "Favor no dejar campos vacios", "Ok");

            }
            else
            {
                await DisplayAlert("Aviso", "Firma Registrada con Exito!!!", "Ok");
                await App.FirmasAppDB.GuardarFirma(firma);
                FirmaPad.Clear();
                txtNombre.Text = "";
                txtDescripcion.Text = "";
            }

            await Navigation.PopAsync();

        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}