using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneResetManager : MonoBehaviour
{
    [Header("Referensi Container Objek (2 Target)")]
    public Transform organContainer1;
    public Transform organContainer2;

    public Transform activeContainer;

    [Header("Referensi Click Detector")]
    public ObjectClickDetector clickDetector;

    [Header("Referensi Script Rotasi")]
    public CustomRotateControl customRotateControl;
    public RotateControl rotateControl;

    [Header("UI Utama")]
    public TMP_Text txtNamaObjekUtama;
    [Tooltip("Nama Default/Kategori untuk Objek 1")]
    public string namaDefaultObjek1 = "Sistem Pencernaan";
    [Tooltip("Nama Default/Kategori untuk Objek 2")]
    public string namaDefaultObjek2 = "Sistem Pernapasan";

    [Header("UI Tombol")]
    public GameObject btnFocus;
    public GameObject btnReset;

    private class TransformData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;
    }

    private Dictionary<Transform, TransformData> initialTransforms = new Dictionary<Transform, TransformData>();

    void Start()
    {
        SaveAllInitialTransforms();
        SetMainPanelTitle();

        if (btnFocus != null) btnFocus.SetActive(true);
        if (btnReset != null) btnReset.SetActive(true);
    }

  

    public void SaveAllInitialTransforms()
    {
        initialTransforms.Clear();
        SaveContainerData(organContainer1);
        SaveContainerData(organContainer2);
    }

    private void SaveContainerData(Transform container)
    {
        if (container == null) return;

        SaveTransformData(container);
        Transform[] allChildren = container.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allChildren)
        {
            SaveTransformData(t);
        }
    }

    private void SaveTransformData(Transform t)
    {
        if (!initialTransforms.ContainsKey(t))
        {
            initialTransforms.Add(t, new TransformData
            {
                localPosition = t.localPosition,
                localRotation = t.localRotation,
                localScale = t.localScale
            });
        }
    }

    public void ResetEntireScene()
    {
        if (customRotateControl != null)
            customRotateControl.ResetRotation();

        if (rotateControl != null)
            rotateControl.ResetRotation();

        if (clickDetector != null)
            clickDetector.ResetAllToDefault();

        if (activeContainer != null)
            ResetSingleContainer(activeContainer);

        SetMainPanelTitle();

        if (btnFocus != null) btnFocus.SetActive(true);
        if (btnReset != null) btnReset.SetActive(true);
    }

    private void ResetSingleContainer(Transform container)
    {
        if (container == null) return;

        ResetTransform(container);

        Transform[] allChildren = container.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in allChildren)
        {
            ResetTransform(t);

            Renderer rend = t.GetComponent<Renderer>();
            if (rend != null) rend.enabled = true;

            Collider col = t.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            if (t.name == "InfoPanel")
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void ResetTransform(Transform t)
    {
        if (initialTransforms.ContainsKey(t))
        {
            t.localPosition = initialTransforms[t].localPosition;
            t.localRotation = initialTransforms[t].localRotation;
            t.localScale = initialTransforms[t].localScale;
        }
    }

    private void SetMainPanelTitle()
    {
        if (txtNamaObjekUtama == null)
            return;

        if (activeContainer == organContainer1)
            txtNamaObjekUtama.text = namaDefaultObjek1;
        else if (activeContainer == organContainer2)
            txtNamaObjekUtama.text = namaDefaultObjek2;
        else
            txtNamaObjekUtama.text = "";
    }
}