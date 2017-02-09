using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDuck : MonoBehaviour
{

    //List<List<Quaternion>> poses = new List<List<Quaternion>>();
    //List<Vector3> hipspos = new List<Vector3>();
    // Use this for initialization
    //public void StartUp(List<List<Quaternion>> p_poses, List<Vector3> p_hipspos)
    //{
    //    poses = p_poses;
    //    hipspos = p_hipspos;
    //    print(hipspos.Count);
    //    print(hipspos[0] + " " + hipspos[1] + "iStartupANimDUCK");
    //}

    public void UpdateAnimation(float p_transition, List<List<Quaternion>> p_poses, List<Vector3> p_hipspos)
    {
        GameObject t_hips = GameObject.FindGameObjectWithTag("Bicubic");
        Transform[] t_bones = t_hips.GetComponentsInChildren<Transform>();
        if (p_transition < 0.5f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.Slerp(p_poses[0][i], p_poses[1][i], p_transition / 0.5f);
                //t_bones[i].rotation = poses[1][i];
            }
            Vector3 test = Vector3.Lerp(p_hipspos[0], p_hipspos[1], p_transition / 0.5f);
            t_bones[0].transform.position = new Vector3(test.x, test.y, test.z);
            //t_bones[0].transform.position = new Vector3(hipspos[1].x, hipspos[1].y, hipspos[1].z);
        }
        else if (p_transition > 0.5f)
        {
            for (int i = 0; i < t_bones.Length; i++)
            {
                t_bones[i].rotation = Quaternion.Slerp(p_poses[1][i], p_poses[0][i], (p_transition - 0.5f) / 0.5f);
            }
            Vector3 test = Vector3.Lerp(p_hipspos[1], p_hipspos[0], (p_transition - 0.5f) / 0.5f);
            t_bones[0].transform.position = new Vector3(test.x, test.y, test.z);
        }
    }

}