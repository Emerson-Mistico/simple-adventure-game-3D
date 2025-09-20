using NUnit.Framework;
using UnityEngine;

namespace Screens
{
    public class ScreenHelper : MonoBehaviour
    {
        public ScreenType screenType;

        public void OnClick()
        {
            ScreenManager.Instance.ShowByType(screenType);
        }

    }
}



