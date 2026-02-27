using UnityEngine;
using UnityEngine.UI;
using System;
using CardMatch.Core.Domain;

namespace CardMatch.Presentation.Views
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _visualRoot;
        [SerializeField] private GameObject _front;
        [SerializeField] private GameObject _back;

        public int CardId { get; private set; }

        private CardModel _model;
        private Action<int> _onSelected;

        private bool _isAnimating;

        public void Initialize(CardModel model, Action<int> onSelected)
        {
            _model = model;
            CardId = model.Id;
            _onSelected = onSelected;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClicked);

            SyncImmediate();
        }

        private void OnClicked()
        {
            if (_isAnimating)
                return;

            _onSelected?.Invoke(CardId);
        }

        public void PlayFlipUp()
        {
            if (_isAnimating) return;
            StartCoroutine(FlipRoutine(true));
        }

        public void PlayFlipDown()
        {
            if (_isAnimating) return;
            StartCoroutine(FlipRoutine(false));
        }

        public void PlayMatch()
        {
            _button.interactable = false;
        }

        private System.Collections.IEnumerator FlipRoutine(bool reveal)
        {
            _isAnimating = true;

            float duration = 0.15f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 0f, elapsed / duration);
                _visualRoot.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }

            _front.SetActive(reveal);
            _back.SetActive(!reveal);

            elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(0f, 1f, elapsed / duration);
                _visualRoot.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }

            _visualRoot.localScale = Vector3.one;
            _isAnimating = false;
        }

        public void SyncImmediate()
        {
            switch (_model.State)
            {
                case CardState.Hidden:
                    _front.SetActive(false);
                    _back.SetActive(true);
                    break;

                case CardState.Revealed:
                case CardState.Matched:
                    _front.SetActive(true);
                    _back.SetActive(false);
                    break;
            }
        }
    }
}