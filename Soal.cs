using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Soal
{
    [Header("Pertanyaan")]
    [TextArea(2, 5)]
    public string pertanyaan;

    [Header("Pilihan Jawaban")]
    public string pilihanA;
    public string pilihanB;
    public string pilihanC;
    public string pilihanD;

    [Header("Jawaban Benar")]
    [Range(0, 3)]
    public int jawabanBenar;
    // 0 = A
    // 1 = B
    // 2 = C
    // 3 = D

    [Header("Penjelasan")]
    [TextArea(3, 6)]
    public string penjelasan;
}