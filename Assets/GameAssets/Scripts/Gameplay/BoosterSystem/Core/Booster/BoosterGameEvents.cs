using System;
using UnityEngine;

namespace Gameplay
{
    public static class BoosterGameEvents
    {
        public static Action<BoosterType, int> OnBoosterCountChanged;
        public static Action<BoosterType> OnBoosterUsed;
    }
}