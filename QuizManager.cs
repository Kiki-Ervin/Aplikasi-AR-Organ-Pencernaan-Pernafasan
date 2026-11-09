using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuizManager : MonoBehaviour
{
    [Header("Data Soal")]
    public Soal[] daftarSoal;

    [Header("Timer")]
    public TMP_Text txtWaktu;
    private float waktu = 0f;
    private bool timerBerjalan = false;

    [Header("Progress")]
    public RectTransform progressBar;
    public TMP_Text txtPersen;
    private float maxWidth;

    [Header("Panel")]
    public GameObject PanelIntro;
    public GameObject PanelQuiz;
    public GameObject PanelHasil;
    public GameObject AlertBenar;
    public GameObject AlertSalah;
    public GameObject Panel_PojokKanan;

    [Header("UI Quiz")]
    public TMP_Text txtNomor;
    public TMP_Text txtPertanyaan;
    public TMP_Text txtPilihanA;
    public TMP_Text txtPilihanB;
    public TMP_Text txtPilihanC;
    public TMP_Text txtPilihanD;

    [Header("Hasil")]
    public TMP_Text txtNilai;
    public TMP_Text txtBenar;
    public TMP_Text txtSalah;
    public TMP_Text txtWaktuHasil;
    public TMP_Text txtPesan;

    [Header("Button")]
    public Button btnA;
    public Button btnB;
    public Button btnC;
    public Button btnD;
    public Button btnJawab;

    [Header("Selected")]
    public GameObject selectA;
    public GameObject selectB;
    public GameObject selectC;
    public GameObject selectD;

    [Header("Alert Benar")]
    public TMP_Text txtJawabanUserBenar;
    public TMP_Text txtPenjelasanBenar;

    [Header("Alert Salah")]
    public TMP_Text txtJawabanUserSalah;
    public TMP_Text txtJawabanBenar;
    public TMP_Text txtPenjelasanSalah;

    private int currentSoal = 0;
    private int jawabanDipilih = -1;

    private int jumlahBenar = 0;
    private int jumlahSalah = 0;

    void Start()
    {
        PanelIntro.SetActive(true);
        PanelQuiz.SetActive(false);
        PanelHasil.SetActive(false);
        AlertBenar.SetActive(false);
        AlertSalah.SetActive(false);
        Panel_PojokKanan.SetActive(false);

        btnJawab.interactable = false;

        selectA.SetActive(false);
        selectB.SetActive(false);
        selectC.SetActive(false);
        selectD.SetActive(false);

        maxWidth = progressBar.sizeDelta.x;
        progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);

        txtPersen.text = "0%";
        txtWaktu.text = "00:00";
    }

    void Update()
    {
        if (timerBerjalan)
        {
            waktu += Time.deltaTime;

            int menit = Mathf.FloorToInt(waktu / 60);
            int detik = Mathf.FloorToInt(waktu % 60);

            txtWaktu.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
    }

    void FisherYatesShuffle()
    {
        System.Random random = new System.Random();

        for (int i = daftarSoal.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            Soal temp = daftarSoal[i];
            daftarSoal[i] = daftarSoal[j];
            daftarSoal[j] = temp;
        }
    }

    public void MulaiQuiz()
    {
        PanelIntro.SetActive(false);
        PanelQuiz.SetActive(true);
        PanelHasil.SetActive(false);
        Panel_PojokKanan.SetActive(true);

        currentSoal = 0;
        jumlahBenar = 0;
        jumlahSalah = 0;
        jawabanDipilih = -1;

        FisherYatesShuffle();

        waktu = 0f;
        timerBerjalan = true;

        progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);
        txtPersen.text = "0%";

        
        UpdateProgress();
        TampilkanSoal();
    }

    void TampilkanSoal()
    {
        Soal soal = daftarSoal[currentSoal];

        txtNomor.text = "Soal " + (currentSoal + 1) + " / " + daftarSoal.Length;
        txtPertanyaan.text = soal.pertanyaan;

        txtPilihanA.text = soal.pilihanA;
        txtPilihanB.text = soal.pilihanB;
        txtPilihanC.text = soal.pilihanC;
        txtPilihanD.text = soal.pilihanD;

        jawabanDipilih = -1;

        selectA.SetActive(false);
        selectB.SetActive(false);
        selectC.SetActive(false);
        selectD.SetActive(false);

        btnJawab.interactable = false;
    }

    void UpdateProgress()
    {
        float progress = (float)currentSoal / daftarSoal.Length;

        progressBar.sizeDelta = new Vector2(
            progress * maxWidth,
            progressBar.sizeDelta.y
        );

        txtPersen.text = Mathf.RoundToInt(progress * 100f) + "%";
    }

    public void PilihJawaban(int index)
    {
        jawabanDipilih = index;

        btnJawab.interactable = true;

        selectA.SetActive(false);
        selectB.SetActive(false);
        selectC.SetActive(false);
        selectD.SetActive(false);

        switch (index)
        {
            case 0:
                selectA.SetActive(true);
                break;

            case 1:
                selectB.SetActive(true);
                break;

            case 2:
                selectC.SetActive(true);
                break;

            case 3:
                selectD.SetActive(true);
                break;
        }
    }

    public void Jawab()
    {
        if (jawabanDipilih == -1)
            return;

        

        Soal soal = daftarSoal[currentSoal];

        if (jawabanDipilih == soal.jawabanBenar)
        {
            jumlahBenar++;

            txtJawabanUserBenar.text = GetJawabanText(soal, jawabanDipilih);
            txtPenjelasanBenar.text = soal.penjelasan;

            AlertBenar.SetActive(true);
        }
        else
        {
            jumlahSalah++;

            txtJawabanUserSalah.text = GetJawabanText(soal, jawabanDipilih);
            txtJawabanBenar.text = GetJawabanText(soal, soal.jawabanBenar);
            txtPenjelasanSalah.text = soal.penjelasan;

            AlertSalah.SetActive(true);
        }
    }

    public void Lanjut()
    {
        AlertBenar.SetActive(false);
        AlertSalah.SetActive(false);

        currentSoal++;

        UpdateProgress();

        if (currentSoal >= daftarSoal.Length)
        {
            timerBerjalan = false;

            PanelQuiz.SetActive(false);
            Panel_PojokKanan.SetActive(false);

            TampilkanHasil();

            PanelHasil.SetActive(true);
            return;
        }

        PanelQuiz.SetActive(true);
        TampilkanSoal();
    }

    void TampilkanHasil()
    {
        int nilai = Mathf.RoundToInt((float)jumlahBenar / daftarSoal.Length * 100);

        txtNilai.text = nilai.ToString();
        txtBenar.text = jumlahBenar.ToString();
        txtSalah.text = jumlahSalah.ToString();

        int menit = Mathf.FloorToInt(waktu / 60);
        int detik = Mathf.FloorToInt(waktu % 60);

        txtWaktuHasil.text = string.Format("{0:00}:{1:00}", menit, detik);

        if (jumlahBenar == 10)
        {
            txtPesan.text = "Luar Biasa!";
        }
        else if (jumlahBenar >= 8)
        {
            txtPesan.text = "Sangat Baik!";
        }
        else if (jumlahBenar >= 6)
        {
            txtPesan.text = "Baik!";
        }
        else if (jumlahBenar >= 4)
        {
            txtPesan.text = "Cukup.";
        }
        else
        {
            txtPesan.text = "Jangan Menyerah!";
        }
    }

    string GetJawabanBenar(Soal soal)
    {
        switch (soal.jawabanBenar)
        {
            case 0: return soal.pilihanA;
            case 1: return soal.pilihanB;
            case 2: return soal.pilihanC;
            case 3: return soal.pilihanD;
            default: return "";
        }
    }

    string GetJawabanUser(Soal soal)
    {
        switch (jawabanDipilih)
        {
            case 0: return soal.pilihanA;
            case 1: return soal.pilihanB;
            case 2: return soal.pilihanC;
            case 3: return soal.pilihanD;
            default: return "";
        }
    }

    string GetJawabanText(Soal soal, int index)
    {
        switch (index)
        {
            case 0: return soal.pilihanA;
            case 1: return soal.pilihanB;
            case 2: return soal.pilihanC;
            case 3: return soal.pilihanD;
        }

        return "";
    }

    public void UlangiQuiz()
    {
        MulaiQuiz();
    }
}