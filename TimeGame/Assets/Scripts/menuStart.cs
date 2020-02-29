using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuStart : MonoBehaviour
{
   public void changeMenuSceneP1()
    {
        PlayerSet.numPlayers = 0;
        SceneManager.LoadScene("Clock");
    }
    public void changeMenuSceneP2()
    {
        PlayerSet.numPlayers = 1;
        SceneManager.LoadScene("Clock");
    }
}
