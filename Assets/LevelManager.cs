using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadToScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
    public void v_Suara(int id)
    {
        SistemKumpulanSuara.instance.v_SuaraSFX(id);
    }
}