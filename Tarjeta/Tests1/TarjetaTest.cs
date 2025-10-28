using NUnit.Framework;
using System.Collections.Generic;

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
    }
}
