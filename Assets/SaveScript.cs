using JetBrains.Annotations;
using System.Collections.Generic;

[System.Serializable]

public class GameState
{
    public List<(string ShopeName, int Gold, int Lvl ) > valuesAndScene;
}   