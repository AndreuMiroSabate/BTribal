using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void ChangeSceneFunction()
    {
        EditorSceneManager.LoadScene(sceneName);
    }

}
