using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Typper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float timeBetweenLetters = .1f;
    public string phrase;

    private void Awake()
    {
        textMesh.text = "";
    }

    [NaughtyAttributes.Button]
    public void StartType()
    {
        if (!EditorApplication.isPlaying) { return; }
        StartCoroutine(Type(phrase));
    }

    IEnumerator Type (string s) 
    {
        textMesh.text = "";
        foreach(char l in s.ToCharArray())
        {
            textMesh.text += l;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        
    }
  
}
