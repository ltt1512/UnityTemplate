﻿// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace Doozy.Runtime.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary> Get a random item from the target list </summary>
        /// <param name="target"> Target list </param>
        /// <typeparam name="T"> Item type </typeparam>
        /// <returns> Random item from list </returns>
        public static T GetRandomItem<T>(this List<T> target) =>
            target[Random.Range(0, target.Count)];

        /// <summary> Shuffle the contents of the target list </summary>
        /// <param name="target"> Target list </param>
        /// <typeparam name="T"> Item type </typeparam>
        /// <returns> Shuffled list </returns>
        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);// Lấy ngẫu nhiên chỉ số k trong khoảng [0, n]
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        /// <summary> Remove null entries from the target list </summary>
        /// <param name="target"> Target list </param>
        /// <returns> The list without any null reference </returns>
        public static List<T> RemoveNulls<T>(this List<T> target)
        {
            for (int i = target.Count - 1; i >= 0; i--)
                if (target[i] == null)
                    target.RemoveAt(i);

            return target;
        }

    }
}
