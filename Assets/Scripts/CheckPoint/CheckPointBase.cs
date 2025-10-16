using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    [Header("CheckPointSettings")]
    public MeshRenderer meshItemCheckPoint;
    public float intensity = 5.8f;
    public int checkPointID = 1;
    public bool showFoundMessage = true;

    private bool _isActivated = false;

    private void Awake()
    {
        TurnCheckPointOFF();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !_isActivated)
        {
            CheckCheckPoint();
        }
    }

    private void CheckCheckPoint()
    {
        SaveCheckPoint();
        TurnCheckPointON();
    }

    private void SaveCheckPoint()
    {
        /*
        if (checkPointID > PlayerPrefs.GetInt("LastCheckPoint"))
        {
            PlayerPrefs.SetInt("LastCheckPoint", checkPointID);
            _isActivated = true;            
        }
        */

        CheckPointManager.Instance.SaveCheckPoint(checkPointID, showFoundMessage);
        _isActivated = true;
    }

    [NaughtyAttributes.Button]
    private void TurnCheckPointON()
    {
        meshItemCheckPoint.material.SetColor("_EmissionColor", Color.white * intensity);
    }   

    [NaughtyAttributes.Button]
    private void TurnCheckPointOFF()
    {
        meshItemCheckPoint.material.SetColor("_EmissionColor", Color.grey * -intensity);
        _isActivated = false;
    }
}
