using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UNE AMÉLIORATION DU SHOP : un seul script pour les 3 boutons !
// Chaque bouton a ses propres réglages dans l'Inspector (nom, prix, bonus).
// À glisser sur un Button (UI). Dans le composant Button :
// On Click () → + → glisser le bouton → choisir ShopUpgrade > Buy
[RequireComponent(typeof(Button))]
public class ShopUpgrade : MonoBehaviour
{
    public string upgradeName = "Grandma";
    public int cost = 25;
    public float costMultiplier = 1.5f; // le prix augmente à chaque achat
    public int bonusPerClick = 0;        // cookies en plus à chaque clic
    public float bonusPerSecond = 1f;    // cookies gagnés automatiquement

    public TextMeshProUGUI label;        // le texte du bouton

    int level = 0;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        UpdateLabel();
    }

    void Update()
    {
        // Le bouton est grisé si on n'a pas assez de cookies
        button.interactable = GameManager.Instance.GetCookies() >= cost;
    }

    // Appelée par le bouton (réglé dans On Click de l'Inspector)
    public void Buy()
    {
        if (!GameManager.Instance.SpendCookies(cost))
        {
            return; // pas assez de cookies
        }

        GameManager.Instance.AddBonus(bonusPerClick, bonusPerSecond);

        level++;
        cost = Mathf.RoundToInt(cost * costMultiplier);
        UpdateLabel();
    }

    void UpdateLabel()
    {
        // "\n" = retour à la ligne
        label.text = upgradeName + " (lvl " + level + ")\n" + cost + " cookies";
    }
}
