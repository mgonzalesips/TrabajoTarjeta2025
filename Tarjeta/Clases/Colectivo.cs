namespace Tarjeta.Clases
{
    public class Colectivo
    {
        public string Linea { get; set; }
        public Colectivo(string linea)
        {
            Linea = linea;
        }

        public Boleto? PagarCon(Tarjeta tarjeta, decimal monto = 1580)
        {
            if (tarjeta.Saldo >= monto)
            {
                tarjeta.Saldo -= monto;
                return new Boleto(Linea, monto);
            }
            return null;
        }
        public override string ToString()
        {
            return $"Línea: {Linea}";
        }
    }
}