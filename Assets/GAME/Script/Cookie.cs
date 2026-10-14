using UnityEngine;
using UnityEngine.InputSystem;

// LE COOKIE : détecte le clic et prévient le GameManager.
// À glisser sur le sprite du cookie. Unity ajoute un Collider 2D si besoin
// (mettez de préférence un "Circle Collider 2D").
[RequireComponent(typeof(Collider2D))]
public class Cookie : MonoBehaviour
{
    public float clickScale = 0.9f;   // le cookie rétrécit un peu au clic
    public float returnSpeed = 10f;   // vitesse de retour à la taille normale

    Collider2D cookieCollider;
    Vector3 normalScale;

    void Start()
    {
        cookieCollider = GetComponent<Collider2D>();
        normalScale = transform.localScale;
    }

    void Update()
    {
        // Le cookie reprend doucement sa taille normale
        transform.localScale = Vector3.Lerp(transform.localScale, normalScale, returnSpeed * Time.deltaTime);

        // Pointer = souris OU doigt sur un écran tactile (New Input System)
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            // 1. Où est la souris à l'écran (en pixels) ?
            Vector2 screenPosition = Pointer.current.position.ReadValue();

            // 2. On convertit en position dans le monde du jeu
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // 3. Est-ce que ce point est dans le collider du cookie ?
            if (cookieCollider.OverlapPoint(worldPosition))
            {
                OnCookieClicked();
            }
        }
    }

    void OnCookieClicked()
    {
        GameManager.Instance.AddCookies();

        // Petit effet visuel : le cookie s'écrase
        transform.localScale = normalScale * clickScale;
    }
}
