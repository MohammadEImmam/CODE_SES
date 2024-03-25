namespace Computer
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Software that opens the Job Search UI
    /// </summary>
    public class JobSearchManager : Software
    {
        public override void OnStart()
        {
            base.OnStart();

            StartCoroutine(LoadScene());

        }

        IEnumerator LoadScene()
        {
            //load the shop scene
            yield return SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            yield return null;
            // GameObject.FindWithTag("ShopManager").GetComponent<shopManager>().onExit += () =>
            // {
            //     computer.Close(this);
            // };
        }

        public override void OnEnd()
        {
            base.OnEnd();
            StopAllCoroutines();
            //remove the shop  scene
            SceneManager.UnloadScene(2);
        }
    }
}