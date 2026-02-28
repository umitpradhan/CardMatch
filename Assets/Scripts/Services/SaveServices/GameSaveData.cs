using System;
using System.Collections.Generic;

namespace CardMatch.Services.SaveServices
{
    [Serializable]
    public sealed class GameSaveData
    {
        public int Version = 1;

        public int Rows;
        public int Columns;
        public int Seed;

        public int CurrentScore;
        public int CurrentCombo;

        public float RemainingTime;

        public List<CardSaveData> Cards;
    }
}