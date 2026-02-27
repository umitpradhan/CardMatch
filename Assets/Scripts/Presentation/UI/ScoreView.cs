using UnityEngine;
using TMPro;
using CardMatch.Services.ScoreServices;

namespace CardMatch.Presentation.UI
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void Bind(ScoreService service)
        {
            service.OnScoreChanged += UpdateScore;
            UpdateScore(service.CurrentScore);
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}