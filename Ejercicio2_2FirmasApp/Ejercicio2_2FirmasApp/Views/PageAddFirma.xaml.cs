using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using SignaturePad.Forms;
using Ejercicio2_2FirmasApp.Models;

namespace Ejercicio2_2FirmasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAddFirma : ContentPage
    {
        public PageAddFirma()
        {
            InitializeComponent();
        }

        byte[] ImageBytes;

        private async void btnAgregar_Clicked(object sender, EventArgs e)
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
                FirmaNombre = txtNombre.Text,
                FirmaDescripcion = txtDescripcion.Text,
                FirmaImagen = ImageBytes
            };


            //var resultado = await App.BaseDatos.GuardarSitio(sitio);

            if (String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Aviso", "Favor no dejar campos vacios", "Ok");

            }
            else
            {
                try
                {
                    await DisplayAlert("Aviso", "Firma Registrada con Exito!!!", "Ok");
                    await App.FirmasAppDB.GuardarFirma(firma);
                    FirmaPad.Clear();
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Advertencia", ex.Message + " Tabla Firmas Creada !", "Ok");
                }

            }

            await Navigation.PopAsync();

        }

        private async void btnListar_Clicked(object sender, EventArgs e)
        {
            var consultaFirmas = new Views.PageConsultaFirmas();
            await Navigation.PushAsync(consultaFirmas);
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}