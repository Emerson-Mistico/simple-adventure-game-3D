using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Gun Configuration")]
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;

    public Coroutine _currentCoroutine;

    public KeyCode keyToShoot = KeyCode.C;    

    private void Update()
    {
        if (Input.GetKeyDown(keyToShoot))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyToShoot))
        {
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;

    }        
    
}
