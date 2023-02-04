using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] private int maxSpores = 3;
    private int spores = 0;
    public int Spores { get { return spores; } }
    
    // Event that changes when spore count changes
    public event Action<int> OnSporesChanged;

    public bool CanAddSpores(int amount)
    {
        return spores + amount <= maxSpores;
    }
    
    public void AddSpores(int amount)
    {
        if(!CanAddSpores(amount))
        {
            return;
        }
        
        spores += amount;
        TriggerOnSporesChanged();
    }
    
    public bool TrySpendSpores(int amount)
    {
        if(!(spores - amount >= 0))
        {
            return false;
        }

        SpendSpores(amount);
        return true;
    }

    public void SpendSpores(int amount)
    {
        if (spores - amount < 0)
        {
            throw new Exception("Not enough spores to spend! (Spores: " + spores + ", Amount: " + amount + ")");
        }
        
        spores -= amount;
        TriggerOnSporesChanged();
    }

    private void TriggerOnSporesChanged()
    {
        OnSporesChanged?.Invoke(spores);
    }
}
