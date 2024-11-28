using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCharacter : MonoBehaviour
{
    [SerializeField]
    bool EnemyCostume = false;

    [SerializeField]
    GameObject Costume, Arms;

    public int skinNr, armsNr, currentSprite;

    // This is where your spritesheets go
    // In the inspector, set the size to, for example 5, if you have 5 spritesheets
    // Then open each individual element and add the individual sprites from the spritesheets in here
    // This means if your spritesheet has 10 frames, the Sprites element in the inspector needs to contain these 10 sprites
    public Skins[] skins;
    public Weaponry[] weapons;

    SpriteRenderer costumeSpriteRenderer, armsSpriteRenderer, spriteRenderer;

    // This spriteNr is helpful to easily add more accessories using the CustomizableAccessories.cs script
    public int spriteNr;

    void Start()
    {
        costumeSpriteRenderer = Costume.GetComponent<SpriteRenderer>();
        armsSpriteRenderer = Arms.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        currentSprite = int.Parse(spriteRenderer.sprite.name.Split('_')[1]);
        if (skinNr > skins.Length - 1) skinNr = 0;
        else if (skinNr < 0) skinNr = skins.Length - 1;
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
        SPR.sprite = weapons[armsNr].sprites[currentSprite];
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