using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    public bool IsTransisi, IsTidakPerlu;
    string SaveNamaScene;

    public void btn_suara(int id)
    {
        SistemKumpulanSuara.instance.v_SuaraSFX(0);
    }

    public void Btn_Pindah(string nama)
    {
        this.gameObject.SetActive(true);
        SaveNamaScene = nama;
        GetComponent<Animator>().Play("end");
    }



    public void Btn_Restart()
    {
        SaveNamaScene = SceneManager.GetActiveScene().name;
        GetComponent<Animator>().Play("end");
    }

    public void pindah()
    {
        SceneManager.LoadScene(SaveNamaScene);
    }
    public void Btn_KeluarGame()
    {
        Application.Quit();
    }
}