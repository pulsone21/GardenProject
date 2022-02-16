using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

namespace Utilities
{
    public static class Utils
    {
        public static float NormalizeBetweenNegative1And1(float value, float minValue, float maxValue)
        {
            return (1 - -1) * (value - minValue) / (maxValue - minValue) + -1;
        }

        public static float NormalizeBetween0And1(float value, float minValue, float maxValue)
        {
            return (value - minValue) / (maxValue - minValue);
        }


        public static List<T> ArrayToList<T>(T[] _array)
        {
            List<T> _newList = new List<T>();
            foreach (T _item in _array)
            {
                _newList.Add(_item);
            }
            return _newList;
        }


        public static TextMeshPro CreateWorldText(Transform _parent, string _text, Vector3 _worldPosition, Color _fontColor = default, int _fontSize = 20)
        {
            GameObject GO = new GameObject("World_Text", typeof(TextMeshPro));
            Transform transform = GO.transform;
            transform.SetParent(_parent);
            transform.position = _worldPosition;
            TextMeshPro textMesh = GO.GetComponent<TextMeshPro>();
            textMesh.alignment = TextAlignmentOptions.Center;
            textMesh.text = _text;
            textMesh.fontSize = _fontSize;
            textMesh.color = _fontColor;
            return textMesh;
        }

    }

}