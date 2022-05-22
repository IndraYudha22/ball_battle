using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorFraction", menuName = "ScriptableObject/Colors/Color Fraction")]
public class SO_ColorFraction : ScriptableObject
{
    [Header("COLOR")]
    [SerializeField] private Color colorPlayer;
    [SerializeField] private Color colorEnemy;
    [SerializeField] private Color deactivateSoldier;

    [Header("MATERIAL")]
    [SerializeField] private Material materialPlayer;
    [SerializeField] private Material materialEnemy;
    [SerializeField] private Material materialDeactivate;

    public Color SetColorDisable()
    { 
        return deactivateSoldier;
    }

    public Color SetColor(Fractions fraction, float transparant = 1f)
    {
        Color _color;

        if (fraction == Fractions.player)
        {
            _color = colorPlayer;
            _color.a = transparant;
        }
        else
        {
            _color = colorEnemy;
            _color.a = transparant;
        }

        return _color;
    }

    public Material SetMaterialDisable()
    {
        return materialDeactivate;
    }

    public Material SetMaterial(Fractions fractions)
    {
        Material _material;

        if (fractions == Fractions.player)
        {
            _material = materialPlayer;
        }
        else
        {
            _material = materialEnemy;
        }

        return _material;
    }
}
