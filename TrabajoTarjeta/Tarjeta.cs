using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabajoTarjeta
{
    public class Tarjeta
    {
        protected decimal saldo;
        private readonly List<decimal> cargasAceptadas = new List<decimal>
        { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        private const decimal LIMITE_SALDO = 40000;
        private const decimal SALDO_NEGATIVO_MAXIMO = -1200;

        public Tarjeta()
        {
            saldo = 0;
        }

        public virtual decimal ObtenerSaldo()
        {
            return saldo;
        }

        public virtual void Cargar(decimal monto)
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

        public virtual bool PuedeDescontar(decimal monto)
        {
            return (saldo - monto) >= SALDO_NEGATIVO_MAXIMO;
        }

        public virtual void Descontar(decimal monto)
        {
            if (!PuedeDescontar(monto))
            {
                throw new InvalidOperationException($"No se puede descontar ${monto}. El saldo quedaría por debajo del límite permitido de ${SALDO_NEGATIVO_MAXIMO}.");
            }
            saldo -= monto;
        }

        public virtual decimal CalcularTarifa(decimal tarifaBase)
        {
            return tarifaBase;
        }

        // Método para obtener el límite de saldo negativo (útil para tests)
        public decimal ObtenerLimiteSaldoNegativo()
        {
            return SALDO_NEGATIVO_MAXIMO;
        }
    }
}