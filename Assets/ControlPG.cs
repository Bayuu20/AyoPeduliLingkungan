using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPG : MonoBehaviour
{
    public AudioSfx audioSfx;

    [System.Serializable]
    public class Soals
    {
        [System.Serializable]
        public class ElementSoals
        {
            public Sprite spriteSoal;
            public string stringsoal;
            public Sprite[] spriteJawaban;
            public int KunciJawaban;
        }

        public ElementSoals elementSoals;
    }

    public Soals[] soals;

    public static int DataDarah = 5;
    public RectTransform GambarDarah;

    public int[] indexRandomSoal;
    public int[] indexRandomJawaban;
    public int totalSoal;
    public int urutanSoal;
    int jawabanBenar;

    public Image imageSoal;
    public Text textsoal;
    public Image[] imageJawaban;

    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public Button buttonPlay;

    public Text textScore;
    public Text textScoreAkhir;
    public Text textScoreGameOver;
    public int increaseScore;
    public int totalScoreAkhir;
    public int decreaseScore;
    public GameObject PanelEndGame;

    [Header("Obj GUI")]
    public GameObject Gui_Pause;
    public bool GameAktif;

    public GameObject PanelGameOver;
    public AudioClip gameOverClip;
    public AudioSource gameOverAudioSource;

    public bool isJawabanHarusBenar;
    public bool isJawabanBenar;

    void Start()
    {
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
        v_setDarah();
    }

    void Update()
    {

    }

    public void v_SetDataAwal()
    {
        v_setDarah();
    }

    public void v_setDarah()
    {
        GambarDarah.sizeDelta = new Vector2(232f * DataDarah, 194f);
    }

    public void ButtonPause(bool pause)
    {
        audioSfx.soundButton();
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

    void DecreaseScore()
    {
        if (totalScoreAkhir - decreaseScore >= 0)
        {
            totalScoreAkhir -= decreaseScore;
        }
        else
        {
            totalScoreAkhir = 0;
        }

        textScore.text = totalScoreAkhir.ToString();
    }

    void IncreaseScore()
    {
        totalScoreAkhir += increaseScore;
        textScore.text = totalScoreAkhir.ToString();
    }

    void StopVoiceOver()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            buttonPlay.interactable = true;
            CancelInvoke();
        }
    }

    void ReactiveButton()
    {
        buttonPlay.interactable = true;
    }

    public void ButtonPlayVoiceOver()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip[indexRandomSoal[urutanSoal]];
            audioSource.Play();
            buttonPlay.interactable = false;

            Invoke("ReactiveButton", audioClip[indexRandomSoal[urutanSoal]].length);
        }
    }

    public void ButtonJawaban(int indexJawaban)
    {
        if (indexRandomJawaban[indexJawaban] == jawabanBenar)
        {
            StopVoiceOver();
            IncreaseScore();
            isJawabanBenar = true;
            audioSfx.SoundSfxBenar();
        }
        else
        {
            DecreaseScore();
            DecreaseDarah();
            audioSfx.SoundSfxSalah();

            if (DataDarah == 0)
            {
                GameOver(); // Memanggil GameOver jika darah habis
                return; // Menghentikan eksekusi lebih lanjut
            }
        }

        GenerateNextSoal(); // Pindah ke soal berikutnya terlepas dari jawaban benar atau salah
    }

    void DecreaseDarah()
    {
        if (DataDarah > 0)
        {
            DataDarah--;
            v_setDarah();
        }
        else
        {
            DataDarah = 0;
            v_setDarah();
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        PanelGameOver.SetActive(true);
        textScoreGameOver.text = totalScoreAkhir.ToString(); // Update text score game over
        gameOverAudioSource.clip = gameOverClip;
        gameOverAudioSource.Play();
    }

    void GenerateNextSoal()
    {
        if (urutanSoal < totalSoal - 1)
        {
            urutanSoal++;
            GenerateIndexRandomJawaban();
            GenerateSoal();
            isJawabanBenar = false;
        }
        else
        {
            PanelEndGame.SetActive(true);
            textScoreAkhir.text = totalScoreAkhir.ToString();
            audioSfx.soundFinish();
            audioSfx.soundFinish2();
        }
    }

    void GenerateIndexRandomJawaban()
    {
        indexRandomJawaban = new int[2];
        for (int i = 0; i < indexRandomJawaban.Length; i++)
        {
            indexRandomJawaban[i] = i;
        }

        for (int i = 0; i < indexRandomJawaban.Length; i++)
        {
            int temp = indexRandomJawaban[i];
            int randomIndex = UnityEngine.Random.Range(0, indexRandomJawaban.Length);
            indexRandomJawaban[i] = indexRandomJawaban[randomIndex];
            indexRandomJawaban[randomIndex] = temp;
        }
    }

    void GenerateIndexRandomSoal()
    {
        indexRandomSoal = new int[soals.Length];
        for (int i = 0; i < indexRandomSoal.Length; i++)
        {
            indexRandomSoal[i] = i;
        }

        for (int i = 0; i < indexRandomSoal.Length; i++)
        {
            int temp = indexRandomSoal[i];
            int randomIndex = UnityEngine.Random.Range(0, indexRandomSoal.Length);
            indexRandomSoal[i] = indexRandomSoal[randomIndex];
            indexRandomSoal[randomIndex] = temp;
        }
    }

    void GenerateSoal()
    {
        imageSoal.sprite = soals[indexRandomSoal[urutanSoal]].elementSoals.spriteSoal;
        textsoal.text = soals[indexRandomSoal[urutanSoal]].elementSoals.stringsoal;

        for (int i = 0; i < 2; i++)
        {
            imageJawaban[i].sprite = soals[indexRandomSoal[urutanSoal]].elementSoals.spriteJawaban[indexRandomJawaban[i]];
        }

        jawabanBenar = soals[indexRandomSoal[urutanSoal]].elementSoals.KunciJawaban;
    }

    // Tambahkan fungsi untuk tombol "Ulang"
    public void ButtonUlang()
    {
        ResetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Tambahkan fungsi untuk tombol "Beranda"
    public void ButtonBeranda()
    {
        ResetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Beranda");
    }

    // Fungsi untuk mereset data darah
    void ResetData()
    {
        DataDarah = 5;
        v_setDarah();
        totalScoreAkhir = 0;
        urutanSoal = 0;
        isJawabanBenar = false;
        GenerateIndexRandomSoal();
        GenerateIndexRandomJawaban();
        GenerateSoal();
    }
}
