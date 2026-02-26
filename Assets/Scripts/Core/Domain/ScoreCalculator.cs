namespace CardMatch.Core.Domain
{
    public sealed class ScoreCalculator
    {
        private readonly ScoreConfig _config;

        public ScoreCalculator(ScoreConfig config)
        {
            _config = config;
        }

        public int CalculateMatchScore(int comboCount, float remainingTime)
        {
            int basePoints = _config.BaseMatchPoints;

            float multiplier = 1f + (comboCount * _config.ComboStep);
            int comboScore = (int)(basePoints * multiplier);

            int timeBonus = (int)(remainingTime * _config.TimeBonusFactor);

            return comboScore + timeBonus;
        }

        public int CalculateMismatchPenalty()
        {
            return _config.MismatchPenalty;
        }
    }
}