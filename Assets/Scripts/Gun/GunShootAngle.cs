using UnityEngine;

public class GunShootAngle : GunShootLimit
{

    public int amountPershoot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        int multiplicador = 0;
        
        for(int i=0; i < amountPershoot; i++)
        {

            if(i%2 == 0)
            {
                multiplicador++;
            }

            var projectile = Instantiate(prefabProjectile, positionToShoot);

            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * multiplicador;
            
            projectile.speed = speed;
            projectile.transform.parent = null;
        }       
        
    }

}
