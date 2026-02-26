namespace CardMatch.Core.Domain
{
    public sealed class ScoreConfig
    {
        public int BaseMatchPoints = 100;
        public float ComboStep = 0.5f;
        public int MismatchPenalty = 20;
        public float TimeBonusFactor = 2f;
    }
}