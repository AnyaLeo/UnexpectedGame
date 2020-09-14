using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator portraitAnim;

    public void beforeTheGame()
    {
        portraitAnim.SetTrigger("chilling");
    }

    public void startTheGame()
    {
        portraitAnim.SetTrigger("playing cards");
    }
   
    public void lostTheGame()
    {
        portraitAnim.SetTrigger("confused");
    }

    public void getsAngry()
    {
        portraitAnim.SetTrigger("angry");
    }

    public void emptyRoom()
    {
        portraitAnim.SetTrigger("exit");
    }

    public void annoyed()
    {
        portraitAnim.SetTrigger("annoyed");
    }

    public void softSmile()
    {
        portraitAnim.SetTrigger("soft smile");
    }

    public void softestSmile()
    {
        portraitAnim.SetTrigger("softest smile");
    }

    public void cryStage1()
    {
        portraitAnim.SetTrigger("cry1");
    }

    public void cryStage2()
    {
        portraitAnim.SetTrigger("cry2");
    }

    public void cryStage3()
    {
        portraitAnim.SetTrigger("cry3");
    }

    public void focused()
    {
        portraitAnim.SetTrigger("focused");
    }
}
