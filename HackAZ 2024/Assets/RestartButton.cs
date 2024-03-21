using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void ResetScene()
    {
        Tracker.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
