using System;
using Tarjeta.Clases;

namespace Tarjeta
{
    class Program
    {
        static void Main(string[] args)
        {
            // chatgpt gracias
            Console.WriteLine("=== Sistema de Transporte Urbano de Rosario ===");
            Console.WriteLine("Demo básico del funcionamiento");
            Console.WriteLine();

            // Crear objetos del sistema
            var tarjeta = new Clases.Tarjeta("12345");
            var colectivo = new Colectivo("144");

            Console.WriteLine($"Tarjeta inicial: {tarjeta}");
            
            // Demostrar recarga exitosa
            Console.WriteLine("\n--- Recarga de Tarjeta ---");
            if (tarjeta.Recargar(5000))
            {
                Console.WriteLine("✓ Recarga de $5000 exitosa");
                Console.WriteLine($"Estado: {tarjeta}");
            }

            // Demostrar viaje exitoso
            Console.WriteLine("\n--- Viaje en Colectivo ---");
            var boleto = colectivo.PagarCon(tarjeta);
            if (boleto != null)
            {
                Console.WriteLine("✓ Viaje realizado exitosamente");
                Console.WriteLine($"Boleto: {boleto}");
                Console.WriteLine($"Estado tarjeta: {tarjeta}");
            }

            Console.WriteLine("\nPara ver todas las pruebas unitarias, ejecuta: dotnet test");
            Console.WriteLine("Total de 53 tests que validan todos los casos del sistema.");
        }
    }
}