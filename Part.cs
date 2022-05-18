using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part
{
    public delegate complexNumber getComplexPropertyMethod(double frequency);

    public class InvalidOperationException: Exception
    {
        public InvalidOperationException(string msg) : base(msg)
        {
            
        }
    }

    public class complexNumber
    {
        private double _R { set; get; }
        private double _i { set; get; }
        private double _z { set; get; }
        private double _theta { set; get; }

        public double R
        {
            set
            {
                _R = value;
                _z = pythagoricTheorem(_R, _i);
                _theta = Math.Atan2(_i, _R);
            }
            get
            {
                return _R;
            }
        }
        public double i
        {
            set
            {
                _i = value;
                _z = pythagoricTheorem(_R, _i);
                _theta = Math.Atan2(_i, _R);
            }
            get
            {
                return _i;
            }
        }
        public double z
        {
            set
            {
                _z = value;
                _R = Math.Abs(_z) * Math.Cos(_theta);
                _i = Math.Abs(_z) * Math.Sin(_theta);
            }
            get
            {
                return _z;
            }
        }
        public double theta
        {
            set
            {
                _theta = value;
                _R = Math.Abs(_z) * Math.Cos(_theta);
                _i = Math.Abs(_z) * Math.Sin(_theta);
            }
            get
            {
                return _theta;
            }
        }

        public complexNumber()
        {
            R = 0.0f;
            i = 0.0f;
        }
        public complexNumber(double Real, double Imaginary)
        {
            R = Real;
            i = Imaginary;
        }

        public static complexNumber splitZ(double z, double theta)
        {
            return new complexNumber(Math.Abs(z) * Math.Cos(theta), Math.Abs(z) * Math.Sin(theta));
        }
        
        public static double pythagoricTheorem(double a, double b)
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }

        public static complexNumber reciprocal(complexNumber a)
        {
            return new complexNumber(a.R / (Math.Pow(a.R, 2) + Math.Pow(a.i, 2)), a.i / (Math.Pow(a.R, 2) + Math.Pow(a.i, 2)));
        }

        public static complexNumber operator +(complexNumber a, complexNumber b)
        {
            return new complexNumber(a.R + b.R, a.i + b.i);
        }
        public static complexNumber operator -(complexNumber a, complexNumber b)
        {
            return new complexNumber(a.R - b.R, a.i - b.i);
        }
        public static complexNumber operator *(complexNumber a, complexNumber b)
        {
            return new complexNumber(a.R * b.R - a.i * b.i, a.R * b.i + a.i * b.R);
        }
        public static complexNumber operator /(complexNumber a, complexNumber b)
        {
            return a * reciprocal(b);
        }
    }

    public class ComplexPart
    {
        //public Attributes
        public getComplexPropertyMethod ResistivePropertyFunction { set; get; }
        public double PartValue { set; get; }

        //private Attributes
        public complexNumber ComplexResistiveProperty
        {
            get
            {
                return ResistivePropertyFunction(PartValue);
            }
        }

        public ComplexPart(getComplexPropertyMethod propertyMethod)
        {
            ResistivePropertyFunction = propertyMethod;
            PartValue = 0;
        }
        public ComplexPart(getComplexPropertyMethod properyMethod, double value)
        {
            ResistivePropertyFunction = properyMethod;
            PartValue = value;
        }
    }
}
