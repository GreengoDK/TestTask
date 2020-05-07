using UnityEngine;

//Interface for hold all needed player info 
//instead getting different components
//in another scripts
//Also its contain player requirement components
//for prevent runtime errors
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DamageableComponent))]
[RequireComponent(typeof(PlayerRotationComponent))]
[RequireComponent(typeof(PlayerMovementComponent))]
[RequireComponent(typeof(SelectWeaponComponent))]
[RequireComponent(typeof(PlayerShootingController))]
public class PlayerInstanceComponent : BaseMonoBehaviour
{
    public static PlayerInstanceComponent instance = null;
    
    private PlayerShootingController shootingController;
    private DamageableComponent playerDamageableComponent;

    private void Start()
    {
        GameStateManager.instance.RegisterPlayer(this);
        playerDamageableComponent = GetComponent<DamageableComponent>();
        shootingController = GetComponent<PlayerShootingController>();
    }

    private void OnDestroy()
    {
        if(GameStateManager.instance)
            GameStateManager.instance.GameOver();
    }

    //We dont need to change all fields below,from outside,
    //but we need to get access to it
    public float GetPlayerHealth
    {
        get
        {
            if(playerDamageableComponent)
                return playerDamageableComponent.Health;
            return 0f;
        }
    }
      
    public PlayerShootingController ShootingController
    {
        get
        {
            return shootingController;
        }
    }
    public DamageableComponent PlayerDamageableComponent
    {
        get
        {
            return playerDamageableComponent;
        }
    }

    public string GetPlayerWeaponName
    {
        get
        {
            if (ShootingController)
            {
                if(ShootingController.activeShootingComponent)
                    return ShootingController.activeShootingComponent.WeaponName;
            }
            return "";
        }
    }
}
