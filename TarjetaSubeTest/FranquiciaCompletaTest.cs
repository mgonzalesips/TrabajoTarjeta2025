using NUnit.Framework;
using System;

namespace TarjetaSubeTest
{
    [TestFixture]
    public class TarjetaFranquiciaCompletaTest
    {
        private Colectivo colectivo;
        private const decimal TARIFA_GRATUITA = 0m;

        [SetUp]
        public void Setup()
        {
            colectivo = new Colectivo("152", "Rosario Bus");
        }

        // Tests básicos de creación
        [Test]
        public void TestCreacionTarjetaFranquiciaCompleta()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Assert.IsNotNull(tarjeta);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        [Test]
        public void TestTarjetaInicializaConSaldoCero()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Assert.AreEqual(0m, tarjeta.Saldo);
            Assert.AreEqual(0m, tarjeta.SaldoPendiente);
        }

        // Tests de tarifa
        [Test]
        public void TestObtenerTarifaEsCero()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Assert.AreEqual(TARIFA_GRATUITA, tarjeta.ObtenerTarifa());
        }

        [Test]
        public void TestTarifaSiempreEsCero()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            // Verificar múltiples veces que la tarifa es cero
            Assert.AreEqual(0m, tarjeta.ObtenerTarifa());
            Assert.AreEqual(0m, tarjeta.ObtenerTarifa());
            Assert.AreEqual(0m, tarjeta.ObtenerTarifa());
        }

        // Tests de PagarPasaje sin saldo
        [Test]
        public void TestPagarPasajeSinSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo); // El saldo sigue en cero
        }

        [Test]
        public void TestPagarPasajeNoDescuentaSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(5000m);
            decimal saldoInicial = tarjeta.Saldo;
            
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoInicial, tarjeta.Saldo); // El saldo no cambia
        }

        [Test]
        public void TestPagarPasajeSiempreRetornaTrue()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Assert.IsTrue(tarjeta.PagarPasaje());
            Assert.IsTrue(tarjeta.PagarPasaje());
            Assert.IsTrue(tarjeta.PagarPasaje());
        }

        // Tests de viajes gratuitos con colectivo
        [Test]
        public void TestViajeGratuitoSinSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(TARIFA_GRATUITA, boleto.MontoPagado);
            Assert.AreEqual(0m, boleto.SaldoRestante);
        }

        [Test]
        public void TestViajeGratuitoConSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(5000m);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(TARIFA_GRATUITA, boleto.MontoPagado);
            Assert.AreEqual(5000m, boleto.SaldoRestante);
            Assert.AreEqual(5000m, tarjeta.Saldo); // El saldo no cambia
        }

        [Test]
        public void TestMultiplesViajesGratuitos()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(3000m);
            
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto1);
            Assert.IsNotNull(boleto2);
            Assert.IsNotNull(boleto3);
            
            Assert.AreEqual(0m, boleto1.MontoPagado);
            Assert.AreEqual(0m, boleto2.MontoPagado);
            Assert.AreEqual(0m, boleto3.MontoPagado);
            
            // El saldo nunca cambia
            Assert.AreEqual(3000m, tarjeta.Saldo);
        }

        // Tests de propiedades del boleto
        [Test]
        public void TestBoletoContieneInformacionCorrecta()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(2000m);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.AreEqual(TARIFA_GRATUITA, boleto.MontoPagado);
            Assert.AreEqual("152", boleto.LineaColectivo);
            Assert.AreEqual("Rosario Bus", boleto.Empresa);
            Assert.AreEqual(2000m, boleto.SaldoRestante);
            Assert.IsNotNull(boleto.FechaHora);
        }

        [Test]
        public void TestTipoTarjetaEnBoleto()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(2000m);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.AreEqual("Normal", boleto.TipoTarjeta); // Usa el tipo de la clase base
        }

        // Tests de carga de saldo (aunque no es necesaria)
        [Test]
        public void TestCargarSaldoFuncionaNormalmente()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            bool resultado = tarjeta.Cargar(3000m);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(3000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarMultiplesVeces()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            tarjeta.Cargar(2000m);
            tarjeta.Cargar(3000m);
            
            Assert.AreEqual(5000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarMontoNoPermitido()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            bool resultado = tarjeta.Cargar(1500m);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(0m, tarjeta.Saldo);
        }

        // Tests de diferentes líneas y empresas
        [Test]
        public void TestViajesEnDiferentesLineas()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Colectivo colectivo1 = new Colectivo("K", "Las Delicias");
            Colectivo colectivo2 = new Colectivo("143", "Semtur");
            
            Boleto boleto1 = colectivo1.PagarCon(tarjeta);
            Boleto boleto2 = colectivo2.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto1);
            Assert.IsNotNull(boleto2);
            Assert.AreEqual("K", boleto1.LineaColectivo);
            Assert.AreEqual("143", boleto2.LineaColectivo);
            Assert.AreEqual(0m, boleto1.MontoPagado);
            Assert.AreEqual(0m, boleto2.MontoPagado);
        }

        // Tests de viajes consecutivos sin restricciones
        [Test]
        public void TestViajesConsecutivosInmediatos()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            // Múltiples viajes inmediatos (sin espera)
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            Boleto boleto3 = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto1);
            Assert.IsNotNull(boleto2);
            Assert.IsNotNull(boleto3);
        }

        [Test]
        public void TestNoHayLimiteDeViajesDiarios()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(2000m); // CORREGIDO: 2000 es un monto válido
            
            // Realizar muchos viajes
            for (int i = 0; i < 10; i++)
            {
                Boleto boleto = colectivo.PagarCon(tarjeta);
                Assert.IsNotNull(boleto);
                Assert.AreEqual(0m, boleto.MontoPagado);
            }
            
            // El saldo no cambia
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        // Tests de ID de tarjeta
        [Test]
        public void TestIdTarjetaEnBoleto()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            int idTarjeta = tarjeta.Id;
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.AreEqual(idTarjeta, boleto.IdTarjeta);
        }

        [Test]
        public void TestDiferentesTarjetasTienenDiferentesIds()
        {
            TarjetaFranquiciaCompleta tarjeta1 = new TarjetaFranquiciaCompleta();
            TarjetaFranquiciaCompleta tarjeta2 = new TarjetaFranquiciaCompleta();
            
            Assert.AreNotEqual(tarjeta1.Id, tarjeta2.Id);
        }

        // Tests de fecha y hora en boletos
        [Test]
        public void TestFechaHoraEnBoleto()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            DateTime antes = DateTime.Now;
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            DateTime despues = DateTime.Now;
            
            Assert.IsNotNull(boleto.FechaHora);
            Assert.GreaterOrEqual(boleto.FechaHora, antes);
            Assert.LessOrEqual(boleto.FechaHora, despues);
        }

        [Test]
        public void TestMultiplesBoletosTienenFechasDiferentes()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            System.Threading.Thread.Sleep(10);
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            
            Assert.LessOrEqual(boleto1.FechaHora, boleto2.FechaHora);
        }

        // Tests de límite de saldo
        [Test]
        public void TestCargarHastaLimiteSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            tarjeta.Cargar(30000m);
            tarjeta.Cargar(25000m); // CORREGIDO: 25000 es válido (total 55000)
            
            Assert.AreEqual(55000m, tarjeta.Saldo);
        }

        [Test]
        public void TestCargarExcediendoLimiteGeneraSaldoPendiente()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            tarjeta.Cargar(30000m);
            tarjeta.Cargar(30000m);
            
            Assert.AreEqual(56000m, tarjeta.Saldo);
            Assert.AreEqual(4000m, tarjeta.SaldoPendiente);
        }

        // Tests de Descontar (heredado, pero no usado en viajes)
        [Test]
        public void TestDescontarSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(3000m);
            
            bool resultado = tarjeta.Descontar(1000m);
            
            Assert.IsTrue(resultado);
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        [Test]
        public void TestDescontarMasDelSaldoDisponible()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(2000m);
            
            bool resultado = tarjeta.Descontar(3000m);
            
            Assert.IsFalse(resultado);
            Assert.AreEqual(2000m, tarjeta.Saldo);
        }

        // Tests de viaje con saldo pendiente
        [Test]
        public void TestViajeConSaldoPendiente()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(30000m);
            tarjeta.Cargar(30000m); // Genera saldo pendiente
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.MontoPagado);
            Assert.AreEqual(56000m, tarjeta.Saldo);
            Assert.AreEqual(4000m, tarjeta.SaldoPendiente);
        }

        // Tests de consistencia
        [Test]
        public void TestConsistenciaSaldoDespuesDeMultiplesViajes()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(5000m);
            
            for (int i = 0; i < 5; i++)
            {
                colectivo.PagarCon(tarjeta);
            }
            
            Assert.AreEqual(5000m, tarjeta.Saldo);
        }

        [Test]
        public void TestBoletosConSaldoRestanteConsistente()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(3000m);
            
            Boleto boleto1 = colectivo.PagarCon(tarjeta);
            Boleto boleto2 = colectivo.PagarCon(tarjeta);
            
            Assert.AreEqual(3000m, boleto1.SaldoRestante);
            Assert.AreEqual(3000m, boleto2.SaldoRestante);
        }

        // Tests de comparación con otras tarjetas
        [Test]
        public void TestDiferenciasConTarjetaNormal()
        {
            TarjetaFranquiciaCompleta franquicia = new TarjetaFranquiciaCompleta();
            franquicia.Cargar(5000m);
            
            Tarjeta normal = new Tarjeta();
            normal.Cargar(5000m);
            
            Boleto boletoFranquicia = colectivo.PagarCon(franquicia);
            Boleto boletoNormal = colectivo.PagarCon(normal);
            
            Assert.AreEqual(0m, boletoFranquicia.MontoPagado);
            Assert.AreEqual(1580m, boletoNormal.MontoPagado);
            
            Assert.AreEqual(5000m, franquicia.Saldo);
            Assert.AreEqual(3420m, normal.Saldo);
        }

        // Tests de casos extremos
        [Test]
        public void TestViajeConSaldoNegativo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            // Saldo queda en cero por defecto
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.MontoPagado);
        }

        [Test]
        public void TestPagarPasajeNoRequiereSaldo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            // Sin cargar saldo
            bool resultado = tarjeta.PagarPasaje();
            
            Assert.IsTrue(resultado);
        }

        // Test de tipo de tarjeta
        [Test]
        public void TestObtenerTipo()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            
            // Usa el tipo de la clase base
            Assert.AreEqual("Normal", tarjeta.ObtenerTipo());
        }

        // Tests de acreditación de saldo pendiente
        [Test]
        public void TestAcreditarSaldoPendiente()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(30000m);
            tarjeta.Cargar(30000m); // 56000 + 4000 pendiente
            
            // Descontar para hacer espacio
            tarjeta.Descontar(10000m);
            
            // CORREGIDO: Después de descontar 10000, saldo = 46000
            // AcreditarCarga() se llama automáticamente y acredita los 4000 pendientes
            // Resultado: 46000 + 4000 = 50000, pendiente = 0
            Assert.AreEqual(50000m, tarjeta.Saldo);
            Assert.AreEqual(0m, tarjeta.SaldoPendiente);
        }

        // Test de propiedades adicionales del boleto
        [Test]
        public void TestPropiedadesAdicionalesBoleto()
        {
            TarjetaFranquiciaCompleta tarjeta = new TarjetaFranquiciaCompleta();
            tarjeta.Cargar(2000m);
            
            Boleto boleto = colectivo.PagarCon(tarjeta);
            
            Assert.AreEqual(0m, boleto.TotalAbonado);
            Assert.AreEqual(2000m, boleto.Saldo);
            Assert.IsNotNull(boleto.Fecha);
        }
    }
}