using System;
using System.Runtime.CompilerServices;
using TrabajoTarjeta;
using static TrabajoTarjeta.BoletoGratuitoEstudiantil;

class Program
{
    static void Main()
    {
        Tarjeta tarjeta = null;
        Console.WriteLine("Indique si tiene alguna franquicia en su tarjeta\n");
        Console.WriteLine("\n1. Boleto Gratuito Estudiantil");
        Console.WriteLine("\n2. Medio Boleto Estudiantil");
        Console.WriteLine("\n3. Franquicia Completa");
        Console.WriteLine("\n4. No tengo Franquicia");
        Console.Write("\n\nSeleccione una opción: ");
        string opcionT = Console.ReadLine();
        bool salirT = false;

        while (!salirT)
        {
            switch (opcionT)
            {
                case "1":
                    tarjeta = new BoletoGratuitoEstudiantil();
                    Console.WriteLine("\nBeneficio de Franquicia Cargado");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    salirT = true;
                    break;
                case "2":
                    tarjeta = new MedioBoletoEstudiantil();
                    Console.WriteLine("\nBeneficio de Franquicia Cargado");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    salirT = true;
                    break;
                case "3":
                    tarjeta = new FranquiciaCompleta();
                    Console.WriteLine("\nBeneficio de Franquicia Cargado");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    salirT = true;
                    break;
                case "4":
                    tarjeta = new Tarjeta();
                    Console.WriteLine("\nNingun Beneficio de Franquicia Cargado");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    salirT = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
        

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

