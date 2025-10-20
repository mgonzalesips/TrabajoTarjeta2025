namespace Tarjeta.Clases
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public Tarjeta Tarjeta { get; set; }
        public List<Boleto> HistorialBoletos { get; set; }

        public Usuario(string nombre, Tarjeta tarjeta, List<Boleto>? historialBoletos = null)
        {
            Nombre = nombre;
            Tarjeta = tarjeta;
            HistorialBoletos = historialBoletos ?? [];
        }

        public override string ToString()
        {
            return $"Usuario: {Nombre}, {Tarjeta}";
        }
    }
}