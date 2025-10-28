using System;

namespace TarjetaSube
{
    public class Tarjeta
    {
        private const int TARIFA = 1580;
        private const int LIMITE_SALDO = 40000;

        int[] montosAceptados = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };

        public int Saldo { get; private set; }

        public Tarjeta(int saldoInicial = 0)
        {
            if (saldoInicial < 0 || saldoInicial > LIMITE_SALDO)
                throw new ArgumentException("Saldo inicial inválido");
            Saldo = saldoInicial;
        }

        public void Cargar(int monto)
        {
            if (Array.IndexOf(montosValidos, monto) == -1)
                throw new ArgumentException("Monto inválido");

            if (Saldo + monto > LIMITE_SALDO)
                throw new InvalidOperationException("No se puede superar el límite de saldo");

            Saldo += monto;
        }

        public bool Pagar()
        {
            if (Saldo < TARIFA)
                return false;

            Saldo -= TARIFA;
            return true;
        }
    }
}

