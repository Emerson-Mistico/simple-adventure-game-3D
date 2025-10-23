using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [Header("Boss Activation Setup")]
    public GameObject bossToActivate;

    public string tagToCompare = "Player";

    private void Awake()
    {
        bossToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (tagToCompare == collision.transform.tag && PlayerPrefs.GetInt("Boss01Alive") == 1)
        {
            // Debug.Log(collision.transform.tag + " entrou na área do BOSS!");
            PlayerPrefs.SetInt("BossFightState", 1);
            bossToActivate.SetActive(true);
        }
    }
}
