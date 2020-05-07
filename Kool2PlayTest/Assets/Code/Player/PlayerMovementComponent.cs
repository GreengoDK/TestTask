using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementComponent : BaseMonoBehaviour, IPlayerInputImplementable
{
    [SerializeField]
    int BaseSpeed = 15;

    //I think force movement it's funny :)
    private bool useForseMovement = true;
    private Rigidbody rb;
    private Vector3 movement;

    // Use this for initialization
    private void Start ()
    {
        base.RegisterSelf();
        rb = GetComponent<Rigidbody>();

        //setup player params from gameSettings
        useForseMovement = GameSettingsManager.instance.FunMovement;
        if (!useForseMovement)
        {
            BaseSpeed *= 3;
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }
            
	}

    public void OnPlayerInput(InputMap playerInput)
    {
        //Nedeed for disable velocity
        if (!useForseMovement)
            rb.velocity = Vector3.zero;

        if (playerInput.forward)
        {
            if (useForseMovement)
                rb.AddForce(transform.forward * BaseSpeed, ForceMode.Acceleration);
            else
            {
                movement = new Vector3(transform.forward.x, 0f, transform.forward.z)*BaseSpeed*Time.deltaTime;
                rb.MovePosition(transform.position+movement);
            }
        }
        else if (playerInput.back)
        {
            if (useForseMovement)
                rb.AddForce(-1 * transform.forward * BaseSpeed, ForceMode.Acceleration);
            else
            {
                movement = new Vector3(transform.forward.x, 0f, transform.forward.z) * BaseSpeed * Time.deltaTime;
                rb.MovePosition(transform.position - movement*0.5f);
            }
        }
    }
}
