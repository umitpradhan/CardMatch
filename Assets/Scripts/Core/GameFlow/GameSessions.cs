using CardMatch.Core.Domain;

namespace CardMatch.Core.GameFlow
{
    public sealed class GameSession
    {
        public BoardModel Board { get; }
        public TurnController TurnController { get; }

        public GameSession(BoardModel board, TurnController turnController)
        {
            Board = board;
            TurnController = turnController;
        }

        public bool IsGameOver()
        {
            return Board.AllMatched();
        }
    }
}