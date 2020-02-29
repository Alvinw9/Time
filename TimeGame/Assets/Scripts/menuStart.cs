using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuStart : MonoBehaviour
{
   public void changeMenuScene()
    {
        SceneManager.LoadScene("Clock");
    }
}
