using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using ESM_TOOLS.Core.Singleton;

public class GameManager : Singleton<GameManager>
{    
    [Header("Player Setup")]   
    public string playerName = "Jogador 1";
}
