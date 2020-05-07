using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//That component should be on a weapon for implement guns specific shooting
public class BaseWeaponShootingComponent : BaseMonoBehaviour
{
    //Write a custom weapon name here!
    public string WeaponName;

    //dmgInstigator can be null or something,
    //if we'll use it on enemy
    [HideInInspector]
    public GameObject dmgInstigator = null;
    
    private IShooting shootImplementation;

    //Component should be initialized for using
    public void Init(IShooting myIShooting)
    {
        shootImplementation = myIShooting;
    }   

    public void Shoot()
    {
        if (shootImplementation != null)
        {
            shootImplementation.Shoot();
        }
        else
        {
            throw new System.Exception("Shoot impl is null");
        }
    }
}
