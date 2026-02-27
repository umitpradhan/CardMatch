using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.Presentation.Layout
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public sealed class GridAutoScaler : MonoBehaviour
    {
        private GridLayoutGroup _grid;
        private RectTransform _rect;

        private void Awake()
        {
            _grid = GetComponent<GridLayoutGroup>();
            _rect = GetComponent<RectTransform>();
        }

        public void Configure(int rows, int columns)
        {
            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _grid.constraintCount = columns;

            UpdateCellSize(rows, columns);
        }

        private void UpdateCellSize(int rows, int columns)
        {
            float width = _rect.rect.width;
            float height = _rect.rect.height;

            float spacingX = _grid.spacing.x * (columns - 1);
            float spacingY = _grid.spacing.y * (rows - 1);

            float cellWidth = (width - spacingX) / columns;
            float cellHeight = (height - spacingY) / rows;

            float size = Mathf.Min(cellWidth, cellHeight);

            _grid.cellSize = new Vector2(size, size);
        }
    }
}