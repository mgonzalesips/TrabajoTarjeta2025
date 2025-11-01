using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private const decimal TARIFA_BASICA = 1580m;
        public string Linea { get; private set; }

        public Colectivo(string linea)
        {
            Linea = linea;
        }

        public Boleto? PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                return null;
            }

            decimal saldoAntes = tarjeta.ObtenerSaldo();

            if (!tarjeta.Descontar(TARIFA_BASICA))
            {

                return null;
            }

            decimal saldoDespues = tarjeta.ObtenerSaldo();

            // Crear y devolver el boleto
            Boleto boleto = new Boleto("Normal", Linea, TARIFA_BASICA, saldoDespues);
            return boleto;
        }
    }
}