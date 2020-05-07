using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementController : BaseMonoBehaviour, IUpdateable
{
    [SerializeField]
    float BaseSpeed = 3f;

    //I think force movement it's funny :)
    private bool useForseMovement = true;
    private Rigidbody rb;

    private void Start()
    {
        base.RegisterSelf();
        rb = GetComponent<Rigidbody>();

        //setup enemy params from gameSettings
        useForseMovement = GameSettingsManager.instance.FunMovement;
        if (!useForseMovement)
        {
            BaseSpeed *= 5;
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }
    }

    public void CustomUpdate()
    {
        if (GameStateManager.instance.PlayerInstance)
        {
            //always look at player
            transform.LookAt(GameStateManager.instance.PlayerInstance.gameObject.transform);

            //always try to run forward
            if (useForseMovement)
                rb.AddForce(transform.forward * BaseSpeed);
            else
            {
                //need for control velocity value, without that
                //enemy will fly to space in one shot, cause MovePosition 
                //didnt affect velocity
                if (rb.velocity != Vector3.zero)
                    rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime);

                Vector3 movement = new Vector3(transform.forward.x, 0f, transform.forward.z) * BaseSpeed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }
        }
    }
    
    
}
