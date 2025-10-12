using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class GunBase : MonoBehaviour
{
    [Header("Gun Configuration")]
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;
    public float amunitionMultipliyer = 1f;

    public GameObject tartgetToShoot = null;

    public Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.transform.localScale = Vector3.one * amunitionMultipliyer;
        projectile.speed = speed;
        if (tartgetToShoot != null)
        {
            projectile.transform.LookAt(tartgetToShoot.transform.position);
        }
    }    
    
    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }
    public void StopShoot()
    {
        if(_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

    }
    
}
