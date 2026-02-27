using System.Collections.Generic;
using UnityEngine;

namespace CardMatch.Presentation.Pooling
{
    public sealed class CardViewPool
    {
        private readonly CardMatch.Presentation.Views.CardView _prefab;
        private readonly Transform _parent;

        private readonly Stack<CardMatch.Presentation.Views.CardView> _pool;

        public CardViewPool(CardMatch.Presentation.Views.CardView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<CardMatch.Presentation.Views.CardView>();
        }

        public CardMatch.Presentation.Views.CardView Get()
        {
            if (_pool.Count > 0)
            {
                var view = _pool.Pop();
                view.gameObject.SetActive(true);
                return view;
            }

            return Object.Instantiate(_prefab, _parent);
        }

        public void Release(CardMatch.Presentation.Views.CardView view)
        {
            view.gameObject.SetActive(false);
            _pool.Push(view);
        }
    }
}