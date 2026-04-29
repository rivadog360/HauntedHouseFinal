using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private bool canAttack = true;
    private Collider attackHitbox;
    [SerializeField] private ParticleSystem attackPart;
    public GameObject attackIcon;
    void Start()
    {
        attackHitbox = transform.Find("AttackHitbox").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canAttack)
            {
                attackPart.Play();
                attackHitbox.enabled = true;
                Invoke("removeHitbox", 1f);
                attackIcon.SetActive(false);
            }
            else
            {
                print("Cant attack");
            }
        }
    }
    void removeHitbox()
    {
        attackPart.Stop();
        attackHitbox.enabled = false;
        canAttack = false;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
