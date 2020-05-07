using UnityEngine;

//That component allow to apply damage 
//to gameObject
[RequireComponent(typeof(Rigidbody))]
public class DamageableComponent : BaseMonoBehaviour
{
    
    public float Health = 200;

    // required only dmg value, but also can get dmgInstigator and apply impulse
    //to damaged gameObject if it needed
    public void GetDamage(float dmg, GameObject dmgInstigator = null, Vector3 addImpulse = new Vector3())
    {
        Health -= dmg;
        if (addImpulse != new Vector3())
        {
            GetComponent<Rigidbody>().AddForce(addImpulse);
        }
        if (Health <= 0)
        {
            if (GameStateManager.instance.PlayerInstance)
            {
                if (dmgInstigator == GameStateManager.instance.PlayerInstance.gameObject)
                {
                    GameStateManager.instance.KilledEnemies++;
                    MainCanvas.canvas.UpdateCanvas();
                }
            }
            
            Destroy(gameObject);
        }
    }
}
