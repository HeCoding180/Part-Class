using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCL_Class
{
    public enum Dimension
    {
        Real,
        Imaginary,
        Magnitude
    }

    public enum Direction
    {
        Negative = -1,
        Positive = 1
    }

    public enum EngineType
    {
        PartRelative,
        Absolute
    }

    public enum EngineOperation
    {
        ParallelAddition,
        SeriesAddition,
        Addition
    }

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

    public class Part
    {
        //public Attributes
        public Dimension dimension { set; get; }
        public Direction direction { set; get; }
        public double partValue
        {
            set
            {
                switch (dimension)
                {
                    case Dimension.Real:
                        complexPartValue.R = (int)direction * value;
                        break;
                    case Dimension.Imaginary:
                        complexPartValue.i = (int)direction * value;
                        break;
                    case Dimension.Magnitude:
                        complexPartValue = complexNumber.splitZ(value, partTheta);
                        break;
                }
            }
            get
            {
                return complexPartValue.z;
            }
        }
        public double partTheta
        {
            set
            {
                if(dimension == Dimension.Magnitude)
                {
                    complexPartValue = complexNumber.splitZ(partValue, value);
                }
            }
            get
            {
                return complexPartValue.theta;
            }
        }

        //private Attributes
        private complexNumber complexPartValue { set; get; }

        public Part(Dimension partDim)
        {
            dimension = partDim;
            direction = Direction.Positive;
            complexPartValue = new complexNumber(0.0f, 0.0f);
        }
        public Part(Dimension partDim, Direction vectorDirection)
        {
            dimension = partDim;
            direction = vectorDirection;
            complexPartValue = new complexNumber(0.0f, 0.0f);
        }
        public Part(Dimension partDim, double Value)
        {
            dimension = partDim;
            complexPartValue = new complexNumber(0.0f, 0.0f);
            partValue = Value;
        }
        public Part(Dimension partDim, Direction vectorDirection, double Value)
        {
            dimension = partDim;
            direction = vectorDirection;
            complexPartValue = new complexNumber(0.0f, 0.0f);
            partValue = Value;
        }
        public Part(Dimension partDim, double Value, double theta)
        {
            dimension = partDim;
            complexPartValue = new complexNumber(0.0f, 0.0f);
        }
        public Part(Dimension partDim, Direction vectorDirection, double Value, double Theta)
        {
            dimension = partDim;
            direction = vectorDirection;
            complexPartValue = new complexNumber(0.0f, 0.0f);
            partValue = Value;
            partTheta = Theta;
        }
    }

    public sealed class Resistance : Part
    {
        public Resistance() : base(Dimension.Real)
        {

        }
        public Resistance(double Value) : base(Dimension.Real, Value)
        {

        }
    }
    public sealed class Capacitance : Part
    {
        public Capacitance() : base(Dimension.Imaginary, Direction.Negative)
        {

        }
        public Capacitance(double Value) : base(Dimension.Imaginary, Direction.Negative, Value)
        {

        }
    }
    public sealed class Inductance : Part
    {
        public Inductance() : base(Dimension.Imaginary, Direction.Positive)
        {

        }
        public Inductance(double Value) : base(Dimension.Imaginary, Direction.Positive, Value)
        {

        }
    }
    public sealed class Impedance : Part
    {
        public Impedance() : base(Dimension.Magnitude)
        {

        }
        public Impedance(double Value) : base(Dimension.Magnitude, Value)
        {

        }
        public Impedance(double Value, double theta) : base(Dimension.Magnitude, Value, theta)
        {

        }
    }
}
