using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganNavigator : MonoBehaviour
{
    [Header("Daftar Panel Organ (Urutkan!)")]
    public GameObject[] panels;

    [Header("Tombol")]
    public GameObject btnNext;
    public GameObject btnBack;

    private int currentIndex = 0;

    void Start()
    {
        UpdateButton();
    }

    public void BukaPanel(int index)
    {
        if (index < 0 || index >= panels.Length) return;

        // Sembunyikan semua panel
        foreach (GameObject p in panels)
            p.SetActive(false);

        // Tampilkan panel yang dipilih
        panels[index].SetActive(true);

        currentIndex = index;

        UpdateButton();
    }

    public void Next()
    {
        if (currentIndex >= panels.Length - 1) return;

        BukaPanel(currentIndex + 1);
    }

    public void Back()
    {
        if (currentIndex <= 0) return;

        BukaPanel(currentIndex - 1);
    }

    void UpdateButton()
    {
        btnBack.SetActive(currentIndex > 0);
        btnNext.SetActive(currentIndex < panels.Length - 1);
    }
}