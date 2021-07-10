using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    int sceneToGo;
    [SerializeField] Animator animator;

    internal void startFade(int sceneIndex)
    {
        sceneToGo = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
