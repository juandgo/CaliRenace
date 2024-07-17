using UnityEngine;

public class AnimationsLoginUI : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject startGroup;
    // [SerializeField] private GameObject extraMenu;
    [SerializeField] private GameObject blackBg;
    // [SerializeField] private GameObject acceptQuitGame;
    [SerializeField] private RectTransform logoAnimation;

    private void Start()
    {
        LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(LowerAlpha);
        LeanTween.scale(title.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    private void LowerAlpha()
    {
        LeanTween.alpha(startGroup.GetComponent<RectTransform>(), 0f, 1f).setDelay(0.5f);
        startGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private void ApearTitle()
    {
        LeanTween.scale(title.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f);
    }

    public void ActivateAnimation()
    {
        LeanTween.moveY(logoAnimation, -75, 1f).setLoopClamp();
        //LeanTween.moveY(logoAnimation, -75, 1f).setLoopPingPong();
        //LeanTween.moveY(logoAnimation, -75, 1f).setLoopOnce();
    }

    public void Quit()
    {
        Debug.Log("Closing the game");
        Application.Quit();
    }

}
