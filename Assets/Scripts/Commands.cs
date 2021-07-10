using UnityEngine;

public class Commands : MonoBehaviour
{
    TransitionController _transitionController;
    OptionsController _optionsController;

    private void Start()
    {
        _transitionController = FindObjectOfType(typeof(TransitionController)) as TransitionController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
    }

    public void startGame()
    {
        _optionsController.StartCoroutine(_optionsController.changeMusic(_optionsController.startClip));
        _transitionController.startFade(2);
    }

    public void titleScreen()
    {
        _transitionController.startFade(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
