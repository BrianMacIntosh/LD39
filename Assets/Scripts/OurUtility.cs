using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class OurUtility
{
	public static T GetOrAddComponent<T>(GameObject obj) where T : Component
	{
		T component = obj.GetComponent<T>();
		if (component == null)
		{
			component = obj.AddComponent<T>();
		}
		return component;
	}
}