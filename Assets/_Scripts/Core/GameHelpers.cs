using System;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.Core
{
    /// <summary>
    /// Static helper class which contains method for vary components, scripts and situations.
    /// </summary>
    public static class GameHelper
    {
        #region UI
        public static void SetAlpha(this Image image, float alpha)
        {
            var curColor = image.color;
            curColor.a = alpha;
            image.color = curColor;
        }
        
        public static Color Alpha1(this Color color)
        {
            color.a = 1;
            return color;
        }

        public static Color Alpha0(this Color color)
        {
            color.a = 0;
            return color;
        }
        
        #endregion
        
        #region Randomness
        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion
    }
}
