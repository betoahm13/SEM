using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SEM.Models;
using System.Data;

namespace SEM.Data
{
    public class BL
    {
        DL dl = new DL();

        public List<NodoMenu> GetNodosMenu(int? IdNodoPadre = null, int? IdUsuario = 0)
        {
            var lstNodosMenu = new List<NodoMenu>();

            try
            {
                using (DataTable dt = dl.ExecuteSP(new { IdNodoPadre, IdUsuario }, "cfg.SelectNodosMenu"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstNodosMenu.Add(new NodoMenu()
                        {
                            Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                            Nombre = dt.Rows[i]["Nombre"].ToString(),
                            Link = dt.Rows[i]["Link"].ToString(),
                            ActionName = dt.Rows[i]["ActionName"].ToString(),
                            ControllerName = dt.Rows[i]["ControllerName"].ToString(),
                            Target = dt.Rows[i]["Target"].ToString(),
                            IdNodoPadre = int.Parse(dt.Rows[i]["IdNodoPadre"].ToString()),
                            IdGrupo = int.Parse(dt.Rows[i]["IdGrupo"].ToString()),
                            Icono = dt.Rows[i]["Icono"].ToString(),
                            Color = dt.Rows[i]["Color"].ToString(),
                            Par = dt.Rows[i]["Par"].ToString(),
                            IdUsuario = int.Parse(dt.Rows[i]["IdUsuario"].ToString()),
                            IdEstatus = int.Parse(dt.Rows[i]["IdEstatus"].ToString()),
                            Llave = dt.Rows[i]["Llave"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                string msgError = ex.Message;
            }

            return lstNodosMenu;
        }

        public UsuarioModel ValidaUsrPwd(string Usr, string Pwd)
        {
            var u = new UsuarioModel();

            try
            {
                using (DataTable dt = dl.ExecuteSP(new { Usr, Pwd }, "[usr].[ValidaUsrPwd]"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        u.Id = int.Parse(dt.Rows[0]["Id"].ToString());
                        u.Nombre = dt.Rows[0]["Nombre"].ToString();
                        u.ApPat = dt.Rows[0]["ApPat"].ToString();
                        u.Usuario = dt.Rows[0]["Usuario"].ToString();
                        u.Emails = dt.Rows[0]["Emails"].ToString();
                        u.Telefonos = dt.Rows[0]["Telefonos"].ToString();
                        u.Genero = dt.Rows[0]["Genero"].ToString();
                        u.Validado = Convert.ToBoolean(dt.Rows[0]["Validado"].ToString());
                        u.Color = dt.Rows[0]["Color"].ToString();

                        var rl = new List<RolModel>();
                        var r = new RolModel();

                        for (int i = 0; i < dt.Rows[0]["Roles"].ToString().Split(',').Length; i++)
                        {
                            r = new RolModel();
                            r.Id = int.Parse((dt.Rows[0]["Roles"].ToString().Split(',')[i]).Split('|')[0]);
                            r.Nombre = (dt.Rows[0]["Roles"].ToString().Split(',')[i]).Split('|')[1];

                            rl.Add(r);
                        }

                        u.Roles = rl;
                    }
                }
            }
            catch (Exception ex)
            {
                string msgError = ex.Message;
            }

            return u;
        }

        public string SelectBase()
        {
            string result = "";

            using (DataTable dt = dl.ExecuteSP(new { }, "dbo.SelectBase"))
            {
                result = ConvertDataTabletoJsonString(dt);
            }

            return result;
        }


        public List<SelectListItem> DtToSli(DataTable dt, string value = "Id", string text = "Nombre", bool seleccione = false, bool todas = false)
        {
            List<SelectListItem> li = new List<SelectListItem>();

            if (seleccione)
                li.Add(new SelectListItem { Value = "-1", Text = "-Seleccione una opción-" });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                li.Add(new SelectListItem { Value = dt.Rows[i]["Id"].ToString(), Text = dt.Rows[i]["Nombre"].ToString() });
            }

            if (todas)
                li.Add(new SelectListItem { Value = "0", Text = "-Todas-" });

            return li;
        }
        public DataTable ConvertArrayToStringTable(string[] arreglo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "valor" });
            if (arreglo != null)
            {
                try
                {
                    for (int i = 0; i < arreglo.Length; i++)
                    {
                        dt.Rows.Add(dt.NewRow()["valor"] = arreglo[i]);
                    }
                }
                catch (Exception e) { }
            }
            return dt;
        }
        public List<string> ConvertDatTableToStringList(DataTable dt)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] res = new string[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    res[j] = dt.Rows[i][j].ToString().Replace("\"", "''");

                }
                list.Add("[\"" + string.Join("\", \"", res) + "\"]");
            }
            return list;
        }
        public string ConvertDataTabletoJsonString(DataTable dt)
        {
            string r = JsonConvert.SerializeObject(dt);
            return r;
        }
    }
}
