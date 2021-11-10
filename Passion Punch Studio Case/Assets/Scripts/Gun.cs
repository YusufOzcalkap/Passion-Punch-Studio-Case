using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    [Header("Guns Tools")]
    public int index;
    public Camera _fpsCam;
    public float _damage;
    public float _range;
    public float _spread;
    public GameObject _pellet;
    public Transform _barrelExit;

    [Header("Particle System")]
    public GameObject _impact;
    public ParticleSystem _Fireimpact;

    void Update()
    {
        // Fire
        if (Input.GetButtonDown("Fire1") && !IsMouseOverUI()) Fire();
        if (Input.GetButton("Fire1") && !IsMouseOverUI() && index == 1) Fire();
    }
    
    private void Fire()
    {
        #region Gun
        if (index == 1)
        {
            GameManager.instance._gunBulletCount++;
            RaycastHit hit;
            if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out hit, _range))
            {
                Instantiate(_Fireimpact, _barrelExit.position, Quaternion.identity);
                var Barrel = Instantiate(_pellet, _barrelExit.position, Quaternion.LookRotation(hit.normal));
                Barrel.GetComponent<GunBullet>().target = hit.point;
                var impactEffect = Instantiate(_impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactEffect.gameObject, 10f);
                if (GameManager.instance._timeBombBullet == false) Destroy(Barrel.gameObject, 0.5f);
                if (hit.transform.CompareTag("Target"))
                    hit.transform.GetChild(0).GetChild(0).transform.DOShakeRotation(2f, new Vector3(50, 50, 50), randomness: 2).SetLoops(0);
            }
        }
        #endregion

        #region Shotgun
        if (index == 2)
        {
            _Fireimpact.Play();
            for (int i = 0; i < GameManager.instance._shotGunBullets; i++)
            {
                Transform t_spawn = _fpsCam.transform;

                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
                t_bloom += Random.Range(-_spread, _spread) * t_spawn.up;
                t_bloom += Random.Range(-_spread, _spread) * t_spawn.right;
                t_bloom -= t_spawn.position;
                t_bloom.Normalize();

                for (int a = 0; a < 1; a++)
                {
                    GameManager.instance._shotGunBulletCount++;
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(t_spawn.position, t_bloom, out hit, _range))
                    {
                        var Barrel = Instantiate(_pellet, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal)) as GameObject;
                        var impactEffect = Instantiate(_impact, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal)) as GameObject;
                        Barrel.transform.LookAt(hit.point + hit.normal);

                        if (GameManager.instance._timeBombBullet == false) Destroy(Barrel.gameObject, 0.5f);

                        Destroy(impactEffect.gameObject, 10f);
                        impactEffect.transform.LookAt(hit.point + hit.normal);
                        if (hit.transform.CompareTag("Target"))
                            hit.transform.GetChild(0).GetChild(0).transform.DOShakeRotation(2f, new Vector3(50, 50, 50), randomness: 2).SetLoops(0);                       
                    }
                }
            }
        }
        #endregion
    }

    //Function written in UI to not fire when pressing something
    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
