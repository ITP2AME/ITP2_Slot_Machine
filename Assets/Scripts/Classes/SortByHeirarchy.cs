using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SortByHierarchy : IComparer<Transform>
{
	public int Compare(Transform x, Transform y)
	{
		if (x == y)
			return 0;
		if (y.IsChildOf(x))
		{
			return -1;
		}
		if (x.IsChildOf(y))
		{
			return 1;
		}
		List<Transform> xparentList = GetParents(x);
		List<Transform> yparentList = GetParents(y);
		for (int xIndex = 0; xIndex < xparentList.Count; xIndex++)
		{
			if (y.IsChildOf(xparentList[xIndex]))
			{
				int yIndex = yparentList.IndexOf(xparentList[xIndex]) - 1;
				xIndex -= 1;
				return xparentList[xIndex].GetSiblingIndex() - yparentList[yIndex].GetSiblingIndex();
			}
		}
		return xparentList[xparentList.Count - 1].GetSiblingIndex() - yparentList[yparentList.Count - 1].GetSiblingIndex();
	}

	private List<Transform> GetParents(Transform t)
	{
		List<Transform> parents = new List<Transform>();
		parents.Add(t);
		while (t.parent != null)
		{
			parents.Add(t.parent);
			t = t.parent;
		}
		return parents;
	}
}