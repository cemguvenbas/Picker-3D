using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topitem : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string ItemTuru;
    [SerializeField] private int BonusTopIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplayiciSinirObje"))
        {
            if (ItemTuru=="Palet")
            {
                _GameManager.PaletleriOrtayaCikart();
                gameObject.SetActive(false);
            }
            else
            {
                _GameManager.BonusToplariEkle(BonusTopIndex);
                gameObject.SetActive(false);
            }

        }
    }
}
