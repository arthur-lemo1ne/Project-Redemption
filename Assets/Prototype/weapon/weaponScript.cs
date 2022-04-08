using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float FireRate = 1f;
    public float ReloadTime = 3f;
    public int magSize = 7;

    public Camera fpsCam;
    public Transform Ejection;
    public ParticleSystem MuzzleFlash;
    public GameObject Impact;
    public GameObject Caldridg;
    public Transform player;

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
        LayerMask mask = 1<<7;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~mask))
        {
            if (hit.transform.CompareTag("Destructible"))
            {
                destructible src = hit.transform.GetComponent<destructible>();
                src.Kill();
            }else if(hit.transform.CompareTag("Enemi"))
            {
                Enemie src = hit.transform.GetComponent<Enemie>();
                src.TakeDamage(damage);
            }else
            { 
                GameObject impactGO = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
        Invoke("SpawnCaldridge", 0.1f);
    }

    void Reload()
    {
        Anim.SetBool("Empty", false);

        BulletsLeft = magSize;
        
    }

    void SpawnCaldridge()
    {
        GameObject bullt = Instantiate(Caldridg, Ejection.position, player.rotation);
        bullt.transform.localScale /= 6f;
        Rigidbody bul = bullt.GetComponentInChildren<Rigidbody>();
        bul.AddRelativeForce(new Vector3(10f, 0f, -10f));
        bul.AddRelativeTorque(new Vector3(-5f, 0f, 0f));
        Destroy(bullt, 2f);
    }
}
