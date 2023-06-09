using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]

public class TopAlaniTeknikİslemler
{
    public Animator TopAlaniAsansor;
    public TextMeshProUGUI SayiText;
    public int AtilmasiGerekenTop;
    public GameObject[] Toplar;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pickerObj;
    [SerializeField] private GameObject[] ToplayiciPaletler;
    [SerializeField] private GameObject[] BonusToplar;
    bool PaletlerVarmi;
    [SerializeField] private GameObject TopKontrolObjesi;
    [SerializeField] public bool pickerMovementSituation;

    int AtilanTopSayisi;
    int ToplamCheckPointSayisi;
    int mevcutCheckPointIndex;
    [SerializeField] public List<TopAlaniTeknikİslemler> _TopAlaniTeknikİslemler = new List<TopAlaniTeknikİslemler>();
    float ParmakPozX;
    void Start()
    {
        pickerMovementSituation = true;
        for (int i = 0; i < _TopAlaniTeknikİslemler.Count; i++)
        {
            _TopAlaniTeknikİslemler[i].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknikİslemler[i].AtilmasiGerekenTop;
        }
       
        ToplamCheckPointSayisi = _TopAlaniTeknikİslemler.Count-1;
       
    }

    void Update()
    {
        if (pickerMovementSituation)
        {
            // transform.forward = local, Vector3.forward = world
            //pickerObj.transform.position += 5f * Time.deltaTime * Vector3.forward;
            pickerObj.transform.Translate(Vector3.forward * 5 * Time.deltaTime, Space.World);
            if (Time.timeScale != 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 TouchPositon = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    switch(touch.phase)
                    {
                        case TouchPhase.Began:
                            ParmakPozX = TouchPositon.x - pickerObj.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (TouchPositon.x-ParmakPozX > -1.15 && TouchPositon.x - ParmakPozX < 1.15)
                            {
                                pickerObj.transform.position = Vector3.Lerp(pickerObj.transform.position
                                    , new Vector3(TouchPositon.x - ParmakPozX, pickerObj.transform.position.y,
                                    pickerObj.transform.position.z),3f);
                            }
                            break;
                    }
                }
                //if (Input.GetKey(KeyCode.LeftArrow))
                //{
                //    pickerObj.transform.position = Vector3.Lerp(pickerObj.transform.position, new Vector3(pickerObj.transform.position.x - .1f,
                //        pickerObj.transform.position.y, pickerObj.transform.position.z), .05f);
                //}
                //if (Input.GetKey(KeyCode.RightArrow))
                //{
                //    pickerObj.transform.position = Vector3.Lerp(pickerObj.transform.position, new Vector3(pickerObj.transform.position.x + .1f,
                //        pickerObj.transform.position.y, pickerObj.transform.position.z), .05f);
                //}
            }
        }
    }

    public void SiniraGelindi()
    {
        if (PaletlerVarmi)
        {
            ToplayiciPaletler[0].SetActive(false);
            ToplayiciPaletler[1].SetActive(false);
        }
        pickerMovementSituation = false;
        Invoke("AsamaKontrol", 2f);
        Collider[] HitColl = Physics.OverlapBox(TopKontrolObjesi.transform.position, TopKontrolObjesi.transform.localScale / 2, Quaternion.identity);
        int i = 0;
        while (i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .8f), ForceMode.Impulse);
            i++;
        }
        //Debug.Log(i);
    }

    public void ToplariSay()
    {
        AtilanTopSayisi++;
        _TopAlaniTeknikİslemler[mevcutCheckPointIndex].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknikİslemler[mevcutCheckPointIndex].AtilmasiGerekenTop;
    }

    void AsamaKontrol()
    {
        if (AtilanTopSayisi >= _TopAlaniTeknikİslemler[mevcutCheckPointIndex].AtilmasiGerekenTop)
        {          
            AtilanTopSayisi = 0;
            _TopAlaniTeknikİslemler[mevcutCheckPointIndex].TopAlaniAsansor.Play("Asansor");
            foreach (var item in _TopAlaniTeknikİslemler[mevcutCheckPointIndex].Toplar)
            {
                item.SetActive(false);
            }
            if (mevcutCheckPointIndex== ToplamCheckPointSayisi)
            {
               
                Debug.Log("OYUN BİTTİ - KAZANDIN PANELİ ORTAYA ÇIKABİLİR.");
                Time.timeScale = 0;
            }
            else
            {
                mevcutCheckPointIndex++;
                AtilanTopSayisi = 0;
                if (PaletlerVarmi)
                {
                    ToplayiciPaletler[0].SetActive(true);
                    ToplayiciPaletler[1].SetActive(true);
                }
            }            
        }
        else
        {
            Debug.Log("LOSE - KAYBETTİN PANELİ ORTAYA ÇIKABİLİR.");
        }
    }

    public void PaletleriOrtayaCikart()
    {
        PaletlerVarmi = true;
        ToplayiciPaletler[0].SetActive(true);
        ToplayiciPaletler[1].SetActive(true);

    }

    public void BonusToplariEkle(int BonusTopIndex)
    {
        BonusToplar[BonusTopIndex].SetActive(true);
    }

}
