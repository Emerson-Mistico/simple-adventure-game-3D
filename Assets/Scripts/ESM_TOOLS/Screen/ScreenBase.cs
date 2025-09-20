using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;

namespace Screens
{

    public enum ScreenType
    { 
        Panel,
        Profile,
        Settings,
        Win,
        Failled
    }

    public class ScreenBase: MonoBehaviour
    {
        public ScreenType ScreenType;

        public List<Transform> listOfObjects;
        public List<Typper> listOfPhrases;

        //  public bool startHide = false;
        // public Image uiImageBackground;
        public GameObject uiPanel;

        public float delayBetweenObjects = .05f;
        public float animationDuration = .3f;

        private void Start()
        {
            /*if (startHide)
            {
                HideObjects();
            }*/
        }

        [Button]      
        public virtual void Force()
        {
            if (!EditorApplication.isPlaying) { return; }
            ForceShowObjects();            
        }

        [Button]      
        public virtual void Show()
        {
            if (!EditorApplication.isPlaying) { return; }
            uiPanel.SetActive(true);
            ShowObjects();            
        }
        
        [Button]
        public virtual void Hide()
        {
            if (!EditorApplication.isPlaying) { return; }
            uiPanel.SetActive(false);
            HideObjects();
            
        }
        private void HideObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(false));
        }
        private void ShowObjects()
        {

            for (int i = 0; i < listOfObjects.Count; i++) 
            {
                var obj = listOfObjects[i];

                obj.gameObject.SetActive(true);
                obj.DOScale(0, animationDuration).From().SetDelay(i * delayBetweenObjects);
            }

            Invoke(nameof(StartType), delayBetweenObjects * listOfObjects.Count);

        }
        private void StartType()
        {
            
            for (int i = 0; i < listOfPhrases.Count; i++) 
            {
                listOfPhrases[i].StartType();                                
            }
        }
        private void ForceShowObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(true));            
        }
        
    }

}
