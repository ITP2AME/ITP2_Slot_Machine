
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
            public static List<T> FisherYatesShuffle<T>(List<T> toShuffle)
            {
                for (int i = toShuffle.Count - 1; i > 0; i--)
                {
                    int j = Random.Range(0,i);
                    //Debug.Log(j);
                    //Debug.Log(i);
                    //Debug.Log(toShuffle.Count);
                    T temp = toShuffle[j];
                    toShuffle[j] = toShuffle[i];
                    toShuffle[i] = temp;
                }
                return toShuffle;
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
                resetBag();
                reshuffle();
            }

            public void resetBag()
            {
                shuffle.AddRange(defBag);
                for (int i = 1; i < toScale; i++)
                {
                    shuffle.AddRange(defBag);
                }
            }

            public void reshuffle() {  shuffle = Math.FisherYatesShuffle(shuffle);  }

            public int Next()
            {
                string list = "";
                if (shuffle.Count == 0) resetBag();
                int toReturn = shuffle[0];
                shuffle.RemoveAt(0);
                for (int i = 0; i < shuffle.Count; i++)
                {
                    list += shuffle[i].ToString() + ", ";
                }

                //Debug.Log(list);
                return toReturn;
            }
        }
    }
}
