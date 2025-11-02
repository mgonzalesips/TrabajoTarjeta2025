using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class BoletoTest
    {
        [Test]
        public void TestCrearBoleto()
        {
            Boleto boleto = new Boleto("Normal", "120", 1580, 3420);

            Assert.AreEqual("Normal", boleto.TipoTarjeta);
            Assert.AreEqual("120", boleto.LineaColectivo);
            Assert.AreEqual(1580, boleto.TotalAbonado);
            Assert.AreEqual(3420, boleto.SaldoRestante);
            Assert.IsNotNull(boleto.Fecha);
        }

        [Test]
        public void TestBoletoTieneFecha()
        {
            Boleto boleto = new Boleto("Normal", "115", 1580, 5000);
            
            Assert.IsTrue(boleto.Fecha <= System.DateTime.Now);
            Assert.IsTrue(boleto.Fecha >= System.DateTime.Now.AddSeconds(-5));
        }

        [Test]
        public void TestPropiedadesBoletoSonCorrectas()
        {
            int totalAbonado = 1580;
            int saldoRestante = 8420;
            string linea = "133";
            string tipo = "Normal";

            Boleto boleto = new Boleto(tipo, linea, totalAbonado, saldoRestante);

            Assert.AreEqual(tipo, boleto.TipoTarjeta);
            Assert.AreEqual(linea, boleto.LineaColectivo);
            Assert.AreEqual(totalAbonado, boleto.TotalAbonado);
            Assert.AreEqual(saldoRestante, boleto.SaldoRestante);
        }
    }
}
