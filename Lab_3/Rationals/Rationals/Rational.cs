using System;
using System.Data;

namespace Rationals
{
    public struct Rational
    {
        public Rational(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
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

        public static Rational operator +(Rational r1, Rational r2)
        {
            return r1.Add(r2);
        }

        public static Rational operator -(Rational r1, Rational r2)
        {
            return r1.Substract(r2);
        }

        public static Rational operator *(Rational r1, Rational r2)
        {
            return r1.Multiply(r2);
        }

        public static Rational operator /(Rational r1, Rational r2)
        {
            return r1.Divide(r2);
        }

        public Rational Add(Rational rational)
        {
            int newNumerator = Numerator * rational.Denominator + rational.Numerator*Denominator;
            int newDenominator = Denominator * rational.Denominator;
            Rational result = new Rational(newNumerator, newDenominator);
            result.Reduce();
            return result;
        }

        public Rational Substract(Rational rational)
        {
            int newNumerator = Numerator * rational.Denominator - rational.Numerator * Denominator;
            int newDenominator = Denominator * rational.Denominator;
            Rational result = new Rational(newNumerator, newDenominator);
            result.Reduce();
            return result;
        }

        public Rational Multiply(Rational rational)
        {
            int newNumerator = Numerator * rational.Numerator;
            int newDenominator = Denominator * rational.Denominator;
            Rational result = new Rational(newNumerator, newDenominator); 
            result.Reduce();
            return result;
        }

        public Rational Divide(Rational rational)
        {
            if (rational.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            int newNumerator = Numerator * rational.Denominator;
            int newDenominator = Denominator * rational.Numerator;
            Rational result = new Rational(newNumerator, newDenominator);
            result.Reduce();
            return result;
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
            return $"{Numerator}/{Denominator}";
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
                return false;
        }

        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
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