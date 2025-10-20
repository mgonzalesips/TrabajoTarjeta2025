namespace TrabajoTarjeta
{
    /// <summary>
    /// Tarjeta de boleto gratuito estudiantil.
    /// Permite a estudiantes viajar sin costo alguno.
    /// Similar a la franquicia completa, no consume saldo al utilizarse.
    /// </summary>
    public class BoletoGratuito : Tarjeta
    {
        /// <summary>
        /// Calcula la tarifa para boleto gratuito estudiantil.
        /// </summary>
        /// <param name="tarifaBase">Tarifa completa del boleto ($1580).</param>
        /// <returns>Siempre devuelve $0 (viaje gratuito para estudiantes).</returns>
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return 0;
        }

        /// <summary>
        /// Verifica si puede descontar el monto del saldo.
        /// Como el boleto es gratuito (tarifa = $0), siempre puede viajar.
        /// </summary>
        /// <param name="monto">Monto que se desea descontar (será $0).</param>
        /// <returns>Siempre devuelve true, permitiendo viajar sin saldo disponible.</returns>
        public override bool PuedeDescontar(decimal monto)
        {
            // Siempre puede descontar porque la tarifa es 0
            return true;
        }
    }
}