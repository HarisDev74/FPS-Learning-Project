using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class wea : MonoBehaviour
{
    public Transform muzzlePoint;
    public float shootRange = 100f;
    public float shootforce = 30f;
    public GameObject muzzleFlashanimator;
    public float flashduration = 0.33f;

    public GameObject bulletImpact;

    public AudioSource gunAudio;
    public AudioClip shootSound;

    public Transform playerCamera;

    public float recoil = 0.5f;
    public float recoilSpeed = 8f;

    private Vector3 currentRotation = Vector3.zero;
    private Vector3 targetRotation = Vector3.zero;


    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();

            targetRotation += new Vector3(-recoil, UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
        }
            currentRotation = Vector3.Lerp(currentRotation, targetRotation, recoilSpeed * Time.deltaTime);
            playerCamera.localRotation *= quaternion.Euler(currentRotation);
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, recoilSpeed * Time.deltaTime);
    }

    void Shoot()
    {

        if (shootSound != null && gunAudio != null)
        {
            gunAudio.PlayOneShot(shootSound);
        }
        RaycastHit hit;

        
        if (muzzleFlashanimator != null)
        {
            StartCoroutine(PlayMuzzleFlash());
        }



        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, shootRange))
        {
            Debug.Log("hit : " + hit.transform.name + " at distance " + hit.distance);

            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hit.distance, Color.red, 1f);

            GameObject ImpactGo = Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(ImpactGo, 1.5f);


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

