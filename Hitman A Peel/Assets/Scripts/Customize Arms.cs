using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject costume, arms;

    public int skinNr, weaponsNr, currentSprite;

    public Skins[] skins;
    public Weaponry[] weapons;

    SpriteRenderer costumeSpriteRenderer, armsSpriteRenderer, spriteRenderer;

    // This spriteNr is helpful to easily add more accessories using the CustomizableAccessories.cs script
    public int spriteNr;

    void Start()
    {
        costumeSpriteRenderer = costume.GetComponent<SpriteRenderer>();
        armsSpriteRenderer = arms.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        currentSprite = int.Parse(spriteRenderer.sprite.name.Split('_')[1]);

        if (skinNr > skins.Length - 1) skinNr = 0;
        else if (skinNr < 0) skinNr = skins.Length - 1;
        if (weaponsNr > weapons.Length - 1) weaponsNr = 0;
        else if (weaponsNr < 0) weaponsNr = weapons.Length - 1;

    }

    void LateUpdate()
    {
        SkinChoice(costumeSpriteRenderer);
        WeaponryChoice(armsSpriteRenderer);
    }

    void SkinChoice(SpriteRenderer SPR)
    {
        SPR.sprite = skins[skinNr].sprites[currentSprite];
    }

    void WeaponryChoice(SpriteRenderer SPR)
    {
        SPR.sprite = weapons[weaponsNr].sprites[currentSprite];
    }
}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}

[System.Serializable]
public struct Weaponry
{
    public Sprite[] sprites;
}