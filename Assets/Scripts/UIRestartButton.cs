using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
