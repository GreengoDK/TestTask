using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BaseWeaponShootingComponent))]
public class GunShootImplemetation : BaseMonoBehaviour, IShooting
{
    [SerializeField]
    GameObject ProjectileForSpawn, ProjectileSpawnPoint;
    [SerializeField]
    float ShootDelay = 0.5f;

    private bool inShoot = false;
    private BaseWeaponShootingComponent shootingComponent = null;

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
            if (ProjectileForSpawn && ProjectileSpawnPoint)
            {
                //Spawn projectile and setup it
                GameObject newProj = Instantiate(ProjectileForSpawn);
                newProj.transform.SetPositionAndRotation(ProjectileSpawnPoint.transform.position,
                                                         ProjectileSpawnPoint.transform.rotation
                                                        );
                newProj.GetComponent<GunProjectileComponent>().dmgInstigator = shootingComponent.dmgInstigator;

                //Clear memory
                newProj = null;
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
            if(MainCanvas.canvas)
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
