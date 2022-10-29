using Connections;
using Libraries;
using System.Data.SqlClient;
using System.Data;

namespace SEM.Data
{
    public class DL
    {
        public DataTable ExecuteSP(object m, string spFullName, string Conexion = "")
        {
            SqlParameter[] parametros = new SqlParameter[0];

            DataTable dtResult = new DataTable();

            Conexion = "Server=tcp:serveralbertoh.database.windows.net,1433;Initial Catalog=semDB;Persist Security Info=False;User ID=semAppLogin;Password=s3mApp2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            try
            {
                if (m != null)
                {
                    parametros = new SqlParameter[m.GetType().GetProperties().LongCount()];

                    for (int i = 0; i < parametros.Length; i++)
                    {
                        parametros[i] = new SqlParameter("@" + m.GetType().GetProperties()[i].Name, m.GetType().GetProperty(m.GetType().GetProperties()[i].Name).GetValue(m));
                    }

                    parametros = parametros.Where(x => x != null).ToArray<SqlParameter>();
                }

                dtResult = SqlHelper.ExecuteDataset(Conexion, CommandType.StoredProcedure, spFullName, parametros).Tables[0]; //Comentar para pruebas...

            }

            catch (Exception ex)
            {
                string msgError = ex.Message;
            }

            return dtResult;
        }
    }
}
