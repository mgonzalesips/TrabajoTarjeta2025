using TarjetaSube;
using NUnit.Framework;
using System;

public class TarjetaTest
{
    public static void Main()
    {
        TestCargasValidas();
        TestCargasInvalidas();
        TestPagos();
    }

    public static void TestCargasValidas()
    {
        Console.WriteLine("=== Test de Cargas Válidas ===");
        
        decimal[] montosAceptados = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
        
        foreach (decimal monto in montosAceptados)
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Cargar(monto);
            Console.WriteLine($"Carga de ${monto}: {(resultado ? "ÉXITO" : "FALLÓ")} - Saldo: ${tarjeta.Saldo}");
        }
    }

    public static void TestCargasInvalidas()
    {
        Console.WriteLine("\n=== Test de Cargas Inválidas ===");
        
        Tarjeta tarjeta = new Tarjeta();
        
        bool resultado1 = tarjeta.Cargar(1000);
        Console.WriteLine($"Carga de $1000 (no aceptado): {(resultado1 ? "ÉXITO" : "FALLÓ")} - Esperado: FALLÓ");
        
        bool resultado2 = tarjeta.Cargar(50000);
        Console.WriteLine($"Carga de $50000 (excede límite): {(resultado2 ? "ÉXITO" : "FALLÓ")} - Esperado: FALLÓ");
    }

    public static void TestPagos()
    {
        Console.WriteLine("\n=== Test de Pagos ===");
        
        Tarjeta tarjeta = new Tarjeta();
        tarjeta.Cargar(5000);
        
        Colectivo colectivo = new Colectivo("132", "Rosario Bus");
        
        Console.WriteLine($"Saldo inicial: ${tarjeta.Saldo}");
        
        Boleto boleto = colectivo.PagarCon(tarjeta);
        
        if (boleto != null)
        {
            Console.WriteLine($"Pago exitoso:");
            Console.WriteLine($"- Monto: ${boleto.MontoPagado}");
            Console.WriteLine($"- Línea: {boleto.LineaColectivo}");
            Console.WriteLine($"- Saldo restante: ${boleto.SaldoRestante}");
            Console.WriteLine($"- Fecha: {boleto.FechaHora}");
        }
        else
        {
            Console.WriteLine("Pago fallido");
        }
        
        Console.WriteLine($"Saldo final: ${tarjeta.Saldo}");
    }
}
