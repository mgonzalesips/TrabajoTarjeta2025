namespace TransporteUrbanoRosario
{
    public class Colectivo
    {
        public string Linea { get; }

        public Colectivo(string linea)
        {
            Linea = linea;
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta.PagarViaje())
            {
                return new Boleto(this, DateTime.Now);
            }
            else
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar el viaje.");
            }
        }
    }
}
