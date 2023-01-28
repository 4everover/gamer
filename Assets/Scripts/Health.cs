using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    MonoBehaviour entityScript;
    Animator animator;

    [SerializeField] float health = 50;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material[] gotHitMaterials;

    float initialHealth;
    Material[] initialMaterials;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PlayerController>()) { entityScript = GetComponent<PlayerController>(); }
        else if (GetComponent<RabbitController>()) { entityScript = GetComponent<RabbitController>(); }
        else if (GetComponent<RabbitBossController>()) { entityScript = GetComponent<RabbitBossController>(); }
        animator = GetComponent<Animator>();
        initialHealth = health;
        initialMaterials = skinnedMeshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            StartCoroutine(InitiateDeathSequence());
        }
        else
        {
            if (GetComponent<EnemyAudio>())
            {
                GetComponent<EnemyAudio>().PlayGetHitSound();
            }
            else if (GetComponent<PlayerAudio>())
            {
                GetComponent<PlayerAudio>().PlayGetHitSound();
            }
            StartCoroutine(FlashFromHit());
        }
    }

    IEnumerator FlashFromHit()
    {
        skinnedMeshRenderer.materials = gotHitMaterials;
        yield return new WaitForSeconds(0.2f);
        skinnedMeshRenderer.materials = initialMaterials;
    }

    public void ResetHealth()
    {
        health = initialHealth;
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    IEnumerator InitiateDeathSequence()
    {
        Destroy(entityScript);
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
