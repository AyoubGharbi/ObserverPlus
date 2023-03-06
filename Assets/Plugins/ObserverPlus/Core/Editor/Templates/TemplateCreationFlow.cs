/*
 * Copyright (c) 2023 Ayoub Gharbi
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
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

        private string _scriptName = string.Empty;
        private string _scriptType = string.Empty;

        private const string PluginsPath = "Plugins/ObserverPlus";
        private const string TemplatesPath = "Core/Editor/Templates";
        private const string GeneratedPath = "Generated";
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
            return Path.Combine(Application.dataPath, PluginsPath, GeneratedPath, concreteFolder);
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This will create three scripts (event, event listener, and event editor).", MessageType.Info);

            GUILayout.Label("Create New Event Scripts", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            _scriptName = EditorGUILayout.TextField("Script Name:", _scriptName);
            EditorGUILayout.LabelField(new GUIContent("Event/EventListener.cs"));
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            _scriptType = EditorGUILayout.TextField("Type:", _scriptType);

            if (GUILayout.Button("Select Primitive Type"))
            {
                GenericMenu menu = new();
                foreach (Type type in GetPrimitiveTypes())
                {
                    string typeName = type.Name.ToLower();
                    menu.AddItem(new GUIContent(typeName), false, () => _scriptType = typeName);
                }
                menu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Create Scripts"))
            {
                CreateScript(TemplateType.Event);
                CreateScript(TemplateType.Listener);
                CreateScript(TemplateType.EventEditor);

                AssetDatabase.Refresh();
                Close();
            }
        }

        private Type[] GetPrimitiveTypes()
        {
            return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.IsPrimitive
                    select type).ToArray();
        }

        private void CreateScript(TemplateType templateType)
        {
            string combinedScriptName = $"{_scriptName}{templateType}";
            string templatePath = Path.Combine(Application.dataPath, PluginsPath, TemplatesPath, $"{templateType}.cs.template");

            string template = File.ReadAllText(templatePath);
            template = template.Replace("#SCRIPTNAME#", _scriptName);
            template = template.Replace("#SCRIPTRTYPE#", _scriptType);

            string concretePath = BaseCreationPath(templateType);
            if (!Directory.Exists(concretePath))
            {
                Directory.CreateDirectory(concretePath);
            }

            string filePath = Path.Combine(concretePath, $"{combinedScriptName}{CsExtension}");
            File.WriteAllText(filePath, template);
        }
    }
}
