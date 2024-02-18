using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UlManager : MonoBehaviour
{
    
    public Animator LayoutAnimator;
    
    
    public void Privacy_Policy()
    {
        Application.OpenURL("https://www.tosugames.com/privacy-policy/");
    }
    public void TermOfUse()
    {
        Application.OpenURL("https://www.tosugames.com/sample-page/");
    }

    public void LayoutSettingsOpen()
    {
        LayoutAnimator.SetTrigger("Slide_in");
    }
    public void LayoutSettingsClose()
    {
        LayoutAnimator.SetTrigger("Slide_out");
    }

    //public void NextScene(int sahne_ýd)
    //{
    //    SceneManager.LoadScene(sahne_ýd);
        
    //}

    public void RestartSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
    
    

    
}
