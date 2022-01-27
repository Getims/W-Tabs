using System.Collections.Generic;
using UnityEngine;

namespace WTabs
{
    [CreateAssetMenu(fileName = "WTabsSettings", menuName = "W-Tabs/Settings", order = 1)]
    public class WTabsSettings : ScriptableObject
    {
        [SerializeField] private List<string> _windows = new List<string>();

        public List<string> Windows => _windows;

        private void OnValidate()
        {
            if (_windows.Count > 32)
                _windows = new List<string>(_windows.GetRange(0, 32));
        }
    }
}