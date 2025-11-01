using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TarjetaFranquiciaCompletaTest
    {
        private TarjetaFranquiciaCompleta tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaFranquiciaCompleta();
            colectivo = new Colectivo("K", "Las Delicias");
        }

        [Test]
        public void TestObtenerTarifaRetornaCero()
        {
            Assert.AreEqual(0m, tarjeta.ObtenerTarifa());
        }

        [Test]
        public void TestPagarPasajeSiempreRetornaTrue()
        {
            bool resultado = tarjeta.PagarPasaje();
            Assert.IsTrue(resultado);
        }

        [Test]
        public void TestPagarPasajeSinSaldo()
        {
            // Franquicia completa puede pagar sin saldo
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasajeConSaldo()
        {
            tarjeta.Cargar(5000);
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(5000m, tarjeta.Saldo); // El saldo no se descuenta
        }

        [Test]
        public void TestMultiplesPasajesSinDescontarSaldo()
        {
            tarjeta.Cargar(3000);
            
            tarjeta.PagarPasaje();
            Assert.AreEqual(3000m, tarjeta.Saldo);
            
            tarjeta.PagarPasaje();
            Assert.AreEqual(3000m, tarjeta.Saldo);
            
            tarjeta.PagarPasaje();
            Assert.AreEqual(3000m, tarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasaje10VecesNoAfectaSaldo()
        {
            tarjeta.Cargar(8000);
            
            for (int i = 0; i < 10; i++)
            {
                bool resultado = tarjeta.PagarPasaje();
                Assert.IsTrue(resultado, $"Pasaje {i + 1} debería ser exitoso");
                Assert.AreEqual(8000m, tarjeta.Saldo, "El saldo no debe cambiar");
            }
        }

        [Test]
        public void TestPagarEnColectivoConFranquiciaCompleta()
        {
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.MontoPagado);
            Assert.AreEqual(0m, boleto.SaldoRestante);
            Assert.AreEqual("K", boleto.LineaColectivo);
            Assert.AreEqual("Las Delicias", boleto.Empresa);
        }

        [Test]
        public void TestFranquiciaCompletaSiemprePuedePagar()
        {
            // Sin cargar saldo, realizar múltiples viajes
            for (int i = 0; i < 20; i++)
            {
                Boleto boleto = colectivo.PagarCon(tarjeta);
                Assert.IsNotNull(boleto, $"Boleto {i + 1} no debería ser null");
                Assert.AreEqual(0m, boleto.MontoPagado);
            }
        }

        [Test]
        public void TestCargarSaldoFuncionaNormalmente()
        {
            bool resultado = tarjeta.Cargar(10000);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(10000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarTodosLosMontosValidos()
        {
            decimal[] montosValidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
            
            foreach (var monto in montosValidos)
            {
                var tarjetaTemp = new TarjetaFranquiciaCompleta();
                bool resultado = tarjetaTemp.Cargar(monto);
                Assert.IsTrue(resultado, $"Debería aceptar el monto {monto}");
                Assert.AreEqual(monto, tarjetaTemp.Saldo);
            }
        }

        [Test]
        public void TestCargarMontoInvalido()
        {
            bool resultado = tarjeta.Cargar(1500);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestDescontarSaldoFuncionaNormalmente()
        {
            tarjeta.Cargar(5000);
            bool resultado = tarjeta.Descontar(2000);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(3000m, tarjeta.Saldo);
        }

        [Test]
        public void TestDescontarConSaldoInsuficiente()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Descontar(3000);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasajeNoLlamaADescontar()
        {
            tarjeta.Cargar(8000);
            decimal saldoInicial = tarjeta.Saldo;
            
            // Pagar varios pasajes
            tarjeta.PagarPasaje();
            tarjeta.PagarPasaje();
            tarjeta.PagarPasaje();
            
            // El saldo debe permanecer igual
            Assert.AreEqual(saldoInicial, tarjeta.Saldo);
        }

        [Test]
        public void TestVariosColectivosConFranquiciaCompleta()
        {
            Colectivo colectivo1 = new Colectivo("152", "Rosario Bus");
            Colectivo colectivo2 = new Colectivo("143", "Semtur");
            Colectivo colectivo3 = new Colectivo("K", "Las Delicias");
            
            Boleto boleto1 = colectivo1.PagarCon(tarjeta);
            Boleto boleto2 = colectivo2.PagarCon(tarjeta);
            Boleto boleto3 = colectivo3.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto1);
            Assert.IsNotNull(boleto2);
            Assert.IsNotNull(boleto3);
            
            Assert.AreEqual(0m, boleto1.MontoPagado);
            Assert.AreEqual(0m, boleto2.MontoPagado);
            Assert.AreEqual(0m, boleto3.MontoPagado);
            
            Assert.AreEqual("152", boleto1.LineaColectivo);
            Assert.AreEqual("143", boleto2.LineaColectivo);
            Assert.AreEqual("K", boleto3.LineaColectivo);
        }

        [Test]
        public void TestFranquiciaCompletaConLimiteDeSaldo()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000); 
            
            bool resultado = tarjeta.PagarPasaje();
            Assert.IsTrue(resultado);
            Assert.AreEqual(40000m, tarjeta.Saldo); // Saldo no cambia
        }

        [Test]
        public void TestFranquiciaCompletaNoSuperaLimiteCarga()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000); 
            
            bool resultado = tarjeta.Cargar(2000); 
            Assert.IsFalse(resultado);
            Assert.AreEqual(40000m, tarjeta.Saldo);
        }

        [Test]
        public void TestIntegracionCargarDescontarPagar()
        {
            // Cargar saldo
            tarjeta.Cargar(5000);
            Assert.AreEqual(5000m, tarjeta.Saldo);
            
            // Pagar pasaje (no descuenta)
            tarjeta.PagarPasaje();
            Assert.AreEqual(5000m, tarjeta.Saldo);
            
            // Descontar manualmente
            tarjeta.Descontar(1000);
            Assert.AreEqual(4000m, tarjeta.Saldo);
            
            // Pagar otro pasaje (sigue sin descontar)
            tarjeta.PagarPasaje();
            Assert.AreEqual(4000m, tarjeta.Saldo);
        }

        [Test]
        public void TestHeredaDeTarjeta()
        {
            Assert.IsInstanceOf<Tarjeta>(tarjeta);
        }

        [Test]
        public void TestSaldoInicial()
        {
            TarjetaFranquiciaCompleta nuevaTarjeta = new TarjetaFranquiciaCompleta();
            Assert.AreEqual(0m, nuevaTarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasaje50VecesSinSaldo()
        {
            // Sin cargar nada, verificar que puede pagar 50 veces
            for (int i = 0; i < 50; i++)
            {
                bool resultado = tarjeta.PagarPasaje();
                Assert.IsTrue(resultado, $"Intento {i + 1} debería ser exitoso");
            }
            
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestObtenerTarifaDiferenteDeTarjetaNormal()
        {
            Tarjeta tarjetaNormal = new Tarjeta();
            Assert.AreNotEqual(tarjetaNormal.ObtenerTarifa(), tarjeta.ObtenerTarifa());
            Assert.AreEqual(1580m, tarjetaNormal.ObtenerTarifa());
            Assert.AreEqual(0m, tarjeta.ObtenerTarifa());
        }

        [Test]
        public void TestBoletoSiempreTieneMontoCero()
        {
            tarjeta.Cargar(10000);
            
            for (int i = 0; i < 5; i++)
            {
                Boleto boleto = colectivo.PagarCon(tarjeta);
                Assert.AreEqual(0m, boleto.MontoPagado, $"Boleto {i + 1} debe tener monto 0");
                Assert.AreEqual(10000m, boleto.SaldoRestante, "Saldo debe permanecer en 10000");
            }
        }
    }
}