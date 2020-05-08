using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunHitScanShotImplementation : MonoBehaviour, IShooting
{
    [SerializeField]
    GameObject ShootInitPosition;
    [SerializeField]
    float ShootDelay = 1f, Damage = 140, ImpulseScale = 200;

    private BaseWeaponShootingComponent shootingComponent = null;
    private bool inShoot = false;
    private Vector3 rayForward,rayRight;

    private void Start()
    {
        //Init a Shooting component for that weapon
        shootingComponent = GetComponent<BaseWeaponShootingComponent>();
        shootingComponent.Init(this);
    }
    public void Shoot()
    {
        if (!inShoot)
        {
            inShoot = true;
            Ray ray = new Ray();
            ray.origin = ShootInitPosition.transform.position;
            rayForward = ShootInitPosition.transform.forward;
            rayRight = ShootInitPosition.transform.right;
            float i = -0.5f;
            RaycastHit outHit;
            while (i < 0.5)
            {
                ray.direction = Vector3.SlerpUnclamped(rayForward, rayRight, i) * 5f;
                if (Physics.Raycast(ray, out outHit))
                {
                    if (outHit.collider.GetComponent<DamageableComponent>())
                    {
                        Vector3 impulse = (outHit.collider.transform.position - ray.origin).normalized * ImpulseScale;
                        outHit.collider.GetComponent<DamageableComponent>().GetDamage(Damage, shootingComponent.dmgInstigator, impulse);
                    }
                    Debug.DrawRay(ray.origin, Vector3.SlerpUnclamped(rayForward, rayRight, i) * 5f, Color.red, 30f);
                    i += 0.05f;
                }
            }
            
            //reload
            StartCoroutine(ShootWithDelay());
        }
    }

    
    //perform reload
    private IEnumerator ShootWithDelay()
    {
        //Show alert to player
        if (MainCanvas.canvas)
            MainCanvas.canvas.PlayerMessage(ShootDelay, "Reloading ammo");

        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(ShootDelay);
            inShoot = false;
        }

    }

    //perform reload after equip
    private void OnEnable()
    {
        inShoot = true;
        StartCoroutine(ShootWithDelay());
    }
}
