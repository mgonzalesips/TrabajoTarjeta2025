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
        public void TestTarjetaTieneIdUnico()
        {
            Tarjeta tarjeta1 = new Tarjeta();
            Tarjeta tarjeta2 = new Tarjeta();

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
            Assert.Greater(tarjeta1.Id, 0);
            Assert.Greater(tarjeta2.Id, 0);
        }

        [Test]
        public void TestIdSeIncrementaAutomaticamente()
        {
            Tarjeta tarjeta1 = new Tarjeta();
            int id1 = tarjeta1.Id;

            Tarjeta tarjeta2 = new Tarjeta();
            int id2 = tarjeta2.Id;

            Assert.AreEqual(id1 + 1, id2);
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
            tarjeta.Cargar(30000); 

            Assert.AreEqual(56000, tarjeta.ObtenerSaldo());
            Assert.Greater(tarjeta.SaldoPendiente, 0); 
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
        public void TestDescontarConSaldoInsuficientePeroDentroDelLimiteNegativo()
        {
            Tarjeta tarjeta = new Tarjeta();
            bool resultado = tarjeta.Descontar(1000);
            Assert.IsTrue(resultado);
            Assert.AreEqual(-1000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestNoPermitirPasarDelLimiteNegativo()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Descontar(1200);
            bool resultado = tarjeta.Descontar(1);
            Assert.IsFalse(resultado);
            Assert.AreEqual(-1200, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestRecuperarSaldoNegativoAlCargar()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Descontar(1200);
            tarjeta.Cargar(2000);
            Assert.AreEqual(800, tarjeta.ObtenerSaldo());
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

        [Test]
        public void TestMedioBoletoDescuentaMitad()
        {
            MedioBoleto tarjeta = new MedioBoleto();
            tarjeta.Cargar(2000);
            bool resultado = tarjeta.Pagar();
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000 - 1580 / 2, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestMedioBoletoTieneIdUnico()
        {
            MedioBoleto tarjeta1 = new MedioBoleto();
            MedioBoleto tarjeta2 = new MedioBoleto();

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
        }

        [Test]
        public void TestBoletoGratuitoNoDescuenta()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            BoletoGratuito tarjeta = new BoletoGratuito();
            tarjeta.Cargar(2000);

            bool resultado = tarjeta.DescontarSegunViajes(tiempoFalso);
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestBoletoGratuitoTercerViajeDescuentaTarifaCompleta()
        {
            TiempoFalso tiempoFalso = new TiempoFalso();
            BoletoGratuito tarjeta = new BoletoGratuito();
            tarjeta.Cargar(5000);

            tarjeta.DescontarSegunViajes(tiempoFalso);
            tarjeta.RegistrarViaje(tiempoFalso);

            tarjeta.DescontarSegunViajes(tiempoFalso);
            tarjeta.RegistrarViaje(tiempoFalso);

            bool resultado = tarjeta.DescontarSegunViajes(tiempoFalso);
            Assert.IsTrue(resultado);
            Assert.AreEqual(3420, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestBoletoGratuitoTieneIdUnico()
        {
            BoletoGratuito tarjeta1 = new BoletoGratuito();
            BoletoGratuito tarjeta2 = new BoletoGratuito();

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
        }

        [Test]
        public void TestFranquiciaCompletaSiemprePuedePagar()
        {
            FranquiciaCompleta tarjeta = new FranquiciaCompleta();
            tarjeta.Cargar(0);
            bool resultado = tarjeta.Pagar();
            Assert.IsTrue(resultado);
            Assert.AreEqual(0, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestFranquiciaCompletaTieneIdUnico()
        {
            FranquiciaCompleta tarjeta1 = new FranquiciaCompleta();
            FranquiciaCompleta tarjeta2 = new FranquiciaCompleta();

            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
        }

        [Test]
        public void TestCargaConExcesoGeneraSaldoPendiente()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(30000);
            tarjeta.Cargar(30000);
            Assert.AreEqual(56000, tarjeta.ObtenerSaldo());
            Assert.Greater(tarjeta.SaldoPendiente, 0);
        }

        [Test]
        public void TestAcreditarCargaCuandoSeDescuenta()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(30000);
            tarjeta.Cargar(30000);
            int saldoPendienteInicial = tarjeta.SaldoPendiente;

            tarjeta.Descontar(1580);

            Assert.Less(tarjeta.SaldoPendiente, saldoPendienteInicial);
            Assert.LessOrEqual(tarjeta.ObtenerSaldo(), 56000);
        }

        [Test]
        public void TestAcreditarCargaVariasVecesHastaLimite()
        {
            Tarjeta tarjeta = new Tarjeta();
            tarjeta.Cargar(30000);
            tarjeta.Cargar(30000);

            for (int i = 0; i < 10; i++)
                tarjeta.Descontar(1580);

            Assert.LessOrEqual(tarjeta.SaldoPendiente, 0);
            Assert.LessOrEqual(tarjeta.ObtenerSaldo(), 56000);
        }


        [Test]
        public void TestCargaQueSuperaElMaximoDejaSaldoPendiente()
        {
            Tarjeta tarjeta = new Tarjeta();

            tarjeta.Cargar(30000);
            tarjeta.Cargar(30000); 

            Assert.AreEqual(56000, tarjeta.ObtenerSaldo());

            tarjeta.Descontar(2000);
            tarjeta.AcreditarCarga();

            Assert.AreEqual(56000, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void TestSaldoPendienteSeAcreditaDespuesDeUnViaje()
        {
            Tarjeta tarjeta = new Tarjeta();

            tarjeta.Cargar(30000);
            tarjeta.Cargar(30000); 

            tarjeta.Descontar(2000); 

            Assert.AreEqual(56000, tarjeta.ObtenerSaldo()); 
        }
    }

}
