using System.IO;
using UnityEditor;
using UnityEngine;

namespace ObserverPlus.Editor.Template
{
    /// <summary>
    /// Editor window for creating new ObserverPlus event scripts.
    /// </summary>
    public class TemplateCreationFlow : EditorWindow
    {
        private enum TemplateType
        {
            Event,
            Listener,
            EventEditor
        }

        private string _scriptName;
        private string _scriptType;

        private const string PluginsPath = "Plugins/ObserverPlus";
        private const string TemplatesPath = "Scripts/Editor/Templates";
        private const string ConcretePath = "Concrete";
        private const string EditorFolder = "Editor";
        private const string CsExtension = ".cs";

        [MenuItem("ObserverPlus/Create New Event Script")]
        public static void ShowWindow()
        {
            GetWindow<TemplateCreationFlow>("New ObserverPlus Event");
        }

        private string BaseCreationPath(TemplateType templateType)
        {
            string concreteFolder = templateType == TemplateType.EventEditor ? EditorFolder : "";
            return Path.Combine(Application.dataPath, PluginsPath, ConcretePath, concreteFolder);
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This will create three scripts (event, event listener, and event editor).", MessageType.Info);

            GUILayout.Label("Create New Event Scripts", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            _scriptName = EditorGUILayout.TextField("Script Name", _scriptName);
            EditorGUILayout.LabelField(new GUIContent("Event/EventListener.cs"));
            EditorGUILayout.EndHorizontal();
            _scriptType = EditorGUILayout.TextField("Type", _scriptType);

            if (GUILayout.Button("Create Scripts"))
            {
                CreateScript(TemplateType.Event);
                CreateScript(TemplateType.Listener);
                CreateScript(TemplateType.EventEditor);

                AssetDatabase.Refresh();
                Close();
            }
        }

        private void CreateScript(TemplateType templateType)
        {
            string combinedScriptName = $"{_scriptName}{templateType}";
            string templatePath = Path.Combine(Application.dataPath, PluginsPath, TemplatesPath, $"{templateType}.cs.template");

            string template = File.ReadAllText(templatePath);
            template = template.Replace("#SCRIPTNAME#", _scriptName);
            template = template.Replace("#SCRIPTRTYPE#", _scriptType);

            string concretePath = BaseCreationPath(templateType);
            Directory.CreateDirectory(concretePath);

            string filePath = Path.Combine(concretePath, $"{combinedScriptName}{CsExtension}");
            File.WriteAllText(filePath, template);
        }
    }
}
