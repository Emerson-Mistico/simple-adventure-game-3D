using Enemy;
using UnityEngine;

public class EnemyActivationArea : MonoBehaviour
{
    [Header("Enemy Activation Setup")]
    public EnemyShoot enemyToActivate;
    public string tagToCompare = "Player";

    private void Awake()
    {
        if (enemyToActivate != null && enemyToActivate.gunBase != null)
        {
            enemyToActivate.gunBase.StopShoot();
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("PlayerIsAlive") == 0)
        {
            enemyToActivate.gunBase.StopShoot();
        }
    }

    private void OnTriggerEnter(Collider collisionIN)
    {
        if (collisionIN.CompareTag(tagToCompare))
        {
            if (enemyToActivate != null && enemyToActivate.gunBase != null)
            {
                if (enemyToActivate.IsAlive())
                {
                    enemyToActivate.gunBase.StartShoot();
                    // Debug.Log("Inimigo começou a atirar!");
                }
                else
                {
                    // Debug.Log("Inimigo está morto, não pode atirar!");
                }
            }
        }
    }    
}
