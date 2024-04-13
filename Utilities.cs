using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;


public static class Utilities
{
	public static int CarouselIndex(int start, int offset, int min, int max)
	{
		int end = start;
		while (Mathf.Abs(offset) > 0)
		{
			if (offset > 0)
			{
				++end;
				if (end > max)
				{
					end = min;
				}
				--offset;
			}
			else if (offset < 0)
			{
				--end;
				if (end < min)
				{
					end = max;
				}
				++offset;
			}
		}
		
		return end;
	}

    public static float HueToRGB(float p, float q, float t)
    {
    	if(t < 0f) 
    		t += 1f;
        if(t > 1f) 
        	t -= 1f;
        if(t < 1f/6f) 
        	return p + (q - p) * 6f * t;
        if(t < 1f/2f) 
        	return q;
        if(t < 2f/3f) 
        	return p + (q - p) * (2f/3f - t) * 6f;
        return p;
    }
    // adapted from http://en.wikipedia.org/wiki/HSL_color_space

    public static Color HSLtoRGB(float h, float s, float l)
    {
    	Color output = Color.black;
    	if (s < 0.01f)
    	{
    		return output;
    	}

        float q = l < 0.5f ? l * (1 + s) : l + s - l * s;
        float p = 2f * l - q;
        output.r = HueToRGB(p, q, h + 1f/3f);
        output.g = HueToRGB(p, q, h);
        output.b = HueToRGB(p, q, h - 1f/3f);
        return output;
    }

	public static bool StringContainsCaseInsensitive(string lhs, string rhs)
	{
		return (lhs.IndexOf(rhs, StringComparison.OrdinalIgnoreCase) >= 0);
	}

    public static Quaternion GetRotationFromMatix(Matrix4x4 m)
    {
        float qw = Mathf.Sqrt(1f + m.m00 + m.m11 + m.m22) / 2.0f;
        float w = 4.0f * qw;
        float qx = (m.m21 - m.m12) / w;
        float qy = (m.m02 - m.m20) / w;
        float qz = (m.m10 - m.m01) / w;
        return new Quaternion(qx, qy, qz, qw);
    }
	    public static Transform TransformByName(Transform xform, string name)
    {
        foreach (Transform child in xform)
        {
            if (child.name == name)
            {
                return child;
            }
            else
            {
                var result = TransformByName(child, name);
                if (result)
                {
                    return result;
                }
            }
        }
        return null;
    }

    public static float DistanceToObject(GameObject go, Vector3 pos)
    {
        if(!go)
        {
            return 999.9f;
        }
        Vector3 diff = go.transform.position - pos;
        diff.y = 0.0f;
        return diff.magnitude;
    }
}
