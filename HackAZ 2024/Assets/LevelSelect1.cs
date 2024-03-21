using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect1 : MonoBehaviour
{
    public int level;

    public void OpenScene() {
        Tracker.Reset();
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
