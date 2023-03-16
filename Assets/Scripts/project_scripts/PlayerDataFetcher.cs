using UnityEngine;

public class PlayerDataFetcher : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    string fetchedEnemyName;
    void Start()
    {
        
        fetchedEnemyName = enemy.GetComponent<EnemyData>().EnemyName;
       // Debug.Log(fetchedEnemyName);
    }

    void Update()
    {

    }
}
