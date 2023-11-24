using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] float hp = 100;
    [SerializeField] float maxHP = 100;
    [SerializeField] float mp = 100;
    [SerializeField] float maxMP = 100;

    float attackBase;
    float defenseBase;
    float magicBase;
    float speedBase;

    public float level = 1;
    public float attackFinal;
    public float defenseFinal;
    public float magicFinal;
    public float speedFinal;

    Player_CollectItem inventory;
    public FloatEvent OnHPChanged;
    public FloatEvent OnMPChanged;

    void Start()
    {
        inventory = GetComponent<Player_CollectItem>();
        inventory.inventory.OnChangeEquipment.AddListener(ApplyEquipmentBonus);
        LevelUp();
    }

    public void LevelUp()
    {
        attackBase = level + 5;
        defenseBase = level + 4;
        magicBase = level + 2;
        speedBase = level + 0.2f;
        hp = level + 100;
        maxHP = level + 100;
        ApplyEquipmentBonus();
    }

    void ApplyEquipmentBonus()
    {
        SO_Armor helm = inventory.inventory.equipmentList[0].item as SO_Armor;
        SO_Armor armor = inventory.inventory.equipmentList[1].item as SO_Armor;
        SO_Weapon weapon = inventory.inventory.equipmentList[2].item as SO_Weapon;
        SO_Armor shield = inventory.inventory.equipmentList[3].item as SO_Armor;
        SO_Armor boot = inventory.inventory.equipmentList[4].item as SO_Armor;

        attackFinal = attackBase + weapon.attackBonus;

        defenseFinal = defenseBase + armor.defenseBonus + helm.defenseBonus 
            + shield.defenseBonus + boot.defenseBonus;

        magicFinal = magicBase + weapon.magicBonus;

        speedFinal = speedBase + weapon.speedBonus + armor.speedBonus + helm.speedBonus
            + shield.speedBonus + boot.speedBonus;
    }


    public void TakeDamage(float damage)
    {
        hp -= (damage - defenseFinal);
        OnHPChanged.Invoke(hp / maxHP);

        if(hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UseMP(float value)
    {
        mp -= value;
        OnMPChanged.Invoke(mp / maxMP);
    }
}
