using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Amunition Setup")]
    public float timeToDestroy = 2f;
    public int amountDamage = 5;
    public int amountCost = 5;
    public float speed = 100f;

    [Header("To HIT")]
    public List<string> tagsToHit;

    private int _currentPlayerIsAlive;

    private void Awake()
    {
        //ItemManager.Instance.RemoveItemEnergy(amountCost);
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        _currentPlayerIsAlive = PlayerPrefs.GetInt("PlayerIsAlive");
        // transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        if(_currentPlayerIsAlive == 1)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Tiro atingiu: " + collision.transform.tag);

        foreach (var tagToCompare in tagsToHit)
        {
            // Debug.Log("Tag para dano: " + tagToCompare);

            if(collision.transform.tag == tagToCompare)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 damageDirection = collision.transform.position - transform.position;
                    damageDirection = -damageDirection.normalized;
                    damageDirection.y = 0;

                    damageable.Damage(amountDamage, damageDirection);
                    Destroy(gameObject);
                }

                break;
            }            
        }        
    }
}
