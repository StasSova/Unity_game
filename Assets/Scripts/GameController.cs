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
        if (!character)
        {
            Debug.LogError("Character is not assigned!");
            return;
        }

        Debug.Log($"Character name: {character.name}, position: {character.transform.position}");

        if (payload.Equals("Destroy"))
        {
            var coin = GameObject.Instantiate(coinPrefab);





            /* Умови створення нової 
            - не ближче за 10 (minCoinCharacterDistance) від персонажу
            - не далі за 30 (maxCoinCharacterDistance) від персонажу
            - розміщення випадкове - як попереду, так і позаду персонажу
            - не ближче за 50 (spawnOffset) від краю світу (мапи)
            - по висоті: випадково, але у досяжності персонажа Срозмір +
            У новій позиціЇ монета не пертинєс з нши коле
            */

            Vector3 coinDelta;
            int cnt = 0;
            do
            {
                coinDelta = new Vector3(
                Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance),
                0,
                Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance)
                );
                cnt += 1;
            }
            while (cnt < 100 && (
                coinDelta.magnitude < minCoinCharacterDistance ||
                coinDelta.magnitude > maxCoinCharacterDistance
            ));
            Vector3 newPosition = character.transform.position + coinDelta;
            float terrainHeight = Terrain.activeTerrain.SampleHeight(newPosition);
            newPosition.y = terrainHeight + Random.Range(-minCoinSpawnHeight, maxCoinSpawnHeight);
            
            coin.transform.position = newPosition;
        }
        Debug.Log($"Event: {type}, payload: {payload}");
    }



    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinEvent, "Coin");
    }
}
