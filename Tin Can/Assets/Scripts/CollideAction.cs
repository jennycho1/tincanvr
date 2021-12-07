using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CollideAction : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] TextMeshPro textUI;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] public ParticleSystem winFX;
    //[SerializeField] ParticleSystem loseFX;
    
    public int canCount = 6;
    public int ballCount = 1;

    void Start()
    {
        canCount = 6;
        ballCount = 1;
    }

    private void OnCollisionEnter(Collision other)
    {
        // update variables
        switch (other.gameObject.tag)
        {
            case "Can":
                Debug.Log("can collided: "+canCount);
                canCount--;
                break;
            case "Ball":
                Debug.Log("ball collided: " + ballCount);
                StartCoroutine(BallRespawn(other.gameObject));
                break;
            default:
                break;
        }
        // check for status
        if (canCount == 0 && ballCount <= 4)
        {
            Debug.Log("Winning");
            WinAnimation();
        }
        
    }

    IEnumerator BallRespawn(GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Ball respawn");
        ballCount++;
        if (ballCount < 4)
        {
            textUI.text = $"Round: {ballCount}/3";
        }
       
        Destroy(ball);
        Instantiate(ballPrefab, new Vector3(0f,0.916f,0.324f), Quaternion.identity);
        if (canCount > 0 && ballCount == 4)
        {
            Debug.Log("Losing");
            LoseAnimation();
        }
    }
    void WinAnimation()
    {
        winFX.Play();
        FindObjectOfType<CustomAudioManager>().Play("win");
        textUI.text = "You won!!!";
        Invoke("ReloadScene", 2f);
    }
    void LoseAnimation(){
        FindObjectOfType<CustomAudioManager>().Play("lose");
        textUI.text = "You lost:( Try again";
        Invoke("ReloadScene", 2f);
    }
    void ReloadScene()
    {
        Debug.Log("Reload Level");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CollideAction))]
public class CollideActionEditor : Editor
{
    public CollideAction scriptItem => target as CollideAction;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Test Particle"))
        {
            scriptItem.winFX.Play();
        }
    }
}
#endif
