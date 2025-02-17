using System.Xml.Serialization;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject character;

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
            var coin = Instantiate(coinPrefab);
            coin.transform.position = character.transform.position + character.transform.forward * 3;
        }
        Debug.Log($"Event: {type}, payload: {payload}");
    }



    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinEvent, "Coin");
    }
}
