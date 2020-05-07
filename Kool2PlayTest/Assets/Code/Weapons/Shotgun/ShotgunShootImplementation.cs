using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseWeaponShootingComponent))]
public class ShotgunShootImplementation : BaseMonoBehaviour, IShooting
{
    [HideInInspector]
    public List<GameObject> enemiesForApplyDamage = new List<GameObject>();

    //needed for get gameObjects which be affected by shotgun shoot
    [SerializeField]
    GameObject ShotgunDamageZone;

    [SerializeField]
    float ShootDelay = 1f, Damage = 140, ImpulseScale = 200;

    private BaseWeaponShootingComponent shootingComponent = null;
    private bool inShoot = false;

    //Needed for show shotgun effect
    private Renderer shootingArea;

    private void Start()
    {
        if (ShotgunDamageZone.GetComponent<ShotgunDamageZoneComponent>())
            ShotgunDamageZone.GetComponent<ShotgunDamageZoneComponent>().Init(this);

        if (ShotgunDamageZone.GetComponent<Renderer>())
            shootingArea = ShotgunDamageZone.GetComponent<Renderer>();

        //Init a Shooting component for that weapon
        shootingComponent = GetComponent<BaseWeaponShootingComponent>();
        shootingComponent.Init(this);
    }
    public void Shoot()
    {
        if (!inShoot)
        {
            inShoot = true;

            //show shoot effect
            if (shootingArea)
            {
                shootingArea.enabled = true;
                StartCoroutine(HideShootingArea());
            }

            //apply damage and impulse to enemies
            foreach (GameObject enemy in enemiesForApplyDamage)
            {
                if (enemy)
                {
                    if (enemy.GetComponent<DamageableComponent>())
                    {
                        //calculate impulse direction + add impulse length
                        Vector3 impulse = (enemy.transform.position - transform.position).normalized * ImpulseScale;
                        enemy.GetComponent<DamageableComponent>().GetDamage(Damage, shootingComponent.dmgInstigator, impulse);
                    }
                }
            }
            //reload
            StartCoroutine(ShootWithDelay());
        }
    }

    //show shoot effect with delay
    private IEnumerator HideShootingArea()
    {
        if (shootingArea)
        {
            for (int i = 0; i < 1; i++)
            {
                yield return new WaitForSeconds(0.05f);
            }
            shootingArea.enabled = false;
        }
    }

    //perform reload
    private IEnumerator ShootWithDelay()
    {
        //Show alert to player
        if (MainCanvas.canvas)
            MainCanvas.canvas.PlayerMessage(ShootDelay, "Reloading ammo");

        //Recalculate alive enemies in list to avoid null references 
        List<GameObject> tmpEnemies = new List<GameObject>();
        foreach (GameObject enemy in enemiesForApplyDamage)
        {
            if (enemy)
                tmpEnemies.Add(enemy);
        }
        enemiesForApplyDamage = tmpEnemies;

        //Clear memory
        tmpEnemies = null;        

        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(ShootDelay);
            inShoot = false;
        }

    }

    //Clear memory
    private void OnDisable()
    {
        enemiesForApplyDamage.Clear();
    }

    //perform reload after equip
    private void OnEnable()
    {
        inShoot = true;
        StartCoroutine(ShootWithDelay());
    }
}
