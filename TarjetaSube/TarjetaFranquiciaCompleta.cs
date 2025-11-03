public class TarjetaFranquiciaCompleta : Tarjeta
{
    public override bool PagarPasaje()
    {
        ultimoViaje = System.DateTime.Now;
        return true;
    }

    public override decimal ObtenerTarifa()
    {
        return 0m;
    }
}