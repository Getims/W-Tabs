using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;

namespace WTabs
{
    public class WTabsWindow : EditorWindow
    {
        Editor _baseEditor;
        private static WTabsSettings _windowsMeta;
        private Vector2 _mainScroll = Vector2.zero;

        [MenuItem("Tools/W-Tabs/Settings _0", false, -101)]
        public static void ShowWindow()
        {
            EditorWindow wnd = EditorWindow.GetWindow(typeof(WTabsWindow));
            wnd.titleContent = new GUIContent("W-Tabs");
            wnd.minSize = new Vector2(500, 250);

            if (_windowsMeta != null)
                return;
            FindMeta();
        }

        private static void FindMeta()
        {
            //Debug.LogWarning("#W-Tabs: Find settings");
            string typeName = "t:" + typeof(WTabsSettings).FullName;
            string[] guids = AssetDatabase.FindAssets(typeName);
            if (guids.Length > 0)
                _windowsMeta = AssetDatabase.LoadAssetAtPath<WTabsSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
        }

        private void OnGUI()
        {
            GUIStyle centeredLabelBold = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.UpperCenter
            };
            GUIStyle centeredLabel = new GUIStyle()
            {
                alignment = TextAnchor.UpperCenter
            };

            GUILayout.Label("W-Tabs can create up to 32 windows!", centeredLabelBold);

            if (_windowsMeta == null)
                FindMeta();

            EditorGUILayout.BeginVertical("box");
            {
                _mainScroll = EditorGUILayout.BeginScrollView(_mainScroll, false, false);
                {
                    if (_windowsMeta != null)
                    {
                        GUILayout.Label("For every list element will be created a window with name of this element.", centeredLabel);
                        GUILayout.Space(10);
                        Editor.DrawFoldoutInspector(_windowsMeta, ref _baseEditor);

                        if (_windowsMeta.Windows.Count > 0)
                        {
                            if (GUILayout.Button("Create first window")) //8
                            {
                                BaseTab.ShowWindow(typeof(W_0), _windowsMeta.Windows[0]);
                            }
                        }
                        else
                            GUILayout.Label("Add first window", centeredLabelBold);
                    }
                    else
                    {
                        GUILayout.Label("WTabs settings file not found!", centeredLabelBold);
                        if (GUILayout.Button("Create settings file")) //8
                        {
                            CreateMeta();
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            GUILayout.Label("version 0.1.4", centeredLabelBold);
        }

        private static void CreateMeta()
        {
            WTabsSettings asset = ScriptableObject.CreateInstance<WTabsSettings>();

            string path = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            path = path.Replace(System.IO.Directory.GetCurrentDirectory(), "").Replace('\\', '/').Replace(string.Concat(nameof(WTabsWindow), ".cs"), "").Remove(0, 1);
            AssetDatabase.CreateAsset(asset, string.Concat(path, nameof(WTabsSettings), ".asset"));
            Debug.Log(string.Concat("Create ", nameof(WTabsSettings), " at path: ", path));
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        #region Shortcuts
        private static void ShowWindow(int number)
        {
            if (_windowsMeta == null)
                FindMeta();

            if (_windowsMeta == null)
            {
                Debug.LogError("WTabs settings file not found!");
                return;
            }

            if (number >= _windowsMeta.Windows.Count)
            {
                Debug.LogError("WTabs window " + (number+1).ToString()+ " not Setuped!");
                return;
            }

            BaseTab.ShowWindow(BaseTab.WindowsTypes[number], _windowsMeta.Windows[number]);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_1 _1", false, -101)]
        private static void ShowWindow_1()
        {
            ShowWindow(0);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_2 _2", false, -101)]
        private static void ShowWindow_2()
        {
            ShowWindow(1);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_3 _3", false, -101)]
        private static void ShowWindow_3()
        {
            ShowWindow(2);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_4 _4", false, -101)]
        private static void ShowWindow_4()
        {
            ShowWindow(3);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_5 _5", false, -101)]
        private static void ShowWindow_5()
        {
            ShowWindow(4);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_6 _6", false, -101)]
        private static void ShowWindow_6()
        {
            ShowWindow(5);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_7 _7", false, -101)]
        private static void ShowWindow_7()
        {
            ShowWindow(6);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_8 _8", false, -101)]
        private static void ShowWindow_8()
        {
            ShowWindow(7);
        }

        [MenuItem("Tools/W-Tabs/ShowWindow_9 _9", false, -101)]
        private static void ShowWindow_9()
        {
            ShowWindow(8);
        }
        #endregion
    }
}