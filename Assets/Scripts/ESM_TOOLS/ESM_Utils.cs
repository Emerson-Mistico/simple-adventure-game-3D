using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public static class ESM_Utils
{

#if UNITY_EDITOR
    #region MENU ITENS
    [MenuItem("ESM UTILS/Msg Console %M,", false, 100000)]

    public static void ConsoleMessage()
    {
        Debug.Log("ESM Utils: test");
    }  
    #endregion
#endif

    #region UTILS
    public static void spawnItemByType(PrimitiveType type)
    {
        GameObject itemBytype = GameObject.CreatePrimitive(type);
        itemBytype.name = type + " - Criado via Barra de Ferramentas";
        Undo.RegisterCreatedObjectUndo(itemBytype, "Criar "+ type + " via Barra de Ferramentas");
        itemBytype.transform.position = Vector3.zero;
        Debug.Log(type + " criado!");
    }
    #endregion

    #region RANDOM TOOLS
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    #endregion
}
