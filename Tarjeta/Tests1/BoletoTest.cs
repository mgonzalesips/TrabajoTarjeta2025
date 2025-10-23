using NUnit.Framework;
using Tarjeta;

namespace Tests1
{
    public class BoletoTest
    {
        private Tarjeta.Boleto _boleto;

        [SetUp]
        public void Setup()
        {
            _boleto = new Tarjeta.Boleto(1580f); 
        }

        [Test]
        public void Constructor_DeberiaCrearBoletoConPrecioCorrecto()
        {
            // Assert
            Assert.That(_boleto.Precio, Is.EqualTo(1580f));
        }

        [Test]
        public void Precio_DeberiaPoderModificarValor() 
        {
            // Arrange
            float nuevoPrecio = 2000f;

            // Act
            _boleto.Precio = nuevoPrecio;

            // Assert
            Assert.That(_boleto.Precio, Is.EqualTo(nuevoPrecio)); 
        }
    }
}