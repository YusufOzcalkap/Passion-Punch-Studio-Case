using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Scale;
    [Header("Particle System")]
    public ParticleSystem _explosionTimeBomb;
    public ParticleSystem _explosionTimeBombScale;
    void Update()
    {
        //if time bomb is checked
        if (GameManager.instance._timeBombBullet)
            StartCoroutine(SetTimeBomb());

        //if Change Scale is checked
        if (GameManager.instance._changeScaleBullet)
            transform.localScale = new Vector3(Scale, Scale, Scale);

        //if Change Color is checked
        if (GameManager.instance._changeColorBullet)
            GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Time bomb explodes in one second
    IEnumerator SetTimeBomb()
    {
        yield return new WaitForSeconds(1f);
        if (GameManager.instance._changeScaleBullet == false) Instantiate(_explosionTimeBomb, transform.position, Quaternion.identity);
        if (GameManager.instance._changeScaleBullet == true) Instantiate(_explosionTimeBombScale, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
