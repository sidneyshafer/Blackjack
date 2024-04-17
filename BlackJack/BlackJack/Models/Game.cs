namespace BlackJack.Models
{
    public class Game : IGame
    {
        private ISession session { get; set; }
        public Deck Deck { get; set; }
        public Player Player { get; set; }
        public Dealer Dealer { get; set; }
        public bool NeedsDeal { get; set; }

        // enum for letting controller know result of deal, hit, or stand action
        public enum Result  {
            Shuffling, Continue, DealerBust, DealerBlackJack, DealerWin,
            PlayerBust, PlayerBlackJack, PlayerWin, DoubleBlackJack, Push
        }

        public Game(IHttpContextAccessor accessor)
        {
            // retrieve game data from session
            session = accessor.HttpContext?.Session!;

            Deck = session.GetObject<Deck>("deck") ?? new Deck();
            Player = session.GetObject<Player>("player") ?? new Player();
            Dealer = session.GetObject<Dealer>("dealer") ?? new Dealer();

            int dealVal = session.GetInt32("deal") ?? 1;  // default value of true (1)
            NeedsDeal = Convert.ToBoolean(dealVal);
        }

        public Result Deal()
        {
            var result = Result.Continue;  // default result

            // on first load, deck is null - shuffle
            if (Deck.Cards == null) Deck.Shuffle();

            if (Deck.Cards?.Count < 4)
            {
                Deck.Shuffle();
                result = Result.Shuffling;
            }
            else
            {
                Player.NewHand(Deck.Deal(), Deck.Deal());
                Dealer.NewHand(Deck.Deal(), Deck.Deal());

                Dealer.Hand.HideHoleCard = true;
                NeedsDeal = false;
            }

            if (Player.Hand.HasBlackJack && Dealer.Hand.HasBlackJack)
            {
                Update();
                Dealer.ShowCards();
                result = Result.DoubleBlackJack;
            }
            else if (Player.Hand.HasBlackJack)
            {
                Update();
                Dealer.ShowCards();
                result = Result.PlayerBlackJack;
            }
            else if (Dealer.Hand.HasBlackJack)
            {
                Update();
                Dealer.ShowCards();
                result = Result.DealerBlackJack;
            }

            Save();
            return result;
        }

        public Result Hit()
        {
            var result = Result.Continue;  // default result

            bool needsShuffle = HitPlayer();

            if (needsShuffle)
            {
                Deck.Shuffle();
                result = Result.Shuffling;
            }
            else if (Player.Hand.IsBusted)
            {
                Update();
                Dealer.ShowCards();
                result = Result.PlayerBust;
            }

            Save();
            return result;
        }

        public Result Stand()
        {
            var result = Result.Continue;

            Dealer.ShowCards();

            if (Dealer.MustHit)
            {
                bool needsShuffle = HitDealer();
                if (needsShuffle)
                {
                    Deck.Shuffle();
                    result = Result.Shuffling;
                }
                else if (Dealer.Hand.IsBusted)
                {
                    Update();
                    Save();
                    return Result.DealerBust;  // short-circuit if dealer busts
                }
            }

            if (Dealer.MustHit) // if dealer still needs to hit
            {
                // don't do anything - will return default 'Continue' Result value
            }
            else if (IsDealerHandHigher)
            {
                Update();
                result = Result.DealerWin;
            }
            else if (IsPlayerHandHigher)
            {
                Update();
                result = Result.PlayerWin;
            }
            else if (IsPush)
            {
                Update();
                result = Result.Push;
            }

            Save();
            return result;
        }

        /****************************************
         * Private helper properties and methods
         ***************************************/
        private int Bet => 10;
        private bool IsDealerHandHigher => Dealer.Hand.Total > Player.Hand.Total;
        private bool IsPlayerHandHigher => Player.Hand.Total > Dealer.Hand.Total;
        private bool IsPush => Player.Hand.Total == Dealer.Hand.Total;

        private bool HitPlayer()
        {
            bool needsShuffle = false;
            if (Deck.Cards.Count == 0)
                needsShuffle = true;
            else
                Player.Hit(Deck.Deal());
            return needsShuffle;
        }

        private bool HitDealer()
        {
            bool needsShuffle = false;
            if (Deck.Cards.Count == 0)
                needsShuffle = true;
            else
                Dealer.Hit(Deck.Deal());
            return needsShuffle;
        }

        private void Update()
        {
            if (Dealer.Hand.IsBusted || (IsPlayerHandHigher && !Player.Hand.IsBusted))
            {
                int winnings = (Player.Hand.HasBlackJack) ? Bet + (Bet / 2) : Bet;
                Player.TotalWinnings += winnings;
            }
            else if (Player.Hand.IsBusted || (IsDealerHandHigher && !Dealer.Hand.IsBusted))
            {
                Player.TotalWinnings -= Bet;
            }
            NeedsDeal = true;
        }

        private void Save()
        {
            // save game data to session
            session.SetObject("deck", Deck);
            session.SetObject("player", Player);
            session.SetObject("dealer", Dealer);
            session.SetInt32("deal", Convert.ToInt32(NeedsDeal));
        }


    }
}
