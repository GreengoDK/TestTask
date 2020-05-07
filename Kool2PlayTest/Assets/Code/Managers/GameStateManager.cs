using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//That manager allow to control game state
//Contains actually player, functions for reload scene etc...
//Will be used for change something in game..
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance = null;

    //We dont want to setup it in inspector, but we need to get access
    [HideInInspector]
    public PlayerInstanceComponent PlayerInstance;

    private int killedEnemies=0;

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

    public void RegisterPlayer(PlayerInstanceComponent playerInstance)
    {
        PlayerInstance = playerInstance;

        //Enable player default HUD
        //Later we can put some tutorial here if 
        //ist new game
        MainCanvas.canvas.EnableHUD();
    }

    public void GameOver()
    {
        //Its a final, but u can try again!
        MainCanvas.canvas.GameOver();
        StartCoroutine(Restart());
        PlayerInstance = null;
    }

    //Its fine when somebody cant change value directly
    public int KilledEnemies
    {
        get
        {
            return killedEnemies;
        }
        set
        {
            killedEnemies++;
        }
    }

    //perform restart with delay
    private IEnumerator Restart()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
