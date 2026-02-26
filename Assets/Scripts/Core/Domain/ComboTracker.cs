namespace CardMatch.Core.Domain
{
    public sealed class MatchResolver
    {
        public MatchResult Evaluate(CardModel first, CardModel second)
        {
            if (first == null || second == null)
                return MatchResult.Mismatch;

            if (first.Id == second.Id)
                return MatchResult.Mismatch;

            return first.PairId == second.PairId
                ? MatchResult.Match
                : MatchResult.Mismatch;
        }
    }
}