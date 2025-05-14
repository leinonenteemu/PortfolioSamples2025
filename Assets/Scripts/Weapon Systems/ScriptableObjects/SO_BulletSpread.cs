using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Spread", menuName = "ScriptableObjects/BulletSpread")]
public class SO_BulletSpread : ScriptableObject
{
    [SerializeField] AnimationCurve distribution;
    public float spreadMultiplier;

    [SerializeField] private float[] normalizedValues = new float[10];
    [SerializeField] private DistributionSlice[] slices = new DistributionSlice[10];
    private void OnValidate()
    {
        Initialize();
    }

    public void Initialize()
    {
        float curveEvaluationPoint = 0.05f;
        for (int i = 0; i < 10; i++)
        {
            float y = distribution.Evaluate(curveEvaluationPoint);
            DistributionSlice slice = new DistributionSlice(new Vector2(curveEvaluationPoint, y));
            curveEvaluationPoint += 0.1f;
            slices[i] = slice;
        }

        CalculateTotalValue();
    }

    private void CalculateTotalValue()
    {
        float total = 0;
        // float distributionValue = 0;
        for (int i = 0; i < slices.Length; i++)
        {
            total += slices[i].values.y;
        }

        for (int i = 0; i < slices.Length; i++)
        {
            normalizedValues[i] = slices[i].values.y / total;
        }
    }

    public float GetRandom()
    {
        float r = Random.Range(0, 1f);
        int selectedIndex = 0;

        for (int i = 0; i < slices.Length; i++)
        {
            if (r - normalizedValues[i] < 0)
            {
                selectedIndex = i;
                break;
            }

            r -= normalizedValues[i];
        }

        var selected = slices[selectedIndex];
        var localRandom = Random.Range(selected.values.x - 0.05f, selected.values.x + 0.05f);
        int positive = Random.Range(0, 2);
        return positive == 0 ? -localRandom * spreadMultiplier : localRandom * spreadMultiplier;
    }

    public float GetRandom(float overrideSpreadMultiplier)
    {
        float r = Random.Range(0, 1f);
        int selectedIndex = 0;

        for (int i = 0; i < slices.Length; i++)
        {
            if (r - normalizedValues[i] < 0)
            {
                selectedIndex = i;
                break;
            }

            r -= normalizedValues[i];
        }

        var selected = slices[selectedIndex];
        var localRandom = Random.Range(selected.values.x - 0.05f, selected.values.x + 0.05f);
        int positive = Random.Range(0, 2);
        return positive == 0 ? -localRandom * overrideSpreadMultiplier : localRandom * overrideSpreadMultiplier;
    }
}

[System.Serializable]
public class DistributionSlice
{
    public Vector2 values;
    public DistributionSlice(Vector2 values) => this.values = values;
}

