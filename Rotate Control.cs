using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateControl : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("Daftar Target")]
    public Transform target1;
    public Transform target2;

    [Header("Setting")]
    public float rotateSpeed = 0.3f;

    private Vector2 lastPos;
    private Quaternion initialRot1;
    private Quaternion initialRot2;

    void Start()
    {
        // Simpan posisi/rotasi awal masing-masing objek
        if (target1 != null) initialRot1 = target1.localRotation;
        if (target2 != null) initialRot2 = target2.localRotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Ambil objek mana yang lagi aktif/muncul di kamera AR
        Transform activeTarget = GetActiveTarget();
        if (activeTarget == null) return;

        float deltaX = eventData.position.x - lastPos.x;
        activeTarget.Rotate(Vector3.up, -deltaX * rotateSpeed, Space.World);
        lastPos = eventData.position;
    }

    // Fungsi pembantu sederhana: Cek objek mana yang lagi aktif di scene
    public Transform GetActiveTarget()
    {
        if (target1 != null && target1.gameObject.activeInHierarchy) return target1;
        if (target2 != null && target2.gameObject.activeInHierarchy) return target2;
        return null;
    }

    // Dipanggil tombol Reset
    public void ResetRotation()
    {
        if (target1 != null) target1.localRotation = initialRot1;
        if (target2 != null) target2.localRotation = initialRot2;
    }
}