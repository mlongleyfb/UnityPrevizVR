using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LightingSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public string[] sceneName;
    public Material[] sceneSkyboxes;

    private bool initLoadUp = false;
    private int currScene;
    private int prevScene;

    private TextMeshPro tmpText;


    void Start()
    {
        tmpText = GameObject.Find("/OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/OVRControllerPrefab/Text").GetComponent<TextMeshPro>();
        initLoadUp = true;
        currScene = 1;
        SwitchLevel();
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            currScene = currScene + 1;
            UnloadAllScenesExcept("PersistentScene");
            SwitchLevel();
        }


        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            currScene = currScene - 1;
            if (currScene == 0)
            {
                currScene = 1;
            }
            UnloadAllScenesExcept("PersistentScene");
            SwitchLevel();

        }

        if(currScene > sceneName.Length)
        {
            currScene = sceneName.Length - sceneName.Length + 1;
            UnloadAllScenesExcept("PersistentScene");
            SwitchLevel();
        }

        /*  if (Input.GetKeyDown(KeyCode.Alpha9))
          {
              currScene = currScene - 1;
              prevScene = prevScene - 1;
              SceneManager.LoadSceneAsync(currScene, LoadSceneMode.Additive);
              SceneManager.UnloadSceneAsync(prevScene, UnloadSceneOptions.None);
          }

          if (currScene==0)
          {
              currScene = sceneName.Length;
          }
          if (prevScene ==-2 )
          {
              prevScene = sceneName.Length;
          }*/

        /* if (Input.GetKeyDown(KeyCode.Alpha1))
         {
             SceneManager.LoadSceneAsync("scene1", LoadSceneMode.Additive);
             SceneManager.UnloadScene("scene2");
         }
         if (Input.GetKeyDown(KeyCode.Alpha2))
         {
             SceneManager.LoadSceneAsync("scene2", LoadSceneMode.Additive);
             SceneManager.UnloadScene("scene1");
         }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void SwitchLevel()
    {
        SceneManager.LoadSceneAsync(currScene, LoadSceneMode.Additive);
        tmpText.SetText(sceneName[currScene-1]);
        RenderSettings.skybox = sceneSkyboxes[currScene - 1];
        
    }

    void UnloadAllScenesExcept(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

}
