using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemSkor : MonoBehaviour
{
    public int DataTotalSkor;

    public Image GambarInfoAtas;
    public Sprite[] GambarInfo;

    public Text TeksSkor;
    public Text TeksSkorTertinggi;

    int TargetSkor = 1200;



    private void OnEnable()
    {
        DataTotalSkor = SistemGame.DataScore;

        TeksSkor.text = DataTotalSkor.ToString("N0");

        if(DataTotalSkor > PlayerPrefs.GetInt("Skor"))
        {
            PlayerPrefs.SetInt("Skor", DataTotalSkor);
        }



        if(DataTotalSkor >= TargetSkor)
        {
            GambarInfoAtas.sprite = GambarInfo[0];
            SistemKumpulanSuara.instance.v_SuaraSFX(4);
            SistemKumpulanSuara.instance.v_SuaraSFX(6);
        }
        else
        {
            GambarInfoAtas.sprite = GambarInfo[1];
            SistemKumpulanSuara.instance.v_SuaraSFX(5);
        }
    }

}
