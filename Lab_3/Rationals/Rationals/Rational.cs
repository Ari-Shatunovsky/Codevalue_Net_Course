using System;
using System.Data;

namespace Rationals
{
    public struct Rational
    {
        public Rational(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public Rational(int numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }

        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public float Value
        {
            get { return  (float) Numerator / Denominator; }
        }

        public Rational Add(Rational rational)
        {
            int newNumerator = Numerator * rational.Denominator + rational.Numerator*Denominator;
            int newDenominator = Denominator * rational.Denominator;
            Rational result = new Rational(newNumerator, newDenominator);
            result.Reduce();
            return result;
        }

        public Rational Mul(Rational rational)
        {
            int newNumerator = Numerator * rational.Numerator;
            int newDenominator = Denominator * rational.Denominator;
            return new Rational(newNumerator, newDenominator);
        }

        public void Reduce()
        {
            int gcd = GCD(Numerator, Denominator);
            while (gcd != 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
                gcd = GCD(Numerator, Denominator);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }

        public bool Equals(Rational other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object other)
        {
            if (other is Rational)
            {
                return Equals((Rational) other);
            }
            else
            {
                return false;
            }
        }

        private static int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return GCD(b, a % b);
            }
        }
    }
}