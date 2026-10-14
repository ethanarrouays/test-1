using UnityEngine;
using TMPro;

// LE CERVEAU DU JEU : il garde le score et met à jour l'affichage.
// Il n'y a qu'un seul GameManager dans la scène, alors on le rend
// accessible partout avec "Instance".
// À glisser sur un objet vide nommé "GameManager".
public class GameManager : MonoBehaviour
{
    // "static" = partagé : n'importe quel script peut écrire GameManager.Instance
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;      // "125 cookies"
    public TextMeshProUGUI perSecondText;  // "3 per second"

    public int cookiesPerClick = 1;
    public float cookiesPerSecond = 0f;

    // float, car la production automatique ajoute des morceaux de cookie à chaque image
    float cookies = 0f;

    // Awake() est appelée avant tous les Start()
    void Awake()
    {
        Instance = this;
    }

    // Update() est appelée à chaque image
    void Update()
    {
        cookies += cookiesPerSecond * Time.deltaTime;
        UpdateUI();
    }



    // Appelée par le Cookie quand on clique dessus
    public void AddCookies()
    {
        cookies += cookiesPerClick;

        Debug.Log("Nombre cookie :" + cookies);
    }

    // Appelée par le shop : combien de cookies a-t-on ?
    public float GetCookies()
    {
        return cookies;
    }

    // Appelée par le shop : on paie si on a assez. Renvoie true si l'achat a marché.
    public bool SpendCookies(int amount)
    {
        if (cookies < amount)
        {
            return false;
        }

        cookies -= amount;
        return true;
    }

    // Appelée par le shop quand on achète une amélioration
    public void AddBonus(int bonusPerClick, float bonusPerSecond)
    {
        cookiesPerClick += bonusPerClick;
        cookiesPerSecond += bonusPerSecond;
    }

    void UpdateUI()
    {
        // Mathf.FloorToInt = on arrondit vers le bas pour ne pas afficher de virgule
        scoreText.text = "cookies " + Mathf.FloorToInt(cookies);
        //perSecondText.text = cookiesPerSecond + " per second";
    }
}
