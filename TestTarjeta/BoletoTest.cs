using NUnit.Framework;
using System;
using TrabajoTarjeta;

namespace TestTarjeta
{
    [TestFixture]
    public class BoletoTest
    {
        private DateTime fechaHoraPrueba;
        private Boleto boleto;

        [SetUp]
        public void SetUp()
        {
            fechaHoraPrueba = new DateTime(2024, 9, 16, 14, 30, 0);
            boleto = new Boleto(fechaHoraPrueba, 1580, 3420, "102 Rojo");
        }

        [Test]
        public void TestCrearBoletoConParametrosValidos()
        {
            Assert.AreEqual(fechaHoraPrueba, boleto.ObtenerFechaHora());
            Assert.AreEqual(1580, boleto.ObtenerTarifa());
            Assert.AreEqual(3420, boleto.ObtenerSaldoRestante());
            Assert.AreEqual("102 Rojo", boleto.ObtenerLinea());
        }

        [Test]
        public void TestCrearBoletoConLineaNulaLanzaExcepcion()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new Boleto(fechaHoraPrueba, 1580, 3420, null));
        }

        [Test]
        public void TestObtenerFechaHora()
        {
            DateTime fechaEsperada = new DateTime(2024, 12, 25, 10, 15, 30);
            Boleto boletoConFecha = new Boleto(fechaEsperada, 1580, 2000, "144");

            Assert.AreEqual(fechaEsperada, boletoConFecha.ObtenerFechaHora());
        }

        [Test]
        public void TestObtenerTarifa()
        {
            Assert.AreEqual(1580, boleto.ObtenerTarifa());
        }

        [Test]
        public void TestObtenerSaldoRestante()
        {
            Assert.AreEqual(3420, boleto.ObtenerSaldoRestante());
        }

        [Test]
        public void TestObtenerLinea()
        {
            Assert.AreEqual("102 Rojo", boleto.ObtenerLinea());
        }

        [Test]
        public void TestToStringContieneDatosDelBoleto()
        {
            string resultado = boleto.ToString();

            Assert.That(resultado, Does.Contain("102 Rojo"));
            Assert.That(resultado, Does.Contain("1580"));
            Assert.That(resultado, Does.Contain("3420"));
            Assert.That(resultado, Does.Contain("16/09/2024"));
            Assert.That(resultado, Does.Contain("14:30:00"));
        }

        [Test]
        public void TestBoletoConDiferentesTarifasYSaldos()
        {
            Boleto boletoEspecial = new Boleto(fechaHoraPrueba, 1580, 38420, "121 Verde");

            Assert.AreEqual(1580, boletoEspecial.ObtenerTarifa());
            Assert.AreEqual(38420, boletoEspecial.ObtenerSaldoRestante());
            Assert.AreEqual("121 Verde", boletoEspecial.ObtenerLinea());
        }

        [Test]
        public void TestBoletoConSaldoCero()
        {
            Boleto boletoSinSaldo = new Boleto(fechaHoraPrueba, 1580, 0, "144 Negro");

            Assert.AreEqual(0, boletoSinSaldo.ObtenerSaldoRestante());
        }
    }
}