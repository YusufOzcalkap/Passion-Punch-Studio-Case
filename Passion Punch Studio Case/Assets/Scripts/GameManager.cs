using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int _shotGunBullets;
    public Animator _aimAnimator;

    [Header("Guns")]
    public GameObject _gun;
    public GameObject _shutGun;
    public TextMeshProUGUI _gunText;
    [HideInInspector] public int _gunBulletCount;
    public TextMeshProUGUI _shotGunText;
    [HideInInspector]public int _shotGunBulletCount;

    [HideInInspector]
    public bool _timeBombBullet;
    public bool _changeScaleBullet;
    public bool _changeColorBullet;

    public Image[] _ButtonsTick;
    public Material[] _shutgunMaterials;

    [Header("Counts")]
    private int CountChangegun = 0;
    private int CountTimeBomb = 0;
    private int CountChangeScale = 0;
    private int CountChangeColor = 0;

    void Awake()
    {
        instance = this;
        _shutGun.SetActive(true);
        _gunText.gameObject.SetActive(false);
    }

    void Update()
    {
        _gunText.text = _gunBulletCount.ToString();
        _shotGunText.text = _shotGunBulletCount.ToString();

        // Change Guns Color
        if (!_timeBombBullet && !_changeScaleBullet && !_changeColorBullet)
            _shutGun.transform.GetChild(1).GetComponent<MeshRenderer>().material = _shutgunMaterials[0];

        if (_timeBombBullet)
            _shutGun.transform.GetChild(1).GetComponent<MeshRenderer>().material = _shutgunMaterials[2];

        if (_changeColorBullet)
            _shutGun.transform.GetChild(1).GetComponent<MeshRenderer>().material = _shutgunMaterials[1];

        // marking of buttons
        if (Input.GetKeyDown(KeyCode.Q)) SetButtonTimeBomb();
        if (Input.GetKeyDown(KeyCode.E)) SetButtonChangeScale();
        if (Input.GetKeyDown(KeyCode.R)) SetButtonChangeColor();
        if (Input.GetKeyDown(KeyCode.F)) ChangeGun();
    }

    #region functions of keys
    public void ChangeGun()
    {
        CountChangegun++;

        if (CountChangegun % 1 == 0)
        {
            _gun.SetActive(true);
            _shutGun.SetActive(false);
            _shotGunText.gameObject.SetActive(false);
            _gunText.gameObject.SetActive(true);
        }

        if (CountChangegun % 2 == 0)
        {
            _gun.SetActive(false);
            _shutGun.SetActive(true);
            _shotGunText.gameObject.SetActive(true);
            _gunText.gameObject.SetActive(false);
        }
    }

    public void SetButtonTimeBomb()
    {
        CountTimeBomb++;

        if (CountTimeBomb % 1 == 0)
        {
            _timeBombBullet = true;
            _ButtonsTick[0].gameObject.SetActive(true);
        }

        if (CountTimeBomb % 2 == 0)
        {
            _timeBombBullet = false;
            _ButtonsTick[0].gameObject.SetActive(false);
        }
    }

    public void SetButtonChangeScale()
    {
        CountChangeScale++;

        if (CountChangeScale % 1 == 0)
        {
            _changeScaleBullet = true;
            _ButtonsTick[1].gameObject.SetActive(true);
        }

        if (CountChangeScale % 2 == 0)
        {
            _changeScaleBullet = false;
            _ButtonsTick[1].gameObject.SetActive(false);
        }
    }

    public void SetButtonChangeColor()
    {
        CountChangeColor++;

        if (CountChangeColor % 1 == 0)
        {
            _changeColorBullet = true;
            _ButtonsTick[2].gameObject.SetActive(true);
        }

        if (CountChangeColor % 2 == 0)
        {
            _changeColorBullet = false;
            _ButtonsTick[2].gameObject.SetActive(false);
        }
    }
    #endregion
}
