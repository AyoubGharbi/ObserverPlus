using System.IO;
using UnityEditor;
using UnityEngine;

namespace ObserverPlus.Editor.Template
{
    public class TemplateCreationFlow : EditorWindow
    {
        private enum TemplateType
        {
            Event = 0,
            Listener = 1,
            EventEditor = 2
        }

        private string _scriptName;
        private string _scriptType;

        [MenuItem("ObserverPlus/Create New Event Script")]
        public static void ShowWindow()
        {
            GetWindow<TemplateCreationFlow>("New ObserverPlus Event");
        }

        private string GetBaseCreationPath(TemplateType templateType)
        {
            string concreteFolder = templateType != TemplateType.EventEditor ? "Concrete/" : "Editor/Concrete/";
            return Path.Combine(Path.GetDirectoryName((Path.GetDirectoryName(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)))))), concreteFolder);
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This will create two scripts (event and event listener).", MessageType.Info);

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

                CleanUp();
            }
        }

        private void CreateScript(TemplateType templateType)
        {
            string combinedScriptName = $"{_scriptName}{templateType}";
            string templatePath = GetTemplatePath(templateType);
            string template = File.ReadAllText(templatePath);

            // Replace the placeholders in the template with the actual values
            template = template.Replace("#SCRIPTNAME#", $"{_scriptName}");
            template = template.Replace("#SCRIPTRTYPE#", _scriptType);

            // Create the new script file
            string concretePath = GetBaseCreationPath(templateType);
            string filePath = $"{concretePath}{combinedScriptName}.cs";

            if (!Directory.Exists($"{concretePath}"))
                Directory.CreateDirectory($"{concretePath}");

            File.WriteAllText(filePath, template);
        }

        private void CleanUp()
        {
            AssetDatabase.Refresh();
            Close();
        }

        private string GetTemplatePath(TemplateType templateType)
        {
            string templateName = $"{templateType}.cs.template";
            return Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this))), templateName);
        }
    }
}