namespace Tarjeta.Clases
{
   public class Tarjeta
    {
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        private const decimal LIMITE_SALDO = 40000;

        public Tarjeta(string numero, decimal saldoInicial = 0)
        {
            Numero = numero;
            Saldo = saldoInicial;
        }

        public void Recargar(decimal monto)
        {
            decimal[] cargasAceptadas = [2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000];
            if (Array.Exists(cargasAceptadas, x => x == monto))
            {
                if (Saldo + monto <= LIMITE_SALDO)
                {
                    Saldo += monto;
                }
                // un else
            }
        }

        public override string ToString()
        {
            return $"Tarjeta Nº: {Numero}, Saldo: ${Saldo:F2}";
        }
    }
}