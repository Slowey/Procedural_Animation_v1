using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Quaternion GiveRandomVecThree()//Quaternion p_vec3In, Quaternion p_originalValues, float p_randomRange)
    {
        //Quaternion t_differencesInVec = new Quaternion(p_vec3In.x - p_originalValues.x, p_vec3In.y - p_originalValues.y, p_vec3In.z - p_originalValues.z, p_vec3In.w-p_originalValues.w);
        //Quaternion.FromToRotation()
        Vector3 t_vec3Random = Random.insideUnitSphere*100;
        //return Random.insideUnitSphere
        //print(t_vec3Random + "      " + Quaternion.Euler(t_vec3Random.x, t_vec3Random.y, t_vec3Random.z));
        return Quaternion.Euler(t_vec3Random.x, t_vec3Random.y, t_vec3Random.z);
    }
    float GiveRandomFloatBack(float p_floatIn, float p_originalValue, float p_randomRange)
    {
        Random rnd = new Random();
        //int month = Random.
        return 0.0f;
    }
}
