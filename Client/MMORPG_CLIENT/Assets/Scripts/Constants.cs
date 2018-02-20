using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour
{
    public static Constants instance;

    private void Awake()
    {
        instance = this;
    }

    public const int MAX_PLAYERS = 10;
}
