using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        animator.SetTrigger("OnCollected");
        GameEventSystem.EmitEvent("Coin", "OnCollected");
    }

    public void OnAnimationEnd()
    {
        GameEventSystem.EmitEvent("CoinDestroying", gameObject);
        Destroy(gameObject);
        GameEventSystem.EmitEvent("Coin", "Destroy");
    }
}
