using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFactory : WorkBuilding
{
    public override Dictionary<CivilianType, int> MaxWorkers()
    {
        Dictionary<CivilianType,int> maxWorkers = new Dictionary<CivilianType,int>();

        maxWorkers.Add(CivilianType.LowSkillWorker, 50);
        maxWorkers.Add(CivilianType.Engineer, 10);

        return maxWorkers;
    }




    // Update is called once per frame
    void Update()
    {
        
    }


}
