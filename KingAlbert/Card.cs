using System.Drawing;

namespace KingAlbert
{
    public class Card
    {
        public enum Titles { ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king }
        public enum Suit { cherva, buba, pika, trefa }

        public Titles titles { get; private set; }
        public Suit suit { get; private set; }
        private Bitmap img;

        public Card(Suit _suit, Titles _title)
        {
            suit = _suit;
            titles = _title;
            img = (Bitmap)Properties.Resources.ResourceManager.GetObject($"{_title}_{_suit}", null);
        }

        public Bitmap GetImgCard()
        {
            return img;
        }
    }
}
