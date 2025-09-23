namespace TrabajoTarjeta
{
    public class MedioBoleto : Tarjeta
    {
        public override decimal CalcularTarifa(decimal tarifaBase)
        {
            return tarifaBase / 2;
        }
    }
}