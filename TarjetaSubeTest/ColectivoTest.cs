using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class ColectivoTest
    {
        [Test]
        public void TestCrearColectivo()
        {
            Colectivo colectivo = new Colectivo("120");
            Assert.AreEqual("120", colectivo.Linea);
        }

        [Test]
        public void TestPagarConSaldoSuficiente()
        {
            Colectivo colectivo = new Colectivo("120");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(5000m);

            Boleto? boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(1580m, boleto.TotalAbonado);
            Assert.AreEqual(3420m, boleto.SaldoRestante);
            Assert.AreEqual("120", boleto.LineaColectivo);
            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual(3420m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConSaldoInsuficiente()
        {
            Colectivo colectivo = new Colectivo("133");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(2000m);
            tarjeta.Descontar(1580m);

            Boleto? boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNull(boleto);
            Assert.AreEqual(420m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestPagarConTarjetaSinSaldo()
        {
            Colectivo colectivo = new Colectivo("102");
            Tarjeta tarjeta = new Tarjeta();

            Boleto? boleto = colectivo.PagarCon(tarjeta);

            Assert.IsNull(boleto);
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestMultiplesPagos()
        {
            Colectivo colectivo = new Colectivo("115");
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(10000m);

            Boleto? boleto1 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto1);
            Assert.AreEqual(8420m, tarjeta.ObtenerSaldo());

            Boleto? boleto2 = colectivo.PagarCon(tarjeta);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual(6840m, tarjeta.ObtenerSaldo());
        }
    }
}
