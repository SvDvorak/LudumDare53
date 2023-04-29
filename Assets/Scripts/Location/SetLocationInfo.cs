using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetLocationInfo : MonoBehaviour
{
    public Image Picture;
    public TMP_Text Name;
    public TMP_Text Description;

    public void SetLocation(GameInfo.Location location)
    {
        Name.text = location.Name;
        Description.text = location.Description;
    }
}
