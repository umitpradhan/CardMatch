using System;

namespace CardMatch.Core.Domain
{
    public sealed class CardModel
    {
        public int Id { get; }
        public int PairId { get; }
        public CardState State { get; private set; }

        public CardModel(int id, int pairId)
        {
            Id = id;
            PairId = pairId;
            State = CardState.Hidden;
        }

        public bool CanFlip => State == CardState.Hidden;

        public void FlipUp()
        {
            if (State != CardState.Hidden)
                return;

            State = CardState.Revealed;
        }

        public void FlipDown()
        {
            if (State != CardState.Revealed)
                return;

            State = CardState.Hidden;
        }

        public void MarkMatched()
        {
            if (State != CardState.Revealed)
                return;

            State = CardState.Matched;
        }
    }
}