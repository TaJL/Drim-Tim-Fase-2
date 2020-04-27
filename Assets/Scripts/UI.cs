using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image ratingBar;
    [SerializeField] private Image drunknessBar;


    private void Start()
    {
        Events.OnUIUpdateRating += UpdateRatings;
    }

    public void UpdateRatings(float ratingPercentage)
    {
        ratingBar.fillAmount = ratingPercentage ;
        print(string.Format("Percentage: {0}",ratingPercentage));

    }

    public void UpdateDrunkess(float drunknessPercentage)
    {
        drunknessBar.fillAmount = drunknessPercentage ;
    }
}
