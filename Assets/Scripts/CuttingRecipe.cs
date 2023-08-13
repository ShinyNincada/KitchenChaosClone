using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipe : ScriptableObject
{
    public KitchenSO input;
    public KitchenSO output;
    public int cuttingProgressMax;
}
