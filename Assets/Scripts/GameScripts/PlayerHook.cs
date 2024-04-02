using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public static PlayerHook instance;

    private void Awake()
    {
        instance = this;
    }
}