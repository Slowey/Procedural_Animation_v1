using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SQUAD (Spherical Spline Quaternions, [Shomake 1987]) implementation for Unity by Vegard Myklebust.
// Made available under Creative Commons license CC0. License details can be found here:
// https://creativecommons.org/publicdomain/zero/1.0/legalcode.txt 
// Translated from BOO to C# by Lucas Holmqvist and Eric Ahlström
public static class SQUAD
{

    // Returns a smoothed quaternion along the set of quaternions making up the spline, each quaternion is along an equidistant value in t
    //public static Quaternion Spline(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    //{
    //    int section = (int)(4 * (float)t);
    //    float alongLine = 4 * t - section;
    //    if (section == 0)
    //    {
    //        //Prova med q4 som start (loopinterpolation) typ
    //        return SplineSegment(q4, q1, q2, q3, alongLine);
    //    }
    //    else if (section == 1)
    //    {
    //        return SplineSegment(q1, q2, q3, q4, alongLine);
    //    }
    //    else if (section == 2)
    //    {
    //        return SplineSegment(q2, q3, q4, q1, alongLine);
    //    }
    //    else if (section == 3)
    //    {
    //        return SplineSegment(q3, q4, q1, q2, alongLine);
    //    }
    //    return new Quaternion(-1,-1,-1,-1);
    //}
    public struct testStruct
    {
        public Quaternion quat;
        public float alongLine;
        public float m_section;
    }
    public static Quaternion SplineFromListLoop(List<Quaternion> p_quatList, float t)
    {
        Quaternion r_quat = new Quaternion(0,0,0,0);

        int t_listSize = p_quatList.Count;
        int section = (int)Mathf.Floor((t_listSize) * t);// Mathf.Floor(4.0f * t); // kanske knas här
        float alongLine = (t_listSize) * t - section;
        if (section == 0)
        {
            r_quat = SplineSegment(p_quatList[p_quatList.Count-1], p_quatList[section], p_quatList[section + 1], p_quatList[section + 2], alongLine);
        }
        else if (section == t_listSize - 2 && section > 0)
        {
            r_quat = SplineSegment(p_quatList[section - 1], p_quatList[section], p_quatList[section + 1], p_quatList[0], alongLine);
            return r_quat;
        }
        else if (section == t_listSize - 1 && section > 0)
        {
            r_quat = SplineSegment(p_quatList[section - 1], p_quatList[section], p_quatList[0], p_quatList[1], alongLine);
            return r_quat;
        }
        else if (section >= 1 && section < t_listSize - 2)
        {
            r_quat = SplineSegment(p_quatList[section - 1], p_quatList[section], p_quatList[section + 1], p_quatList[section + 2], alongLine);
            return r_quat;
        }


        //int t_listSize = p_quatList.Count;
        //int section = (int)Mathf.Floor((t_listSize - 1) * t);// Mathf.Floor(4.0f * t); // kanske knas här
        //float alongLine = (t_listSize - 1) * t - section;
        //if (section == 0)
        //{
        //    r_quat = SplineSegment(p_quatList[p_quatList.Count - 1], p_quatList[section], p_quatList[section + 1], p_quatList[section + 2], alongLine);
        //}
        //else if (section == t_listSize - 2 && section > 0)
        //{
        //    r_quat = SplineSegment(p_quatList[section - 1], p_quatList[section], p_quatList[section + 1], p_quatList[0], alongLine);
        //    return r_quat;
        //}
        //else if (section >= 1 && section < t_listSize - 2)
        //{
        //    r_quat = SplineSegment(p_quatList[section - 1], p_quatList[section], p_quatList[section + 1], p_quatList[section + 2], alongLine);
        //    return r_quat;
        //}
        return r_quat;
    }
    public static testStruct Spline(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    {
        float section = Mathf.Floor(4.0f * t);
        float alongLine = 4.0f * t - section;
        
        if (section == 0)
        {
            testStruct d = new testStruct();
            //Prova med q4 som start (loopinterpolation) typ
            d.quat = SplineSegment(q4, q1, q2, q3, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 1)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegment(q1, q2, q3, q4, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 2)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegment(q2, q3, q4, q1, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 3)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegment(q3, q4, q1, q2, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        testStruct f = new testStruct();
        f.quat = new Quaternion(-1, -1, -1, -1);
        f.alongLine = 0;
        return f;
    }
    /// ////////////////////////////// SplineMoreThanFivePoints

    public static testStruct SplineMoreThanFivePoints(List<Quaternion> p_qList, float t)
    {
        int t_listSize = p_qList.Count;
        int section =(int) Mathf.Floor((t_listSize - 1) * t);// Mathf.Floor(4.0f * t); // kanske knas här
        float alongLine = (t_listSize -1) * t - section;
        if (section == 0)
        {
            testStruct d = new testStruct();
            //Prova med q4 som start (loopinterpolation) typ
            d.quat = SplineSegment(p_qList[section + 1], p_qList[section], p_qList[section + 1], p_qList[section + 2], alongLine);// SplineSegment(q4, q1, q2, q3, alongLine);
            d.alongLine = alongLine;
            d.m_section = section;
            return d;
            //return SplineSegment(quaternions[section], quaternions[section], quaternions[section+1], quaternions[section+2], alongLine)
        }
        else if (section == t_listSize - 2 && section > 0)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegment(p_qList[section - 1], p_qList[section], p_qList[section + 1], p_qList[section], alongLine);// SplineSegment(q1, q2, q3, q4, alongLine);
            
            d.alongLine = alongLine;
            d.m_section = section;
            return d;
        }
        else if (section >= 1 && section < t_listSize - 2)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegment(p_qList[section - 1], p_qList[section], p_qList[section + 1], p_qList[section + 2], alongLine);//SplineSegment(q2, q3, q4, q1, alongLine);
            d.alongLine = alongLine;
            d.m_section = section;
            return d;
        }

        testStruct f = new testStruct();
        f.quat = new Quaternion(-1, -1, -1, -1);
        f.alongLine = 0;
            f.m_section = section;
        return f;
    }
    /// ////////////////////////////////

    public static testStruct SplineForceShortWay(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    {
        float section = Mathf.Floor(4.0f * t);
        float alongLine = 4.0f * t - section;
        if (section == 0)
        {
            testStruct d = new testStruct();
            //Prova med q4 som start (loopinterpolation) typ
            d.quat = SplineSegmentForceShortWay(q4, q1, q2, q3, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 1)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegmentForceShortWay(q1, q2, q3, q4, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 2)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegmentForceShortWay(q2, q3, q4, q1, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        else if (section == 3)
        {
            testStruct d = new testStruct();
            d.quat = SplineSegmentForceShortWay(q3, q4, q1, q2, alongLine);
            d.alongLine = alongLine;
            return d;
        }
        testStruct f = new testStruct();
        f.quat = new Quaternion(-1, -1, -1, -1);
        f.alongLine = 0;
        return f;
    }

    //public static Quaternion Spline(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    //{
    //    int section = (int)(4.0f * t);
    //    float alongLine = 4.0f * t - section;
    //    //if(t==0.0f)
    //    //{
    //    //    return SplineSegment(q1, q2, q3, q4, t);
    //    //}
    //    //if (t==1.0f)
    //    //{
    //    //    return SplineSegment(q4, q1, q2, q3, t);
    //    //}
    //    if (section == 0)
    //    {
    //        //Prova med q4 som start (loopinterpolation) typ
    //        return SplineSegment(q4, q1, q2, q3, alongLine);
    //    }
    //    else if (section == 1)
    //    {
    //        return SplineSegment(q1, q2, q3, q4, alongLine);
    //    }
    //    else if (section == 2)
    //    {
    //        return SplineSegment(q2, q3, q4, q1, alongLine);
    //    }
    //    else if (section == 3)
    //    {
    //        return SplineSegment(q3, q4, q1, q2, alongLine);
    //    }
    //    return new Quaternion(-1, -1, -1, -1);
    //}
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
        return PerformSQUAD(q2, qa, qb, q3, t); //prova byta ordning?
    }
    static Quaternion SplineSegmentForceShortWay(Quaternion q1, Quaternion q2, Quaternion q3, Quaternion q4, float t)
    {
        Quaternion qa = Intermediate(q1, q2, q3);
        Quaternion qb = Intermediate(q2, q3, q4);
        return PerformSQUADForceShortWay(q2, qa, qb, q3, t); //prova byta ordning?
    }
    // Tries to compute sensible tangent values for the quaternion
    static Quaternion Intermediate(Quaternion q1, Quaternion q2, Quaternion q3)
    {
        Quaternion q2inv = Quaternion.Inverse(q2);
        Quaternion c1 = q2inv * q3;
        Quaternion c2 = q2inv * q1;
        c1 = c1.Loged();
        c2 = c2.Loged();
        Quaternion c3 = c1.Add(c2); //bytte ordning på c1 å c2
        c3 = c3.Scaled(-0.25f);
        c3 = c3.Exped();
        Quaternion r_quat = q2 * c3;
        r_quat = r_quat.Normalized();
        return r_quat;
    }
    // Returns a smooth approximation between q1 and q2 using t1 and t2 as 'tangents'
    static Quaternion PerformSQUAD(Quaternion q2, Quaternion t1, Quaternion t2, Quaternion q3, float t)
    {
        float slerpT = 2.0f * t * (1.0f - t);
        Quaternion slerp1 = QuaternionExtensionsC.SlerpNoInvert(q2, q3, t);
        Quaternion slerp2 = QuaternionExtensionsC.SlerpNoInvert(t1, t2, t);
        return QuaternionExtensionsC.SlerpNoInvert(slerp1, slerp2, slerpT);
    }
    static Quaternion PerformSQUADForceShortWay(Quaternion q2, Quaternion t1, Quaternion t2, Quaternion q3, float t)
    {
        float slerpT = 2.0f * t * (1.0f - t);
        //Quaternion slerp1 = QuaternionExtensionsC.SlerpNoInvert(q2, q3, t);
        //Quaternion slerp2 = QuaternionExtensionsC.SlerpNoInvert(t1, t2, t);
        //return QuaternionExtensionsC.SlerpNoInvertForceShortWay(slerp1, slerp2, slerpT);
        Quaternion slerp1 = QuaternionExtensionsC.SlerpNoInvertForceShortWay(q2, q3, t);
        Quaternion slerp2 = QuaternionExtensionsC.SlerpNoInvertForceShortWay(t1, t2, t);
        return QuaternionExtensionsC.SlerpNoInvertForceShortWay(slerp1, slerp2, slerpT);
    }
}
