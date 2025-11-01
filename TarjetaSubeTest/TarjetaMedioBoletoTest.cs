using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TarjetaMedioBoletoTest
    {
        private TarjetaMedioBoleto tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaMedioBoleto();
            colectivo = new Colectivo("152", "Rosario Bus");
        }

        [Test]
        public void TestObtenerTarifaMedioBoleto()
        {
            decimal tarifaEsperada = 790m; 
            Assert.AreEqual(tarifaEsperada, tarjeta.ObtenerTarifa());
        }

        [Test]
        public void TestPagarPasajeConSaldoSuficiente()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(1210m, tarjeta.Saldo); 
        }

        [Test]
        public void TestPagarPasajeConSaldoInsuficiente()
        {
            tarjeta.Cargar(2000);
            tarjeta.Descontar(1500);
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(500m, tarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasajeConSaldoExacto()
        {
            tarjeta.Cargar(2000);
            tarjeta.Descontar(1210);
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestMultiplesPasajesMedioBoleto()
        {
            tarjeta.Cargar(5000);
            
            // Primer pasaje
            tarjeta.PagarPasaje();
            Assert.AreEqual(4210m, tarjeta.Saldo); 
            
            // Segundo pasaje
            tarjeta.PagarPasaje();
            Assert.AreEqual(3420m, tarjeta.Saldo); 
            
            // Tercer pasaje
            tarjeta.PagarPasaje();
            Assert.AreEqual(2630m, tarjeta.Saldo); 
        }

        [Test]
        public void TestPagarEnColectivoConMedioBoleto()
        {
            tarjeta.Cargar(3000);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(790m, boleto.MontoPagado);
            Assert.AreEqual(2210m, boleto.SaldoRestante);
            Assert.AreEqual("152", boleto.LineaColectivo);
            Assert.AreEqual("Rosario Bus", boleto.Empresa);
        }

        [Test]
        public void TestCargarSaldoMedioBoleto()
        {
            bool resultado = tarjeta.Cargar(3000);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(3000m, tarjeta.Saldo);
        }

        [Test]
        public void TestDescontarMedioBoleto()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Descontar(500);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(1500m, tarjeta.Saldo);
        }

        [Test]
        public void TestMedioBoletoSaldoCero()
        {
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestMedioBoletoVariosViajesHastaQuedarSinSaldo()
        {
            tarjeta.Cargar(3000);
            
            // Primer viaje: 3000 - 790 = 2210
            Assert.IsTrue(tarjeta.PagarPasaje());
            
            // Segundo viaje: 2210 - 790 = 1420
            Assert.IsTrue(tarjeta.PagarPasaje());
            
            // Tercer viaje: 1420 - 790 = 630
            Assert.IsTrue(tarjeta.PagarPasaje());
            
            // Cuarto viaje: 630 < 790, no alcanza
            Assert.IsFalse(tarjeta.PagarPasaje());
            Assert.AreEqual(630m, tarjeta.Saldo);
        }
    }
}