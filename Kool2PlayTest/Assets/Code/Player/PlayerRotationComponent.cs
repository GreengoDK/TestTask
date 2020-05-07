using UnityEngine;

public class PlayerRotationComponent : BaseMonoBehaviour, IUpdateable
{    
    Quaternion newRotation, targetRotation;
    Vector3 targetPoint;
    Ray ray;

    void Start()
    {
        base.RegisterSelf();
        //we dont need to change x and z params
        newRotation = transform.rotation;
    }

    public void CustomUpdate()
    {
        //Get mouse position on viewport, I know, its not optimal way, but
        //Camera.main.ScreenToWorldPoint(Input.mousePosition) dont working as it should (Sure, its my fail)
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Getting direction
        targetPoint = ray.GetPoint((Camera.main.transform.position - transform.position).magnitude);        
        targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        //Setup new rotation
        newRotation.y = targetRotation.y;
        newRotation.w = targetRotation.w;
        transform.rotation = newRotation;
    }

    

}
