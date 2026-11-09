using UnityEngine;
using UnityEngine.SceneManagement;

public class MateriManager : MonoBehaviour
{
    [Header("Menu Utama")]
    public GameObject PanelUtama;

    [Header("Menu Kategori")]
    public GameObject PanelPernapasan;
    public GameObject PanelPencernaan;

    [Header("Pernapasan")]
    public GameObject PanelPernapasan1;
    public GameObject PanelPernapasan2_1;
    public GameObject PanelPernapasan2_2;
    public GameObject PanelPernapasan2_3;
    public GameObject PanelPernapasan2_4;
    public GameObject PanelPernapasan2_5;
    public GameObject PanelPernapasan3;

    [Header("Pencernaan")]
    public GameObject PanelPencernaan1;
    public GameObject PanelPencernaan2_1;
    public GameObject PanelPencernaan2_2;
    public GameObject PanelPencernaan2_3;
    public GameObject PanelPencernaan2_4;
    public GameObject PanelPencernaan2_5;
    public GameObject PanelPencernaan2_6;

    public GameObject PanelPencernaan3;

    [Header("Tombol")]
    public GameObject BtnHome;
    public GameObject bgHome;
    public GameObject BtnNext;
    public GameObject BtnBack;

    [Header("Organ Pernapasan")]
    public GameObject[] OrganPernapasan;

    [Header("Organ Pencernaan")]
    public GameObject[] OrganPencernaan;

    private GameObject[] currentOrgan;
    private int currentIndex;

    void Start()
    {
        BtnHome.SetActive(false);
        bgHome.SetActive(false);
        BtnNext.SetActive(false);
        BtnBack.SetActive(false);
    }

    //==========================
    // MENU
    //==========================

    public void BukaPernapasan()
    {
        PanelUtama.SetActive(false);
        PanelPernapasan.SetActive(true);

        BtnHome.SetActive(true);
        bgHome.SetActive(true);
    }

    public void BukaPencernaan()
    {
        PanelUtama.SetActive(false);
        PanelPencernaan.SetActive(true);

        BtnHome.SetActive(true);
        bgHome.SetActive(true);
    }

    //==========================
    // PERNAPASAN
    //==========================

    public void BukaPernapasan1()
    {
        PanelPernapasan.SetActive(false);
        PanelPernapasan1.SetActive(true);

        BtnNext.SetActive(false);
        BtnBack.SetActive(false);
    }

    public void BukaPernapasan2_1()
    {
        PanelPernapasan.SetActive(false);

        currentOrgan = OrganPernapasan;
        currentIndex = 0;

        ShowOrgan();
    }

    public void BukaPernapasan3()
    {
        PanelPernapasan.SetActive(false);
        PanelPernapasan3.SetActive(true);

        BtnNext.SetActive(false);
        BtnBack.SetActive(false);
    }

    //==========================
    // PENCERNAAN
    //==========================

    public void BukaPencernaan1()
    {
        PanelPencernaan.SetActive(false);
        PanelPencernaan1.SetActive(true);

        BtnNext.SetActive(false);
        BtnBack.SetActive(false);
    }

    public void BukaPencernaan2_1()
    {
        PanelPencernaan.SetActive(false);

        currentOrgan = OrganPencernaan;
        currentIndex = 0;

        ShowOrgan();
    }

    public void BukaPencernaan3()
    {
        PanelPencernaan.SetActive(false);
        PanelPencernaan3.SetActive(true);

        BtnNext.SetActive(false);
        BtnBack.SetActive(false);
    }

    //==========================
    // ORGAN
    //==========================

    void ShowOrgan()
    {
        if (currentOrgan == null) return;

        foreach (GameObject g in currentOrgan)
            g.SetActive(false);

        currentOrgan[currentIndex].SetActive(true);

        BtnBack.SetActive(currentIndex > 0);
        BtnNext.SetActive(currentIndex < currentOrgan.Length - 1);
    }

    public void Next()
    {
        if (currentOrgan == null) return;

        if (currentIndex >= currentOrgan.Length - 1)
            return;

        currentIndex++;
        ShowOrgan();
    }

    public void Back()
    {
        if (currentOrgan == null) return;

        if (currentIndex <= 0)
            return;

        currentIndex--;
        ShowOrgan();
    }

    //==========================
    // HOME
    //==========================

    public void Home()
    {
        SceneManager.LoadScene("Main");
    }

    //==========================
    // KEMBALI
    //==========================

    public void Kembali()
    {
        // Pernapasan
        if (PanelPernapasan1.activeSelf)
        {
            PanelPernapasan1.SetActive(false);
            PanelPernapasan.SetActive(true);
        }
        else if (PanelPernapasan2_1.activeSelf)
        {
            PanelPernapasan2_1.SetActive(false);
            PanelPernapasan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPernapasan2_2.activeSelf)
        {
            PanelPernapasan2_2.SetActive(false);
            PanelPernapasan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPernapasan2_3.activeSelf)
        {
            PanelPernapasan2_3.SetActive(false);
            PanelPernapasan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPernapasan2_4.activeSelf)
        {
            PanelPernapasan2_4.SetActive(false);
            PanelPernapasan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPernapasan2_5.activeSelf)
        {
            PanelPernapasan2_5.SetActive(false);
            PanelPernapasan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPernapasan3.activeSelf)
        {
            PanelPernapasan3.SetActive(false);
            PanelPernapasan.SetActive(true);
        }

        // Pencernaan
        else if (PanelPencernaan1.activeSelf)
        {
            PanelPencernaan1.SetActive(false);
            PanelPencernaan.SetActive(true);
        }
        else if (PanelPencernaan2_1.activeSelf)
        {
            PanelPencernaan2_1.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPencernaan2_2.activeSelf)
        {
            PanelPencernaan2_2.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPencernaan2_3.activeSelf)
        {
            PanelPencernaan2_3.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPencernaan2_4.activeSelf)
        {
            PanelPencernaan2_4.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPencernaan2_5.activeSelf)
        {
            PanelPencernaan2_5.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }
        else if (PanelPencernaan2_6.activeSelf)
        {
            PanelPencernaan2_6.SetActive(false);
            PanelPencernaan.SetActive(true);

            BtnNext.SetActive(false);
            BtnBack.SetActive(false);
        }

        else if (PanelPencernaan3.activeSelf)
        {
            PanelPencernaan3.SetActive(false);
            PanelPencernaan.SetActive(true);
        }

        // Menu
        else if (PanelPernapasan.activeSelf)
        {
            PanelPernapasan.SetActive(false);
            PanelUtama.SetActive(true);

            BtnHome.SetActive(false);
            bgHome.SetActive(false);
        }
        else if (PanelPencernaan.activeSelf)
        {
            PanelPencernaan.SetActive(false);
            PanelUtama.SetActive(true);

            BtnHome.SetActive(false);
            bgHome.SetActive(false);
        }
        else if (PanelUtama.activeSelf)
        {
            SceneManager.LoadScene("Main");
        }
    }
}