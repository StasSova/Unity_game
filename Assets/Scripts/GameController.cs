using System.Xml.Serialization;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject character;

    private float minCoinCharacterDistance = 10.0f;
    private float maxCoinCharacterDistance = 30.0f;
    private float spawnOffset = 50.0f;
    private float minCoinSpawnHeight = 0.8f;
    private float maxCoinSpawnHeight = 4.0f;

    void Start()
    {
        GameEventSystem.AddListener(OnCoinEvent, "Coin");
    }

    private void OnCoinEvent(string type, object payload)
    {
        if (payload.Equals("Destroy"))
        {
            SpawnCoin();
            SpawnCoin();
        }
        //Debug.Log($"Event: {type}, payload: {payload}");
    }

    private void SpawnCoin()
    {
        /* Умови створення нової 
            - не ближче за 10 (minCoinCharacterDistance) від персонажу
            - не далі за 30 (maxCoinCharacterDistance) від персонажу
            - розміщення випадкове - як попереду, так і позаду персонажу
            - не ближче за 50 (spawnOffset) від краю світу (мапи)
            - по висоті: випадково, але у досяжності персонажа Срозмір +
            У новій позиціЇ монета не пертинєс з нши коле
            */

        Vector3 coinDelta;
        Vector3 newPosition;
        int cnt = 0;
        do
        {
            coinDelta = new Vector3(
            Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance),
            0,
            Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance)
            );
            newPosition = character.transform.position + coinDelta;
            cnt += 1;
        }
        while (cnt < 100 && (
            coinDelta.magnitude < minCoinCharacterDistance ||
            coinDelta.magnitude > maxCoinCharacterDistance ||
            newPosition.x < spawnOffset ||
            newPosition.z < spawnOffset ||
            newPosition.x > 1000 - spawnOffset ||
            newPosition.z > 1000 - spawnOffset
        ));

        float terrainHeight = Terrain.activeTerrain.SampleHeight(newPosition);
        newPosition.y = terrainHeight + Random.Range(-minCoinSpawnHeight, maxCoinSpawnHeight);

        var coin = GameObject.Instantiate(coinPrefab);
        coin.transform.position = newPosition;
        GameEventSystem.EmitEvent("CoinSpawn", coin);
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinEvent, "Coin");
    }
}
