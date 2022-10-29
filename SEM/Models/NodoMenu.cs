namespace SEM.Models
{
    public class NodoMenu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Link { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Target { get; set; }
        public int IdNodoPadre { get; set; }
        public int IdGrupo { get; set; }
        public string Icono { get; set; }
        public string Color { get; set; }
        public string Par { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstatus { get; set; }
        public string Llave { get; set; }
    }
}
