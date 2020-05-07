using UnityEngine;

//Input map which will be used in game
//Sure, we can split it later on pad and keyboard
//or something like that
public struct InputMap
{
    public bool left;
    public bool right;
    public bool back;
    public bool forward;
    public bool mouseLeftButton;
    public bool mouseWheelUp;
    public bool mouseWheelDown;
}

public class InputManager : MonoBehaviour
{

    public static InputManager instance = null;

    private InputMap playerInput = new InputMap();

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
    
    //Get actually input values
    void Update()
    {
            playerInput.forward = Input.GetKey(KeyCode.W);
            playerInput.back = Input.GetKey(KeyCode.S);
            playerInput.right = Input.GetKey(KeyCode.D);
            playerInput.left = Input.GetKey(KeyCode.A);
            playerInput.mouseLeftButton = Input.GetMouseButton(0);
            playerInput.mouseWheelUp = Input.mouseScrollDelta.y > 0;
            playerInput.mouseWheelDown = Input.mouseScrollDelta.y < 0;
       
    }

    //We dont need to change input map from the outside
    //but we need to get it
    public InputMap PlayerInput
    {
        get
        {
            return playerInput;
        }
    }
}
