using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("MenuCanvas"));
        SceneManager.LoadScene(1);
    }
}
