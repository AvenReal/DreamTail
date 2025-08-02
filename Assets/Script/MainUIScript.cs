using TMPro;
using UnityEngine;

public class MainUIScript : MonoBehaviour
{
    private PlayerScript Player => PlayerScript.Instance;
    public TMP_Text JumpText;
    public TMP_Text DashText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JumpText.text = $"Number of Dashes : {Player.NbOfJumps}/{Player.MaxNbOfJumps}";
        DashText.text = $"Number of Dashes : {Player.NbOfDashes}/{Player.MaxNbOfDashes}";
    }
}
