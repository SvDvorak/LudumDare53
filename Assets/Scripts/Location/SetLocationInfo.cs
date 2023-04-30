using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLocationInfo : MonoBehaviour
{
    public Image Picture;
    public TMP_Text Name;
    public TMP_Text Description;
    public LocationImages Images;

    public void SetLocation(GameInfo.Location location)
    {
        Picture.sprite = Images.GetImage(location.ID);
        Name.text = location.Name;
        Description.text = location.Description;
    }
}
