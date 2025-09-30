using System;
using System.Runtime.CompilerServices;
using TrabajoTarjeta;

class Program
{
    static void Main()
    {
        var tarjeta = new Tarjeta();

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de transporte público\n");
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("\n1. Cargar tarjeta");
            Console.WriteLine("\n2. Elegir linea");
            Console.WriteLine("\n3. Pagar boleto");
            Console.WriteLine("\n4. Salir");
            Console.Write("\n\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Clear();
                    tarjeta.CargarTarjeta(tarjeta);
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    //tienda.ProductosDisponibles(tienda);
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    //tienda.ProductosPorCategoria(tienda);
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }

        /*tarjeta.Cargar(20000);
        var colectivo = new Colectivo("143");
        var boleto = colectivo.PagarCon(tarjeta);
        if (boleto != null)
        {
            boleto.Imprimir();
        }
        else
        {
            Console.WriteLine("Saldo insuficiente");
        }*/
    }
}

