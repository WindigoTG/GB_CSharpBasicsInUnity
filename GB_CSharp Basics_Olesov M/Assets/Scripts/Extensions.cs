using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BallGame
{
	public static class Extensions
	{
		public static T AddTo<T>(this T self, ICollection<T> coll)
		{
			coll.Add(self);
			return self;
		}

		public static bool TryBool(this string self)
		{
			return Boolean.TryParse(self, out var res) && res;
		}

		public static float TrySingle(this string self)
		{
			if (Single.TryParse(self, out var res))
			{
				return res;
			}
			return 0;
		}

		public static bool IsOneOf<T>(this T self, params T[] elem)
		{
			return elem.Contains(self);
		}

		public static T GetOrAddComponent<T>(this Component child) where T : Component
		{
			T result = child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
			return result;
		}

		public static T[] Concat<T>(this T[] x, T[] y)
		{
			if (x == null) throw new ArgumentNullException("x");
			if (y == null) throw new ArgumentNullException("y");
			var oldLen = x.Length;
			Array.Resize<T>(ref x, x.Length + y.Length);
			Array.Copy(y, 0, x, oldLen, y.Length);
			return x;
		}

		public static T DeepCopy<T>(this T self)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("Type must be iserializable");
			}
			if (ReferenceEquals(self, null))
			{
				return default;
			}

			var formatter = new BinaryFormatter();
			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, self);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}

		public static int ReturnNearestIndex(this Vector3[] nodes, Vector3 destination)
		{
			var nearestDistance = Mathf.Infinity;
			var index = 0;
			var length = nodes.Length;
			for (var i = 0; i < length; i++)
			{
				var distanceToNode = (destination + nodes[i]).sqrMagnitude;
				if (!(nearestDistance > distanceToNode)) continue;
				nearestDistance = distanceToNode;
				index = i;
			}

			return index;
		}

		public static T ReturnRandom<T>(this List<T> list, T[] itemsToExclude)
		{
			var val = list[UnityEngine.Random.Range(0, list.Count)];

			while (itemsToExclude.Contains(val))
				val = list[UnityEngine.Random.Range(0, list.Count)];

			return val;
		}

		public static T ReturnRandom<T>(this List<T> list)
		{
			var val = list[UnityEngine.Random.Range(0, list.Count)];
			return val;
		}

		public static T Random<T>(this T[] vals)
		{
			return vals[UnityEngine.Random.Range(0, vals.Length)];
		}

		public static float GetRandom(this Vector2 v)
		{
			return UnityEngine.Random.Range(v.x, v.y);
		}

		public static Vector3 MultiplyX(this Vector3 v, float val)
		{
			v = new Vector3(val * v.x, v.y, v.z);
			return v;
		}

		public static Vector3 MultiplyY(this Vector3 v, float val)
		{
			v = new Vector3(v.x, val * v.y, v.z);
			return v;
		}

		public static Vector3 MultiplyZ(this Vector3 v, float val)
		{
			v = new Vector3(v.x, v.y, val * v.z);
			return v;
		}

		public static T[] Increase<T>(this T[] values, int increment)
		{
			T[] array = new T[values.Length + increment];
			values.CopyTo(array, 0);
			return array;
		}

		public static Transform FindDeep(this Transform obj, string id)
		{
			if (obj.name == id)
			{
				return obj;
			}

			var count = obj.childCount;
			for (var i = 0; i < count; ++i)
			{
				var posObj = obj.GetChild(i).FindDeep(id);
				if (posObj != null)
				{
					return posObj;
				}
			}

			return null;
		}

		public static List<T> GetAll<T>(this Transform obj)
		{
			var results = new List<T>();
			obj.GetComponentsInChildren(results);
			return results;
		}

		public static Color SetColorAlpha(this Color c, float alpha)
		{
			return new Color(c.r, c.g, c.b, alpha);
		}
	}
}
