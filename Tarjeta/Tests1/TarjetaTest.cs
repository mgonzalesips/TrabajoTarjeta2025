using NUnit.Framework;
using System.Collections.Generic;
using Tarjeta;

namespace Tests1
{
    public class TarjetaTest
    {
        private Tarjeta.Tarjeta _tarjeta;

        [SetUp]
        public void Setup()
        {
            _tarjeta = new Tarjeta.Tarjeta(1000f, 1);
        }

        // ============================================
        //            TESTS DE ITERACIÓN 1 
        // ============================================

        [Test]
        public void Constructor_DeberiaCrearTarjetaConSaldoYIdCorrectos()
        {
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_tarjeta.Saldo, Is.EqualTo(1000f));
                Assert.That(_tarjeta.Id, Is.EqualTo(1));
            });
        }
        [Test]
        public void Cargar_CantidadPermitida_DeberiaAumentarSaldo()
        {
            // Act
            _tarjeta.Cargar(2000f);

            // Assert
            Assert.That(_tarjeta.Saldo, Is.EqualTo(3000f));
        }

        [Test]
        public void Cargar_CantidadNoPermitida_NoDeberiaCambiarSaldo()
        {
            // Arrange
            float saldoInicial = _tarjeta.Saldo;

            // Act
            _tarjeta.Cargar(2500f); // Cantidad no permitida

            // Assert
            Assert.That(_tarjeta.Saldo, Is.EqualTo(saldoInicial));
        }

        [Test]
        public void Cargar_SuperaLimiteMaximo_NoDeberiaCambiarSaldo()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(35000f, 1); // Casi en el límite
            float saldoInicial = _tarjeta.Saldo;

            // Act
            _tarjeta.Cargar(10000f); // Esto superaría 40,000

            // Assert
            Assert.That(_tarjeta.Saldo, Is.EqualTo(saldoInicial));
        }

        [Test]
        public void Cargar_CantidadPermitidaEnLimite_DeberiaAceptarCarga()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(35000f, 1);

            // Act
            _tarjeta.Cargar(5000f); // Llegaría exactamente a 40,000

            // Assert
            Assert.That(_tarjeta.Saldo, Is.EqualTo(40000f));
        }

        [Test]
        public void Cargar_VerificarTodasCargasPermitidas()
        {
            // Arrange
            var cargasPermitidas = new List<float>
            {
                2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000
            };

            foreach (var carga in cargasPermitidas)
            {
                // Arrange para cada test
                _tarjeta = new Tarjeta.Tarjeta(0f, 1);

                // Act
                _tarjeta.Cargar(carga);

                // Assert
                Assert.That(_tarjeta.Saldo, Is.EqualTo(carga));
            }
        }

        [Test]
        public void Saldo_DeberiaPoderModificarValor()
        {
            // Act
            _tarjeta.Saldo = 5000f;

            // Assert
            Assert.That(_tarjeta.Saldo, Is.EqualTo(5000f));
        }

        [Test]
        public void Id_DeberiaPoderModificarValor()
        {
            // Act
            _tarjeta.Id = 2;

            // Assert
            Assert.That(_tarjeta.Id, Is.EqualTo(2));
        }

        // ============================================
        // TESTS DE ITERACIÓN 2 - DESCUENTO DE SALDOS
        // ============================================

        [Test]
        public void DescontarSaldo_SaldoSuficiente_DeberiaDescontarYRetornarTrue()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(2000f, 1);
            float montoADescontar = 1580f;
            float saldoEsperado = 420f; // 2000 - 1580

            // Act
            bool resultado = _tarjeta.DescontarSaldo(montoADescontar);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.True, "Debería retornar true cuando hay saldo suficiente");
                Assert.That(_tarjeta.Saldo, Is.EqualTo(saldoEsperado), "El saldo debería descontarse correctamente");
            });
        }

        [Test]
        public void DescontarSaldo_SaldoInsuficiente_DeberiaRetornarFalseYNoModificarSaldo()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(1000f, 1);
            float montoADescontar = 1580f;
            float saldoInicial = _tarjeta.Saldo;

            // Act
            bool resultado = _tarjeta.DescontarSaldo(montoADescontar);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.False, "Debería retornar false cuando no hay saldo suficiente");
                Assert.That(_tarjeta.Saldo, Is.EqualTo(saldoInicial), "El saldo no debería cambiar");
            });
        }

        [Test]
        public void DescontarSaldo_SaldoExacto_DeberiaDejarEnCeroYRetornarTrue()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(1580f, 1);

            // Act
            bool resultado = _tarjeta.DescontarSaldo(1580f);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.True, "Debería retornar true con saldo exacto");
                Assert.That(_tarjeta.Saldo, Is.EqualTo(0f), "El saldo debería quedar en cero");
            });
        }

        [Test]
        public void DescontarSaldo_MontoMayorQueSaldo_NoDeberiaModificarSaldo()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(500f, 1);
            float saldoInicial = _tarjeta.Saldo;

            // Act
            bool resultado = _tarjeta.DescontarSaldo(1000f);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.False);
                Assert.That(_tarjeta.Saldo, Is.EqualTo(saldoInicial));
            });
        }

        [Test]
        public void DescontarSaldo_MultipleDescuentos_DeberiaDescontarCorrectamente()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(5000f, 1);

            // Act & Assert - Primer descuento
            bool descuento1 = _tarjeta.DescontarSaldo(1580f);
            Assert.That(descuento1, Is.True);
            Assert.That(_tarjeta.Saldo, Is.EqualTo(3420f)); // 5000 - 1580

            // Act & Assert - Segundo descuento
            bool descuento2 = _tarjeta.DescontarSaldo(1580f);
            Assert.That(descuento2, Is.True);
            Assert.That(_tarjeta.Saldo, Is.EqualTo(1840f)); // 3420 - 1580

            // Act & Assert - Tercer descuento
            bool descuento3 = _tarjeta.DescontarSaldo(1580f);
            Assert.That(descuento3, Is.True);
            Assert.That(_tarjeta.Saldo, Is.EqualTo(260f)); // 1840 - 1580

            // Act & Assert - Cuarto descuento (debe fallar)
            bool descuento4 = _tarjeta.DescontarSaldo(1580f);
            Assert.That(descuento4, Is.False);
            Assert.That(_tarjeta.Saldo, Is.EqualTo(260f)); // No debe cambiar
        }

        [Test]
        public void DescontarSaldo_DescuentoPequeno_DebeFuncionar()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(100f, 1);

            // Act
            bool resultado = _tarjeta.DescontarSaldo(50f);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.True);
                Assert.That(_tarjeta.Saldo, Is.EqualTo(50f));
            });
        }

        [Test]
        public void DescontarSaldo_ConSaldoCero_DeberiaRetornarFalse()
        {
            // Arrange
            _tarjeta = new Tarjeta.Tarjeta(0f, 1);

            // Act
            bool resultado = _tarjeta.DescontarSaldo(100f);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultado, Is.False);
                Assert.That(_tarjeta.Saldo, Is.EqualTo(0f));
            });
        }
    }
}