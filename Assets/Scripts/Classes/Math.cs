
using Meshadieme;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meshadieme
{
    namespace Math
    {
        class Math
        {
            //Decide later to pass by reference or value etc.
            public IEnumerable<T> FisherYatesShuffle<T>(IEnumerable<T> toShuffle, int Count) 
            {
                //List<T> shuffling = new List<T>(toShuffle);
                //for (int i = shuffling.Count; i > 1; i--)
                //{
                //    int j = Random.Range(i, shuffling.Count);
                //    T temp = shuffling[j];
                foreach (T index in toShuffle)
                {
                    int j = Random.Range(i, Count);
                    T temp = toShuffle[j];



                }
            }
        }

        class shuffleBag
        {
            int[] defBag;
            List<int> shuffle = new List<int>();
            int toScale;

            public shuffleBag (int scale, int[] ratio)
            {
                toScale = scale;
                defBag = ratio;
                shuffle.AddRange(defBag);
                for (int i = 1; i < toScale; i++)
                {
                    shuffle.AddRange(defBag);
                }
                shuffle = Math.FisherYatesShuffle(shuffle);
            }
        }
    }
}
