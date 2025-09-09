using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabajoTarjeta
{
    public class Tarjeta
    {
        private decimal saldo;
        private readonly List<decimal> cargasAceptadas = new List<decimal>
        { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        private const decimal LIMITE_SALDO = 40000;

        public Tarjeta()
        {
            saldo = 0;
        }

        public decimal ObtenerSaldo()
        {
            return saldo;
        }

        public void Cargar(decimal monto)
        {
            if (!cargasAceptadas.Contains(monto))
            {
                throw new ArgumentException($"El monto {monto} no es una carga válida.");
            }

            if (saldo + monto > LIMITE_SALDO)
            {
                throw new InvalidOperationException($"La carga excede el límite de saldo de ${LIMITE_SALDO}.");
            }

            saldo += monto;
        }

        public bool PuedeDescontar(decimal monto)
        {
            return saldo >= monto;
        }

        public void Descontar(decimal monto)
        {
            if (!PuedeDescontar(monto))
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar el descuento.");
            }

            saldo -= monto;
        }
    }
}