using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectileComponent : MonoBehaviour
{
    [HideInInspector]
    public GameObject dmgInstigator = null;
    [HideInInspector]
    public Vector3 endPoint = new Vector3();

    private bool isExploded = false;
    private bool useNavigation = false;

    //Needed to perform explosion if it wasnt hitted by something
    Coroutine autoExplosion;

    //Affected colliders
    Collider[] collidersForApplyDamage;

    private void Start()
    {
        //Wait before enable navigation and start timer for auto destroy
        StartCoroutine(ChangeMovement());
        autoExplosion = StartCoroutine(ExplodeByTimer());
    }
    void FixedUpdate()
    {
        if (!isExploded)
        {
            //rotate to target and fly forward
            if (useNavigation)
                transform.LookAt(endPoint);
            transform.Translate(0, 0, Time.deltaTime * 10f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isExploded)
            Explode();       
    }

    //perform explosion
    private void Explode()
    {
        if (autoExplosion != null)
            StopCoroutine(autoExplosion);

        isExploded = true;

        //hide projectile
        if(GetComponent<Renderer>())
            GetComponent<Renderer>().enabled = false;

        //get all gameObjects in explosion location
        collidersForApplyDamage = Physics.OverlapSphere(transform.position, 5f);
        
        //turn off constraints and apply force to gameObjects
        if (collidersForApplyDamage.Length > 0)
        {
            foreach (var collider in collidersForApplyDamage)
            {
                if (collider.GetComponent<DamageableComponent>() && !collider.GetComponent<PlayerInstanceComponent>())
                {
                    collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    collider.GetComponent<Rigidbody>().AddForce(0f, 700f, 0f, ForceMode.Acceleration);
                }
            }
        }

        //start to kill them all
        StartCoroutine(ApplyDamage());
    }

    //enable navigation
    private IEnumerator ChangeMovement()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1f);
            useNavigation = true;
        }
    }

    //waiting for wow effect and apply damage after it
    private IEnumerator ApplyDamage()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(2f);
            foreach (var collider in collidersForApplyDamage)
            {
                if (collider.GetComponent<DamageableComponent>() && !collider.GetComponent<PlayerInstanceComponent>())
                {
                    collider.GetComponent<DamageableComponent>().GetDamage(10000f,dmgInstigator);
                }
            }
            Destroy(gameObject);
        }
    }

    //perform explode by end of time if it dont be canceled
    private IEnumerator ExplodeByTimer()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(7f);
            Explode();
        }
    }
}
