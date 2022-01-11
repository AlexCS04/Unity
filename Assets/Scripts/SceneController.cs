using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int LevelTLoad;
    public bool LoadLevel = false;
    public static int NextLevel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NextLevel == 5)
        {
            LoadLevel = true;
        }
        else
        {
            LoadLevel = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject collisionGameObject= collision.gameObject;
        if(collisionGameObject.name == "ruby")
        {
            LoadScene();
            

        }
    }
    void LoadScene()
    {
        if (LoadLevel)
        {
            SceneManager.LoadScene(LevelTLoad);
            NextLevel = 0;

        }

    }
    
}

