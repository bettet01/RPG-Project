﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    Transform cam;

    Transform ui;
    Image healthSlider;

    float visableTime = 5f;
    float lastMadeVisableTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += onHealthChanged;

        ui.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if(Time.time - lastMadeVisableTime > visableTime)
            {
                ui.gameObject.SetActive(false);
            }
        }

    }


    void onHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisableTime = Time.time;
            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }

    }
}
