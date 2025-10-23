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
                var messageManagerTemp = MessageManager.Instance;
                if (messageManagerTemp != null)
                {
                    messageManagerTemp.ChangeMessageTemporary(messageToActivate, 1.5f);
                    Debug.Log("Last CheckPoint: " + lastCheckPoint);
                }
            }            
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {   
        var checkPoint = checkPoints.Find(i => i.checkPointID == lastCheckPoint);
        return checkPoint.transform.position;
    }

}
