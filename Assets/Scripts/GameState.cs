
using UnityEngine;

public class GameState
{
    public static float gameTime24 { get; set; } = 12.0f;

    #region coinSpawnRadius (spawn zone)
    public const float coinSpawnRadiusMin = 10.0f;
    public const float coinSpawnRadiusMax = 100.0f;
    public const float coinSpawnZoneRatio = 1.5f;

    private static float _coinSpawnRadius = 30.0f;
    public static float coinSpawnRadius {
        get => _coinSpawnRadius;
        set
        {
            _coinSpawnRadius = value;
            GameEventSystem.EmitEvent(nameof(GameState), nameof(coinSpawnRadius));
        } 
    }
    #endregion

    #region coinSpawnProbability
    public const float coinSpawnProbabilityFactor = 10f;
    private static float _coinSpawnProbability  = 0.3f;
    public static float coinSpawnProbability {
        get => _coinSpawnProbability;
        set
        {
            _coinSpawnProbability = value;
            GameEventSystem.EmitEvent(nameof(GameState), nameof(coinSpawnProbability));
        }
    }
    #endregion

    public static float radarRadius             { get; set; } = 30.0f;
    public static float staminaLimit            { get; set; } = 10.0f;


    #region isCompassVisible
    public static bool _isCompassVisible = true;
    public static bool isCompassVisible
    {
        get => _isCompassVisible;
        set
        {
            if (value != _isCompassVisible)
            {
                _isCompassVisible = value;
                GameEventSystem.EmitEvent(nameof(GameState), nameof(isCompassVisible));
            }
        }
    }
    #endregion

    #region isRadarVisible
    public static bool _isRadarVisible = true;
    public static bool isRadarVisible
    {
        get => _isRadarVisible;
        set
        {
            if (value != _isRadarVisible)
            {
                _isRadarVisible = value;
                GameEventSystem.EmitEvent(nameof(GameState), nameof(isRadarVisible));
            }
        }
    }
    #endregion

    #region isHintsVisible
    public static bool _isHintsVisible = true;
    public static bool isHintsVisible
    {
        get => _isHintsVisible;
        set
        {
            if (value != _isHintsVisible)
            {
                _isHintsVisible = value;
                GameEventSystem.EmitEvent(nameof(GameState), nameof(isHintsVisible));
            }
        }
    }
    #endregion

    #region isClockVisible
    private static bool _isClockVisible = true;
    public static bool isClockVisible
    {
        get => _isClockVisible;
        set
        {
            if (value != _isClockVisible)
            {
                _isClockVisible = value;
                GameEventSystem.EmitEvent(nameof(GameState), nameof(isClockVisible));
            }
        }
    }
    #endregion
}
