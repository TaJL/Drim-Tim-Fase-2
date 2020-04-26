using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image ratingBar;
    [SerializeField] private Image drunknessBar;

    public void UpdateRatings(float ratingPercentage)
    {
        ratingBar.fillAmount = ratingPercentage ;
    }

    public void UpdateDrunkess(float drunknessPercentage)
    {
        drunknessBar.fillAmount = drunknessPercentage ;
    }
}
