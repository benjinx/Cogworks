using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class StatDebuggerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playerReference;
    
    private StatManager statManager;

    [SerializeField]
    private TextMeshProUGUI displayText;

    private void Update()
    {
        if (playerReference == null)
            return;
        
        statManager = playerReference.GetComponent<StatManager>();
        
        if (statManager == null || displayText == null)
            return;

        //displayText.enabled = Input.GetKey(KeyCode.Tab);
        
        displayText.text = GetFormattedStatText();
    }

    private string GetFormattedStatText()
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<StatManager.StatType, Stat> kvp in statManager.stats)
        {
            Stat stat = kvp.Value;

            float finalValue = stat.GetValue();
            
            sb.AppendLine($"<b>{kvp.Key}:</b> {finalValue:F2}");

            foreach (StatModifier statModifier in stat.GetModifiers())
            {
                string typeShort = statModifier.statModifierType switch
                {
                    StatModifier.StatModifierType.Flat => "+",
                    StatModifier.StatModifierType.PercentAdditive => "%+",
                    StatModifier.StatModifierType.PercentMultiplicative => "%x",
                    _ => "?"
                };

                string tempFlag = statModifier.isTemporary ? $" (temp) {statModifier.duration:F1}s" : "";
                sb.AppendLine($"    {typeShort}{statModifier.value} {tempFlag}{statModifier.description} (from {statModifier.modifierName}) ");
            }
        }
        
        return sb.ToString();
    }
}
