using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemKumpulanSuara : MonoBehaviour
{
    public static SistemKumpulanSuara instance;
    public AudioClip[] DataSuara;
    public AudioSource SourceSuaraSFX;
    public AudioSource SourceSuaraBGM;


    private void OnEnable()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void v_SuaraSFX(int IDSuara)
    {
        SourceSuaraSFX.PlayOneShot(DataSuara[IDSuara]);
    }
}
