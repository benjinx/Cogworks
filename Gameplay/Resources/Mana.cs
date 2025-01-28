using UnityEngine;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public int maxMana;
    public int currentMana;

    public UnityEvent<int> onManaChanged;
    public UnityEvent<int> onDecrease;
    public UnityEvent<int> onIncrease;
    public UnityEvent onEmpty;

    void Start()
    {
        currentMana = maxMana;

        onManaChanged?.Invoke(currentMana);
    }

    public void Decrease(int amount)
    {
        currentMana -= amount;

        if (currentMana <= 0)
        {
            currentMana = 0;
            onEmpty?.Invoke();
        }
        else
        {
            onDecrease?.Invoke(amount);
        }

        onManaChanged?.Invoke(amount);
    }

    public void Increase(int amount)
    {
        currentMana += amount;

        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
        }

        onIncrease?.Invoke(amount);
        onManaChanged?.Invoke(amount);
    }
}
