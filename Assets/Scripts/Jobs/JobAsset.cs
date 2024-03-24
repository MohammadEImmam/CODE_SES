namespace CrossConnections
{
    using UnityEngine;
    /// <summary>
    /// Asset that contains a job. can be loaded from resources
    /// </summary>

    [CreateAssetMenu(fileName = "JobAsset", menuName = "JobAsset", order = 0)]
    public class JobAsset : ScriptableObject
    {
        public Job job;

        [TextArea]
        public string solution;

        [TextArea]
        public string loadJobFromJson;
        public bool loadJob;

        private void OnValidate()
        {
            if (loadJob)
            {
                job = JsonUtility.FromJson<Job>(loadJobFromJson);
                loadJob = false;
            }
        }


    }
}