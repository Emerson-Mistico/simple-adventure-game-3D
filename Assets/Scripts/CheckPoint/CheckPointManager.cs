using UnityEngine;
using ESM.Core.Singleton;
using NUnit.Framework;
using System.Collections.Generic;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public string messageToActivate = "New Checkpoint Found!";
    public int lastCheckPoint = 0;
    public List<CheckPointBase> checkPoints;    

    public bool HasCheckPoint()
    {
        return lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int checkPointID, bool showMessage)
    {
        if (checkPointID > lastCheckPoint)
        {
            lastCheckPoint = checkPointID;

            if (showMessage)
            {
                var mm = MessageManager.Instance;
                if (mm != null)
                {
                    mm.ChangeMessageTemporary(messageToActivate, 1.5f);
                }
                else
                {
                    Debug.LogError("MessageManager.Instance é null. Adicione um MessageManager na cena ou inicialize antes de usar.");
                }
            }            
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        Debug.Log("Last = " + lastCheckPoint);
        var checkPoint = checkPoints.Find(i => i.checkPointID == lastCheckPoint);
        return checkPoint.transform.position;
    }

}
