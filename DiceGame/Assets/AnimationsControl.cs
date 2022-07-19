using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControl : MonoBehaviour
{
    public Animator thisAnim;
    // Start is called before the first frame update
    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Run()
    {
        thisAnim.SetBool("Run", true);
    }

    public void RunOff()
    {
        thisAnim.SetBool("Run", false);
    }

}
