using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float FireRate = 1f;
    public float ReloadTime = 3f;
    public int magSize = 7;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject Impact;

    private int BulletsLeft;
    private float lastFire = 0f;
    private float StartReload = 0f;
    private bool isReloading = false;
    private Animator Anim;

    private void Start()
    {
        BulletsLeft = magSize;
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetBool("IsReloading", isReloading);
        if (Input.GetButton("Fire1") && BulletsLeft > 0 && (Time.time > lastFire + FireRate) && !isReloading)
        {
            Shoot();
            BulletsLeft--;
            lastFire = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload = Time.time;
            isReloading = true;
            Reload();
            
        }
        if (Time.time >= StartReload + ReloadTime)
        {
            isReloading = false;
        }
    }

    void Shoot()
    {
        if(BulletsLeft == 1)
        {
            Anim.SetBool("Empty", true);
        }
        Anim.SetTrigger("Shoot");
        MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            GameObject impactGO = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void Reload()
    {
        Anim.SetBool("Empty", false);

        BulletsLeft = magSize;
        
    }
}
