using UnityEngine;

public class HitboxExample : MonoBehaviour
{
    private Hitbox hitbox;

    void Start()
    {
        hitbox = GetComponent<Hitbox>();
    }

    public void OnHit()
    {
        Debug.Log("Hit Detected: " + gameObject.name);
        hitbox.DisableHitbox();
    }
}
