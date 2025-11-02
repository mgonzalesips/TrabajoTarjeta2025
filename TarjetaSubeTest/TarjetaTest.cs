using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TarjetaTest
    {
        [Test]
        public void TestTarjetaNuevaTieneSaldoCero()
        {
            Tarjeta tarjeta = new Tarjeta();
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
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
        public void TestCargasValidas(int monto)
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Cargar(monto);
            Assert.IsTrue(resultado);
            Assert.AreEqual(monto, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestCargaInvalida()
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Cargar(1000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestCargasAcumuladas()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);
            tarjeta.Cargar(3000);
            Assert.AreEqual(8000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestLimiteDeSaldo()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000);
            Assert.AreEqual(40000, tarjeta.ObtenerSaldo());

            bool resultado = tarjeta.Cargar(2000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(40000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestDescontarConSaldoSuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);
            bool resultado = tarjeta.Descontar(1580);
            Assert.IsTrue(resultado);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestDescontarConSaldoInsuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000);
            tarjeta.Descontar(1580);
            bool resultado = tarjeta.Descontar(1000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(420, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestNoPermitirSaldoNegativo()
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Descontar(100);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarTarifa()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Pagar();
            Assert.IsTrue(resultado);
            Assert.AreEqual(420, tarjeta.ObtenerSaldo());
        }
    }
}
