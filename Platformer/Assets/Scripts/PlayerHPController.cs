using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public GameObject deatUI;
    public Slider hpSlider;
    public GameObject playerModel;

    private void Start()
    {
        currentHP = maxHP;
        deatUI.SetActive(false);
        UpdateHPUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
        else if (other.CompareTag("HealthPack"))
        {
            Heal(20, other.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            currentHP = 0;
            PlayerDeath();
        }
        UpdateHPUI();
    }

    public void Heal(int healAmount, GameObject other)
    {
        currentHP += healAmount;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
            Destroy(other);
        }
        UpdateHPUI();
    }

    private void PlayerDeath()
    {
        deatUI.SetActive(true);
        playerModel.SetActive(false);
    }

    private void UpdateHPUI()
    {
        hpSlider.value = currentHP;
    }
}
