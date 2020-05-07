using UnityEngine;

//That manager will contain game settings
//It should be able to load it from file and save to file
//But for now it just have 1 param :(
public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager instance = null;

    public bool FunMovement = true;

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
}
