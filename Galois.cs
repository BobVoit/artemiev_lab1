using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace artemiev_lab1
{
    internal class Galois
    {
        public int value { get; set; }
        public static int p = 2;

        public Galois()
        {
            this.value = 0;
        }
        public Galois(int value) 
        {
            this.value = value;
        }

        public Galois(Galois gal)
        {
            this.value = gal.value;
        }

        public Galois getOppositeValue()
        {
            if (this.value == 0) 
                return new Galois(this.value);
            return new Galois(Galois.p - this.value);
        }

        public static Galois operator +(Galois gal1, Galois gal2)
        {
            int value = (gal1.value + gal2.value);
            if (value >= Galois.p)
            {
                return new Galois(value % Galois.p);
            }
            return new Galois(value);
        }

        public static Galois operator *(Galois gal1, Galois gal2)
        {
            int value = gal1.value * gal2.value;
            if (value >= Galois.p)
            {
                return new Galois(value % Galois.p);
            }
            return new Galois(value);
        }

        public static Galois operator -(Galois gal1, Galois gal2)
        {         

            int gal3Value = Galois.p - gal2.value;

            int value = (gal1.value + gal3Value);
            if (value >= Galois.p)
            {
                return new Galois(value % Galois.p);
            }
            return new Galois(value);
        }

        public static Galois operator /(Galois gal1, Galois gal2)
        {

            //int value = gal1.value * gal2.getReverseValue().value;
            //if (value >= Galois.p)
            //{
            //    return new Galois(value % Galois.p);
            //}
            //return new Galois(value);

            for (int val = 0; val < Galois.p; val++) 
            {
                Galois res = new Galois(val);
                if ((gal1 * res).value == gal2.value)
                    return res;
            }

            return new Galois(0);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

    }
}
