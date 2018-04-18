using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Runtime.Serialization;
namespace SequenceCalculator
{

        /// <summary>
        /// En klasse some beregner de n første elementene i en følge
        /// </summary>
        [Serializable()]
        public class SequenceCalculator:ISerializable
        {
            public SequenceCalculator(ICalculateNextElement nextFinder)
            {
                SavedInts = new List<BigInteger>();
                NextFinder = nextFinder;
                _numbersInInitialElements = 0;
                _populated = false;
            }


        //To-Do: Serialisering av instanser som har andre følger enn Fibonacci 

        // Deserialiseringsfunksjonen
        public SequenceCalculator(SerializationInfo info, StreamingContext ctxt)
        {
            //Henter verdier fra info setter dem til til egenskaper og felt
            SavedInts = (List<BigInteger>)info.GetValue("SavedInts", typeof(List<BigInteger>));
            _numbersInInitialElements = (int)info.GetValue("numbersInInitialElements", typeof(int));
            var NextFinder = new FibonacciNext();
                }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Nøkkel:verdi par for serialisering i binært format
            info.AddValue("NextFinderType", NextFinder.GetType());
            info.AddValue("SavedInts", SavedInts );
            info.AddValue("numbersInInitialElements", _numbersInInitialElements);
        }


        //Egenskaper og Felter
        public ICalculateNextElement NextFinder { get; }
            public List<BigInteger> SavedInts { get; set; }
            private int _numbersInInitialElements;
            private bool _populated;

            //Metoder
            private BigInteger[] FillArray(int numOfElements, BigInteger[] initialElements)
            {

                BigInteger[] ArrayToFill = new BigInteger[numOfElements];
                for (int i = 0; i < initialElements.Length; i++)
                {
                    ArrayToFill[i] = initialElements[i];
                }
                for (int i = initialElements.Length; i < numOfElements; i++)
                {
                    ArrayToFill[i] = NextFinder.CalculateNext(ref initialElements);
                }
                return ArrayToFill;
            }

                
            public void Clear()
        {
            SavedInts.Clear();
            _populated = false;
        }
            public void PopulateList(int numOfElements, BigInteger[] initialElements)
            {
                _numbersInInitialElements = initialElements.Length;
                SavedInts.Clear();
                BigInteger[] newListOfInts = FillArray(numOfElements, initialElements);
                SavedInts.AddRange(newListOfInts);
                _populated = true;
            }
            public void ExpandList(int numToExpand)
            {
                if (_populated)
                {
                    var initialElementsExpand = SavedInts.GetRange(SavedInts.Count - _numbersInInitialElements, _numbersInInitialElements).ToArray();
                    BigInteger[] intsToAdd = FillArray(numToExpand + _numbersInInitialElements, initialElementsExpand);
                    SavedInts.InsertRange(SavedInts.Count - _numbersInInitialElements, intsToAdd);
                    SavedInts.RemoveRange(SavedInts.Count - _numbersInInitialElements, _numbersInInitialElements);
                }
                else { Console.WriteLine("Please run PopulateList prior to Expanding"); }
            }
            public void Print()
            {
                if (SavedInts.Count == 0)
            {
                Console.WriteLine("No Items to print");
                return;
            }
                for (int i = 0; i < SavedInts.Count; i++)
                {
                    Console.WriteLine($"Element {i + 1} of the given sequence is :{SavedInts[i]}");
                }
                Console.WriteLine($"Current list length: {SavedInts.Count}");
            }
            //To-Do: Trenger enkapsulering
        }

    /// <summary>
    /// Implementerer ICalculateNextElement for fibonaccifølgen.
    /// </summary>
    public class FibonacciNext : ICalculateNextElement
        {

            public BigInteger CalculateNext(ref BigInteger[] a)
            {
                BigInteger returnVal = a[0] + a[1];
                a[0] = a[1];
                a[1] = returnVal;
                return returnVal;
            }
    }

        /// <summary>
        /// Implementerer ICalculateNextElement for aritmetiske følger.
        /// </summary>
        public class ArithmetricNext : ICalculateNextElement
        {
            public ArithmetricNext(int d = 2)
            {
                _d = d;
            }
            private int _d;

            public BigInteger CalculateNext(ref BigInteger[] a)
            {
                BigInteger returnVal = a[0] + _d;
                a[0] = returnVal;
                return returnVal;
            }
        }

        /// <summary>
        /// Interface som skal brukes til å utvide funksjonaliteten til SequenceCalculator.
        /// Implementer CalculateNext slik at den returnerer det neste tallet i rekka, og 
        /// oppdaterer matrisen a[] med verdier som skal brukes i neste beregning.
        /// </summary>
        public interface ICalculateNextElement
        {
            BigInteger CalculateNext(ref BigInteger[] a);
        }


    

}