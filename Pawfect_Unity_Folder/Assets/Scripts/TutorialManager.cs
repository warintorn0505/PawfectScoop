using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Overlay Images")]
    public GameObject overlayCone;
    public GameObject overlayVanilla;
    public GameObject overlayTopping;
    public GameObject overlayTrash;
    public GameObject overlayServe;

    [Header("Buttons")]
    public GameObject tutorialConeButton;
    public GameObject tutorialVanillaButton;
    public GameObject tutorialSprinkleButton;
    public GameObject tutorialTrashButton;
    public GameObject tutorialCustomerButton;

    [Header("UI")]
    public GameObject tutorialUI;
    public TextMeshProUGUI tutorialText;
    public GameObject returnButton;

    [Header("Typewriter")]
    public TypewriterEffect typewriterEffect;

    [Header("Preview Visuals")]
    public GameObject previewCone;
    public GameObject previewVanilla;
    public GameObject previewSprinkle;

    [Header("Texts")]
    [TextArea] public string coneText = "Let's make your first ice cream! Start by choosing a cone.";
    [TextArea] public string vanillaText = "Nice! Now pick vanilla~";
    [TextArea] public string toppingText = "Yum! Add some topping.";
    [TextArea] public string choiceText = "Oops! Made a mistake? You can throw it away here. If it's ready, click the customer to serve.";
    [TextArea] public string finishText = "Perfect! You're ready to play!";

    void Start()
    {
        HideAllSteps();

        if (returnButton != null)
            returnButton.SetActive(false);

        ClearPreview();
        ShowConeStep();
    }

    void HideAllSteps()
    {
        if (overlayCone != null) overlayCone.SetActive(false);
        if (overlayVanilla != null) overlayVanilla.SetActive(false);
        if (overlayTopping != null) overlayTopping.SetActive(false);
        if (overlayTrash != null) overlayTrash.SetActive(false);
        if (overlayServe != null) overlayServe.SetActive(false);

        if (tutorialConeButton != null) tutorialConeButton.SetActive(false);
        if (tutorialVanillaButton != null) tutorialVanillaButton.SetActive(false);
        if (tutorialSprinkleButton != null) tutorialSprinkleButton.SetActive(false);
        if (tutorialTrashButton != null) tutorialTrashButton.SetActive(false);
        if (tutorialCustomerButton != null) tutorialCustomerButton.SetActive(false);
    }

    void ClearPreview()
    {
        if (previewCone != null)
            previewCone.SetActive(false);

        if (previewVanilla != null)
            previewVanilla.SetActive(false);

        if (previewSprinkle != null)
            previewSprinkle.SetActive(false);
    }

    void ShowTutorialText(string message)
    {
        if (typewriterEffect != null)
        {
            typewriterEffect.StartTyping(message);
        }
        else if (tutorialText != null)
        {
            tutorialText.text = message;
        }
    }

    public void ShowConeStep()
    {
        HideAllSteps();

        if (tutorialUI != null)
            tutorialUI.SetActive(true);

        if (overlayCone != null)
            overlayCone.SetActive(true);

        if (tutorialConeButton != null)
            tutorialConeButton.SetActive(true);

        ShowTutorialText(coneText);
    }

    public void ShowVanillaStep()
    {
        HideAllSteps();

        if (overlayVanilla != null)
            overlayVanilla.SetActive(true);

        if (tutorialVanillaButton != null)
            tutorialVanillaButton.SetActive(true);

        ShowTutorialText(vanillaText);
    }

    public void ShowToppingStep()
    {
        HideAllSteps();

        if (overlayTopping != null)
            overlayTopping.SetActive(true);

        if (tutorialSprinkleButton != null)
            tutorialSprinkleButton.SetActive(true);

        ShowTutorialText(toppingText);
    }

    public void ShowChoiceStep()
    {
        HideAllSteps();

        if (overlayTrash != null)
            overlayTrash.SetActive(true);

        if (overlayServe != null)
            overlayServe.SetActive(true);

        if (tutorialTrashButton != null)
            tutorialTrashButton.SetActive(true);

        if (tutorialCustomerButton != null)
            tutorialCustomerButton.SetActive(true);

        ShowTutorialText(choiceText);
    }

    public void FinishTutorial()
    {
        HideAllSteps();

        ShowTutorialText(finishText);

        if (returnButton != null)
            returnButton.SetActive(true);
    }

    public void OnTutorialConeClicked()
    {
        if (previewCone != null)
            previewCone.SetActive(true);

        ShowVanillaStep();
    }

    public void OnTutorialVanillaClicked()
    {
        if (previewVanilla != null)
            previewVanilla.SetActive(true);

        ShowToppingStep();
    }

    public void OnTutorialSprinkleClicked()
    {
        if (previewSprinkle != null)
            previewSprinkle.SetActive(true);

        ShowChoiceStep();
    }

    public void OnTutorialTrashClicked()
    {
        ClearPreview();
        ShowConeStep();
    }

    public void OnTutorialCustomerClicked()
    {
        FinishTutorial();
    }
}