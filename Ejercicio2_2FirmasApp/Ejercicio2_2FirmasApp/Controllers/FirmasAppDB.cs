using System;
using System.Collections.Generic;
using System.Text;
using Ejercicio2_2FirmasApp.Models;
using SQLite;
using System.Threading.Tasks;
using System.IO;

namespace Ejercicio2_2FirmasApp.Controllers
{
    public class FirmasAppDB
    {

        readonly SQLiteAsyncConnection db;

        public FirmasAppDB()
        {
        }

        public FirmasAppDB(String pathbasedatos)
        {
            db = new SQLiteAsyncConnection(pathbasedatos);

            db.CreateTableAsync<FirmasAppModel>();
        }

        // Procedimientos y funciones CRUD      
        public Task<List<FirmasAppModel>> ListaFirmas()
        {
            return db.Table<FirmasAppModel>().ToListAsync();
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public Task<Int32> GuardarFirma(FirmasAppModel firma)
        {
            if (firma.FirmaId != 0)
            {
                return db.UpdateAsync(firma);
            }
            else
            {
                return db.InsertAsync(firma);
            }
        }

        public Task<FirmasAppModel> BuscarFirma(int bcodigo)
        {
            return db.Table<FirmasAppModel>()
                .Where(i => i.FirmaId == bcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<Int32> EliminarFirma(FirmasAppModel firma)
        {
            return db.DeleteAsync(firma);
        }

    }

}

