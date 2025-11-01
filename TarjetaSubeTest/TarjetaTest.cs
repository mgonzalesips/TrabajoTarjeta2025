using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TarjetaTest
    {
        private Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta();
        }

        [Test]
        public void TestSaldoInicial()
        {
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarMontoValido()
        {
            bool resultado = tarjeta.Cargar(2000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarTodosLosMontosValidos()
        {
            decimal[] montosValidos = { 2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000 };
            
            foreach (var monto in montosValidos)
            {
                var tarjetaTemp = new Tarjeta();
                bool resultado = tarjetaTemp.Cargar(monto);
                Assert.IsTrue(resultado, $"Debería aceptar el monto {monto}");
                Assert.AreEqual(monto, tarjetaTemp.Saldo);
            }
        }

        [Test]
        public void TestCargarMontoInvalido()
        {
            bool resultado = tarjeta.Cargar(1000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarSuperandoLimite()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000);
            
            bool resultado = tarjeta.Cargar(2000);
            Assert.IsFalse(resultado);
            Assert.AreEqual(40000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarHastaElLimiteExacto()
        {
            tarjeta.Cargar(30000);
            bool resultado = tarjeta.Cargar(10000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(40000m, tarjeta.Saldo);
        }

        [Test]
        public void TestDescontarConSaldoSuficiente()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Descontar(1000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(1000m, tarjeta.Saldo);
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
        public void TestDescontarSaldoCompleto()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Descontar(2000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestPagarPasajeConSaldoSuficiente()
        {
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.PagarPasaje();
            Assert.IsTrue(resultado);
            Assert.AreEqual(420m, tarjeta.Saldo); // 2000 - 1580
        }

        [Test]
        public void TestPagarPasajeSinSaldoSuficiente()
        {
            tarjeta.Cargar(2000);
            tarjeta.Descontar(1000);
            bool resultado = tarjeta.PagarPasaje();
            Assert.IsFalse(resultado);
            Assert.AreEqual(1000m, tarjeta.Saldo);
        }
    }
}