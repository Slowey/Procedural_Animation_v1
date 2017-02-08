using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExtensionsC
{

    // Quaternion extensions for Unity by Vegard Myklebust.
    // Made available under Creative Commons license CC0. License details can be found here:
    // https://creativecommons.org/publicdomain/zero/1.0/legalcode.txt 
    // Translated from BOO to C# by Lucas Holmqvist and Eric Ahlström
    public static void Log(this Quaternion a)
    {
        float a0 = a.w;
        a.w = 0.0f;
        if (Mathf.Abs(a0) < 1.0)
        {
            float angle = Mathf.Acos(a0);
            float sinAngle = Mathf.Sin(angle);
            if (Mathf.Abs(sinAngle) >= 0.0000000000000000000000001f)
            {
                float coeff = angle / sinAngle;
                a.x *= coeff;
                a.y *= coeff;
                a.z *= coeff;
            }
        }
    }

    public static Quaternion Loged(this Quaternion a)
    {
        Quaternion result = a;
        float a0 = result.w;
        result.w = 0.0f;
        if (Mathf.Abs(a0) <= 1.0f)
        {
            float angle = Mathf.Acos(a0);
            float sinAngle = Mathf.Sin(angle);
            if (Mathf.Abs(sinAngle) >= 0.000000000000001f)
            {
                float coeff = angle / sinAngle;
                result.x *= coeff;
                result.y *= coeff;
                result.z *= coeff;
            }
        }
        return result;
    }

    public static void Conjugate(this Quaternion a)
    {
        a.x *= -1;
        a.y *= -1;
        a.z *= -1;
    }

    public static Quaternion Conjugated(this Quaternion a)
    {
        Quaternion result = a;
        result.x *= -1;
        result.y *= -1;
        result.z *= -1;
        return result;
    }

    public static void Scale(this Quaternion a, float s)
    {
        a.w *= s;
        a.x *= s;
        a.y *= s;
        a.z *= s;
    }

    public static Quaternion Scaled(this Quaternion a, float s)
    {
        Quaternion result = a;
        result.w *= s;
        result.x *= s;
        result.y *= s;
        result.z *= s;
        return result;
    }

    public static void Exp(this Quaternion a)
    {
        float angle = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        float sinAngle = Mathf.Sin(angle);
        a.w = Mathf.Cos(angle);
        if (Mathf.Abs(sinAngle) >= 0.0000000000000001f)
        {
            float coeff = sinAngle / angle;
            a.x *= coeff;
            a.y *= coeff;
            a.z *= coeff;
        }
    }

    public static Quaternion Exped(this Quaternion a)
    {
        Quaternion result = a;
        float angle = Mathf.Sqrt(result.x * result.x + result.y * result.y + result.z * result.z); // + result.w *result.w gjorde ingen skillnad
        float sinAngle = Mathf.Sin(angle);
        result.w = Mathf.Cos(angle);
        if (Mathf.Abs(sinAngle) >= 0.00000000000000000000000001f)
        {
            float coeff = sinAngle / angle;
            result.x *= coeff;
            result.y *= coeff;
            result.z *= coeff;
        }
        return result;
    }

    public static float Normalize(this Quaternion a)
    {
        float length = Length(a);
        if (length > 0.000000000000000000000000000000000001f)
        {
            float invlen = 1.0f / length;
            a.w *= invlen;
            a.x *= invlen;
            a.y *= invlen;
            a.z *= invlen;
        }
        else
        {
            length = 0.0f;
            a.w = 0.0f;
            a.x = 0.0f;
            a.y = 0.0f;
            a.z = 0.0f;
        }
        return length;
    }

    public static Quaternion Normalized(this Quaternion a)
    {
        Quaternion result = a;
        float length = Length(a);
        if (length > 0.0000000000001f)
        {
            float invlen = 1.0f / length;
            result.w *= invlen;
            result.x *= invlen;
            result.y *= invlen;
            result.z *= invlen;
        }
        else
        {
            length = 0.0f;
            result.w = 0.0f;
            result.x = 0.0f;
            result.y = 0.0f;
            result.z = 0.0f;
        }
        return result;
    }
    public static float Length(this Quaternion a)
    {
        return Mathf.Sqrt(a.w * a.w + a.x * a.x + a.y * a.y + a.z * a.z);
    }

    //public static Quaternion op_Addition(Quaternion a, Quaternion b)
    //{
    //    return a.Add(b);
    //}
    //public static Quaternion op_Subtraction(Quaternion a, Quaternion b)
    //{
    //    return a.Sub(b);
    //}
    public static Quaternion Add(this Quaternion a, Quaternion b)
    {
        Quaternion r;
        r.w = a.w + b.w;
        r.x = a.x + b.x;
        r.y = a.y + b.y;
        r.z = a.z + b.z;
        return r;
    }
    public static Quaternion Sub(this Quaternion a, Quaternion b)
    {
        Quaternion r;
        r.w = a.w - b.w;
        r.x = a.x - b.x;
        r.y = a.y - b.y;
        r.z = a.z - b.z;
        return r;
    }
    public static Quaternion SlerpNoInvert(Quaternion from, Quaternion to, float factor)
    {
        float dot = Quaternion.Dot(from, to);

        if (Mathf.Abs(dot) > 0.9999f)
            return Quaternion.Lerp(from, to, factor);

        float theta = Mathf.Acos(dot);
        float sinT = 1.0f / Mathf.Sin(theta);
        float newFactor = Mathf.Sin(factor * theta) * sinT;
        float invFactor = Mathf.Sin((1.0f - factor) * theta) * sinT;

        Quaternion r_quat = new Quaternion(invFactor * from.x + newFactor * to.x, invFactor * from.y + newFactor * to.y,
            invFactor * from.z + newFactor * to.z, invFactor * from.w + newFactor * to.w);
        return r_quat;
    }
}
