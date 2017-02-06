using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SQUAD (Spherical Spline Quaternions, [Shomake 1987]) implementation for Unity by Vegard Myklebust.
// Made available under Creative Commons license CC0. License details can be found here:
// https://creativecommons.org/publicdomain/zero/1.0/legalcode.txt 
// Translated from BOO to C# by Lucas Holmqvist and Eric Ahlström
public static class SQUAD{

    // Returns a smoothed quaternion along the set of quaternions making up the spline, each quaternion is along an equidistant value in t
    public static Quaternion Spline(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    {
        int section = (int)(4 * (float)t);
        float alongLine = 4 * t - section;
        if (section == 0)
        {
            //Prova med q4 som start (loopinterpolation) typ
            return SplineSegment(q4, q1, q2, q3, alongLine);
        }
        // wtf section > 0? 1234 ta bort om det funkar utan
        else if (section == 2)
        {
            return SplineSegment(q2, q3, q4, q1, alongLine);
        }
        else if (section == 1)
        {
            return SplineSegment(q1, q2, q3, q4, alongLine);
        }
        else if (section == 3)
        {
            return SplineSegment(q3, q4, q1, q2, alongLine);
        }
        return new Quaternion(-1,-1,-1,-1);
    }
    //public static int Spline(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    //{
    //    int section = (int)(3 * (float)t);
    //    float alongLine = 3 * t - section;
    //    return section;
    //    //if (section < 1)
    //    //{
    //    //    //Prova med q4 som start (loopinterpolation) typ
    //    //    return SplineSegment(q1, q1, q2, q3, alongLine);
    //    //}
    //    //// wtf section > 0? 1234 ta bort om det funkar utan
    //    //else if (section == 2 && section > 0)
    //    //{
    //    //    return SplineSegment(q2, q3, q4, q4, alongLine);
    //    //}
    //    //else if (section >= 1 && section < 2)
    //    //{
    //    //    return SplineSegment(q1, q2, q3, q4, alongLine);
    //    //}
    //    //else
    //    //{
    //    //    return new Quaternion(section, -1, -1, -1);
    //    //}
    //}
    // Returns a quaternion between q1 and q2 as part of a smooth SQUAD segment
    static Quaternion SplineSegment(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    {
        Quaternion qa = Intermediate(q1, q2, q3);
        Quaternion qb = Intermediate(q2, q3, q4);
        return PerformSQUAD(q2, qa, qb, q3, t);
    }
    // Tries to compute sensible tangent values for the quaternion
    static Quaternion Intermediate(Quaternion q1, Quaternion q2, Quaternion q3)
    {
        Quaternion q2inv = Quaternion.Inverse(q2);
        Quaternion c1 = q2inv * q3;
        Quaternion c2 = q2inv * q1;
        c1 = c1.Loged();
        //c1.Log();
        c2 = c2.Loged();
        //c2.Log();
        Quaternion c3 = c2.Add(c1);
        c3 = c3.Scaled(-0.2f);
        c3 = c3.Exped();
        Quaternion r_quat = q2 * c3;
        r_quat = r_quat.Normalized();
        return r_quat;
            }
    // Returns a smooth approximation between q1 and q2 using t1 and t2 as 'tangents'
    static Quaternion PerformSQUAD(Quaternion q2, Quaternion t1, Quaternion t2, Quaternion q3, float t)
    {
        float slerpT = 2.0f * t * (1.0f - t);
        Quaternion slerp1 = QuaternionExtensions.SlerpNoInvert(q2, q3, t);
        Quaternion slerp2 = QuaternionExtensions.SlerpNoInvert(t1, t2, t);
        return QuaternionExtensions.SlerpNoInvert(slerp1, slerp2, slerpT);
    }
}
