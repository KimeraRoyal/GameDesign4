using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RectDivider
    {
        private class Subrect
        {
            readonly uint _iteration;

            Subrect[] _children;

            readonly Rect _bounds;

            public Subrect(uint iteration, Rect bounds)
            {
                _iteration = iteration;
                _bounds = bounds;
            }

            public void Generate(uint maxIterations, Vector2 splitRange, Vector2 minSize, Vector2 padding)
            {
                if (_iteration >= maxIterations) { return; }
                
                bool splitVertically = Random.Range(0, 2) == 1;

                float splitPosition = Random.Range(splitRange.x, splitRange.y);
                Vector2 size = splitVertically ?
                    new Vector2(_bounds.width, _bounds.height * splitPosition) :
                    new Vector2(_bounds.width * splitPosition, _bounds.height);

                if (minSize.magnitude > 0.001f)
                {
                    if (size.x < minSize.x || size.y < minSize.y)
                    {
                        splitVertically = !splitVertically;
                        size = splitVertically ?
                            new Vector2(_bounds.width, _bounds.height * splitPosition) :
                            new Vector2(_bounds.width * splitPosition, _bounds.height);
                    }
                    if (size.x < minSize.x || size.y < minSize.y) { return; }
                }

                Vector2 paddingOffset = padding * (splitVertically ? Vector2.up : Vector2.right);
                Vector2 offset = size * (splitVertically ? Vector2.up : Vector2.right);
                
                Rect aBounds = new(_bounds.position, size - paddingOffset);
                Rect bBounds = new(_bounds.position + offset + paddingOffset, _bounds.size - offset - paddingOffset);
                
                _children = new [] { new Subrect(_iteration + 1, aBounds), new Subrect(_iteration + 1, bBounds) };
                foreach (Subrect child in _children)
                {
                    child.Generate(maxIterations, splitRange, minSize, padding);
                }
            }

            public void GetLowestDepth(List<Rect> output)
            {
                if (_children == null)
                {
                    output.Add(_bounds);
                    return;
                }
                foreach (Subrect child in _children)
                {
                    child.GetLowestDepth(output);
                }
            }
        }

        Rect _paddedBounds;

        public Rect Bounds { get; private set; }
        public Rect PaddedBounds => _paddedBounds;
        public List<Rect> Divisions { get; private set; }

        private RectDivider(Rect bounds)
        {
            Bounds = bounds;
            Divisions = new List<Rect>();
        }

        public static RectDivider Divide(Rect bounds, uint iterations, Vector2 splitRange, Vector2 minSize, Vector2 border, Vector2 padding)
        {
            RectDivider divider = new(bounds);

            splitRange = new Vector2(Mathf.Clamp01(splitRange.x), Mathf.Clamp01(splitRange.y));
            minSize = Vector2.Max(Vector2.zero, minSize);
            
            divider.Divide(iterations, splitRange, minSize, border, padding);
            return divider;
        }

        private void Divide(uint iterations, Vector2 splitRange, Vector2 minSize, Vector2 border, Vector2 padding)
        {
            if (iterations < 1)
            {
                Divisions.Add(Bounds);
                return;
            }

            _paddedBounds = Bounds;
            _paddedBounds.min += border;
            _paddedBounds.max -= border;

            Subrect root = new(0, _paddedBounds);
            root.Generate(iterations, splitRange, minSize, padding);
            root.GetLowestDepth(Divisions);
        }
    }
}
