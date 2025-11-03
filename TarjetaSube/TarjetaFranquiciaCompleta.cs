public class TarjetaFranquiciaCompleta : Tarjeta
{
    public override bool PagarPasaje()
    {
        // Los viajes son gratuitos, no se descuenta saldo
        // Solo actualizamos la fecha del último viaje
        ultimoViaje = System.DateTime.Now;
        return true;
    }

    public override decimal ObtenerTarifa()
    {
        return 0m;
    }
}