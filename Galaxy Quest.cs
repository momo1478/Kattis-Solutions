using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Monish\Desktop\Classes\CS 4150\PS3-6\PS3-6\obj\Debug\test1.in");
        long d = long.Parse(Console.ReadLine().Split(new char[] { ' ' })[0]);

        string line; List<Star> starCoordinates = new List<Star>();
        while ((line = Console.ReadLine()) != null)
        {
            string[] split = line.Split(new char[] { ' ' });

            starCoordinates.Add(new Star(long.Parse(split[0]), long.Parse(split[1])));
        }

        Star majority = FindMajority(starCoordinates, d);

        if(majority == null)
        {
            Console.WriteLine("NO");
        }
        else
        {
            int count = 0;
            for (int i = 0; i < starCoordinates.Count; i++)
            {
                if(InGalaxy(majority,starCoordinates[i] , d))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
        Console.ReadLine();
    }

    static Star FindMajority(List<Star> stars , long d)
    {
        if (stars.Count == 0)
        {
            return null;
        }
        else if (stars.Count == 1)
        {
            return stars[0];
        }
        else
        { 
            //Find A’ and y as described above
            List<Star> starsPrime = new List<Star>();
            for (int i = 0; i < stars.Count; i+=2)
            {
                if(i + 1 == stars.Count)
                {
                    continue;
                }  

                if(InGalaxy(stars[i], stars[i+1], d))
                {
                    starsPrime.Add(stars[i]);
                }
            }
            Star x = FindMajority(starsPrime, d);

            if(x == null)
            {
                //if |A| is odd, count occurrences of y in A, return y or NO as appropriate
                if (stars.Count % 2 == 1)
                {
                    Star y = stars[stars.Count - 1];

                    int count = 0;
                    for (int i = 0; i < stars.Count; i++)
                    {
                        if(InGalaxy(y, stars[i] , d))
                        {
                            count++;
                        }
                    }

                    return count > stars.Count / 2 ? y : null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                int count = 0;
                for (int i = 0; i < stars.Count; i++)
                {
                    if (InGalaxy(x, stars[i], d))
                    {
                        count++;
                    }
                }

                return count > stars.Count / 2 ? x : null;
            }
        }
    }

    static bool InGalaxy(Star star1, Star star2, long d)
    {
        long a = (star1.x - star2.x);
        long b = (star1.y - star2.y);

        return ( (a*a) + (b*b) <= d*d);
    }

    class Star
    {
        internal long x, y;

        public Star(long iX, long iY)
        {
            x = iX;
            y = iY;
        }
    }
}