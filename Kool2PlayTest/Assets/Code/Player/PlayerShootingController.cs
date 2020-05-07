using UnityEngine;

//This component allows us to fire from different weapons
public class PlayerShootingController : BaseMonoBehaviour, IPlayerInputImplementable
{
    //We dont need to setup it in inspector
    //But we need to get and set it from outside
    [HideInInspector]
    public BaseWeaponShootingComponent activeShootingComponent;

    private void Start()
    {
        base.RegisterSelf();
    }

    public void OnPlayerInput(InputMap playerInput)
    {
        if (playerInput.mouseLeftButton)
        {
            if (activeShootingComponent)
            {
                //Setup dmgInstigator to component if needed
                if (activeShootingComponent.dmgInstigator != GameStateManager.instance.PlayerInstance.gameObject)
                    activeShootingComponent.dmgInstigator = GameStateManager.instance.PlayerInstance.gameObject;
                activeShootingComponent.Shoot();
            }
        }
    }
}
