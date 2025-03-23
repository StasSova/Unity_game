using UnityEngine;
using UnityEngine.Device;

public class TopBarScript : MonoBehaviour
{
    private GameObject background;
    void Start()
    {
        background = transform.Find("Background").gameObject;
        GameEventSystem.AddListener(OnGameEvent, nameof(GameState));
        OnGameEvent(nameof(GameState), null);
    }

    private void OnGameEvent(string type, object payload)
    {
        background.SetActive(GameState.isClockVisible || GameState.isCompassVisible);
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnGameEvent, nameof(GameState));
    }
}
