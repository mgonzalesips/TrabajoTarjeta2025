namespace TrabajoTarjeta
{
    /// <summary>
    /// Tarjeta con beneficio de medio boleto estudiantil.
    /// Paga el 50% del valor del pasaje (tarifa reducida a $790).
    /// El descuento se aplica independientemente del día de la semana.
    /// </summary>
    public class MedioBoleto : Tarjeta
    {
        /// <summary>
        /// Calcula la tarifa aplicando el descuento de medio boleto.
        /// </summary>
        /// <param name="tarifaBase">Tarifa completa del boleto ($1580).</param>
        /// <returns>La mitad de la tarifa base ($790).</returns>
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return tarifaBase / 2;
        }
    }
}