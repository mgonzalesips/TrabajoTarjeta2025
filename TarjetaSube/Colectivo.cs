using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private const int TARIFA_BASICA = 1580;
        public string Linea { get; private set; }
        private Tiempo tiempo;

        public Colectivo(string linea) : this(linea, new Tiempo())
        {
        }

        public Colectivo(string linea, Tiempo tiempo)
        {
            Linea = linea;
            this.tiempo = tiempo;
        }

        public Boleto? PagarCon(Tarjeta tarjeta)
        {
            if (tarjeta == null)
            {
                throw new ArgumentNullException(nameof(tarjeta));
            }

            if (!tarjeta.PuedeViajar(tiempo))
            {
                return null;
            }

            int saldoAnterior = tarjeta.ObtenerSaldo();
            bool pagoExitoso;

            if (tarjeta is MedioBoleto medioBoleto)
            {
                pagoExitoso = medioBoleto.DescontarSegunViajes();
            }
            else
            {
                pagoExitoso = tarjeta.Descontar(TARIFA_BASICA);
            }

            if (!pagoExitoso)
            {
                return null;
            }

            int saldoNuevo = tarjeta.ObtenerSaldo();
            
            int totalAbonado = saldoAnterior - saldoNuevo;

            tarjeta.RegistrarViaje(tiempo);

            Boleto boleto = new Boleto(
                tipoTarjeta: tarjeta.GetType().Name,
                lineaColectivo: Linea,
                totalAbonado: totalAbonado,
                saldoRestante: saldoNuevo,
                idTarjeta: tarjeta.Id,
                tiempo: tiempo
            );

            return boleto;
        }
    }
}