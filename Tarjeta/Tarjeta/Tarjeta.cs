using System;
using System.Collections.Generic;

namespace Tarjeta
{
    public class Tarjeta
    {
        // Atributos
        public float Saldo { get; set; }
        public int Id { get; set; }

        // Constantes
        protected const float SALDO_MAXIMO = 40000f;

        // Cargas permitidas
        protected List<float> cargasPermitidas = new List<float>
        {
            2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000
        };

        // Constructor
        public Tarjeta(float saldo, int id)
        {
            Saldo = saldo;
            Id = id;
        }

        // Método cargar
        public virtual void Cargar(float cantidad)
        {
            // Validar que la cantidad esté en la lista de cargas permitidas
            if (!cargasPermitidas.Contains(cantidad))
            {
                Console.WriteLine("Carga no permitida. Las cargas permitidas son: 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000.");
                return;
            }

            // Verificar que el saldo no exceda el límite
            if (Saldo + cantidad > SALDO_MAXIMO)
            {
                Console.WriteLine($"Carga no permitida. El saldo máximo es de ${SALDO_MAXIMO}.");
                return;
            }

            // Sumar la cantidad al saldo
            Saldo += cantidad;
            Console.WriteLine($"Carga exitosa. El nuevo saldo es: {Saldo}");
        }

        public virtual bool PuedeDescontar(float monto)
        {
            // Para la tarea de "Descuento de Saldos" solo verifica que tenga saldo suficiente
            // En la siguiente tarea (Saldo Negativo) esto cambiará
            return Saldo >= monto;
        }

        // Método para descontar saldo
        public virtual bool DescontarSaldo(float monto)
        {
            // Primero verifica si puede descontar
            if (!PuedeDescontar(monto))
            {
                return false;
            }

            // Descuenta el monto
            Saldo -= monto;
            return true;
        }
    }
}

