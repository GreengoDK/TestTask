using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class EnemyAttackComponent : BaseMonoBehaviour
{
    [SerializeField]
    float AttackDelay = 2f, Damage = 10f;

    //needed for disable attacking coroutine if 
    //player cant be damaged by that gameObject 
    Coroutine attackPlayer;

    
    //start attack player
    private void OnTriggerEnter(Collider other)
    {
        if (GameStateManager.instance.PlayerInstance)
        {
            if (other.gameObject == GameStateManager.instance.PlayerInstance.gameObject)
            {
                attackPlayer = StartCoroutine(tryToAttack());
            }
        }
    }

    //end attack player
    private void OnTriggerExit(Collider other)
    {
        if (GameStateManager.instance.PlayerInstance)
        {
            if (other.gameObject == GameStateManager.instance.PlayerInstance.gameObject)
            {
                if (attackPlayer != null)
                    StopCoroutine(attackPlayer);
            }
        }
    }

    //perform attack player
    private IEnumerator tryToAttack()
    {
        while (true)
        {
            if (GameStateManager.instance.PlayerInstance)
            {
                GameStateManager.instance.PlayerInstance.PlayerDamageableComponent.GetDamage(Damage);
                MainCanvas.canvas.UpdateCanvas();
                yield return new WaitForSeconds(AttackDelay);
            }
            else
                break;
        }
    }
}
