using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Question
{
    public string Text;
    public string[] Answers;
    [Range(0,3)]
    public byte correctIndex;
}

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [Header("Answers")]
    [SerializeField] private Button[] answerButtons;
    private TMP_Text[] answerButtonsText;
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource uncorrectSound;
    [SerializeField] private int winnings = 0;
    [SerializeField] private TMP_Text winningsText;
    [SerializeField] private TMP_Text winningsText1;
    [Header("Tips")]
    [SerializeField] private Button tip50;
    [SerializeField] private AudioSource tip50Sound;
    [SerializeField] private Button tipCall;
    [SerializeField] private AudioSource tipCallSound;
    [SerializeField] private Button tipPeople;
    [SerializeField] private AudioSource tipPeopleSound;
    [Range(0,100)][SerializeField] private byte callPersent = 60;
    [Header("Questions")]
    [SerializeField] private Question[] questions;
    [Header("Test")]
    [SerializeField] private byte currentIndex = 0;
    private void Awake()
    {
        //winningsText.GetComponent<TextMeshProUGUI>();
        winningsText = GetComponent<TMP_Text>();
        winningsText1 = GetComponent<TMP_Text>();
       // winningsText.text = "Выигрыш:"+100;
        var length = answerButtons.Length;
        answerButtonsText = new TMP_Text[length];
        for(var i=0;i<length;i++)
        {
            answerButtonsText[i] = answerButtons[i].GetComponentInChildren<TMP_Text>();
        }
    }
    private void SetQuestion()
    {
        var currentQuestion = questions[currentIndex];
        questionText.text = currentQuestion.Text;
        for(var i=0;i<answerButtons.Length;i++)
        {
            var text = currentQuestion.Answers[i];
            switch(i)
            {
                case 0:
                text = $"A: {text}";
                break;
                case 1:
                text = $"B: {text}";
                break;
                case 2:
                text = $"C: {text}";
                break; 
                case 3:
                text = $"D: {text}";
                break;
            }
            answerButtonsText[i].text = text;
            answerButtons[i].gameObject.SetActive(true);
        }
    }
    private void EndGame()
    {
        winningsText1.text = ""+winnings;
        SceneManager.LoadScene("GameOver");
        Debug.Log("Game Over");
        // questionText.text = "Game Over";
        // for(var i=0;i<answerButtons.Length;i++)
        // {
        //     answerButtons[i].gameObject.SetActive(false);
        // }
        // tip50.gameObject.SetActive(false);
        // tipPeople.gameObject.SetActive(false);
        // tipCall.GetComponentInChildren<TMP_Text>().text = "Restart";
        // tipCall.onClick.RemoveAllListeners();
        // tipCall.onClick.AddListener(()=>SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level");
    }
    private void OnButtonClick(byte index)
    {
        var correctIndex = questions[currentIndex].correctIndex;
        if(index == correctIndex)
        {
            Debug.Log("Correct");
            correctSound.Play();
            //winnings += 5000;
            //winningsText.text = "Выигрыш:"+winnings;
            AddWinnings();
            currentIndex++;
            if(currentIndex>=questions.Length)
            {
                Debug.Log("Question ended");
                EndGame();
            }
            else
            {
                SetQuestion();
            }
        }
        else
        {
            Debug.Log("Uncorrect");
            winnings = 0;
            uncorrectSound.Play();
            EndGame();
           // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void AddWinnings()
    {
        winnings += 5000;
       // winningsText1.text = ""+winnings;
    }
    private int GetRandomInt(int left,int right)
    {
        var rand = new System.Random();
        return rand.Next(left,right);
    }
    // Start is called before the first frame update
   private void Start()
    {
      SetQuestion();
      for(byte i=0;i<answerButtons.Length;i++) 
      {
        var index = i;
        answerButtons[i].onClick.AddListener(()=>OnButtonClick(index));
      } 
      tip50.onClick.AddListener(()=>
      {
        tip50Sound.Play();
        var buttonList = new List<Button>();
        var correctIndex = questions[currentIndex].correctIndex;
        for(var i=0;i<answerButtons.Length;i++)
        {
            if(i==correctIndex)
                continue;
            buttonList.Add(answerButtons[i]) ;   
        }
        var randomNumber = GetRandomInt(0,buttonList.Count);
        buttonList.Remove(buttonList[randomNumber]);
        buttonList.ForEach((action)=>action.gameObject.SetActive(false));
        tip50.enabled = false;
      });
      tipCall.onClick.AddListener(()=>
      {
        tipCallSound.Play();
        var randomNumber = GetRandomInt(0,100);
        var correctIndex = questions[currentIndex].correctIndex;
        var answerInt = randomNumber<=callPersent?correctIndex:GetRandomInt(0,3);
        var answerStr = "";
        switch(answerInt)
        {
            case 0:
            answerStr = "A";
            break;
            case 1:
            answerStr = "B";
            break;
            case 2:
            answerStr = "C";
            break;
            case 3:
            answerStr = "D";
            break;
        }
        questionText.text = $"Думаю это {answerStr}";
        tipCall.enabled = false;
      });
      tipPeople.onClick.AddListener(()=>
      {
        tipPeopleSound.Play();
        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;
        for(var i =0;i<100;i++)
        {
            var randomNumber = GetRandomInt(0,3);
            switch(randomNumber)
            {
                case 0:
                a++;
                break;
                case 1:
                b++;
                break;
                case 2:
                c++;
                break;
                case 3:
                d++;
                break;
            }
        }
        questionText.text = $"A: {a} B:{b} C:{c} D:{d}";
        tipPeople.enabled = false;
      });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
