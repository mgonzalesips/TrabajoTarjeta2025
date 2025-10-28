using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarjeta
{
    public class Colectivo
    {
        // Atributos
        public string Linea { get; set; }

        // Constante de precio del boleto
        private const int PRECIO_BOLETO = 1580;

        // Constructor
        public Colectivo(string linea)
        {
            Linea = linea;
        }

        // Método Pagar
        public void Pagar(Tarjeta tarjeta)
        {
            // Verificar si el saldo es suficiente para el pago
            if (tarjeta.Saldo >= PRECIO_BOLETO)
            {
                tarjeta.Saldo -= PRECIO_BOLETO;  // Restar el precio del boleto al saldo de la tarjeta
                Console.WriteLine($"Pago exitoso. El nuevo saldo de la tarjeta es: {tarjeta.Saldo}");
            }
            else
            {
                Console.WriteLine("Saldo insuficiente para realizar el pago.");
            }
        }
    }
}

