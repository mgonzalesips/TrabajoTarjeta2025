using NUnit.Framework;
using System;
using TrabajoTarjeta;

namespace TestTarjeta
{
    [TestFixture]
    public class ColectivoTest
    {
        private Colectivo colectivo;
        private Tarjeta tarjeta;

        [SetUp]
        public void SetUp()
        {
            colectivo = new Colectivo("102 Rojo");
            tarjeta = new Tarjeta();
        }

        [Test]
        public void TestCrearColectivoConLinea()
        {
            Assert.AreEqual("102 Rojo", colectivo.ObtenerLinea());
        }

        [Test]
        public void TestCrearColectivoConLineaNulaLanzaExcepcion()
        {
            Assert.Throws<ArgumentNullException>(() => new Colectivo(null));
        }

        [Test]
        public void TestObtenerTarifaBasica()
        {
            Assert.AreEqual(1580, colectivo.ObtenerTarifaBasica());
        }

        [TestCase(2000)]
        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        [TestCase(8000)]
        [TestCase(10000)]
        [TestCase(15000)]
        [TestCase(20000)]
        [TestCase(25000)]
        [TestCase(30000)]
        public void TestPagarConTarjetaConSaldoSuficiente(decimal montoCarga)
        {
            tarjeta.Cargar(montoCarga);
            Boleto boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(1580, boleto.ObtenerTarifa());
            Assert.AreEqual(montoCarga - 1580, boleto.ObtenerSaldoRestante());
            Assert.AreEqual(montoCarga - 1580, tarjeta.ObtenerSaldo());
            Assert.AreEqual("102 Rojo", boleto.ObtenerLinea());
        }

        [Test]
        public void TestPagarConTarjetaSinSaldoLanzaExcepcion()
        {
            // Con saldo 0, puede descontar hasta -1200, así que 1580 es permitido
            // Para que lance excepción, necesitamos estar en el límite
            tarjeta.Descontar(1200); // Saldo: -1200
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjeta));
        }

        [Test]
        public void TestPagarConTarjetaConSaldoInsuficienteLanzaExcepcion()
        {
            tarjeta.Cargar(2000);
            colectivo.PagarCon(tarjeta); // Saldo: 420
            colectivo.PagarCon(tarjeta); // Saldo: -1160 (permitido)

            // El tercer viaje sí debería fallar (quedaría en -2740)
            Assert.Throws<InvalidOperationException>(() => colectivo.PagarCon(tarjeta));
        }

        [Test]
        public void TestPagarConTarjetaNulaLanzaExcepcion()
        {
            Assert.Throws<ArgumentNullException>(() => colectivo.PagarCon(null));
        }

        [Test]
        public void TestPagarMultiplesPasajes()
        {
            tarjeta.Cargar(10000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(8420, boleto1.ObtenerSaldoRestante());

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(6840, boleto2.ObtenerSaldoRestante());

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(5260, boleto3.ObtenerSaldoRestante());
        }

        [Test]
        public void TestPagarConDiferentesLineasColectivo()
        {
            Colectivo colectivo121 = new Colectivo("121");
            Colectivo colectivo144 = new Colectivo("144 Negro");

            tarjeta.Cargar(5000);

            Boleto boleto1 = colectivo121.PagarCon(tarjeta);
            Boleto boleto2 = colectivo144.PagarCon(tarjeta);

            Assert.AreEqual("121", boleto1.ObtenerLinea());
            Assert.AreEqual("144 Negro", boleto2.ObtenerLinea());
        }

        [Test]
        public void TestPagarConSaldoNegativoPermitido()
        {
            tarjeta.Cargar(2000);

            // Primer viaje: 2000 - 1580 = 420
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(420, boleto1.ObtenerSaldoRestante());

            // Segundo viaje: 420 - 1580 = -1160 (permitido)
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.AreEqual(-1160, boleto2.ObtenerSaldoRestante());
        }
    }
}