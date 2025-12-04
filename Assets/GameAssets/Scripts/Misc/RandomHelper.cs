using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public static class RandomHelper 
    {
        public static int Choose(List<int> probs)
        {
            float total = 0;
            foreach (float elem in probs)
                total += elem;
            var rd = Random.value;
            float randomPoint =rd * total;
            for (int i = 0; i < probs.Count; i++)
            {
                if (randomPoint < probs[i])
                    return i;
                else
                    randomPoint -= probs[i];
            }
            return probs.Count - 1;
        }

        public static void Shuffle<T>(this IList<T> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                var temp = deck[i];
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }
    }
}