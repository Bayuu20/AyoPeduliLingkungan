using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sistem_Drag : MonoBehaviour
{
    public int IDDrag; 
    public SpriteRenderer SR;

    public Vector2 SavePos;

    public BoxCollider2D Collider;
    public bool IsDiatasDrop;
    public bool IsDragBenar; 

    private void Start()
    {
        SavePos = transform.position; 
    }
    private void OnMouseDown()
    {
        SistemKumpulanSuara.instance.v_SuaraSFX(0);
    }

    private void OnMouseDrag()
    {
        if(!IsDragBenar && !SistemGame.instance.Gui_Pause.active)
        {
            Vector2 PosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = PosTarget;
            SR.sortingOrder = 5;
        }
        


    }

    private void OnMouseUp()
    {
        if (!IsDragBenar && !SistemGame.instance.Gui_Pause.active)
        {
            if (IsDiatasDrop)
            {

                if (SaveTempatDrop != null)
                {
                    if (IDDrag == SaveTempatDrop.IDDrop)
                    {
                        SR.sortingOrder = 1;
                        transform.position = SavePosTarget;
                        transform.localScale = SaveSizeTarget;
                        Collider.enabled = false;
                        IsDragBenar = true;

                        SaveTempatDrop = null;
                        SavePosTarget = Vector2.zero;


                        SistemGame.DataScore += 100;
                        SistemGame.instance.IDKartu++;
                        SistemGame.instance.v_SetKartuDrag();

                        SistemGame.instance.SetInfoUI();

                        SistemKumpulanSuara.instance.v_SuaraSFX(1);

                    }
                    else
                    {
                        SistemGame.DataScore -= 20;
                        SistemGame.DataDarah--;
                        SR.sortingOrder = 0;
                        transform.position = SavePos;

                        SistemGame.instance.SetInfoUI();
                        SistemKumpulanSuara.instance.v_SuaraSFX(2);
                    }
                }
                else
                {
                    SR.sortingOrder = 0;
                    transform.position = SavePos;
                }



            }
            else
            {
                SR.sortingOrder = 0;
                transform.position = SavePos;
            }
        }
        
    }
    Vector2 SavePosTarget;
    Vector2 SaveSizeTarget;
    Tempat_Drop SaveTempatDrop;
    private void OnTriggerStay2D(Collider2D trig) 
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            IsDiatasDrop = true;
            SavePosTarget = trig.transform.position;
            SaveSizeTarget = trig.transform.localScale;
            SaveTempatDrop = trig.gameObject.GetComponent<Tempat_Drop>();
        }

    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        IsDiatasDrop = false;
    }
    
}
