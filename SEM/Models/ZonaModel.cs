using SEM.Data;

namespace SEM.Models
{
    public class ZonaModel
    {
        DL dl = new DL();

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Fecha { get; set; }

        public bool Activo { get; set; }

        public ZonaModel(int _Id, string _Nombre)
        {
            _Id = Id;

            _Nombre = Nombre;

        }


        public string InsertZona(ZonaModel zona)
        {
            string reult = "";

            dl.ExecuteSP(zona, "dbo.InsertZona", "");

            return reult;
        }
    }
}
