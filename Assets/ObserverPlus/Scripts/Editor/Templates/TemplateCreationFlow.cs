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
            Listener = 1
        }

        private string _scriptName;
        private string _scriptType;
        private string _baseCreationPath = string.Empty;

        [MenuItem("ObserverPlus/Create New Event Script")]
        public static void ShowWindow()
        {
            GetWindow<TemplateCreationFlow>("New ObserverPlus Event");
        }

        private void OnEnable()
        {
            _baseCreationPath = Path.Combine(Path.GetDirectoryName((Path.GetDirectoryName(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)))))), "Data/");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New ObserverPlus Event Script", EditorStyles.boldLabel);

            _scriptName = EditorGUILayout.TextField("Script Name", _scriptName);
            _scriptType = EditorGUILayout.TextField("Type", _scriptType);

            if (GUILayout.Button("Create Scripts"))
            {
                CreateScript(TemplateType.Event);
                CreateScript(TemplateType.Listener);

                CleanUp();
            }
        }

        private void CreateScript(TemplateType templateType)
        {
            string combinedScriptName = $"{_scriptName}{templateType}";
            string templatePath = GetTemplatePath(templateType);
            string template = File.ReadAllText(templatePath);

            // Replace the placeholders in the template with the actual values
            template = template.Replace("#SCRIPTNAME#", $"{combinedScriptName}");
            template = template.Replace("#SCRIPTRTYPE#", _scriptType);

            // Create the new script file
            string filePath = $"{_baseCreationPath}{combinedScriptName}.cs";

            if (!Directory.Exists($"{_baseCreationPath}"))
                Directory.CreateDirectory($"{_baseCreationPath}");

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