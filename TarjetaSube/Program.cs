using System;
using TarjetaSube;

namespace TarjetaSube
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Colectivos Rosario\n");

            //Uso normal del sistema
            Tarjeta tarjeta1 = new Tarjeta();
            Console.WriteLine($"Saldo inicial: ${tarjeta1.ObtenerSaldo()}");

            tarjeta1.Cargar(5000);
            Console.WriteLine($"Después de cargar $5000: ${tarjeta1.ObtenerSaldo()}");

            Colectivo colectivo120 = new Colectivo("120");
            Boleto? boleto1 = colectivo120.PagarCon(tarjeta1);

            if (boleto1 != null)
            {
                Console.WriteLine($"\n✓ Boleto generado:");
                Console.WriteLine($"  Línea: {boleto1.LineaColectivo}");
                Console.WriteLine($"  Tipo: {boleto1.TipoTarjeta}");
                Console.WriteLine($"  Total abonado: ${boleto1.TotalAbonado}");
                Console.WriteLine($"  Saldo restante: ${boleto1.SaldoRestante}");
                Console.WriteLine($"  Fecha: {boleto1.Fecha:dd/MM/yyyy HH:mm:ss}");
            }

            //Varios viajes
            Tarjeta tarjeta2 = new Tarjeta();
            tarjeta2.Cargar(10000);
            Console.WriteLine($"Saldo inicial: ${tarjeta2.ObtenerSaldo()}");

            Colectivo colectivo133 = new Colectivo("133");

            for (int i = 1; i <= 3; i++)
            {
                Boleto? boleto = colectivo133.PagarCon(tarjeta2);
                if (boleto != null)
                {
                    Console.WriteLine($"Viaje {i} - Saldo restante: ${boleto.SaldoRestante}");
                }
            }

            //Saldo insuficiente
            Tarjeta tarjeta3 = new Tarjeta();
            tarjeta3.Cargar(2000);
            Console.WriteLine($"Saldo: ${tarjeta3.ObtenerSaldo()}");

            // Primer viaje OK
            Colectivo colectivo115 = new Colectivo("115");
            Boleto? boletoOk = colectivo115.PagarCon(tarjeta3);
            if (boletoOk != null)
            {
                Console.WriteLine($"✓ Primer viaje OK - Saldo: ${boletoOk.SaldoRestante}");
            }

            // Segundo viaje falla
            Boleto? boletoFail = colectivo115.PagarCon(tarjeta3);
            if (boletoFail == null)
            {
                Console.WriteLine($"✗ Segundo viaje rechazado - Saldo insuficiente (${tarjeta3.ObtenerSaldo()})");
            }

            //Límite de saldo
            Tarjeta tarjeta4 = new Tarjeta();
            tarjeta4.Cargar(30000);
            tarjeta4.Cargar(10000);
            Console.WriteLine($"Saldo después de 2 cargas: ${tarjeta4.ObtenerSaldo()}");

            bool cargaExitosa = tarjeta4.Cargar(5000);
            Console.WriteLine($"Intento de cargar $5000 más: {(cargaExitosa ? "✓ Éxito" : "✗ Rechazado (límite $40000)")}");
            Console.WriteLine($"Saldo final: ${tarjeta4.ObtenerSaldo()}");

            //Carga inválida
            Tarjeta tarjeta5 = new Tarjeta();
            bool carga1 = tarjeta5.Cargar(1500);
            Console.WriteLine($"Cargar $1500: {(carga1 ? "✓" : "✗")} (monto no permitido)");

            bool carga2 = tarjeta5.Cargar(5000);
            Console.WriteLine($"Cargar $5000: {(carga2 ? "✓" : "✗")} (monto válido)");
            Console.WriteLine($"Saldo final: ${tarjeta5.ObtenerSaldo()}");

            //Todas las cargas válidas
            decimal[] cargasValidas = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
            Console.WriteLine("Cargas permitidas:");
            foreach (decimal monto in cargasValidas)
            {
                Console.Write($"${monto} ");
            }
            Console.WriteLine();
        }
    }
}