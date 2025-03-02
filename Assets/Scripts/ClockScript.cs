using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    
    void Start()
    {
        clock = GetComponent<TMPro.TextMeshProUGUI>();    
    }

    void Update()
    {
        float currentTime = GameState.gameTime24;  

        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60);

        clock.text = $"{hours:D2}:{minutes:D2}";
    }
}
