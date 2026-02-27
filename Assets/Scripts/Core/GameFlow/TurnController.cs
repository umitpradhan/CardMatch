using System;
using System.Collections;
using System.Collections.Generic;
using CardMatch.Core.Domain;
using CardMatch.Infrastructure;
using CardMatch.Presentation.Views;

namespace CardMatch.Core.GameFlow
{
    public sealed class TurnController
    {
        private readonly BoardModel _board;
        private readonly MatchResolver _resolver;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly ComboTracker _comboTracker;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly BoardView _boardView;

        private readonly Queue<CardModel> _flipQueue = new();
        private bool _isProcessing;
        public event Action OnMatch;
        public event Action OnMismatch;

        private int _score;
        private float _remainingTime;

        public int CurrentScore => _score;

        public TurnController(
            BoardModel board,
            MatchResolver resolver,
            ScoreCalculator scoreCalculator,
            ComboTracker comboTracker,
            CoroutineRunner coroutineRunner,
            BoardView boardView)
        {
            _board = board;
            _resolver = resolver;
            _scoreCalculator = scoreCalculator;
            _comboTracker = comboTracker;
            _coroutineRunner = coroutineRunner;
            _boardView = boardView;
        }

        public void SetRemainingTime(float time)
        {
            _remainingTime = time;
        }

        public void OnCardFlipped(CardModel card)
        {
            if (card == null || !card.CanFlip)
                return;

            card.FlipUp();
            _flipQueue.Enqueue(card);

            if (!_isProcessing)
            {
                _coroutineRunner.Run(ProcessQueue());
            }
        }

        private IEnumerator ProcessQueue()
        {
            _isProcessing = true;

            while (_flipQueue.Count >= 2)
            {
                var first = _flipQueue.Dequeue();
                var second = _flipQueue.Dequeue();

                yield return new UnityEngine.WaitForSeconds(0.2f);

                var result = _resolver.Evaluate(first, second);

                if (result == MatchResult.Match)
                {
                    first?.MarkMatched();
                    second?.MarkMatched();

                    _comboTracker.RegisterMatch();

                    int gained = _scoreCalculator.CalculateMatchScore(
                        _comboTracker.CurrentCombo,
                        _remainingTime);

                    _score += gained;

                    var viewA = _boardView.GetView(first.Id);
                    var viewB = _boardView.GetView(second.Id);

                    viewA?.PlayMatch();
                    viewB?.PlayMatch();

                    OnMatch?.Invoke();
                }
                else
                {
                    _comboTracker.RegisterMismatch();

                    _score -= _scoreCalculator.CalculateMismatchPenalty();

                    yield return new UnityEngine.WaitForSeconds(0.4f);

                    first?.FlipDown();
                    second?.FlipDown();

                    var viewA = _boardView.GetView(first.Id);
                    var viewB = _boardView.GetView(second.Id);

                    viewA?.PlayFlipDown();
                    viewB?.PlayFlipDown();

                    OnMismatch?.Invoke();
                }
            }

            _isProcessing = false;
        }
    }
}