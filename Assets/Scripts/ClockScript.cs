using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    
    void Start()
    {
        clock = GetComponent<TMPro.TextMeshProUGUI>();
        GameEventSystem.AddListener(OnGameStateChangedEvent, nameof(GameState));
        OnGameStateChangedEvent(nameof(GameState), null);
    }

    void Update()
    {
        float currentTime = GameState.gameTime24;

        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60);

        if (!clock.enabled) return;

        clock.text = $"{hours:D2}:{minutes:D2}";
    }

    private void OnGameStateChangedEvent(string type, object payload)
    {
        if (payload == null || nameof(GameState.isClockVisible).Equals(payload))
        {
            clock.enabled = GameState.isClockVisible;
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnGameStateChangedEvent, nameof(GameState));
    }
}
