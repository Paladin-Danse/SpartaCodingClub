namespace BlackJack
{
    using System;
    using System.Collections.Generic;

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    // 카드 한 장을 표현하는 클래스
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    // 패를 표현하는 클래스
    public class Hand
    {
        private List<Card> cards;
        public List<Card> getCards { get { return cards; } }
        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 여기부터는 학습자가 작성
    // 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
    public class Dealer : Player
    {
        // 코드를 여기에 작성하세요
    }

    // 블랙잭 게임을 구현하세요. 
    public class Blackjack
    {
        Point dealerTable = new Point(10, 5);
        Point playerTable = new Point(10, 15);
        Point menuPos = new Point(20, 25);
        Point resultPos = new Point(20, 30);
        // 코드를 여기에 작성하세요
        Player player = new Player();
        Dealer dealer = new Dealer();
        Deck deck = new Deck();
        bool isPlayerTurn = true;
        bool isPlayerBurst = false;
        bool isDealerBurst = false;
        public void GameStart()
        {
            //시작할 때 플레이어와 딜러가 카드를 두장씩 뽑음.
            for(int i = 0; i < 2; i++)
            {
                player.DrawCardFromDeck(deck);
                
                dealer.DrawCardFromDeck(deck);
            }

            Console.Clear();
            Console.SetCursorPosition(dealerTable.x, dealerTable.y);
            Console.Write(dealer.Hand.getCards.First().ToString() + " ");

            Console.SetCursorPosition(dealerTable.x, dealerTable.y + 1);
            Console.Write($"▩ ");//두 번째 카드는 필드에 나와있지만 가림.

            int cnt = 0;
            foreach(Card card in player.Hand.getCards)
            {
                Console.SetCursorPosition(playerTable.x, playerTable.y + (cnt++));
                Console.Write(card.ToString() + " ");
            }
            Console.SetCursorPosition(playerTable.x, playerTable.y - 1);
            Console.Write($"total : {player.Hand.GetTotalValue()}");


            if (player.Hand.GetTotalValue() == 21)
            {
                PlayerWin("블랙잭!!");
                return;
            }
            if (dealer.Hand.GetTotalValue() == 21)
            {
                DealerWin("블랙잭!!");
                return;
            }

            Console.SetCursorPosition(menuPos.x, menuPos.y);
            Console.Write("1. 히트(한장 더)");
            Console.SetCursorPosition(menuPos.x, menuPos.y + 1);
            Console.Write("2. 스테이(멈추기)");
            //플레이어 턴
            while (isPlayerTurn)
            {
                int menuSelect;

                Console.SetCursorPosition(menuPos.x, menuPos.y + 2);
                Console.Write(' ');
                Console.SetCursorPosition(menuPos.x, menuPos.y + 2);
                if (int.TryParse(Console.ReadLine(), out menuSelect))
                {
                    if (menuSelect == 1)
                    {
                        Card drawCard = player.DrawCardFromDeck(deck);

                        Console.SetCursorPosition(playerTable.x, playerTable.y - 1);
                        Console.Write($"total : {player.Hand.GetTotalValue()}");
                        Console.SetCursorPosition(playerTable.x, playerTable.y + (player.Hand.getCards.Count - 1));
                        Console.Write(drawCard.ToString() + " ");

                        if(player.Hand.GetTotalValue() > 21)//버스트
                        {
                            isPlayerBurst = true;
                            isPlayerTurn = false;
                        }
                    }
                    else if (menuSelect == 2)
                    {
                        isPlayerTurn = false;
                    }
                }
            }
            if (isPlayerBurst)
            {
                DealerWin("플레이어 버스트!!");
                return;
            }

            Console.SetCursorPosition(dealerTable.x, dealerTable.y + 1);
            Console.Write(dealer.Hand.getCards[1]);//두 번째 카드 공개
            Console.SetCursorPosition(dealerTable.x, dealerTable.y - 1);
            Console.Write($"total : {dealer.Hand.GetTotalValue()}");
            //딜러의 턴
            while (dealer.Hand.GetTotalValue() < 17)
            {
                Thread.Sleep(300);

                Card drawCard = dealer.DrawCardFromDeck(deck);

                Console.SetCursorPosition(dealerTable.x, dealerTable.y - 1);
                Console.Write($"total : {dealer.Hand.GetTotalValue()}");
                Console.SetCursorPosition(dealerTable.x, dealerTable.y + (dealer.Hand.getCards.Count - 1));
                Console.Write(drawCard.ToString());
                if(dealer.Hand.GetTotalValue() > 21)//버스트
                {
                    isDealerBurst = true;
                }
            }
                
            if(isDealerBurst)
            {
                PlayerWin("딜러 버스트!!");
                return;
            }

            Console.SetCursorPosition(resultPos.x, resultPos.y);
            Console.Write($"플레이어 {player.Hand.GetTotalValue()} : 딜러 {dealer.Hand.GetTotalValue()}");
            if(player.Hand.GetTotalValue() > dealer.Hand.GetTotalValue())
            {
                PlayerWin();
            }
            else if(player.Hand.GetTotalValue() < dealer.Hand.GetTotalValue())
            {
                DealerWin();
            }
            else
            {
                Console.SetCursorPosition(resultPos.x, resultPos.y + 1);
                Console.Write("무승부");
            }
        }

        public void PlayerWin(string _message)
        {
            Console.SetCursorPosition(resultPos.x, resultPos.y);
            Console.Write(_message);
            Console.SetCursorPosition(resultPos.x, resultPos.y + 1);
            Console.Write("플레이어의 승리!");
        }
        public void PlayerWin()
        {
            Console.SetCursorPosition(resultPos.x, resultPos.y + 1);
            Console.Write("플레이어의 승리!");
        }
        public void DealerWin(string _message)
        {
            Console.SetCursorPosition(resultPos.x, resultPos.y);
            Console.Write(_message);
            Console.SetCursorPosition(resultPos.x, resultPos.y + 1);
            Console.Write("딜러의 승리!");
        }
        public void DealerWin()
        {
            Console.SetCursorPosition(resultPos.x, resultPos.y + 1);
            Console.Write("딜러의 승리!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Point WindowSize = new Point(80, 40);
            Console.SetWindowSize(WindowSize.x, WindowSize.y);

            // 블랙잭 게임을 실행하세요
            Blackjack blackjack = new Blackjack();
            
            blackjack.GameStart();
            
        }
    }

    class Point
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
