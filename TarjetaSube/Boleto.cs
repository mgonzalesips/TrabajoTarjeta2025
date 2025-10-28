namespace TransporteUrbanoRosario
{
    public class Boleto
    {
        public string Linea { get; }
        public DateTime FechaHora { get; }
        public decimal Tarifa { get; }

        public Boleto(Colectivo colectivo, DateTime fechaHora)
        {
            Linea = colectivo.Linea; 
            FechaHora = fechaHora;
            Tarifa = 1580;
        }
    }
}
