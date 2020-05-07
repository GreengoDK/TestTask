using System.Collections;
using UnityEngine;

//Manager for spawn enemies around player
//Will be good if I add option for pause spawning 
public class SpawnEnemyManager : MonoBehaviour
{
    public static SpawnEnemyManager instance = null;

    [SerializeField]
    GameObject EnemyForSpawn;
    
    private Vector3 spawnPoint = new Vector3();

    //We always will have same direction and can hold it in memory
    private Ray checkSpawnPos = new Ray();
    //We always will have same bounds and can hold it in memory
    private Bounds checkSpawnBounds;

    private void Awake()
    {
        if (EnemyForSpawn)
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                checkSpawnPos.direction = Vector3.down * 10f;
                checkSpawnBounds = EnemyForSpawn.GetComponent<Collider>().bounds;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            throw new System.Exception("Enemy for spawn is not used");
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemiesWithRandomDelay());
    }

    public bool SpawnEnemy()
    {
        if (GameStateManager.instance.PlayerInstance)
        {
            //Get random point around player position and try to raycast down for check ground
            //If raycast will successfully, check, does that place is free and spawn enemy
            spawnPoint = GameStateManager.instance.PlayerInstance.transform.position;
            spawnPoint.x += Random.Range(-30f, 30f);
            spawnPoint.z += Random.Range(-30f, 30f);
            checkSpawnPos.origin = spawnPoint;
            if (Physics.Raycast(checkSpawnPos))
            {
                if (Physics.OverlapBox(spawnPoint, checkSpawnBounds.extents).Length == 0)
                {
                    Instantiate(EnemyForSpawn).transform.position = spawnPoint;
                    return true;
                }
            }
        }
        return false;
    }

    //If cant spawn enemy - try again
    //If enemy was spawned - wait 3 secons before spawn next
    private IEnumerator SpawnEnemiesWithRandomDelay()
    {
        while (true)
        {
            if (SpawnEnemy())
                yield return new WaitForSeconds(3f);
            else
                yield return new WaitForEndOfFrame();
        }
    }
}
