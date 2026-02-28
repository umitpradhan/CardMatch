using System;
using CardMatch.Core.Domain;

namespace CardMatch.Services.SaveServices
{
    [Serializable]
    public sealed class CardSaveData
    {
        public int Id;
        public int PairId;
        public CardState State;
    }
}