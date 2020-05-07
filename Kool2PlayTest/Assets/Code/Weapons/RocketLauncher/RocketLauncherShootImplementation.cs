using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeaponShootingComponent))]
public class RocketLauncherShootImplementation : BaseMonoBehaviour, IShooting
{
    [SerializeField]
    GameObject ProjectileForSpawn, ProjectileSpawnPoint;
    [SerializeField]
    float ShootDelay = 0.5f;

    private bool inShoot = false;
    private BaseWeaponShootingComponent shootingComponent = null;
    private Ray testEndPoint;

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
            if (ProjectileForSpawn && ProjectileSpawnPoint)
            {
                //calculate projectile end point
                testEndPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

                //increase ray length to be sure that he will reach the ground
                testEndPoint.direction *= 10f;

                RaycastHit hit;
                if (Physics.Raycast(testEndPoint, out hit))
                {
                    inShoot = true;

                    //spawn projectile
                    GameObject newProj = Instantiate(ProjectileForSpawn);
                    newProj.transform.SetPositionAndRotation(ProjectileSpawnPoint.transform.position,
                                                             ProjectileSpawnPoint.transform.rotation
                                                            );
                    //setup projectile
                    newProj.GetComponent<RocketProjectileComponent>().dmgInstigator = shootingComponent.dmgInstigator;
                    newProj.GetComponent<RocketProjectileComponent>().endPoint = hit.point;
                    newProj.GetComponent<RocketProjectileComponent>().endPoint.y -= 5f;

                    //Clear memory
                    newProj = null;
                }
                else
                    return; //Dont perform shoot if end point not found
            }
            //reload
            StartCoroutine(ShootWithDelay());

        }
    }

    //perform reload
    private IEnumerator ShootWithDelay()
    {
        for (int i = 0; i < 1; i++)
        {
            MainCanvas.canvas.PlayerMessage(ShootDelay, "Reloading ammo");
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
