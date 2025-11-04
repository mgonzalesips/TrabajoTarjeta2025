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
            int tarifaAplicada = 0;

            if (tarjeta is MedioBoleto medioBoleto)
            {
                // primero aplicar la lógica de descuento/validación, luego obtener la tarifa actual
                pagoExitoso = medioBoleto.DescontarSegunViajes();
                tarifaAplicada = medioBoleto.ObtenerTarifaActual();
            }
            else if (tarjeta is BoletoGratuito boletoGratuito)
            {
                pagoExitoso = boletoGratuito.DescontarSegunViajes(tiempo);
                tarifaAplicada = boletoGratuito.ObtenerTarifaActual();
            }
            else if (tarjeta is FranquiciaCompleta franquiciaCompleta)
            {
                pagoExitoso = franquiciaCompleta.DescontarSegunViajes(tiempo);
                tarifaAplicada = franquiciaCompleta.ObtenerTarifaActual();
            }
            else
            {
                tarifaAplicada = tarjeta.CalcularTarifaConDescuento(TARIFA_BASICA, tiempo);
                pagoExitoso = tarjeta.Descontar(tarifaAplicada);
            }

            if (!pagoExitoso)
            {
                return null;
            }

            int saldoNuevo = tarjeta.ObtenerSaldo();

            // usar la tarifa aplicada como total abonado
            int totalAbonado = tarifaAplicada;

            // registrar el viaje (incrementa contadores diarios/mensuales)
            tarjeta.RegistrarViaje(tiempo);

            return new Boleto(tarjeta.GetType().Name, Linea, totalAbonado, saldoNuevo, tarjeta.Id, tiempo);
        }
    }
}