using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomRotateControl : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("Daftar Target (Isi 2 Objek AR Kamu Di Sini)")]
    public Transform target1;
    public Transform target2;

    [Header("UI")]
    public RectTransform indicator;

    [Header("Rotate")]
    public float rotateSpeed = 0.35f;

    [Header("Indicator")]
    public float indicatorRange = 120f;
    public float returnSpeed = 12f;

    private Vector2 lastPointerPos;
    private float targetIndicatorX = 0f;
    private bool dragging = false;

    // Simpan rotasi awal kedua objek
    private Quaternion initialRot1;
    private Quaternion initialRot2;

    void Start()
    {
        // Catat rotasi awal masing-masing target
        if (target1 != null) initialRot1 = target1.localRotation;
        if (target2 != null) initialRot2 = target2.localRotation;
    }

    void Update()
    {
        if (indicator == null) return;

        // Animasi posisi indicator UI
        Vector2 pos = indicator.anchoredPosition;
        pos.x = Mathf.Lerp(
            pos.x,
            targetIndicatorX,
            Time.deltaTime * returnSpeed);

        indicator.anchoredPosition = pos;
    }

    // Fungsi pembantu untuk mengambil objek mana yang sedang aktif
    public Transform GetActiveTarget()
    {
        if (target1 != null && target1.gameObject.activeInHierarchy) return target1;
        if (target2 != null && target2.gameObject.activeInHierarchy) return target2;
        return null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        lastPointerPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Transform activeTarget = GetActiveTarget();

        if (!dragging || activeTarget == null)
            return;

        float deltaX = eventData.position.x - lastPointerPos.x;

        // Memutar objek yang aktif pada sumbu Y
        activeTarget.Rotate(
            Vector3.up,
            -deltaX * rotateSpeed,
            Space.World);

        targetIndicatorX += deltaX;
        targetIndicatorX = Mathf.Clamp(
            targetIndicatorX,
            -indicatorRange,
            indicatorRange);

        lastPointerPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        targetIndicatorX = 0f;
    }

    // Dipanggil saat tombol Reset diklik
    public void ResetRotation()
    {
        dragging = false;
        targetIndicatorX = 0f;

        if (indicator != null)
        {
            Vector2 pos = indicator.anchoredPosition;
            pos.x = 0f;
            indicator.anchoredPosition = pos;
        }

        // Kembalikan rotasi kedua target ke posisi awal
        if (target1 != null) target1.localRotation = initialRot1;
        if (target2 != null) target2.localRotation = initialRot2;
    }
}