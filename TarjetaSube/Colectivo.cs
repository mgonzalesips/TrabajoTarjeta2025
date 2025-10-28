using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        public int Linea { get; set; }
        public string Empresa { get; set; }
        private const int TARIFA_BASICA = 1580;

        public Colectivo(int linea, string empresa)
        {
            Linea = linea;
            Empresa = empresa;
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta.TieneSaldoSuficiente(TARIFA_BASICA) && tarjeta.PagarConTarifa(TARIFA_BASICA))
            {
                return new Boleto(DateTime.Now, Linea, TARIFA_BASICA);
            }
            return null;
        }
    }
}