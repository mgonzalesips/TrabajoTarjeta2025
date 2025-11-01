using NUnit.Framework;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class TarjetaTests
    {
        private Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta();
        }

        [Test]
        public void Constructor_DebeInicializarSaldoEnCero()
        {
            Assert.AreEqual(0m, tarjeta.Saldo);
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
        public void Cargar_MontoValido_DebeRetornarTrueYAumentarSaldo(decimal monto)
        {
            bool resultado = tarjeta.Cargar(monto);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(monto, tarjeta.Saldo);
        }

        [Test]
        [TestCase(1000)]
        [TestCase(1500)]
        [TestCase(6000)]
        [TestCase(35000)]
        [TestCase(100)]
        public void Cargar_MontoInvalido_DebeRetornarFalseYNoModificarSaldo(decimal monto)
        {
            bool resultado = tarjeta.Cargar(monto);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void Cargar_SuperaLimiteDeSaldo_DebeRetornarFalse()
        {
            tarjeta.Cargar(30000);
            tarjeta.Cargar(10000); // Total = 40000 (límite exacto)
            
            bool resultado = tarjeta.Cargar(2000); // Superaría el límite
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(40000m, tarjeta.Saldo);
        }

        [Test]
        public void Cargar_MultiplesCargasHastaLimite_DebePermitirCargarHastaElLimite()
        {
            tarjeta.Cargar(20000);
            tarjeta.Cargar(20000);
            
            Assert.AreEqual(40000m, tarjeta.Saldo);
        }

        [Test]
        public void Descontar_SaldoSuficiente_DebeRetornarTrueYDescontarMonto()
        {
            tarjeta.Cargar(5000);
            
            bool resultado = tarjeta.Descontar(1580);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(3420m, tarjeta.Saldo);
        }

        [Test]
        public void Descontar_SaldoInsuficiente_DebeRetornarFalseYNoModificarSaldo()
        {
            tarjeta.Cargar(2000);
            
            bool resultado = tarjeta.Descontar(3000);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        [Test]
        public void Descontar_SaldoExacto_DebeRetornarTrueYDejarSaldoEnCero()
        {
            tarjeta.Cargar(2000);
            
            bool resultado = tarjeta.Descontar(2000);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void PagarPasaje_SaldoSuficiente_DebeRetornarTrueYDescontarTarifaBasica()
        {
            tarjeta.Cargar(5000);
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(3420m, tarjeta.Saldo);
        }

        [Test]
        public void PagarPasaje_SaldoInsuficiente_DebeRetornarFalse()
        {
            // Saldo = 0, no puede pagar
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void PagarPasaje_MultiplesViajes_DebeDescontarCorrectamente()
        {
            tarjeta.Cargar(5000);
            
            tarjeta.PagarPasaje(); // 5000 - 1580 = 3420
            tarjeta.PagarPasaje(); // 3420 - 1580 = 1840
            
            Assert.AreEqual(1840m, tarjeta.Saldo);
        }

        [Test]
        public void PagarPasaje_SaldoJusto_DebePermitirPagar()
        {
            tarjeta.Cargar(2000);
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(420m, tarjeta.Saldo);
        }

        [Test]
        public void Cargar_VariasCargasSecuenciales_DebeAcumularSaldoCorrectamente()
        {
            tarjeta.Cargar(2000);
            tarjeta.Cargar(3000);
            tarjeta.Cargar(5000);
            
            Assert.AreEqual(10000m, tarjeta.Saldo);
        }

        [Test]
        public void Saldo_PropiedadDeLectura_DebeRetornarValorCorrecto()
        {
            tarjeta.Cargar(8000);
            
            decimal saldoActual = tarjeta.Saldo;
            
            Assert.AreEqual(8000m, saldoActual);
        }
    }
}