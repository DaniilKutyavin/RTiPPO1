using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace KingAlbert
{
    public partial class Game : Form
    {

        private int selectKoloda;
        private bool isDown;
        private bool isUp;
        public static Card tempCard;

        private Point mousePos;

        public void Init()
        {
            for (int i = 6; i < kolods.Length; i++)
            {
                int countcard = i - 5;
                MoveCard(0, i, countcard);
            }
        }

        public void DrawCard(Graphics g, Card card, Point point)
        {
            g.DrawImage(card.GetImgCard(), point);
        }

        public void MoveCard(int start, int finish, int countCard)
        {
            for (int i = 0; i < countCard; i++)
            {
                if (kolods[start].koloda.Count > 0)
                {
                    kolods[finish].PutCard(kolods[start].TakeCard());
                }
            }
        }

        private void IsFinish()
        {
            if (kolods[2].koloda.Count == 13 && kolods[3].koloda.Count == 13 && kolods[4].koloda.Count == 13 && kolods[5].koloda.Count == 13)
            {
                MessageBox.Show("Победа!");
                newGameToolStripMenuItem_Click(this, null);
            }
        }

        public void DrawReserve(Graphics g)
        {
            int offset = 0;
            for (int i = 0; i < kolods[0].koloda.Count; i++)
            {
                g.DrawImage(kolods[0].koloda[i].GetImgCard(), new Point(kolodaP[0].X + offset, kolodaP[0].Y));
                offset += 20;
            }
        }

        public void DrawKolods(Graphics g)
        {
            for (int i = 0; i < kolods.Length; i++)
            {
                if (kolods[i].koloda.Count != 0)
                {
                    if (i == 0)
                    {
                        DrawReserve(g);
                    }
                    else if (i >= 6)
                    {
                        int ii = 0;
                        foreach (Card card in kolods[i].koloda)
                        {
                            g.DrawImage(card.GetImgCard(), new Point(kolodaP[i].X, kolodaP[i].Y + ii * 30));
                            ii++;
                        }
                    }
                    else
                    {
                        g.DrawImage(kolods[i].GetTopCardImage(), kolodaP[i]);
                    }
                }
            }
        }

        private readonly Point[] kolodaP = new Point[]
        {
            new Point(62, 74),
            new Point(230, 74),
            new Point(518, 74),
            new Point(690, 74),
            new Point(865, 74),
            new Point(1038, 74),
            new Point(118, 300),
            new Point(290, 300),
            new Point(466, 300),
            new Point(638, 300),
            new Point(813, 300),
            new Point(986, 300),
            new Point(1158, 300),
            new Point(1330, 299),
            new Point(1506, 299),
            new Point(1678, 299)
        };

        private string playerName;

        public Game()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            using (var nameForm = new Player())
            {
                if (nameForm.ShowDialog() == DialogResult.OK)
                {
                    playerName = nameForm.PlayerName;
                    lblPlayerName.Text = $"Игрок: {playerName}";
                }
                else
                {
                    Application.Exit();
                    return;
                }
            }


            Init();
        }

        private Koloda[] kolods = new Koloda[]
        {
            new Koloda(52),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
            new Koloda(),
        };

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            counterscope = 0;
            toolStripStatusLabel1.Text = "Твой счет: 0";
            lblPlayerName.Text = $"Игрок: {playerName}";

            kolods = new Koloda[]
            {
                new Koloda(52),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
                new Koloda(),
            };

            Init();
            update();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateScore()
        {
            toolStripStatusLabel1.Text = $"Твой счет: {counterscope}";
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            bool isRestore = true;
            if (isDown)
            {
                for (int i = 0; i < kolods.Length; i++)
                {
                    if ((i >= 2 & i <= 5) && kolodaR[i].Contains(e.Location) && selectKoloda != i && tempCard != null)
                    {
                        if ((kolods[i].koloda.Count - 1) != 13)
                        {
                            if (kolods[i].koloda.Count == 0 && tempCard.titles == Card.Titles.ace)
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);
                                counterscope++;
                                update();
                                UpdateScore();
                                break;
                            }
                            if (kolods[i].koloda.Count > 0 && ((kolods[i].koloda[kolods[i].koloda.Count - 1].titles + 1) == tempCard.titles) && (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == tempCard.suit))
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);
                                counterscope++;
                                update();
                                UpdateScore();
                                break;
                            }
                        }
                        break;
                    }
                    else if (i >= 6)
                    {
                        if (kolods[i].koloda.Count == 0 && kolodaR[i].Contains(e.Location))
                        {
                            if (tempCard != null)
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);
                                counterscope++;
                                UpdateScore();
                                break;
                            }

                        }
                        else if (kolodaR[i].Contains(e.Location) && tempCard != null && selectKoloda != i && (kolods[i].koloda[kolods[i].koloda.Count - 1].titles - 1 == tempCard.titles))
                        {
                            if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.buba) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.cherva))
                            {
                                if ((tempCard.suit == Card.Suit.trefa) || (tempCard.suit == Card.Suit.pika))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard);
                                    counterscope++;
                                    UpdateScore();
                                    break;
                                }
                            }
                            else if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.trefa) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.pika))
                            {
                                if ((tempCard.suit == Card.Suit.cherva) || (tempCard.suit == Card.Suit.buba))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard);
                                    counterscope++;
                                    UpdateScore();
                                    break;
                                }
                            }
                        }

                    }
                }
                if (isRestore)
                {

                    if (selectKoloda == 0)
                    {
                        int cardIndex = (mousePos.X - kolodaP[0].X) / 20;
                        if (cardIndex > kolods[0].koloda.Count)
                            cardIndex = kolods[0].koloda.Count;
                        kolods[0].koloda.Insert(cardIndex, tempCard);
                    }
                    else
                    {
                        kolods[selectKoloda].PutCard(tempCard);
                    }
                    MessageBox.Show("Неправильная попытка поставить карту!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isDown = false;
            }
            isUp = true;
            update();
            tempCard = null;
            IsFinish();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < kolods.Length; i++)
                {
                    if (kolodaR[i].Contains(e.Location))
                    {
                        if (kolods[i].Count > 0)
                        {
                            selectKoloda = i;
                            isDown = true;

                            if (i == 0)
                            {
                                int cardIndex = (e.Location.X - kolodaP[0].X) / 20;
                                if (cardIndex >= kolods[0].koloda.Count)
                                    cardIndex = kolods[0].koloda.Count - 1;
                                tempCard = kolods[0].koloda[cardIndex];
                                kolods[0].koloda.RemoveAt(cardIndex);
                            }
                            else
                            {
                                tempCard = kolods[i].GetTopCard();
                                kolods[i].TakeCard();
                            }

                            mousePos = e.Location;
                            update();
                            break;
                        }
                    }
                }
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown && (tempCard != null))
            {
                update();
                mousePos = e.Location;
            }

        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            DrawKolods(e.Graphics);
            if (isDown && tempCard != null)
            {
                DrawCard(e.Graphics, tempCard, mousePos);
            }

        }

        public void update()
        {
            Invalidate();
        }

        private Rectangle[] kolodaR = new Rectangle[]
        {
            new Rectangle(62, 74, 144, 192),
            new Rectangle(230, 74, 144, 192),
            new Rectangle(518, 74, 144, 192),
            new Rectangle(690, 74, 144, 192),
            new Rectangle(865, 74, 144, 192),
            new Rectangle(1038, 74, 144, 192),
            new Rectangle(118, 300, 144, 666),
            new Rectangle(290, 300, 144, 666),
            new Rectangle(466, 300, 144, 666),
            new Rectangle(638, 300, 144, 666),
            new Rectangle(813, 300, 144, 666),
            new Rectangle(986, 300, 144, 666),
            new Rectangle(1158, 300, 144, 666),
            new Rectangle(1330, 299, 144, 666),
            new Rectangle(1506, 299, 144, 666),
            new Rectangle(1678, 299, 144, 666)
        };



        public int counterscope = 0;
    }
}