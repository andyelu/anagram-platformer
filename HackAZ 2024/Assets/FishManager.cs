using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishManager : MonoBehaviour
{
    public void advanceLevel()
    {
        SceneManager.LoadScene("Level " + 3);
    }
    }

    
