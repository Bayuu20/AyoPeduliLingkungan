using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPengaturan : MonoBehaviour
{
    public Slider Slider_SFX;
    public Slider Slider_BGM;

    private void OnEnable()
    {
        if (SistemKumpulanSuara.instance != null)
        {
            if (Slider_SFX != null && SistemKumpulanSuara.instance.SourceSuaraSFX != null)
            {
                Slider_SFX.value = SistemKumpulanSuara.instance.SourceSuaraSFX.volume;
            }

            if (Slider_BGM != null && SistemKumpulanSuara.instance.SourceSuaraBGM != null)
            {
                Slider_BGM.value = SistemKumpulanSuara.instance.SourceSuaraBGM.volume;
            }
        }
    }

    public void UbahVolume(bool SFX)
    {
        if (SistemKumpulanSuara.instance != null)
        {
            if (SFX && SistemKumpulanSuara.instance.SourceSuaraSFX != null)
            {
                SistemKumpulanSuara.instance.SourceSuaraSFX.volume = Slider_SFX.value;
            }
            else if (!SFX && SistemKumpulanSuara.instance.SourceSuaraBGM != null)
            {
                SistemKumpulanSuara.instance.SourceSuaraBGM.volume = Slider_BGM.value;
            }
        }
    }
}
