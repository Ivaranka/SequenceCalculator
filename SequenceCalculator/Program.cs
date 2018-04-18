using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SequenceCalculator
{
    class Program
    {

            //Dette programmet løser de n første fibonaccitallene.
            //To-Do, generaliser talltype vekk fra BigInteger og til alle numre T.
            //To-Do, Skriv og les inn verdier fra en tekstfil, og fortsett fra disse verdiene.
            //To-Do, utvid klassen slik at man kan fortsette fra sist.
        static void Main(string[] args)
            {

            //Lager en ny instanse av en følge. Beregner de n fibonaccitallene.
            var Fib = new FibonacciNext();
            var seriesCalc = new SequenceCalculator(Fib);
            //BigInteger[] Test=seriesCalc.FillArray(150, new BigInteger[] { 0, 1 });
            seriesCalc.PopulateList(140, new BigInteger[] { 0, 1 });
            seriesCalc.ExpandList(100);
            seriesCalc.ExpandList(200);
            seriesCalc.Print();

                //Et eksempel på bruk der hvor man bruker en annen rekkeberegning på en ny instanse av følgeberegneren.
            var Ari = new ArithmetricNext(9);
            var seriesCalc2 = new SequenceCalculator(Ari);
            seriesCalc2.PopulateList(15, new BigInteger[] { 1 });
            seriesCalc2.ExpandList(10);
            seriesCalc2.Print();

            Console.ReadLine();
        }
        
    }
}
