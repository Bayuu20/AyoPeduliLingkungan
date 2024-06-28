using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemGame : MonoBehaviour
{
    public static SistemGame instance;
    public static bool NewGame = true;
    public static int IDGame;
    public int IDKartu;
    public int TargetKartu;

    [Header("Data Permainan")]
    public bool GameAktif;
    public bool GameFinish;
    public int DataTargetKartu;
    public int Target, DataSaatIni;
    public int SistemAcak;
    public static int DataLevel, DataScore, DataDarah;

    [Header("Komponen UI")]
    public Text teks_Level;
    public Text teks_Score;
    public RectTransform Ui_Darah;

    [Header("Obj GUI")]
    public GameObject Gui_Pause;

    [Header("Sistem Kartu")]
    public Transform TempatKartu;
    public GameObject KartuBenda;
    public Tempat_Drop[] KartuDrop;
    public Sprite[] GambarBenda;

    [Header("Sistem Acaknya")]
    public List<int> AcakSoal = new List<int>();
    public List<int> AcakUrutanMuncul = new List<int>();


    [Space]
    public Tempat_Drop[] Sistem_Drop;

    float s;

    private void OnEnable()
    {
        instance = this;
        v_SetDataAwal();
        v_AcakSoal();
    }

    private void Start()
    {
        GameAktif = false;
        GameFinish = false;
        ResetData();
        Target = Sistem_Drop.Length;
        if (AcakSoal.Count > 0)
            GameAktif = true;
    }

    void ResetData()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "game0")
        {
            DataScore = 0;
            DataDarah = 5;
            DataLevel = 0;
        }
    }

    private void Update()
    {
        SetInfoUI();

        if (GameAktif && !GameFinish)
        {
            if (DataDarah <= 0)
            {
                GameFinish = true;
                GameAktif = false;

                //Fungsi Kalah
                SistemKumpulanSuara.instance.v_SuaraSFX(4);
                SceneManager.LoadScene("GameSelesai");
            }
        }
    }

    public void v_SetDataAwal()
    {
        IDKartu = 0;
        DataTargetKartu = KartuDrop.Length;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "game0")
        {
            DataScore = 0;
            DataDarah = 5;
            IDGame = 0;
        }
        SetInfoUI();
    }

    public void v_AcakSoal()
    {
        AcakSoal.Clear();
        AcakSoal = new List<int>(new int[KartuDrop.Length]);
        int Rand = 0;
        for (int i = 0; i < AcakSoal.Count; i++)
        {
            Rand = Random.Range(1, GambarBenda.Length);
            while (AcakSoal.Contains(Rand))
            {
                Rand = Random.Range(1, GambarBenda.Length);
            }
            AcakSoal[i] = Rand;
            KartuDrop[i].IDDrop = Rand - 1;
            KartuDrop[i].SR.sprite = GambarBenda[Rand - 1];
        }

        v_AcakUrutanMuncul();
    }

    public void SetInfoUI()
    {
        teks_Level.text = (IDGame + 1).ToString();

        teks_Score.text = DataScore.ToString();

        Ui_Darah.sizeDelta = new Vector2(232f * DataDarah, 194f);
    }


    public void v_AcakUrutanMuncul()
    {
        AcakUrutanMuncul.Clear();
        AcakUrutanMuncul = new List<int>(new int[KartuDrop.Length]);
        int Rand = 0;
        for (int i = 0; i < AcakUrutanMuncul.Count; i++)
        {
            Rand = Random.Range(1, AcakSoal.Count + 1);

            while (AcakUrutanMuncul.Contains(Rand))
                Rand = Random.Range(1, AcakSoal.Count + 1);

            AcakUrutanMuncul[i] = Rand;
        }

        v_SetKartuDrag();
    }

    public void v_SetKartuDrag()
    {
        if (IDKartu < DataTargetKartu)
        {
            GameObject Kartu = Instantiate(KartuBenda);
            Kartu.transform.position = TempatKartu.position;
            Kartu.transform.localScale = TempatKartu.localScale;
            Sistem_Drag SistemKartuDrag = Kartu.GetComponent<Sistem_Drag>();
            int DataKartu = AcakSoal[AcakUrutanMuncul[IDKartu] - 1] - 1;
            SistemKartuDrag.IDDrag = DataKartu;
            SistemKartuDrag.SR.sprite = GambarBenda[DataKartu];
            SistemKartuDrag.SavePos = TempatKartu.position;
        }
        else
        {
            SistemKumpulanSuara.instance.v_SuaraSFX(3);
            IDGame++;

            int TargetLevel = 5;
            if (IDGame >= TargetLevel) //Max 5 Level
            {
                Debug.Log("GameSelesai");
                IDGame = TargetLevel - 1;
                SceneManager.LoadScene("GameSelesai");
                SistemKumpulanSuara.instance.v_SuaraSFX(4);
                SistemKumpulanSuara.instance.v_SuaraSFX(6);
                SetInfoUI();
            }
            else
            {
                string TargetSceneSelanjutnya = "game" + IDGame;
                SceneManager.LoadScene(TargetSceneSelanjutnya);
            }
            SetInfoUI();

        }
    }

    public void Btn_Pause(bool pause)
    {
        if (pause)
        {
            GameAktif = false;
            Gui_Pause.SetActive(true);
        }
        else
        {
            GameAktif = true;
            Gui_Pause.SetActive(false);
        }
    }
}