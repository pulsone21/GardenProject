using UnityEngine;

public class OnHoverHighlight : MonoBehaviour
{
    [SerializeField] public Renderer myRenderer;
    private Material mateiral;

    private void Awake()
    {
        myRenderer = gameObject.GetComponentInChildren<Renderer>();
        mateiral = myRenderer.material;
    }

    protected void OnMouseEnter()
    {
        mateiral.SetFloat("_Highlighter", 1f);
    }

    protected void OnMouseExit()
    {
        mateiral.SetFloat("_Highlighter", 0f);
    }
}