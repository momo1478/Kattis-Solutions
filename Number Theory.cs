using System;
using static System.Numerics.BigInteger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            string[] split = line.Split(new char[] { ' ' });

            switch (split[0])
            {
                case "gcd":
                    {
                        Console.WriteLine(gcd(ulong.Parse(split[1]), ulong.Parse(split[2])));
                        break;
                    }
                case "exp":
                    {
                        Console.WriteLine(exp(System.Numerics.BigInteger.Parse(split[1]), System.Numerics.BigInteger.Parse(split[2]), System.Numerics.BigInteger.Parse(split[3])));
                        break;
                    }
                case "inverse":
                    {
                        Console.WriteLine( inverse(ulong.Parse(split[1]), ulong.Parse(split[2])) );
                        break;
                    }
                case "isprime":
                    {
                        Console.WriteLine( (isprime(ulong.Parse(split[1])) ? "yes" : "no") );
                        break;
                    }
                case "key":
                    {
                        key(ulong.Parse(split[1]), ulong.Parse(split[2]));
                        break;
                    }
            }
        }
    }

    public static ulong gcd(ulong a, ulong b)
    {
        while (b > 0)
        {
            ulong mod = a % b;
            a = b;
            b = mod;
        }
        return a;
    }

    public static System.Numerics.BigInteger exp(System.Numerics.BigInteger x, System.Numerics.BigInteger y, System.Numerics.BigInteger N)
    {
        if (y.IsZero)
        {
            return new System.Numerics.BigInteger(1);
        }
        else
        {
            System.Numerics.BigInteger z = exp(x, System.Numerics.BigInteger.Divide(y, 2), N);

            if (y.IsEven)
            {
                return System.Numerics.BigInteger.Remainder(System.Numerics.BigInteger.Pow(z, 2), N);
            }
            else
            {
                return System.Numerics.BigInteger.Remainder(System.Numerics.BigInteger.Multiply(x, System.Numerics.BigInteger.Pow(z, 2)), N);
            }
        }
    }

    public static string inverse(ulong a, ulong N)
    {
        Tuple<ulong, ulong, ulong> eeRes = ee(a, N);

        if (eeRes.Item3 == 1)
        {
            return((eeRes.Item1 + N) % N).ToString();
        }
        else
        {
            return "none";
        }
    }
    static Tuple<ulong, ulong, ulong> ee(ulong a, ulong b)
    {
        if (b == 0)
        {
            return new Tuple<ulong, ulong, ulong>(1, 0, a);
        }
        else
        {
            Tuple<ulong, ulong, ulong> rec = ee(b, a % b);
            ulong xP, yP, d;
            xP = rec.Item1;
            yP = rec.Item2;
            d = rec.Item3;

            return new Tuple<ulong, ulong, ulong>(yP, xP - (a / b) * yP, d);
        }
    }

    public static bool isprime(ulong N)
    {
        int[] a = new int[] { 2, 3, 5 };

        for (int i = 0; i < a.Length; i++)
        {
            if(exp(a[i], N - 1 , N ) != 1)
            {
                return false;
            }
        }
        return true;
    }

    public static void key(ulong p, ulong q)
    {
        ulong N = p * q;
        ulong phi = (p - 1) * (q - 1);

        ulong e = 2;
        while (true)
        {
            if (gcd(e, phi) == 1)
                break;
            e++;
        }
        ulong d = ulong.Parse(inverse(e, phi));

        Console.WriteLine(N + " " + e + " " + d);
    }
}