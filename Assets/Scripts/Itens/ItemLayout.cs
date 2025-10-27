using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Itens
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiValue;
        
        private ItemSetup _currentSetup;
        public void Load(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currentSetup.icon;
        }

        private void Update()
        {
            uiValue.text = _currentSetup.SOInt.value.ToString();
        }

    }
}


