using UnityEngine;

public class CompassScript : MonoBehaviour
{
    private Transform arrow;
    private Transform character;
    private Transform coin;
    private Transform content;

    void Start()
    {
        content = this.transform.Find("Content");    
        arrow = content.Find("Arrow");  
        
        character = GameObject.Find("Character").transform;    
        coin = GameObject.Find("Coin").transform;
        GameEventSystem.AddListener(OnGameEvent, "CoinSpawn", "Coin", nameof(GameState));
        OnGameEvent(nameof(GameState), null);
    }

    void Update()
    {
        if (coin == null) coin = GameObject.FindGameObjectWithTag("Coin").transform;

        Vector3 d = coin.position - character.position;
        Vector3 camFwd = Camera.main.transform.forward;     // character.forward;
        d.y = 0f;
        camFwd.y = 0f;
        float angle = Vector3.SignedAngle(camFwd, d, Vector3.down);
        if (content.gameObject.activeInHierarchy)
        {
            arrow.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void OnGameEvent(string type, object payload)
    {
        switch (type)
        {
            case "Coin":
                if (payload.Equals("Destroy"))
                {
                    coin = null;
                }
                break;
            case "CoinSpawn":
                coin = ((GameObject)payload).transform;
                break;
            case nameof(GameState):
                if (payload == null || nameof(GameState.isCompassVisible).Equals(payload))
                {
                    content.gameObject.SetActive(GameState.isCompassVisible);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnGameEvent, "CoinSpawn", "Coin", nameof(GameState));
    }
}
