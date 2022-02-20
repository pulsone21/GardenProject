using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GridSystem
{
    public interface IGridDebug
    {
        void CreateDebugText();
        void ToogleDebugText();
        void UpdateDebugText(string newText);
    }
}
