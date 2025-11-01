using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class BoletoTest
    {
        [Test]
        public void TestCreacionBoleto()
        {
            decimal montoPagado = 1580m;
            string linea = "152";
            string empresa = "Rosario Bus";
            decimal saldoRestante = 420m;

            Boleto boleto = new Boleto(montoPagado, linea, empresa, saldoRestante);

            Assert.IsNotNull(boleto);
        }

        [Test]
        public void TestMontoPagado()
        {
            decimal montoPagado = 1580m;
            Boleto boleto = new Boleto(montoPagado, "152", "Rosario Bus", 420m);

            Assert.AreEqual(montoPagado, boleto.MontoPagado);
        }

        [Test]
        public void TestLineaColectivo()
        {
            string linea = "152";
            Boleto boleto = new Boleto(1580m, linea, "Rosario Bus", 420m);

            Assert.AreEqual(linea, boleto.LineaColectivo);
        }

        [Test]
        public void TestEmpresa()
        {
            string empresa = "Rosario Bus";
            Boleto boleto = new Boleto(1580m, "152", empresa, 420m);

            Assert.AreEqual(empresa, boleto.Empresa);
        }

        [Test]
        public void TestSaldoRestante()
        {
            decimal saldoRestante = 420m;
            Boleto boleto = new Boleto(1580m, "152", "Rosario Bus", saldoRestante);

            Assert.AreEqual(saldoRestante, boleto.SaldoRestante);
        }

        [Test]
        public void TestFechaHora()
        {
            DateTime antes = DateTime.Now;
            Boleto boleto = new Boleto(1580m, "152", "Rosario Bus", 420m);
            DateTime despues = DateTime.Now;

            Assert.GreaterOrEqual(boleto.FechaHora, antes);
            Assert.LessOrEqual(boleto.FechaHora, despues);
        }

        [Test]
        public void TestTodosLosDatos()
        {
            decimal montoPagado = 1580m;
            string linea = "K";
            string empresa = "Las Delicias";
            decimal saldoRestante = 2500m;

            Boleto boleto = new Boleto(montoPagado, linea, empresa, saldoRestante);

            Assert.AreEqual(montoPagado, boleto.MontoPagado);
            Assert.AreEqual(linea, boleto.LineaColectivo);
            Assert.AreEqual(empresa, boleto.Empresa);
            Assert.AreEqual(saldoRestante, boleto.SaldoRestante);
            Assert.IsNotNull(boleto.FechaHora);
        }

        [Test]
        public void TestBoletoConDiferentesValores()
        {
            Boleto boleto1 = new Boleto(2000m, "143", "Semtur", 3000m);
            Assert.AreEqual(2000m, boleto1.MontoPagado);
            Assert.AreEqual("143", boleto1.LineaColectivo);
            Assert.AreEqual("Semtur", boleto1.Empresa);
            Assert.AreEqual(3000m, boleto1.SaldoRestante);

            Boleto boleto2 = new Boleto(1580m, "K", "Las Delicias", 0m);
            Assert.AreEqual(1580m, boleto2.MontoPagado);
            Assert.AreEqual("K", boleto2.LineaColectivo);
            Assert.AreEqual("Las Delicias", boleto2.Empresa);
            Assert.AreEqual(0m, boleto2.SaldoRestante);
        }
    }
}