namespace TrabajoTarjeta
{
    /// <summary>
    /// Tarjeta con franquicia completa (jubilados/pensionados).
    /// Permite viajar de forma gratuita sin consumir saldo.
    /// No requiere saldo disponible para poder utilizarse.
    /// </summary>
    public class FranquiciaCompleta : Tarjeta
    {
        /// <summary>
        /// Calcula la tarifa para franquicia completa.
        /// </summary>
        /// <param name="tarifaBase">Tarifa completa del boleto ($1580).</param>
        /// <returns>Siempre devuelve $0 (viaje gratuito).</returns>
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return 0;
        }

        /// <summary>
        /// Verifica si puede descontar el monto del saldo.
        /// Como la franquicia completa viaja gratis (tarifa = $0), siempre puede pagar.
        /// </summary>
        /// <param name="monto">Monto que se desea descontar (será $0).</param>
        /// <returns>Siempre devuelve true, permitiendo viajar sin restricciones de saldo.</returns>
        public override bool PuedeDescontar(decimal monto)
        {
            // Franquicia completa siempre puede pagar (tarifa = 0)
            return true;
        }
    }
}