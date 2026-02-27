using UnityEngine;
using TMPro;
using CardMatch.Services.ScoreServices;

namespace CardMatch.Presentation.UI
{
    public sealed class ComboView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _comboText;

        public void Bind(ScoreService service)
        {
            service.OnComboChanged += UpdateCombo;
            UpdateCombo(service.CurrentCombo);
        }

        private void UpdateCombo(int combo)
        {
            _comboText.text = combo > 1 ? $"Combo x{combo}" : "";
        }
    }
}