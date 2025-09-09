using System;
using System.Collections.Generic;

namespace Tarjeta
{
    class Tarjeta
    {
        // Atributos
        public float Saldo { get; set; }
        public int Id { get; set; }

        // Cargas permitidas
        List<float> cargasPermitidas = new List<float>
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
        public void Cargar(float cantidad)
        {
            // Validar que la cantidad esté en la lista de cargas permitidas
            if (!cargasPermitidas.Contains((float)cantidad))  // Convertir cantidad a entero para que coincida con los valores de la lista
            {
                Console.WriteLine("Carga no permitida. Las cargas permitidas son: 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000.");
                return;
            }

            // Verificar que el saldo no exceda el límite de $40,000
            if (Saldo + cantidad > 40000)
            {
                Console.WriteLine("Carga no permitida. El saldo máximo es de $40,000.");
                return;
            }

            // Sumar la cantidad al saldo
            Saldo += cantidad;
            Console.WriteLine($"Carga exitosa. El nuevo saldo es: {Saldo}");
        }
    }
}
