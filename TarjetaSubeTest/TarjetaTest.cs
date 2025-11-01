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
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
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
        public void TestCargasValidas(decimal monto)
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
            bool resultado = tarjeta.Cargar(1000m);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestCargasAcumuladas()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000m);
            tarjeta.Cargar(3000m);
            Assert.AreEqual(8000m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestLimiteDeSaldo()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(30000m);
            tarjeta.Cargar(10000m);
            // Saldo debe ser 40000 (límite máximo)
            Assert.AreEqual(40000m, tarjeta.ObtenerSaldo());

            // Intentar cargar más debe fallar
            bool resultado = tarjeta.Cargar(2000m);
            Assert.IsFalse(resultado);
            Assert.AreEqual(40000m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestDescontarConSaldoSuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000m);
            bool resultado = tarjeta.Descontar(1580m);
            Assert.IsTrue(resultado);
            Assert.AreEqual(3420m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestDescontarConSaldoInsuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000m);
            tarjeta.Descontar(1580m);
            // Intentar descontar más de lo que hay
            bool resultado = tarjeta.Descontar(1000m);
            Assert.IsFalse(resultado);
            Assert.AreEqual(420m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestNoPermitirSaldoNegativo()
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Descontar(100m);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
        }
    }
}
