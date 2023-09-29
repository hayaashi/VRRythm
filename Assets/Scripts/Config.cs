using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Config : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject bar;
    [SerializeField] Toggle toggle;

    [SerializeField] Slider playerHeightSlider;
    [SerializeField] Transform playerHeight;
    [SerializeField] Slider playerWeaponRotX;
    [SerializeField] Slider playerWeaponRotZ;

    [SerializeField] Transform RightHandAnchor;
    [SerializeField] Transform LeftHandAnchor;
    [SerializeField] Transform Bar;

    public TextMeshProUGUI tmpro_playerHeight;
    public TextMeshProUGUI tmpro_rotX;
    public TextMeshProUGUI tmpro_rotZ;
    public TextMeshProUGUI tmpro_bar;

    public float m_force = 20;
    public float m_radius = 5;
    
    Vector3 defaultbar;

    int enable;

    [SerializeField] LaserPointer laserPointer;
    // Start is called before the first frame update
    void Start()
    {
        defaultbar = bar.transform.localScale;
        OnToggleChange(false);
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch) == true)
        {
            enable++;
        }

        if(enable %2 == 0)
        {
            laserPointer.enabled = true;
            GetComponent<CanvasGroup>().alpha = 1f;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            laserPointer.enabled = false;
            GetComponent<CanvasGroup>().alpha = 0f;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }


        playerHeight.position = new Vector3(0f, playerHeightSlider.value, 0f);

        bar.transform.localScale =
            new Vector3(defaultbar.x, slider.value * defaultbar.y, defaultbar.z);
        bar.transform.localPosition =
            new Vector3(0f, (bar.transform.localScale.y / 2f), 0f);

        if (toggle.isOn == false)
        {
            Bar.transform.parent = RightHandAnchor;
            Bar.transform.localPosition = new Vector3(0f, 0f, 0f);
            Bar.transform.localEulerAngles = new Vector3(90f + playerWeaponRotX.value, 0f, 0f + playerWeaponRotZ.value);
        }
        else
        {
            Bar.transform.parent = LeftHandAnchor;
            Bar.transform.localPosition = new Vector3(0f, 0f, 0f);
            Bar.transform.localEulerAngles = new Vector3(90f + playerWeaponRotX.value, 0f, 0f + playerWeaponRotZ.value);
        }

        tmpro_bar.text = (slider.value * defaultbar.y).ToString("N2");
        tmpro_playerHeight.text = playerHeightSlider.value.ToString("N2");
        tmpro_rotX.text = playerWeaponRotX.value.ToString("N2");
        tmpro_rotZ.text = playerWeaponRotZ.value.ToString("N2");
    }

    // Update is called once per frame
    public void OnToggleChange(bool tog)
    {
        if (toggle.isOn == false)
        {
            Bar.transform.parent = RightHandAnchor;
            Bar.transform.localPosition = new Vector3(0f, 0f, 0f);
            Bar.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
        else
        {
            Bar.transform.parent = LeftHandAnchor;
            Bar.transform.localPosition = new Vector3(0f, 0f, 0f);
            Bar.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
    }

}
