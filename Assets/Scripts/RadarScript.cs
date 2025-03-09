using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadarScript : MonoBehaviour
{
    private Image samplePoint;
    private List<RadarPoint> points = new();
    private Image screen;

    private Transform character;
    private Transform coin;

    [SerializeField]
    private float activeScreenRadiusRatio = 0.8f;
    [SerializeField]
    private float maxRadarDistance = 30f;

    void Start()
    {
        character = GameObject.Find("Character").transform;
        coin = GameObject.Find("Coin").transform;
        screen = transform.Find("Screen").gameObject.GetComponent<Image>();
        samplePoint = transform.Find("Screen/Point").gameObject.GetComponent<Image>();
        samplePoint.gameObject.SetActive(false);

        GameEventSystem.AddListener(OnCoinSpawnEvent, "CoinSpawn");
        GameEventSystem.AddListener(OnCoinEvent, "CoinDestroying");

        var point2 = GameObject.Instantiate(samplePoint);
        point2.transform.parent = screen.gameObject.transform;
        point2.rectTransform.localPosition = new Vector3(50, 50);
        point2.gameObject.SetActive(true);

        points.Add(new() { coin = coin, point = point2 });
    }

    private void Update()
    {
        if (points.Count > 0)
        {
            float screenRadius = screen.rectTransform.rect.width * activeScreenRadiusRatio / 2;

            foreach (RadarPoint radarPoint in points)
            {
                Vector3 d = radarPoint.coin.position - character.position;
                Vector3 camFwd = Camera.main.transform.forward; //character.forward;  
                d.y = 0f;
                camFwd.y = 0f;
                float angle = Vector3.SignedAngle(camFwd, d, Vector3.down);

                float r = d.magnitude / maxRadarDistance * screenRadius;
                if (r < screenRadius)
                {
                    if (!radarPoint.point.gameObject.activeInHierarchy)
                    {
                        radarPoint.point.gameObject.SetActive(true);
                    }
                    radarPoint.point.rectTransform.localPosition = new Vector3(
                        -r * Mathf.Sin(angle * Mathf.Deg2Rad),
                        r * Mathf.Cos(angle * Mathf.Deg2Rad)
                    );
                }
                else
                {
                    radarPoint.point.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnCoinSpawnEvent(string type, object payload)
    {
        if (payload is GameObject newCoin)
        {
            points.Add(new()
            {
                coin = newCoin.transform,
                point = GameObject.Instantiate(samplePoint, screen.gameObject.transform)
            });
        }
       
    }

    private void OnCoinEvent(string type, object payload)
    {
        if (payload is GameObject oldCoin)
        {
            RadarPoint toDelete = null;

            foreach (RadarPoint radarPoint in points)
            {
                if (radarPoint.coin.gameObject == oldCoin)
                {
                    toDelete = radarPoint;
                }
            }
            GameObject.Destroy(toDelete.point.gameObject);
            points.Remove(toDelete);
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinSpawnEvent, "CoinSpawn");
        GameEventSystem.RemoveListener(OnCoinEvent, "CoinDestroying");
    }

    private class RadarPoint
    {
        public Image point { get; set; }
        public Transform coin { get; set; }
    }
}
