using System.Xml.Serialization;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject character;

    private float minCoinCharacterDistance;
    private float maxCoinCharacterDistance;
    private float spawnOffset = 50.0f;
    private float minCoinSpawnHeight = 0.8f;
    private float maxCoinSpawnHeight = 4.0f;
    private string[] listenableEvents = { "Coin", nameof(GameState) };

    void Start()
    {
        GameEventSystem.AddListener(OnGameEvent, listenableEvents);
        OnGameEvent(nameof(GameState), null);
    }

    private void OnGameEvent(string type, object payload)
    {
        switch (type)
        {
            case "Coin":
                if (payload.Equals("Destroy"))
                {
                    int coinsLimit = 1 + (int) (GameState.coinSpawnProbability * GameState.coinSpawnProbabilityFactor);
                    if (GameObject.FindGameObjectsWithTag("Coin").Length <= coinsLimit)
                    {
                        SpawnCoin();
                    }
                    for(int i = 0; i <= coinsLimit; i++)
                    {
                        if (Random.value < 1.0f / (coinsLimit + 1)) SpawnCoin();
                    }
                }
                break;
            case nameof(GameState):
                minCoinCharacterDistance = GameState.coinSpawnRadius;
                maxCoinCharacterDistance = GameState.coinSpawnRadius + GameState.coinSpawnZoneRatio;
                Debug.Log(GameState.coinSpawnRadius);
                break;
        }
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
        GameEventSystem.RemoveListener(OnGameEvent, listenableEvents);
    }
}
