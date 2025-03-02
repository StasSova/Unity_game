using UnityEngine;

public class CompassScript : MonoBehaviour
{
    private Transform arrow;
    private Transform character;
    private Transform coin;

    void Start()
    {
        arrow = transform.Find("Arrow");    
        character = GameObject.Find("Character").transform;    
        coin = GameObject.Find("Coin").transform;
        GameEventSystem.AddListener(OnCoinSpawnEvent, "CoinSpawn");
        GameEventSystem.AddListener(OnCoinEvent, "Coin");
    }

    void Update()
    {
        if (coin == null) return;

        Vector3 d = coin.position - character.position;
        Vector3 camFwd = character.forward; // Camera.main.transform.forward;
        d.y = 0f;
        camFwd.y = 0f;
        float angle = Vector3.SignedAngle(camFwd, d, Vector3.down);
        arrow.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnCoinSpawnEvent(string type, object payload)
    {
        coin = ((GameObject)payload).transform;
    }

    private void OnCoinEvent(string type, object payload)
    {
        if (payload.Equals("Destroy"))
        {
            coin = null;
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinSpawnEvent, "CoinSpawn");
        GameEventSystem.RemoveListener(OnCoinEvent, "Coin");
    }
}
