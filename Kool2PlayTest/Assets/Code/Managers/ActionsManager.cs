using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    //List of scripts which should be runned every frame
    private List<BaseMonoBehaviour> registeredScripts  = new List<BaseMonoBehaviour>();
    //temporary list of scripts, which used for validate registered scripts
    private List<BaseMonoBehaviour> tmpScripts;

    //Manager instance
    public static ActionsManager instance = null;

    private void Awake()
    {        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Updatable script can be registered using this
    //to avoid duplicates
    public bool RegisterScript(BaseMonoBehaviour script)
    {
        if (!registeredScripts.Contains(script))
        {
            registeredScripts.Add(script);
            return true;
        }
        else
        {
            throw new System.Exception("script "+ script.name + " cant be registered");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //validate registered scripts
        tmpScripts = new List<BaseMonoBehaviour>();
        foreach (var script in registeredScripts)
        {
            if (script)
            {
                tmpScripts.Add(script);
            }
        }
        registeredScripts = tmpScripts;
        tmpScripts = null;

        //run actions on registered scripts
        foreach (var script in registeredScripts)
        {
            if (script)
            {
                if (script is IPlayerInputImplementable)
                {
                    IPlayerInputImplementable myScript = (IPlayerInputImplementable)script;
                    myScript.OnPlayerInput(InputManager.instance.PlayerInput);
                }
                if (script is IUpdateable)
                {
                    IUpdateable myScript = (IUpdateable)script;
                    myScript.CustomUpdate();
                }
            }
        }

    }
}
