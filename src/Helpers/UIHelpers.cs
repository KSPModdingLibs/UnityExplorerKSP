﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Explorer
{
    public class UIHelpers
    {
        private static bool ScrollUnstrippingFailed = false;

        public static Vector2 BeginScrollView(Vector2 scroll) => BeginScrollView(scroll, null);

        public static Vector2 BeginScrollView(Vector2 scroll, GUIStyle style, params GUILayoutOption[] layoutOptions)
        {
            if (ScrollUnstrippingFailed) return scroll;

            try
            {
                if (style != null)
                    return GUILayout.BeginScrollView(scroll, style, layoutOptions);
                else
                    return GUILayout.BeginScrollView(scroll, layoutOptions);
            }
            catch 
            { 
                ScrollUnstrippingFailed = true;
                return scroll;
            }
        }

        public static void EndScrollView()
        {
            if (ScrollUnstrippingFailed) return;

            GUILayout.EndScrollView();
        }

        // helper for "Instantiate" button on UnityEngine.Objects
        public static void InstantiateButton(Object obj, float width = 100)
        {
            if (GUILayout.Button("Instantiate", new GUILayoutOption[] { GUILayout.Width(width) }))
            {
                var newobj = Object.Instantiate(obj);

                WindowManager.InspectObject(newobj, out _);
            }
        }

        // helper for drawing a styled button for a GameObject or Transform
        public static void GameobjButton(object _obj, Action<Transform> specialInspectMethod = null, bool showSmallInspectBtn = true, float width = 380)
        {
            var obj = (_obj as GameObject) ?? (_obj as Transform).gameObject;

            bool children = obj.transform.childCount > 0;

            string label = children ? "[" + obj.transform.childCount + " children] " : "";
            label += obj.name;

            bool enabled = obj.activeSelf;
            int childCount = obj.transform.childCount;
            Color color;

            if (enabled)
            {
                if (childCount > 0)
                {
                    color = Color.green;
                }
                else
                {
                    color = UIStyles.LightGreen;
                }
            }
            else
            {
                color = Color.red;
            }

            FastGameobjButton(_obj, color, label, obj.activeSelf, specialInspectMethod, showSmallInspectBtn, width);
        }

        public static void FastGameobjButton(object _obj, Color activeColor, string label, bool enabled, Action<Transform> specialInspectMethod = null, bool showSmallInspectBtn = true, float width = 380)
        {
            var obj = _obj as GameObject ?? (_obj as Transform).gameObject;

            if (!obj)
            {
                GUILayout.Label("<i><color=red>null</color></i>", null);
                return;
            }

            // ------ toggle active button ------

            GUILayout.BeginHorizontal(null);
            GUI.skin.button.alignment = TextAnchor.UpperLeft;

            GUI.color = activeColor;

            enabled = GUILayout.Toggle(enabled, "", new GUILayoutOption[] { GUILayout.Width(18) });
            if (obj.activeSelf != enabled)
            {
                obj.SetActive(enabled);
            }

            // ------- actual button ---------

            if (GUILayout.Button(label, new GUILayoutOption[] { GUILayout.Height(22), GUILayout.Width(width) }))
            {
                if (specialInspectMethod != null)
                {
                    specialInspectMethod(obj.transform);
                }
                else
                {
                    WindowManager.InspectObject(_obj, out bool _);
                }
            }

            // ------ small "Inspect" button on the right ------

            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white;

            if (showSmallInspectBtn)
            {
                SmallInspectButton(_obj);
            }

            GUILayout.EndHorizontal();
        }

        public static void SmallInspectButton(object obj)
        {
            if (GUILayout.Button("Inspect", null))
            {
                WindowManager.InspectObject(obj, out bool _);
            }
        }
    }
}
