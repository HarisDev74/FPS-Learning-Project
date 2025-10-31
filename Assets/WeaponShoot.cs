using System.Collections;
using UnityEngine;

public class wea : MonoBehaviour
{
    public Transform muzzlePoint;
    public float shootRange = 100f;
    public float shootforce = 30f;
    public GameObject muzzleFlashanimator;
    public float flashduration = 0.33f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;


        if (muzzleFlashanimator != null)
        {
            StartCoroutine(PlayMuzzleFlash());
        }



        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, shootRange))
        {
            Debug.Log("hit : " + hit.transform.name + " at distance " + hit.distance);

            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hit.distance, Color.red, 1f);
        }
        else
        {
            Debug.Log("Missed!");
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * shootRange, Color.yellow, 1f);
        }
    }
    IEnumerator PlayMuzzleFlash()
    {
        muzzleFlashanimator.SetActive(true);
        yield return new WaitForSeconds(flashduration);
        muzzleFlashanimator.SetActive(false);
    }

}

