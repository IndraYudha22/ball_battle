using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorFaction", menuName = "ScriptableObject/Colors/Color Faction")]
public class SO_ColorFraction : ScriptableObject
{
    [SerializeField] private Color colorPlayer;
    [SerializeField] private Color colorEnemy;

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

    public void SetMaterial(GameObject gameObject, Fractions fraction, float transparant = 1f)
    {
        var _color = gameObject.GetComponent<Renderer>().material.color;

        _color = (fraction ==  Fractions.player) ? colorPlayer : colorEnemy;
        _color.a = transparant;
    }

    public Color GetColorPlayer(){
        return colorPlayer;
    }

    public Color GetColorEnemy(){
        return colorEnemy;
    }

    public void SetMaterialPlayer(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = colorPlayer;
    }

    public void SetMaterialEnemy(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = colorEnemy;
    }
}
