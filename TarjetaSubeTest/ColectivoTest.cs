using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class ColectivoTest
    {
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            colectivo = new Colectivo("152", "Rosario Bus");
        }

        [Test]
        public void TestLinea()
        {
            Assert.AreEqual("152", colectivo.Linea);
        }

        [Test]
        public void TestEmpresa()
        {
            Assert.AreEqual("Rosario Bus", colectivo.Empresa);
        }

        [Test]
        public void TestPagarConTarjetaNula()
        {
            Boleto boleto = colectivo.PagarCon(null);
            Assert.IsNull(boleto);
        }

        [Test]
        public void TestPagarConSaldoInsuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000);
            tarjeta.Descontar(500); // Quedan 1500, menos que 1580

            Boleto boleto = colectivo.PagarCon(tarjeta);
            Assert.IsNull(boleto);
        }

        [Test]
        public void TestPagarConSaldoExacto()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000);
            tarjeta.Descontar(420); // Quedan exactamente 1580

            Boleto boleto = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(1580m, boleto.MontoPagado);
            Assert.AreEqual(0m, boleto.SaldoRestante);
            Assert.AreEqual("152", boleto.LineaColectivo);
            Assert.AreEqual("Rosario Bus", boleto.Empresa);
        }

        [Test]
        public void TestPagarConSaldoSuficiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(3000);

            Boleto boleto = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(1580m, boleto.MontoPagado);
            Assert.AreEqual(1420m, boleto.SaldoRestante); // 3000 - 1580
            Assert.AreEqual("152", boleto.LineaColectivo);
            Assert.AreEqual("Rosario Bus", boleto.Empresa);
        }

        [Test]
        public void TestPagarVariosViajes()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);

            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(3420m, boleto1.SaldoRestante);

            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(1840m, boleto2.SaldoRestante);

            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto3);
            Assert.AreEqual(260m, boleto3.SaldoRestante);
        }

        [Test]
        public void TestDatosColectivo()
        {
            Colectivo colectivoK = new Colectivo("K", "Las Delicias");
            Assert.AreEqual("K", colectivoK.Linea);
            Assert.AreEqual("Las Delicias", colectivoK.Empresa);
        }

        [Test]
        public void TestBoletoContieneInformacionDelColectivo()
        {
            Colectivo colectivo143 = new Colectivo("143", "Semtur");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000);

            Boleto boleto = colectivo143.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual("143", boleto.LineaColectivo);
            Assert.AreEqual("Semtur", boleto.Empresa);
        }
    }
}