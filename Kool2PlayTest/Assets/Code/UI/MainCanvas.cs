using System.Collections;
using UnityEngine.UI;
using UnityEngine;

//Thats a main canvas which should be single in the game
//We dont need to destroy it on load
public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    Text HPBarText, KilledEnemiesText, CurrentWeaponText, AlertText;

    public static MainCanvas canvas = null;

    //Allow us to correctly reshow alert 
    private Coroutine showingMessage;

    private void Start()
    {
        if (canvas == null)
        {
            DontDestroyOnLoad(this);
            canvas = this;
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    //function for update canvas manually
    public void UpdateCanvas()
    {        
        if (GameStateManager.instance.PlayerInstance)
        {            
            HPBarText.text = "Health: " + GameStateManager.instance.PlayerInstance.GetPlayerHealth.ToString();
            CurrentWeaponText.text = "Equipped: " + GameStateManager.instance.PlayerInstance.GetPlayerWeaponName;
            KilledEnemiesText.text = "Killed enemies: " + GameStateManager.instance.KilledEnemies.ToString();
        }
    }

    //function for set default HUD state after restart
    public void EnableHUD()
    {
        HPBarText.enabled = true;
        CurrentWeaponText.enabled = true;
        KilledEnemiesText.enabled = true;
    }

    //That will show custom "Game over" alert without
    //addition game info
    public void GameOver()
    {
        if (showingMessage != null)
            StopCoroutine(showingMessage);

        HPBarText.enabled = false;
        CurrentWeaponText.enabled = false;
        KilledEnemiesText.enabled = false;
        AlertText.alignment = TextAnchor.MiddleCenter;
        AlertText.text = "Game Over :'(";
        AlertText.enabled = true;
    }

    //Show temporary message for player
    public void PlayerMessage(float _time, string _message)
    {
        if (showingMessage != null)
            StopCoroutine(showingMessage);
        showingMessage = StartCoroutine(ShowMessage(_time, _message));
    }

    private IEnumerator ShowMessage(float _time, string _text)
    {
        AlertText.alignment = TextAnchor.UpperCenter;
        for (int i = 0; i < 1; i++)
        {
            AlertText.text = _text;
            AlertText.enabled = true;
            yield return new WaitForSeconds(_time);
            AlertText.enabled = false;
        }
    }
    
}
