using UnityEngine;

public class GunProjectileComponent : MonoBehaviour
{
    [SerializeField]
    float Damage, Speed;

    [HideInInspector]
    public GameObject dmgInstigator = null;

    void FixedUpdate()
    {
        transform.Translate(0,0, Speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Trigger")
        {
            if (other.GetComponent<DamageableComponent>())
            {
                other.GetComponent<DamageableComponent>().GetDamage(Damage, dmgInstigator);
            }
            Destroy(gameObject);
        }
    }
}
