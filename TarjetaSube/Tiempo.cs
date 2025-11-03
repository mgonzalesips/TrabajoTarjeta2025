using System;

namespace TarjetaSube
{
    public class Tiempo
    {
        public Tiempo()
        {
        }
        
        public virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}