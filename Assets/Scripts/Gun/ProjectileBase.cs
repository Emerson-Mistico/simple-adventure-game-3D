using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Amunition Setup")]

    public float timeToDestroy = 2f;
    public int amountDamage = 1;
    public int amountCost = 5;
    public float speed = 50f;

    private void Awake()
    {
        //ItemManager.Instance.RemoveItemEnergy(amountCost);
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
