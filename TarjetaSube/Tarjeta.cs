using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private int saldo;
        private const int LIMITE_SALDO = 40000;
        private int[] CARGAS_VALIDAS = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        private const int TARIFA_PASAJE = 1580;

        // Propiedad pública para tests
        public int Saldo => saldo;

        public Tarjeta()
        {
            saldo = 0;
        }

        public int ObtenerSaldo()
        {
            return saldo;
        }

        public bool Cargar(int monto)
        {
            bool valido = false;
            foreach (var carga in CARGAS_VALIDAS)
            {
                if (monto == carga)
                {
                    valido = true;
                    break;
                }
            }
            if (!valido) return false;
            if (saldo + monto > LIMITE_SALDO) return false;

            saldo += monto;
            return true;
        }

        public bool Descontar(int monto)
        {
            if (monto > saldo) return false;

            saldo -= monto;
            return true;
        }

        public bool Pagar()
        {
            return Descontar(TARIFA_PASAJE);
        }
    }
}
