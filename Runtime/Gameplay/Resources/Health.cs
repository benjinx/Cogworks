using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public UnityEvent<int> onHealthChanged;
    public UnityEvent<int> onDamage;
    public UnityEvent<int> onHeal;
    public UnityEvent onDeath;
    public UnityEvent onRevive;

    void Start()
    {
        if (maxHealth < 1)
        {
            maxHealth = 1;
        }

        currentHealth = maxHealth;

        onHealthChanged?.Invoke(maxHealth);
    }

    public void Damage(int damage)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke();
        }
        else
        {
            onDamage?.Invoke(damage);
        }

        onHealthChanged?.Invoke(damage);
    }

    public void Heal(int health)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        onHeal?.Invoke(currentHealth);
        onHealthChanged?.Invoke(health);
    }

    public void Revive(int health)
    {
        if (currentHealth > 0)
        {
            return;
        }

        if (health <= 0)
        {
            health = 1;
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 1;
        }

        Heal(health);
        onRevive?.Invoke();
    }

    public bool IsAlive()
    {
        if (currentHealth <= 0)
        {
            return false;
        }

        return true;
    }
}
