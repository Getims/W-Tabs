using UnityEngine;
using UnityEditor;

namespace WTabs
{
    public class BaseTab : EditorWindow
    {
        private static int _windowsCount = 0;
        private Vector2 _mainScroll = Vector2.zero;
        private static WTabsSettings _windowsMeta;
        private static string[] _windows;
        private static System.Type _currentType = typeof(BaseTab);

        public static System.Type[] WindowsTypes = {typeof(W_0), typeof(W_1), typeof(W_2), typeof(W_3), typeof(W_4),
                                            typeof(W_5), typeof(W_6), typeof(W_7), typeof(W_8), typeof(W_9),
                                            typeof(W_10), typeof(W_11), typeof(W_12), typeof(W_13), typeof(W_14),
                                            typeof(W_15), typeof(W_16), typeof(W_17), typeof(W_18), typeof(W_19),
                                            typeof(W_20), typeof(W_21), typeof(W_22), typeof(W_23), typeof(W_24),
                                            typeof(W_25), typeof(W_26), typeof(W_27), typeof(W_28), typeof(W_29),
                                            typeof(W_30), typeof(W_31)};

        public static void ShowWindow(System.Type windowType, string windowTitle)
        {
            _currentType = windowType;
            EditorWindow wnd = EditorWindow.GetWindow(windowType);
            wnd.titleContent = new GUIContent(windowTitle);

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
            {
                _windowsMeta = AssetDatabase.LoadAssetAtPath<WTabsSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
                _windows = new string[_windowsMeta.Windows.Count];
                for (int i = 0; i < _windowsMeta.Windows.Count; i++)
                    _windows[i] = _windowsMeta.Windows[i];
                _windowsCount = _windows.Length;
            }
        }

        void OnGUI()
        {
            if (_windowsMeta == null)
                FindMeta();

            if (_windowsMeta == null)
            {
                ShowMetaError();
                return;
            }

            ShowButtons();
            ShowUpdateButton();
            ShowFitButton();
        }

        private void ShowButtons()
        {
            //Debug.LogWarning("#W-Tabs: Draw buttons");
            EditorGUILayout.BeginVertical("box");
            {
                _mainScroll = EditorGUILayout.BeginScrollView(_mainScroll, false, false);
                {
                    for (int i = 0; i < _windowsCount; i++)
                    {
                        if (GUILayout.Button(_windows[i])) //8
                        {
                            ShowWindow(WindowsTypes[i], _windows[i]);
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void ShowUpdateButton()
        {
            if (GUILayout.Button("Update buttons list")) //8
            {
                FindMeta();
            }
        }

        private void ShowFitButton()
        {
            if (_currentType != typeof(BaseTab))
            {
                if (GUILayout.Button("Fit screen size")) //8
                {
                    EditorWindow wnd = EditorWindow.GetWindow(_currentType);
                    Vector2 size = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height - 153);
                    Vector2 position = new Vector2(0, 73);
                    wnd.position = new Rect(position, size);
                }
            }
        }

        private void ShowMetaError()
        {
            GUIStyle centeredLabel = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.UpperCenter
            };

            //Debug.LogWarning("#W-Tabs: Settings not found");
            GUILayout.Label("WTabs settings file not found", centeredLabel);
        }
    }
}