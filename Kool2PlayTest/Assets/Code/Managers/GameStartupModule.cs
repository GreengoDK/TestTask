using System.Collections.Generic;
using UnityEngine;

//That script needed to initialize main components,
//which will be used in game
public class GameStartupModule : MonoBehaviour
{
    [SerializeField]
    List<GameObject> MainComponents = new List<GameObject>();

    public static GameStartupModule instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var component in MainComponents)
        {
            Instantiate(component);
        }
    }
}
