using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplariSay"))
        {
            _GameManager.ToplariSay();
        }
    }
}
