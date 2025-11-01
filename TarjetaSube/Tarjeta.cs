using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private decimal saldo;
        private const decimal LIMITE_SALDO = 40000m;
        private static readonly decimal[] CARGAS_VALIDAS = { 2000m, 3000m, 4000m, 5000m, 8000m, 10000m, 15000m, 20000m, 25000m, 30000m };

        public Tarjeta()
        {
            saldo = 0m;
        }

        public decimal ObtenerSaldo()
        {
            return saldo;
        }

        public bool Cargar(decimal monto)
        {

            bool montoValido = false;
            foreach (decimal cargaValida in CARGAS_VALIDAS)
            {
                if (monto == cargaValida)
                {
                    montoValido = true;
                    break;
                }
            }

            if (!montoValido)
            {
                return false;
            }

            if (saldo + monto > LIMITE_SALDO)
            {
                return false;
            }

            saldo += monto;
            return true;
        }

        public bool Descontar(decimal monto)
        {

            if (saldo < monto)
            {
                return false;
            }

            saldo -= monto;
            return true;
        }
    }
}