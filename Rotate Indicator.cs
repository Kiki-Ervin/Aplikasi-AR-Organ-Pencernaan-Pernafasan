using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIndicator : MonoBehaviour
{
    [Header("Daftar Target (Isi 2 Objek AR yang Sama)")]
    public Transform target1;
    public Transform target2;

    [Header("Indicator Setting")]
    public float range = 120f;

    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Cek objek mana yang lagi aktif/muncul
        Transform activeTarget = null;
        if (target1 != null && target1.gameObject.activeInHierarchy) activeTarget = target1;
        else if (target2 != null && target2.gameObject.activeInHierarchy) activeTarget = target2;

        if (activeTarget == null) return;

        // Hitung sudut rotasi Y
        float angle = activeTarget.eulerAngles.y;
        if (angle > 180) angle -= 360;

        // Ubah ke posisi slider UI
        float x = (angle / 180f) * range;

        Vector2 pos = rt.anchoredPosition;
        pos.x = x;
        rt.anchoredPosition = pos;
    }
}