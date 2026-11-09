using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectClickDetector : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text txtNamaObjek;
    public GameObject btnFocus;
    public GameObject btnReset;

    [Header("Focus Mode & Containers")]
    // Masukkan SEMUA parent objek/organ kamu ke dalam List ini melalui Inspector
    public List<Transform> allOrganContainers = new List<Transform>();

    private Transform activeOrganContainer; // Container yang sedang terpilih saat diklik
    private GameObject currentPanel;
    private Transform currentHit;
    private bool focusMode = false;

    // Highlight & Memory Position
    private Renderer currentRenderer;
    private Coroutine currentHighlight;

    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    void Start()
    {
        if (btnFocus != null) btnFocus.SetActive(true);
        if (btnReset != null) btnReset.SetActive(true);

        // Rekam posisi awal dari SEMUA container yang didaftarkan di Inspector
        foreach (Transform container in allOrganContainers)
        {
            if (container == null) continue;

            Renderer[] allRenderers = container.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer r in allRenderers)
            {
                if (!originalColors.ContainsKey(r))
                    originalColors.Add(r, r.material.color);

                Transform t = r.transform;
                if (!originalPositions.ContainsKey(t))
                {
                    originalPositions.Add(t, t.localPosition);
                    originalRotations.Add(t, t.localRotation);
                }
            }
        }
    }

    void Update()
    {
        if (focusMode) return;
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 screenPos = Pointer.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                txtNamaObjek.text = hit.transform.name;
                currentHit = hit.transform;

                // Cari container mana yang menaungi objek yang sedang diklik
                activeOrganContainer = GetParentContainer(hit.transform);

                ResetHighlightToNormal();
                currentHighlight = StartCoroutine(HighlightObject(hit.transform));

                // ===== Info Panel =====
                Transform organ = hit.transform.parent;
                if (organ != null)
                {
                    Transform infoPanel = organ.Find("InfoPanel");
                    if (infoPanel != null)
                    {
                        if (currentPanel != null) currentPanel.SetActive(false);
                        infoPanel.gameObject.SetActive(true);
                        currentPanel = infoPanel.gameObject;
                    }
                }
            }
        }
    }

    // Helper untuk mendeteksi container teratas dari objek yang diklik
    private Transform GetParentContainer(Transform target)
    {
        foreach (Transform container in allOrganContainers)
        {
            if (target.IsChildOf(container))
                return container;
        }
        return target.root; // Fallback jika tidak ditemukan
    }

    IEnumerator HighlightObject(Transform obj)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null && originalColors.ContainsKey(rend))
        {
            currentRenderer = rend;
            rend.material.color = Color.green;
            yield return new WaitForSeconds(2f);
            rend.material.color = originalColors[rend];
        }
        currentRenderer = null;
        currentHighlight = null;
    }

    private void ResetHighlightToNormal()
    {
        if (currentHighlight != null)
        {
            StopCoroutine(currentHighlight);
            currentHighlight = null;
        }

        if (currentRenderer != null && originalColors.ContainsKey(currentRenderer))
        {
            currentRenderer.material.color = originalColors[currentRenderer];
            currentRenderer = null;
        }
    }

    public void ToggleFocusMode()
    {
        if (currentHit == null || activeOrganContainer == null) return;

        focusMode = !focusMode;

        // Terapkan isolasi Focus hanya pada Container objek yang aktif
        Collider[] allColliders = activeOrganContainer.GetComponentsInChildren<Collider>(true);
        foreach (Collider col in allColliders)
        {
            col.enabled = focusMode ? (col.transform == currentHit) : true;
        }

        Renderer[] renderers = activeOrganContainer.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in renderers)
        {
            r.enabled = focusMode ? (r.transform == currentHit) : true;
        }

        ResetHighlightToNormal();
        currentHighlight = StartCoroutine(HighlightObject(currentHit));
    }

    public void ResetAllToDefault()
    {
        if (activeOrganContainer == null)
            return;

        ResetHighlightToNormal();

        if (activeOrganContainer != null)
        {
            Collider[] allColliders = activeOrganContainer.GetComponentsInChildren<Collider>(true);
            foreach (Collider col in allColliders)
            {
                col.enabled = true;
            }

            Renderer[] allRenderers = activeOrganContainer.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer r in allRenderers)
            {
                r.enabled = true;

                Transform t = r.transform;

                if (originalPositions.ContainsKey(t))
                {
                    t.localPosition = originalPositions[t];
                    t.localRotation = originalRotations[t];
                }
            }
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
            currentPanel = null;
        }

        if (txtNamaObjek != null)
            txtNamaObjek.text = "";

        if (btnFocus != null)
            btnFocus.SetActive(true);

        if (btnReset != null)
            btnReset.SetActive(true);

        currentHit = null;
        activeOrganContainer = null;
        focusMode = false;
    }
}