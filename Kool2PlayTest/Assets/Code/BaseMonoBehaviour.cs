using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    //allow to register script in actions manager
    //for use custom update functions
    //Using  custom manager instead default update will 
    //increase performance, but it's looks strange 
    //https://blogs.unity3d.com/2015/12/23/1k-update-calls/
    protected void RegisterSelf()
    {
        ActionsManager.instance.RegisterScript(this);
    }
}
