using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public class DrunknessController : MonoBehaviour
{
    private UnityAction<float> DrunknessUpdate;

    [SerializeField] private int max_drunkness = 100;

    [SerializeField] private int drunkness_per_shoot = 5;

    private float drunkness = 0;

    public float Drunkness
    {
        get
        {
            return drunkness;
        }
    }

    public void UpdateDrunkness()
    {
        if (drunkness >= max_drunkness)
        {
            return;
        }

        drunkness += drunkness_per_shoot;

        if (drunkness >= max_drunkness)
        {
            drunkness = max_drunkness;
        }

        DrunknessUpdate.Invoke(drunkness);
    }
}
