using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Animator anim;
    bool isFading = true  ;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public IEnumerator FadeToClear()
    {
        isFading =true;
        anim.SetTrigger("FadeIn");
        while (isFading)
            yield return null;
    }
    public IEnumerator FadeToBlack()
    {
        isFading = true;
        anim.SetTrigger("FadeOut");
        while (isFading)
            yield return null;
    }
    void AnimationComplete()
    {
        isFading = false;

    }
}
