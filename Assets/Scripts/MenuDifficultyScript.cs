using UnityEngine;
using UnityEngine.UI;

public class MenuDifficultyScript : MonoBehaviour
{
    private Toggle compassToggle;
    private Toggle clockToggle;
    private Toggle hintsToggle;
    private Toggle radarToggle;

    void Start()
    {
        Transform togglesLayout = transform.Find("Content/DifficultySection/TogglesLayout");

        compassToggle = togglesLayout.Find("Compass/CompassToggle").GetComponent<Toggle>();
        compassToggle.isOn = GameState.isCompassVisible;

        clockToggle = togglesLayout.Find("Clock/ClockToggle").GetComponent<Toggle>();
        clockToggle.isOn = GameState.isClockVisible;

        hintsToggle = togglesLayout.Find("Hints/HintsToggle").GetComponent<Toggle>();
        hintsToggle.isOn = GameState.isHintsVisible;

        radarToggle = togglesLayout.Find("Radar/RadarToggle").GetComponent<Toggle>();
        radarToggle.isOn = GameState.isRadarVisible;


        Transform slidersLayout = transform.Find("Content/DifficultySection/SlidersLayout");

        Slider spawnZoneSlider = slidersLayout.Find("SpawnZone/Slider").GetComponent<Slider>();
        spawnZoneSlider.value = Mathf.Sqrt(
            (GameState.coinSpawnRadius - GameState.coinSpawnRadiusMin) /
            (GameState.coinSpawnRadiusMax - GameState.coinSpawnRadiusMin));

        Slider spawnProbabilitySlider = slidersLayout.Find("SpawnProbability/Slider").GetComponent<Slider>();
        spawnProbabilitySlider.value = GameState.coinSpawnProbability;
    }

    void Update()
    {
        
    }

    public void OnSpawnProbabilitySliderChanged(float value)
    {
        GameState.coinSpawnProbability = value;
    }

    public void OnSpawnZoneSliderChanged(float value)
    {
        GameState.coinSpawnRadius = GameState.coinSpawnRadiusMin +
            (GameState.coinSpawnRadiusMax - GameState.coinSpawnRadiusMin) * value * value;
    }

    public void OnClockToggleChanged(bool value)
    {
        GameState.isClockVisible = value;
    }

    public void OnCompassToggleChanged(bool value)
    {
        GameState.isCompassVisible = value;
    }

    public void OnHintsToggleChanged(bool value)
    {
        GameState.isHintsVisible = value;
    }

    public void OnRadarToggleChanged(bool value)
    {
        GameState.isRadarVisible = value;
    }
}
