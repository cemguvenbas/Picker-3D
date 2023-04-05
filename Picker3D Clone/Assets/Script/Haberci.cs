using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haberci : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplayiciSinirObje"))
        {
            _GameManager.SiniraGelindi();
        }
    }
}
