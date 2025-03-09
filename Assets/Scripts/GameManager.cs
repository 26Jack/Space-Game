using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<GameObject> objectsToDelete = new List<GameObject>();

    public static GameManager Instance; // figure out what a singleton patter is

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep this GameManager across scene reloads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void RegisterObject(GameObject obj)
    {
        objectsToDelete.Add(gameObject);
    }

    public void RestartGame()
    {
        foreach (var obj in objectsToDelete)
        {
            if (obj != null) Destroy(obj);
        }
        objectsToDelete.Clear();

        // Reload the scene (insane)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
