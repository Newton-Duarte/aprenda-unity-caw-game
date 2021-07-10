using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadController : MonoBehaviour
{
    TransitionController _transitionController;
    OptionsController _optionsController;
    bool isVerified;

    // Start is called before the first frame update
    void Start()
    {
        _transitionController = FindObjectOfType(typeof(TransitionController)) as TransitionController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;

        _optionsController.musicSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVerified && !_optionsController.musicSource.isPlaying)
        {
            isVerified = true;
            _transitionController.startFade(3);
        }
    }
}
