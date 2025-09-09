namespace Tarjeta.Clases
{
    public class Boleto
    {
        public DateTime FechaHora { get; set; }
        public string Linea { get; set; }
        public decimal Monto { get; set; }

        public Boleto(string linea, decimal monto)
        {
            FechaHora = DateTime.Now;
            Linea = linea;
            Monto = monto;
        }

        public override string ToString()
        {
            return $"Boleto - Línea: {Linea}, Monto: ${Monto:F2}, Fecha y Hora: {FechaHora}";
        }
    }
}