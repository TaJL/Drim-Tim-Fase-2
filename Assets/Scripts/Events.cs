using UnityEngine;
using UnityEngine.Events;
    
public class Events
{
    public static UnityAction OnStartPouring;
    public static UnityAction OnEndPouring;

    public static UnityAction<int> OnAddMistake;
    //UI
    //public static UnityAction 
    public static UnityAction<float> OnUIUpdateRating;
}
