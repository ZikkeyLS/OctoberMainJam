using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Common.Scripts;

public class NoteManager : MonoBehaviour
{
    [SerializeField] public float duration;
    [SerializeField] public float FadeValue1;
    [SerializeField] public float FadeValue2;
    public GameObject InterractIcon;
    public GameObject FnotePanalUI;
    public GameObject SnotePanalUI;
    public GameObject readButtonUI;
    public TMP_Text noteTextUI;
    private float currentTime;
    private bool opened = false;
    [SerializeField] private Image noteImageUI;
    [SerializeField] private GameObject Player;

    
    
    private void Start()
    {
        FnotePanalUI.SetActive(false);
        SnotePanalUI.SetActive(false);
    }

    private void Update()
    {
        if (opened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void FirstStageOpen(string textNote, Sprite Image)
    {
        opened = true;
        Player.GetComponent<movement>().TurnOffMovement();
        if (Player.GetComponent<Vision>() != null)
        {
            Player.GetComponent<Vision>().enabled = false;   
        }
        //Time.timeScale=0f;
        Image FirstPanelImg=FnotePanalUI.GetComponent<Image>();
        readButtonUI.SetActive(true);
        InterractIcon.SetActive(false);
        FnotePanalUI.SetActive(true);
        noteImageUI.sprite=Image;
        noteTextUI.text=textNote;
        FirstPanelImg.color = new Color(FirstPanelImg.color.r, FirstPanelImg.color.g, FirstPanelImg.color.b, 0);
        StartCoroutine(FadeInCrt(FirstPanelImg,FadeValue1));
    }

    public void SecondStageOpen()
    {
        SnotePanalUI.SetActive(true);
        readButtonUI.SetActive(false);
        Image SecondPanelImg=SnotePanalUI.GetComponent<Image>();
        StopAllCoroutines();
        StartCoroutine(FadeInCrt(SecondPanelImg,FadeValue2));
    }

    public void CloseNote()
    {
        opened = false;
        Player.GetComponent<movement>().TurnOnMovement();
        if (Player.GetComponent<Vision>() != null)
        {
            Player.GetComponent<Vision>().enabled = true;   
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Time.timeScale=1f;
        InterractIcon.SetActive(true);
        FnotePanalUI.SetActive(false);
        SnotePanalUI.SetActive(false);
    }

    private IEnumerator FadeInCrt(Image noteImageUI, float FadeValue)
    {
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0f, FadeValue, currentTime / duration);
            noteImageUI.color = new Color(noteImageUI.color.r, noteImageUI.color.g, noteImageUI.color.b, alpha);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        currentTime = 0;
        yield break;
    }

    private IEnumerator FadeOutCrt(Image noteImageUI)
    {
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
           noteImageUI.color = new Color(noteImageUI.color.r, noteImageUI.color.g, noteImageUI.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        currentTime = 0;
        yield break;
    }
}
