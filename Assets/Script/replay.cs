using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnreplay;

    void OnReplay()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    void Start()
    {
        btnreplay.onClick.AddListener(OnReplay);
    }
}
