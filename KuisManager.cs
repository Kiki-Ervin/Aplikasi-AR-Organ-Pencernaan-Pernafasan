using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KuisManager : MonoBehaviour
{
    public GameObject Panel_Intro;
    public GameObject Panel_Hasil;
    public GameObject PopupKonfirmasi;

    public void Kembali()
    {
        // Jika masih di Panel Intro langsung ke Main
        if (Panel_Intro.activeSelf)
        {
            SceneManager.LoadScene("Main");
        }
        else if (Panel_Hasil.activeSelf)
        {
            SceneManager.LoadScene("Main");
        }
        // Jika sudah masuk quiz, tampilkan popup
        else
    {
        PopupKonfirmasi.SetActive(true);
    }
    }

    // Tombol YA
    public void Ya()
    {
        SceneManager.LoadScene("Main");
    }

    // Tombol TIDAK
    public void Tidak()
    {
        PopupKonfirmasi.SetActive(false);
    }
}