using UnityEngine;

public class HintsScript : MonoBehaviour
{
    private Transform coin;
    private GameObject leftArrow;
    private GameObject rightArrow;

    void Start()
    {
        coin = GameObject.Find("Coin").transform;
        leftArrow = transform.Find("LeftHint").gameObject;
        rightArrow = transform.Find("RightHint").gameObject;
        GameEventSystem.AddListener(OnCoinSpawnEvent, "CoinSpawn");
        GameEventSystem.AddListener(OnCoinEvent, "Coin");
    }
    
    void Update()
    {
        if (coin == null) coin = GameObject.FindGameObjectWithTag("Coin").transform;

        Vector3 wvL = Camera.main.WorldToViewportPoint(coin.position - Camera.main.transform.right * 0.75f);    // 0..1
        Vector3 wvR = Camera.main.WorldToViewportPoint(coin.position + Camera.main.transform.right * 0.75f);    // 0..1


        if (wvL.z > 0 && wvR.z > 0)
        {
            if(wvR.x< 0)
            {
                leftArrow.SetActive(GameState.isHintsVisible);
                rightArrow.SetActive(false);
            }
            else if (wvL.x > 1f)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(GameState.isHintsVisible);
            }
            else
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
            }
        }
        else
        {
            float a = Vector3.SignedAngle(
                Camera.main.transform.forward,
                coin.position - Camera.main.transform.position,
                Vector3.down
            );
            if (a < 0)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(GameState.isHintsVisible);
            }
            else
            {
                leftArrow.SetActive(GameState.isHintsVisible);
                rightArrow.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log(a);
            }
        }
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
