using NUnit.Framework;
using System;
using TrabajoTarjeta;

namespace TestTarjeta
{
    [TestFixture]
    public class TarjetaTest
    {
        private Tarjeta tarjeta;

        [SetUp]
        public void SetUp()
        {
            tarjeta = new Tarjeta();
        }

        [Test]
        public void TestTarjetaNuevaTieneSaldoCero()
        {
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
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
        public void TestCargarMontosValidosActualizaSaldo(decimal monto)
        {
            tarjeta.Cargar(monto);
            Assert.AreEqual(monto, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestCargarMontosValidosMultiples()
        {
            tarjeta.Cargar(5000);
            tarjeta.Cargar(3000);
            tarjeta.Cargar(2000);
            Assert.AreEqual(10000, tarjeta.ObtenerSaldo());
        }

        [TestCase(1000)]
        [TestCase(1500)]
        [TestCase(6000)]
        [TestCase(12000)]
        [TestCase(35000)]
        public void TestCargarMontoInvalidoLanzaExcepcion(decimal montoInvalido)
        {
            Assert.Throws<ArgumentException>(() => tarjeta.Cargar(montoInvalido));
        }

        [Test]
        public void TestCargarExcedeLimiteSaldoLanzaExcepcion()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000);
            // El saldo ahora es 40000 (límite máximo)
            Assert.Throws<InvalidOperationException>(() => tarjeta.Cargar(2000));
        }

        [Test]
        public void TestCargarHastaLimiteSaldo()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000);
            Assert.AreEqual(40000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPuedeDescontarConSaldoSuficiente()
        {
            tarjeta.Cargar(5000);
            Assert.IsTrue(tarjeta.PuedeDescontar(1580));
            Assert.IsTrue(tarjeta.PuedeDescontar(3000));
            Assert.IsTrue(tarjeta.PuedeDescontar(5000));
        }

        [Test]
        public void TestNoPuedeDescontarConSaldoInsuficiente()
        {
            tarjeta.Cargar(2000);
            Assert.IsFalse(tarjeta.PuedeDescontar(3000));
            Assert.IsFalse(tarjeta.PuedeDescontar(2001));
        }

        [Test]
        public void TestDescontarConSaldoSuficiente()
        {
            tarjeta.Cargar(5000);
            tarjeta.Descontar(1580);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestDescontarConSaldoInsuficienteLanzaExcepcion()
        {
            tarjeta.Cargar(2000);
            Assert.Throws<InvalidOperationException>(() => tarjeta.Descontar(3000));
        }

        [Test]
        public void TestDescontarTarifaBasicaMultiplesVeces()
        {
            tarjeta.Cargar(10000);
            tarjeta.Descontar(1580); // Saldo: 8420
            tarjeta.Descontar(1580); // Saldo: 6840
            tarjeta.Descontar(1580); // Saldo: 5260
            Assert.AreEqual(5260, tarjeta.ObtenerSaldo());
        }
    }
}