using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image ratingBar;

    public void UpdateRatings(float ratingPercentage)
    {
        ratingBar.fillAmount = ratingPercentage ;
    }
}
