using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For now it can be used only for
//player, but, if needed, we can a bit rework it :)
[RequireComponent(typeof(PlayerInstanceComponent))]
public class SelectWeaponComponent : BaseMonoBehaviour, IPlayerInputImplementable
{
    //That will be used just for instantiate weapons
    //Sure, we can load weapons from resources,
    //Or make weapon manager, or etc.. but for test its doesnt necessary, I think
    [SerializeField]
    List<GameObject> AllWeapons;

    [SerializeField]
    GameObject HandPivot;
    [SerializeField]
    int SelectedWeapon = 0;
    [SerializeField]
    float SwapWeaponDelay = 0.5f;

    //Array of weapon instanses
    private GameObject[] weapons;
    //Does swap is going now?
    private bool inSwap = false;
    //next weapon index, which need to be activated
    private int weaponForSelect;

    

    //Initialization
    private void Start()
    {
        if (AllWeapons.Count == 0 || SelectedWeapon>=AllWeapons.Count)
        {
            throw new System.Exception("weapons not found (Check weapons list and selected weapon index)");
        }
        else
        {
            if (HandPivot)
            {
                //Instantiate weapons for use it later
                weapons = new GameObject[AllWeapons.Count];
                for (int i = 0; i < AllWeapons.Count; i++)
                {
                    weapons[i] = Instantiate(AllWeapons[i]);
                    weapons[i].transform.SetPositionAndRotation(HandPivot.transform.position, HandPivot.transform.rotation);
                    weapons[i].transform.SetParent(gameObject.transform);

                    if (i != SelectedWeapon)
                        weapons[i].SetActive(false);
                }
                weaponForSelect = SelectedWeapon;
                //Clear memory
                AllWeapons = null;
                //Register script for implement interfaces
                base.RegisterSelf();
            }
            else
            {
                throw new System.Exception("Hand pivot is null");
            }
            //First weapon select
            StartCoroutine(SwapWeapon());
        }
        
    }

    public void OnPlayerInput(InputMap playerInput)
    {
        if (!inSwap && weapons.Length>1)
        {            
            if (playerInput.mouseWheelUp)
                weaponForSelect = SelectedWeapon + 1 < weapons.Length ? SelectedWeapon + 1 : 0;
            if (playerInput.mouseWheelDown)
                weaponForSelect = SelectedWeapon - 1 < 0 ? weapons.Length - 1 : SelectedWeapon - 1;

            //Swap weapon if needed
            if(weaponForSelect != SelectedWeapon)
                StartCoroutine(SwapWeapon());
        }
    }
    //perform weapon swapping
    private IEnumerator SwapWeapon()
    {
        inSwap = true;
        for (int i = 0; i < 1; i++)
        {
            MainCanvas.canvas.PlayerMessage(SwapWeaponDelay, "Swapping Weapon");
            
            //Your anim can be here ;)

            yield return new WaitForSeconds(SwapWeaponDelay);

            //select weapon
            weapons[SelectedWeapon].SetActive(false);
            SelectedWeapon = weaponForSelect;
            weapons[SelectedWeapon].SetActive(true);
            inSwap = false;

            //Setup players active shooting component
            GameStateManager.instance.PlayerInstance.ShootingController.activeShootingComponent
                = weapons[SelectedWeapon].GetComponent<BaseWeaponShootingComponent>();
            //Update canvas
            MainCanvas.canvas.UpdateCanvas();
            
        }
    }    
}
