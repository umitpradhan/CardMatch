using UnityEngine;
using CardMatch.Core.Domain;
using CardMatch.Presentation.Views;
using CardMatch.Services.SaveServices;

namespace CardMatch.Core.GameFlow
{
    public sealed class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private BoardController _boardController;
        [SerializeField] private GameFlowController _gameFlowController;

        private SaveService _saveService;

        private void Start()
        {
            _saveService = new SaveService();

            if (_saveService.HasSave())
            {
                LoadGame();
            }
            else
            {
                StartNewGame();
            }
        }

        private void StartNewGame()
        {
            int rows = 4;
            int columns = 4;
            int seed = Random.Range(0, int.MaxValue);

            var generator = new BoardGenerator();
            var cards = generator.Generate(rows, columns, seed);

            var board = new BoardModel(cards, rows, columns);

            _boardController.Initialize(board);

            var turnController = _boardController.GetComponent<BoardController>();
            _gameFlowController.Initialize(board, null, seed);
        }

        private void LoadGame()
        {
            var saveData = _saveService.Load();

            var board = SaveMapper.ToBoardModel(saveData);

            _boardController.Initialize(board);

            _gameFlowController.Initialize(
                board,
                null,
                saveData.Seed);
        }
    }
}