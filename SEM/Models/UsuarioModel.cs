namespace SEM.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApPat { get; set; }
        public string Usuario { get; set; }
        public string Emails { get; set; }
        public string Telefonos { get; set; }
        public string Genero { get; set; }
        public bool Validado { get; set; }
        public string Llave { get; set; }
        public string Color { get; set; }
        public List<RolModel> Roles { get; set; }
    }
}
