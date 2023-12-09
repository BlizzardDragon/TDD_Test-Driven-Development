using UnityEngine;
using UnityEngine.UI;

public class ViewSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    public Button Button;


    public void Show(Sprite icon)
    {
        _image.sprite = icon;
        _image.enabled = true;
    }

    public void Hide()
    {
        _image.enabled = false;
        _image.sprite = null;
    }
}
