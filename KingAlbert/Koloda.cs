using System;
using System.Collections.Generic;
using System.Drawing;

namespace KingAlbert
{
    public class Koloda
    {
        public List<Card> koloda;

        public Koloda()
        {
            koloda = new List<Card>();
        }

        public Koloda(int countCard)
        {
            koloda = new List<Card>();
            if (countCard == 52)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        koloda.Add(new Card((Card.Suit)i, (Card.Titles)j));
                    }
                }
                RandomList();
            }
        }

        public void RandomList()
        {
            Random rm = new Random();
            int n = koloda.Count;
            while (n > 1)
            {
                n--;
                int k = rm.Next(n + 1);
                Card value = koloda[k];
                koloda[k] = koloda[n];
                koloda[n] = value;
            }
        }

        public void PutCard(Card card)
        {
            koloda.Add(card);
        }

        public Card TakeCard()
        {
            if (koloda.Count > 0)
            {
                Card card = koloda[koloda.Count - 1];
                koloda.RemoveAt(koloda.Count - 1);
                return card;
            }
            return null;
        }



        public Card GetTopCard()
        {
            if (koloda.Count > 0)
            {
                return koloda[koloda.Count - 1];
            }
            return null;
        }

        public Bitmap GetTopCardImage()
        {
            if (koloda.Count > 0)
            {
                return koloda[koloda.Count - 1].GetImgCard();
            }
            return null;
        }

        public int Count
        {
            get { return koloda.Count; }
        }
    }
}
