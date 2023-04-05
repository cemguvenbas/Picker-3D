using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonerKol : MonoBehaviour
{
    bool Don;
    [SerializeField] private float DonusDegeri;
    public void DonmeyeBasla()
    {
        Don = true;
    }

    void Update()
    {
        if(Don)
        transform.Rotate(0, 0, DonusDegeri, Space.Self);
        //transform.DORotate(new Vector3(360.0f, 360.0f, 0.0f), 5.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
    }
}
