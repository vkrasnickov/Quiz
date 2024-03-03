using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Winnings : MonoBehaviour
{
    public int winnings;
    public TextMeshProUGUI winningsText;
    // Start is called before the first frame update
    void Start()
    {
       winningsText.GetComponent<TextMeshProUGUI>(); 
       winningsText.text = ""+winnings;
    }
    private void AddWinnings()
    {
        winnings += 5000;
        winningsText.text = "Выигрыш:"+winnings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
