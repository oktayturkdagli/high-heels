#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Game;

namespace Editor
{
    [CustomEditor(typeof(LevelDataManager), true)]
    public class LevelEditor : UnityEditor.Editor
    {
        private LevelDataManager levelDataManager;
        private LevelData currentLevelData ;
        private Vector3 mousePosition;
        private int levelIndex = 1;
        private bool onMouseDown = false;
        private string[] itemTypesText;
        private int selectedItem = 0;
        private const string HelpBoxText =
            "\nUsing this custom editor you can design your grid based level.\n" +
            "\nNull option deletes the objects you specify with the pointer\n";

        private void OnEnable()
        {
            levelDataManager = target as LevelDataManager;
            ShowItemsInTabMenu();
            SceneView.duringSceneGui += OnScene;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnScene;
        }

        private void OnScene(SceneView sceneView)
        {
            PullLevelData();
            
            if (levelDataManager == null || currentLevelData == null)
                return;
            
            CheckInput();
            HandleInput();
            Draw3DObjectOnScene();
        }
        
        public override void OnInspectorGUI()
        {
            if (currentLevelData == null)
                return;
            
            DrawMyInspector();
        }

        private void DrawMyInspector()
        {
            EditorGUILayout.Space(); GUILayout.Label("Create Your Level", EditorStyles.boldLabel); EditorGUILayout.Space();
            EditorGUILayout.Space(); GUILayout.Label("Draw", EditorStyles.boldLabel); EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Level", EditorStyles.boldLabel);
            levelIndex = EditorGUILayout.IntField(levelIndex);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("width", EditorStyles.boldLabel);
            currentLevelData.width = EditorGUILayout.IntField(currentLevelData.width);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("height", EditorStyles.boldLabel);
            currentLevelData.height = EditorGUILayout.IntField(currentLevelData.height);
            GUILayout.EndHorizontal();

            DrawMyButtons();
            
            EditorGUILayout.Space(20);
            GUILayout.Label("Levels", EditorStyles.boldLabel);
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("levelDataContainer"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox(HelpBoxText, MessageType.None);
            EditorUtility.SetDirty(levelDataManager);
        }

        private void DrawMyButtons()
        {
            if (GUILayout.Button("Draw width x height Road"))
                CreateRoad();
        
            selectedItem = GUILayout.Toolbar(selectedItem, itemTypesText);
        
            EditorGUILayout.Space(20); GUILayout.Label("Clear or Save", EditorStyles.boldLabel); EditorGUILayout.Space();
            if (GUILayout.Button("Remove Last"))
                RemoveAt(mousePosition);
            if (GUILayout.Button("Clear All Of Selected"))
                Clear();
            if (GUILayout.Button("Save"))
                levelDataManager.SaveData();
        }
    
        private void Draw3DObjectOnScene()
        {
            if (currentLevelData == null)
                return;

            Handles.color = Color.magenta;
            Handles.DrawWireDisc(Vector3.zero, Vector3.up, 0.5f);
            if (currentLevelData.levelGrid.Count < 1)
                return;

            for (var i = 0; i < currentLevelData.levelGrid.Count; i++)
            {
                switch (currentLevelData.levelGrid[i].type)
                {
                    case EnvironmentalItemTypes.Null:
                        break;
                    case EnvironmentalItemTypes.Road:
                        Handles.color = Color.white;
                        Handles.DrawWireCube(new Vector3(currentLevelData.levelGrid[i].position.x, currentLevelData.levelGrid[i].position.y, 
                            currentLevelData.levelGrid[i].position.z + currentLevelData.height/2), new Vector3(currentLevelData.width,0f,currentLevelData.height));
                        break;
                    case EnvironmentalItemTypes.Cube:
                        Handles.color = Color.red;
                        Handles.DrawWireCube(currentLevelData.levelGrid[i].position, Vector3.one);
                        break;
                    case EnvironmentalItemTypes.Shoe:
                        Handles.color = Color.magenta;
                        Handles.DrawWireCube(currentLevelData.levelGrid[i].position, Vector3.one);
                        break;
                    case EnvironmentalItemTypes.Diamond:
                        Handles.color = Color.cyan;
                        Handles.DrawWireCube(currentLevelData.levelGrid[i].position, Vector3.one);
                        break;
                    case EnvironmentalItemTypes.Finish:
                        Handles.color = Color.green;
                        Handles.DrawWireDisc(currentLevelData.levelGrid[i].position, Vector3.up, currentLevelData.width);
                        break;
                }
            }
        }
    
        private void CheckInput()
        {
            Event guiEvent = Event.current;
            if (guiEvent.type == EventType.Layout)
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive)); // When the new object is created, Don't assign the inspector to that object 
            else if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0) //When Down the left mouse button
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
                float drawPlaneheight = 0;
                float distanceToDrawPlane = (drawPlaneheight - ray.origin.y) / ray.direction.y;
                mousePosition = ray.GetPoint(distanceToDrawPlane);
                mousePosition.x = Mathf.Round(mousePosition.x);
                mousePosition.z = Mathf.Round(mousePosition.z);
                onMouseDown = true;
            }
            else
            {
                onMouseDown = false;
            }
        }
    
        private void HandleInput()
        {
            if (onMouseDown)
            {
                onMouseDown = false;
                RemoveAt(mousePosition);
                CreateObject(mousePosition);
            }
        }
    
        private void CreateObject(Vector3 position)
        {
            EnvironmentalItemTypes tempEnvironmentalItemType = (EnvironmentalItemTypes)selectedItem;
            if (tempEnvironmentalItemType == EnvironmentalItemTypes.Null)
                return;
        
            var levelItem = new LevelItem
            {
                type = tempEnvironmentalItemType,
                position = position
            };
            currentLevelData.levelGrid.Add(levelItem);
            levelDataManager.SaveData();
        }
    
        private void CreateRoad()
        {
            var position = new Vector3(0, -1, 0);
            selectedItem = 1;
            mousePosition = position;
            RemoveAt(position);
            CreateObject(position);
        }
    
        private void RemoveAt(Vector3 position)
        {
            for (int i = 0; i < currentLevelData.levelGrid.Count; i++)
            {
                if (currentLevelData.levelGrid[i].position == position)
                {
                    currentLevelData.levelGrid.RemoveAt(i);
                    break;
                }
            }
        }

        private void Clear()
        {
            EnvironmentalItemTypes tempEnvironmentalItemType = (EnvironmentalItemTypes)selectedItem;
        
            if (tempEnvironmentalItemType == EnvironmentalItemTypes.Null)
                return;
        
            mousePosition = Vector3.zero;
            for (int i = 0; i < currentLevelData.levelGrid.Count; i++)
            {
                if (currentLevelData.levelGrid[i].type == tempEnvironmentalItemType)
                {
                    currentLevelData.levelGrid.RemoveAt(i);
                    i--;
                }
            }
            levelDataManager.SaveData();
        }
    
        private void ShowItemsInTabMenu()
        {
            int itemTypesLength = Enum.GetValues(typeof(EnvironmentalItemTypes)).Length;
            itemTypesText = new string[itemTypesLength];
            for (int i = 0; i < itemTypesLength; i++)
            {
                itemTypesText[i] = ((EnvironmentalItemTypes)i).ToString();
            }
        }

        private void PullLevelData()
        {
            if (levelDataManager != null && levelDataManager.LevelDataContainer != null)
            {
                if (levelIndex > levelDataManager.LevelDataContainer.levelDataList.Count || levelIndex == 0)
                    levelIndex = levelDataManager.LevelDataContainer.levelDataList.Count;
                
                currentLevelData = levelDataManager.LevelDataContainer.levelDataList[levelIndex - 1];
            }
        }
    }
}
#endif