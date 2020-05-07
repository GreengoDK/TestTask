using UnityEngine;

//needed for get gameObjects which be affected by shotgun shoot
public class ShotgunDamageZoneComponent : BaseMonoBehaviour
{
    private ShotgunShootImplementation shotgunShootScript;

    public void Init(ShotgunShootImplementation _shotgunShootScript)
    {
        shotgunShootScript = _shotgunShootScript;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (shotgunShootScript && other.GetComponent<DamageableComponent>())
        {
            if (!shotgunShootScript.enemiesForApplyDamage.Contains(other.gameObject))
                shotgunShootScript.enemiesForApplyDamage.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (shotgunShootScript && other.GetComponent<DamageableComponent>())
        {
            if(shotgunShootScript.enemiesForApplyDamage.Contains(other.gameObject))
                shotgunShootScript.enemiesForApplyDamage.Remove(other.gameObject);
        }
    }


}
