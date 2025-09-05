using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    public int maxStamina;
    public int currentStamina;

    public UnityEvent<int> onStaminaChanged;
    public UnityEvent<int> onDecrease;
    public UnityEvent<int> onIncrease;
    public UnityEvent onEmpty;

    void Start()
    {
        currentStamina = maxStamina;

        onStaminaChanged?.Invoke(currentStamina);
    }

    public void Decrease(int amount)
    {
        currentStamina -= amount;

        if (currentStamina <= 0)
        {
            currentStamina = 0;
            onEmpty?.Invoke();
        }
        
        onDecrease?.Invoke(amount);
        onStaminaChanged?.Invoke(amount);
    }

    public void Increase(int amount)
    {
        currentStamina += amount;

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }

        onIncrease?.Invoke(amount);
        onStaminaChanged?.Invoke(amount);
    }
}
