using UnityEngine;

public class CanvasMenu : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private Canvas inputCanvas;

    public void OnClick_Start()
    {
        myCanvas.enabled = false;
        inputCanvas.enabled = true;
        GameManager.Instance.Play();
    }
}
