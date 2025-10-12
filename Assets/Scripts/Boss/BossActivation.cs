using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [Header("Boss Activation Setup")]
    public GameObject bossToActivate;

    public string tagToCompare = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if (tagToCompare == collision.transform.tag)
        {
            Debug.Log(collision.transform.tag + " entrou na área do BOSS!");
            bossToActivate.SetActive(true);
        }
    }
}
