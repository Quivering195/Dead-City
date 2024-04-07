using Cinemachine;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

namespace Ring
{
    #region Component

    #region Player

    #region Skins

    public enum PlayerSkin
    {
        Skin1,
        Skin2,
        Skin3,
        Skin4,
        Skin5,
        Skin6,
        Skin7,
        Skin8,
        Skin9,
        Skin10,
        Skin11,
        Skin12,
        Skin13,
        Skin14,
        Skin15
    }

    [Serializable]
    public class ChangeSkins
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Skins")] [ChangeColorLabel(0.2f, 1, 1)]
        public PlayerSkin selectedSkin;

        [ChangeColorLabel(0.2f, 1, 1)] public List<GameObject> listObjectSkins;
    }

    #endregion Skins

    [Serializable]
    public class PlayerComponent
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Component")] [ChangeColorLabel(0.2f, 1, 1)]
        public Rigidbody _rigidbody;

        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _objectModel;
        [ChangeColorLabel(0.2f, 1, 1)] public Collider _collider;
    }

    [Serializable]
    public class PlayerMovement
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Movement")] [ChangeColorLabel(0.2f, 1, 1)]
        public Vector3 _direction;

        [ChangeColorLabel(0.2f, 1, 1)] public float _speed;
        [ChangeColorLabel(0.2f, 1, 1)] public float _moveX;
        [ChangeColorLabel(0.2f, 1, 1)] public float _moveY;
    }

    #endregion Player

    [Serializable]
    public class GameController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Game Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public GameObject _player;
    }

    [Serializable]
    public class BotController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Bot Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public Rigidbody _rigidbodyBot;
    }

    [Serializable]
    public class MusicController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Clip")] [ChangeColorLabel(0.9f, .55f, .95f)]
        public AudioClip audioClip_;

        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Source")] [ChangeColorLabel(0.2f, 1, 1)]
        public AudioSource audioSource_BackGround;
    }

    [Serializable]
    public class UiController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Ui Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public GameObject _winGameObject;
    }

    [Serializable]
    public class ShooterController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Shooter Controller")] [ChangeColorLabel(0.2f, 1, 1)]
        public CinemachineVirtualCamera _aimVirtualCamera;

        [ChangeColorLabel(0.2f, 1, 1)] public StarterAssetsInputs _starterAssetsInputs;
        [ChangeColorLabel(0.2f, 1, 1)] public ThirdPersonController _thirdPersonController;
        [ChangeColorLabel(0.2f, 1, 1)] public float _normalSentivity;
        [ChangeColorLabel(0.2f, 1, 1)] public float _aimSentivity;
        [ChangeColorLabel(0.2f, 1, 1)] public float _consoleSentivity;
        [ChangeColorLabel(0.2f, 1, 1)] public LayerMask _aimColliderLayerMask;
        [ChangeColorLabel(0.2f, 1, 1)] public Animator _animator;

        [MinMax(-5f, 5f)] [ChangeColorLabel(0.2f, 1, 1)]
        public float _rotateAim;
    }

    #endregion Component


    #region Text Color

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HeaderTextColorAttribute))]
    public class HeaderTextColorDecorator : DecoratorDrawer
    {
        private GUIStyle headerStyle;
        private bool initialized;

        private void InitGUIStyle()
        {
            headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = ((HeaderTextColorAttribute)attribute).color;
            initialized = true;
        }

        public override float GetHeight()
        {
            /*if (!initialized)
            {
                InitGUIStyle();
            }*/

            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position)
        {
            if (!initialized)
            {
                InitGUIStyle();
            }

            HeaderTextColorAttribute attribute = (HeaderTextColorAttribute)this.attribute;
            string headerText = attribute.headerText;

            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth + 100, 50);
            EditorGUI.LabelField(labelRect, headerText, headerStyle);
        }
    }

    [CustomPropertyDrawer(typeof(ChangeColorLabelAttribute))]
    public class RedLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the color from the attribute
            ChangeColorLabelAttribute changeColorLabelAttribute = (ChangeColorLabelAttribute)attribute;
            Color labelColor = changeColorLabelAttribute.color;

            // Set the color
            GUIStyle coloredLabelStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = labelColor } };
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(label).x;

            // Draw the colored label
            EditorGUI.LabelField(position, label, coloredLabelStyle);

            // Draw the property without the label
            EditorGUIUtility.labelWidth = originalLabelWidth;
            position.x += EditorGUIUtility.labelWidth;
            position.width -= EditorGUIUtility.labelWidth;
            EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }
    }

#endif

    public class HeaderTextColorAttribute : PropertyAttribute
    {
        public Color color;
        public string headerText;

        public HeaderTextColorAttribute(float r, float g, float b, float a = 1.0f, string headerText = "")
        {
            color = new Color(r, g, b, a);
            this.headerText = headerText;
        }
    }

    public class ChangeColorLabelAttribute : PropertyAttribute
    {
        public Color color;

        public ChangeColorLabelAttribute(float r, float g, float b, float a = 1.0f)
        {
            color = new Color(r, g, b, a);
        }
    }

    #endregion Text Color

    #region Editor Window

#if UNITY_EDITOR

    #region Save Position Object

    public class SavingPositionObject : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Saving Position Object

        //Load luôn position sau khi dừng play
        [MenuItem("Ring/Save Position/Saving Position - After Play Load Position Now")]
        public static void ShowWindow()
        {
            GetWindow<SavingPositionObject>("Saving Position Object");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += HandlePlayModeChange;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeChange;
        }

        private void HandlePlayModeChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                LoadPositions();
                Debug.Log(1);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }
        }

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                if (entry.Key != null)
                {
                    entry.Key.transform.position = entry.Value;
                }
            }
        }

        #endregion Saving Position Object
    }

    public class ObjectPositionSaverEditor : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Object Position Saver

        //Cho phép chọn load hay không sau khi play
        [MenuItem("Ring/Save Position/Saving Position - After Play, Change Button Load Position")]
        public static void ShowWindow()
        {
            GetWindow<ObjectPositionSaverEditor>("Object Position Saver");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }

            if (GUILayout.Button("Load Positions"))
            {
                LoadPositions();
            }
        }

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                entry.Key.transform.position = entry.Value;
            }
        }

        #endregion Object Position Saver
    }

    public class TransformListEditorWindow : EditorWindow
    {
        [SerializeField] private Transform[] objectList;

        //Lưu transform cho list object
        [MenuItem("Ring/Save Position/Save Transform List - VIP(Save Even While Playing)")]
        public static void ShowWindow()
        {
            GetWindow<TransformListEditorWindow>("Transform List Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Transform List Editor", EditorStyles.boldLabel);

            // Hiển thị danh sách Transform
            EditorGUILayout.LabelField("Object List:");
            EditorGUI.indentLevel++;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty objectListProperty = serializedObject.FindProperty("objectList");
            EditorGUILayout.PropertyField(objectListProperty, true);
            serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;

            // Button lưu transform
            if (GUILayout.Button("Save Transform"))
            {
                SaveTransformList();
            }

            // Button load transform khi tắt chế độ chơi
            if (GUILayout.Button("Load Transform"))
            {
                LoadTransformList();
            }
        }

        private void SaveTransformList()
        {
            if (objectList != null && objectList.Length > 0)
            {
                EditorPrefs.SetInt("TransformListCount", objectList.Length);

                for (int i = 0; i < objectList.Length; i++)
                {
                    if (objectList[i] != null)
                    {
                        EditorPrefs.SetString($"Transform_{i}_Position", objectList[i].position.ToString());
                        EditorPrefs.SetString($"Transform_{i}_Rotation", objectList[i].rotation.ToString());
                    }
                }

                Debug.Log("Transform List Saved!");
            }
            else
            {
                Debug.LogWarning("No objects selected to save!");
            }
        }

        private void LoadTransformList()
        {
            int transformListCount = EditorPrefs.GetInt("TransformListCount", 0);

            if (transformListCount > 0)
            {
                for (int i = 0; i < transformListCount; i++)
                {
                    string positionString = EditorPrefs.GetString($"Transform_{i}_Position", "");
                    string rotationString = EditorPrefs.GetString($"Transform_{i}_Rotation", "");

                    if (!string.IsNullOrEmpty(positionString) && !string.IsNullOrEmpty(rotationString))
                    {
                        Vector3 position = ParseVector3(positionString);
                        Quaternion rotation = ParseQuaternion(rotationString);

                        if (objectList != null && i < objectList.Length && objectList[i] != null)
                        {
                            objectList[i].position = position;
                            objectList[i].rotation = rotation;
                        }
                    }
                }

                Debug.Log("Transform List Loaded!");
            }
            else
            {
                Debug.LogWarning("No saved transform list to load!");
            }
        }

        private Vector3 ParseVector3(string vectorString)
        {
            vectorString = vectorString.Replace("(", "").Replace(")", "");
            string[] values = vectorString.Split(',');

            if (values.Length == 3)
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);

                return new Vector3(x, y, z);
            }

            return Vector3.zero;
        }

        private Quaternion ParseQuaternion(string quaternionString)
        {
            quaternionString = quaternionString.Replace("(", "").Replace(")", "");
            string[] values = quaternionString.Split(',');

            if (values.Length == 4)
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                float w = float.Parse(values[3]);

                return new Quaternion(x, y, z, w);
            }

            return Quaternion.identity;
        }
    }

    #endregion Save Position Object

    #region Rename

    #region Rename Scene, Asset

    public class RenamingScenes : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty scenesProp;
        private int startingNumber;

        //Thay tên scenes
        [MenuItem("Ring/Rename/Scenes")]
        public static void ShowWindow()
        {
            RenamingScenes window = (RenamingScenes)GetWindow(typeof(RenamingScenes));
            window.so = new SerializedObject(window);
            window.scenesProp = window.so.FindProperty("scenes");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingNumber = EditorGUILayout.IntField("Starting Number:", startingNumber);

            // Display each scene field
            EditorGUILayout.PropertyField(scenesProp, true);
            so.ApplyModifiedProperties();

            // Button to rename scenes
            if (GUILayout.Button("Rename Scenes"))
            {
                RenameScenes();
            }
        }

        public List<SceneAsset> scenes;

        private void RenameScenes()
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] != null)
                {
                    string newPath = AssetDatabase.GetAssetPath(scenes[i]).Replace(scenes[i].name,
                        startingNumber.ToString());
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(scenes[i]), startingNumber.ToString());
                    AssetDatabase.Refresh();
                    startingNumber++;
                }
            }
        }
    }

    public class RenamingAssets : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty assetsProp;
        private string startingNumber;
        private bool isAddNumber;

        //Thay tên object trên Prefabs
        [MenuItem("Ring/Rename/Assets")]
        public static void ShowWindow()
        {
            RenamingAssets window = (RenamingAssets)GetWindow(typeof(RenamingAssets));
            window.so = new SerializedObject(window);
            window.assetsProp = window.so.FindProperty("assets");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingNumber = EditorGUILayout.TextField("Starting Number:", startingNumber);
            isAddNumber = EditorGUILayout.Toggle("Add Number Behind Name ?:", isAddNumber);
            // Display each asset field
            EditorGUILayout.PropertyField(assetsProp, true);
            so.ApplyModifiedProperties();

            // Button to rename assets
            if (GUILayout.Button("Rename Assets"))
            {
                RenameAssets();
            }
        }

        public List<UnityEngine.Object> assets;

        private void RenameAssets()
        {
            for (int i = 0; i < assets.Count; i++)
            {
                if (assets[i] != null)
                {
                    if (Int32.TryParse(startingNumber, out int result))
                    {
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(assets[i]), startingNumber.ToString());
                        AssetDatabase.Refresh();
                        startingNumber = (result + 1).ToString();
                    }
                    else
                    {
                        if (isAddNumber)
                        {
                            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(assets[i]),
                                startingNumber.ToString() + " " + (i + 1));
                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            Debug.LogError("Must number behind");
                        }
                    }
                }
            }
        }
    }

    #endregion Rename Scene, Asset

    #region Rename Object in Scene

    #region Rename => String

    public class RenamingObjectsInSceneToString : EditorWindow
    {
        private string startingName;
        private SerializedObject so;
        private SerializedProperty objectsProp;
        private bool isAddNumber;
        public List<GameObject> objectsToRename;

        //Thay tên object trên scene
        [MenuItem("Ring/Rename/Objects In Scene")]
        public static void ShowWindow()
        {
            RenamingObjectsInSceneToString window =
                (RenamingObjectsInSceneToString)GetWindow(typeof(RenamingObjectsInSceneToString));
            window.so = new SerializedObject(window);
            window.objectsProp = window.so.FindProperty("objectsToRename");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingName = EditorGUILayout.TextField("Starting Name:", startingName);
            isAddNumber = EditorGUILayout.Toggle("Add Number Behind Name ?:", isAddNumber);

            // Display each object field
            EditorGUILayout.PropertyField(objectsProp, true);
            so.ApplyModifiedProperties();

            // Button to rename objects in scene
            if (GUILayout.Button("Rename Objects in Scene"))
            {
                RenameObjectsInScene();
            }
        }

        private void RenameObjectsInScene()
        {
            if (startingName != null)
            {
                for (int i = 0; i < objectsToRename.Count; i++)
                {
                    if (objectsToRename[i] != null)
                    {
                        if (!Int32.TryParse(startingName, out int result))
                        {
                            if (isAddNumber)
                            {
                                objectsToRename[i].name = startingName + " " + i;
                            }
                            else
                            {
                                objectsToRename[i].name = startingName;
                            }
                        }
                        else
                        {
                            objectsToRename[i].name = result.ToString();
                            startingName = (Int32.Parse(startingName) + 1).ToString();
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Null String Name!");
            }
        }
    }

    #endregion Rename => String

    #endregion Rename Object in Scene

    #region Rename Tag Object Scene

    public class RenamingTag : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty assetsProp;

        //Thay tên hoặc các thuộc tính cho object trên scene
        [MenuItem("Ring/Rename/Rename Prefabs Tag,Name,Layer,Position...")]
        public static void ShowWindow()
        {
            RenamingTag window = (RenamingTag)GetWindow(typeof(RenamingTag));
            window.so = new SerializedObject(window);
            window.assetsProp = window.so.FindProperty("assets");
            window.Show();
        }

        private void OnGUI()
        {
            // Display each asset field
            EditorGUILayout.PropertyField(assetsProp, true);
            so.ApplyModifiedProperties();
            // Button to rename assets
            if (GUILayout.Button("Change Options"))
            {
                ChangeOptions();
            }
        }

        public List<UnityEngine.Object> assets;

        private void ChangeOptions()
        {
            for (int i = 0; i < assets.Count; i++)
            {
                GameObject gameObject = assets[i] as GameObject;
                if (gameObject != null)
                {
                    Transform child = gameObject.transform;

                    // Kiểm tra child tồn tại và có thể đổi tên
                    if (child != null)
                    {
                        // Ghi lại thay đổi
                        Undo.RecordObject(child.gameObject, "Rename Tags");

                        // Thay đổi tên
                        child.gameObject.name = "Mesh";
                        //child.gameObject.tag = "ObjectTouch";
                        //child.gameObject.layer = LayerMask.NameToLayer("ObjectTouch");
                        //child.transform.rotation = Quaternion.Euler(19, 180, 0);

                        // Lưu lại thay đổi
                        EditorUtility.SetDirty(child.gameObject);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }

    #endregion Rename Tag Object Scene

    #endregion Rename

    #region Component

    #region CopyComponent

    //copy component prefabs
    public class CopyComponentEditorWindow : EditorWindow
    {
        // Chỉ sử dụng nhấn từng prefabs 1 để xác định object và click vào Gui sau đó nhấn phím K sẽ tự động lấy asset
        [MenuItem("Ring/Component/Copy Components In One Prefabs (With Separate Assets - Prefabs)")]
        private static void ShowWindow()
        {
            GetWindow<CopyComponentEditorWindow>().Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Copy Components with Separate Assets - Prefabs"))
            {
                CopyComponentsWithSeparateAssets();
            }

            Event e = Event.current;
            if (e.keyCode == KeyCode.K && e.type == EventType.KeyDown)
            {
                CopyComponentsWithSeparateAssets();
            }
        }

        private void CopyComponentsWithSeparateAssets()
        {
            // Get selected object A
            GameObject objA = Selection.activeGameObject;
            if (objA == null)
            {
                Debug.LogWarning("Please select object A to copy components from.");
                return;
            }

            GameObject objB = objA.transform.GetChild(0).gameObject;
            // Get components to copy
            MeshCollider meshFilterA = objA.GetComponent<MeshCollider>();
            // Handle existing components on object B
            MeshFilter meshFilterB = objB.GetComponent<MeshFilter>();

            meshFilterA.sharedMesh = meshFilterB.sharedMesh;
        }
    }

    #endregion CopyComponent

    #region GetComponent

    public class ObjectTouchColliderEditorWindow : EditorWindow
    {
        //Get component in editor
        [MenuItem("Ring/Component/Auto Get Components for Selected Objects (Allows Multiple Objects To Be Selected )")]
        public static void ShowWindow()
        {
            GetWindow<ObjectTouchColliderEditorWindow>("Auto Assign Components");
        }

        private void OnGUI()
        {
            GUILayout.Label("Auto Assign Components for Selected Objects", EditorStyles.boldLabel);

            if (GUILayout.Button("Auto Assign Components"))
            {
                AutoAssignComponentsForSelectedObjects();
            }
        }

        private void AutoAssignComponentsForSelectedObjects()
        {
            //tạo class ObjectTouch và tạo method getcomponent như bình thường
            /*ObjectTouch[] selectedObjects = Selection.GetFiltered<ObjectTouch>(SelectionMode.Editable | SelectionMode.Deep);
            foreach (ObjectTouch obj in selectedObjects)
            {
                Undo.RecordObject(obj, "Auto Assign Components");
                obj.SetupComponents();
                EditorUtility.SetDirty(obj);
            }*/
        }
    }

    #endregion GetComponent

    #endregion Component

#endif

    #endregion Editor Window

    #region Base Method

    public abstract class RingSingleton<T> : MonoBehaviour where T : RingSingleton<T>
    {
        private static T _instance;

        public enum ChangeDestroy
        {
            DontDestroy,
            Destroy
        }

        public ChangeDestroy _changDestroy = ChangeDestroy.Destroy;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) +
                                       " is needed in the scene, but there is none.");
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = (T)this;
            if (_changDestroy == ChangeDestroy.DontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    #endregion Base Method

    #region Attributes

    #region Group

    //EX:group attribute
    /*
        [Group("Group 1", "Red")]
        public int Var1;

        [Group("Group 1", "Green")]
        public int Var2;

        [Group("Group 2", "Yellow")]
        public string Var3;

        public string Var4;
        public string Var5;
     */

    public class GroupAttribute : PropertyAttribute
    {
        public string Name;
        public Color Color;

        public GroupAttribute(string name, string color)
        {
            Name = name;
            switch (color.ToLower())
            {
                case "red":
                    Color = Color.red;
                    break;

                case "green":
                    Color = Color.green;
                    break;

                case "yellow":
                    Color = Color.yellow;
                    break;

                case "brown":
                    Color = new Color(0.65f, 0.16f, 0.16f);
                    break;

                default:
                    Color = Color.white;
                    break;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class GroupedInspector : Editor
    {
        private class Group
        {
            public string Name;
            public Color Color;
            public List<SerializedProperty> Properties = new List<SerializedProperty>();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var groups = new Dictionary<string, Group>();
            var noGroupProperties = new List<SerializedProperty>();

            var iterator = serializedObject.GetIterator();
            iterator.NextVisible(true); // Skip the "m_Script" property

            while (iterator.NextVisible(false))
            {
                var property = serializedObject.FindProperty(iterator.propertyPath);
                var fieldInfo =
                    target.GetType().GetField(iterator.propertyPath,
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var attributes = fieldInfo?.GetCustomAttributes(typeof(GroupAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    var attribute = attributes[0] as GroupAttribute;
                    var groupName = attribute.Name;

                    if (!groups.ContainsKey(groupName))
                    {
                        groups[groupName] = new Group { Name = groupName, Color = attribute.Color };
                    }

                    groups[groupName].Properties.Add(property);
                }
                else
                {
                    noGroupProperties.Add(property);
                }
            }

            // Display the groups first
            foreach (var group in groups.Values)
            {
                GUIStyle style = new GUIStyle(EditorStyles.foldout);
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = group.Color; // Normal color
                style.onNormal.textColor = Color.black; // Color when expanded
                bool foldout = EditorPrefs.GetBool(group.Name, false);
                bool newFoldout = EditorGUILayout.Foldout(foldout, group.Name, true, style);
                if (newFoldout != foldout)
                {
                    EditorPrefs.SetBool(group.Name, newFoldout);
                }

                if (newFoldout)
                {
                    EditorGUI.indentLevel++;
                    foreach (var property in group.Properties)
                    {
                        EditorGUILayout.PropertyField(property, true);
                    }

                    EditorGUI.indentLevel--;
                }
            }

            // Then display properties that don't have a group
            foreach (var property in noGroupProperties)
            {
                EditorGUILayout.PropertyField(property, true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif

    #endregion Group

    #region Min

    //EX:limit attribute
    /*
        [MinMax(0f, 10f)]
        public float someFloat;
     */

    public class MinMaxAttribute : PropertyAttribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public MinMaxAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxAttribute minMax = (MinMaxAttribute)attribute;

            if (property.propertyType == SerializedPropertyType.Float)
            {
                property.floatValue = Mathf.Clamp(property.floatValue, minMax.Min, minMax.Max);
                EditorGUI.PropertyField(position, property, label);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = Mathf.Clamp(property.intValue, (int)minMax.Min, (int)minMax.Max);
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use MinMax with float or int.");
            }
        }
    }

#endif

    #endregion Min

    #region Search Enum

    //EX:search enum by text
    /*
        [SearchableEnum]
        public MyEnum AwesomeKeyCode;
     */

    [AttributeUsage(AttributeTargets.Field)]
    public class SearchableEnumAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SearchableEnumAttribute))]
    public class SearchableEnumDrawer : PropertyDrawer
    {
        private const string TYPE_ERROR =
            "SearchableEnum can only be used on enum fields.";

        private int idHash;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type != "Enum")
            {
                GUIStyle errorStyle = "CN EntryErrorIconSmall";
                Rect r = new Rect(position);
                r.width = errorStyle.fixedWidth;
                position.xMin = r.xMax;
                GUI.Label(r, "", errorStyle);
                GUI.Label(position, TYPE_ERROR);
                return;
            }

            if (idHash == 0) idHash = "SearchableEnumDrawer".GetHashCode();
            int id = GUIUtility.GetControlID(idHash, FocusType.Keyboard, position);

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, id, label);

            GUIContent buttonText;
            if (property.enumValueIndex < 0 || property.enumValueIndex >= property.enumDisplayNames.Length)
            {
                buttonText = new GUIContent();
            }
            else
            {
                buttonText = new GUIContent(property.enumDisplayNames[property.enumValueIndex]);
            }

            if (DropdownButton(id, position, buttonText))
            {
                Action<int> onSelect = i =>
                {
                    property.enumValueIndex = i;
                    property.serializedObject.ApplyModifiedProperties();
                };

                SearchablePopup.Show(position, property.enumDisplayNames,
                    property.enumValueIndex, onSelect);
            }

            EditorGUI.EndProperty();
        }

        private static bool DropdownButton(int id, Rect position, GUIContent content)
        {
            Event current = Event.current;
            switch (current.type)
            {
                case EventType.MouseDown:
                    if (position.Contains(current.mousePosition) && current.button == 0)
                    {
                        Event.current.Use();
                        return true;
                    }

                    break;

                case EventType.KeyDown:
                    if (GUIUtility.keyboardControl == id && current.character == '\n')
                    {
                        Event.current.Use();
                        return true;
                    }

                    break;

                case EventType.Repaint:
                    EditorStyles.popup.Draw(position, content, id, false);
                    break;
            }

            return false;
        }
    }

    public class SearchablePopup : PopupWindowContent
    {
        #region -- Constants --------------------------------------------------

        private const float ROW_HEIGHT = 16.0f;

        private const float ROW_INDENT = 8.0f;

        private const string SEARCH_CONTROL_NAME = "EnumSearchText";

        #endregion -- Constants --------------------------------------------------

        #region -- Static Functions -------------------------------------------

        public static void Show(Rect activatorRect, string[] options, int current, Action<int> onSelectionMade)
        {
            SearchablePopup win =
                new SearchablePopup(options, current, onSelectionMade);
            PopupWindow.Show(activatorRect, win);
        }

        private static void Repaint()
        {
            EditorWindow.focusedWindow.Repaint();
        }

        private static void DrawBox(Rect rect, Color tint)
        {
            Color c = GUI.color;
            GUI.color = tint;
            GUI.Box(rect, "", Selection);
            GUI.color = c;
        }

        #endregion -- Static Functions -------------------------------------------

        #region -- Helper Classes ---------------------------------------------

        private class FilteredList
        {
            public struct Entry
            {
                public int Index;
                public string Text;
            }

            private readonly string[] allItems;

            public FilteredList(string[] items)
            {
                allItems = items;
                Entries = new List<Entry>();
                UpdateFilter("");
            }

            public string Filter { get; private set; }

            public List<Entry> Entries { get; private set; }

            public int MaxLength
            {
                get { return allItems.Length; }
            }

            public bool UpdateFilter(string filter)
            {
                if (Filter == filter)
                    return false;

                Filter = filter;
                Entries.Clear();

                for (int i = 0; i < allItems.Length; i++)
                {
                    if (string.IsNullOrEmpty(Filter) || allItems[i].ToLower().Contains(Filter.ToLower()))
                    {
                        Entry entry = new Entry
                        {
                            Index = i,
                            Text = allItems[i]
                        };
                        if (string.Equals(allItems[i], Filter, StringComparison.CurrentCultureIgnoreCase))
                            Entries.Insert(0, entry);
                        else
                            Entries.Add(entry);
                    }
                }

                return true;
            }
        }

        #endregion -- Helper Classes ---------------------------------------------

        #region -- Private Variables ------------------------------------------

        private readonly Action<int> onSelectionMade;

        private readonly int currentIndex;

        private readonly FilteredList list;

        private Vector2 scroll;

        private int hoverIndex;

        private int scrollToIndex;

        private float scrollOffset;

        #endregion -- Private Variables ------------------------------------------

        #region -- GUI Styles -------------------------------------------------

        private static GUIStyle SearchBox = "ToolbarSeachTextField";
        private static GUIStyle CancelButton = "ToolbarSeachCancelButton";
        private static GUIStyle DisabledCancelButton = "ToolbarSeachCancelButtonEmpty";
        private static GUIStyle Selection = "SelectionRect";

        #endregion -- GUI Styles -------------------------------------------------

        #region -- Initialization ---------------------------------------------

        private SearchablePopup(string[] names, int currentIndex, Action<int> onSelectionMade)
        {
            list = new FilteredList(names);
            this.currentIndex = currentIndex;
            this.onSelectionMade = onSelectionMade;

            hoverIndex = currentIndex;
            scrollToIndex = currentIndex;
            scrollOffset = GetWindowSize().y - ROW_HEIGHT * 2;
        }

        #endregion -- Initialization ---------------------------------------------

        #region -- PopupWindowContent Overrides -------------------------------

        public override void OnOpen()
        {
            base.OnOpen();
            // Force a repaint every frame to be responsive to mouse hover.
            EditorApplication.update += Repaint;
        }

        public override void OnClose()
        {
            base.OnClose();
            EditorApplication.update -= Repaint;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(base.GetWindowSize().x,
                Mathf.Min(600, list.MaxLength * ROW_HEIGHT +
                               EditorStyles.toolbar.fixedHeight));
        }

        public override void OnGUI(Rect rect)
        {
            Rect searchRect = new Rect(0, 0, rect.width, EditorStyles.toolbar.fixedHeight);
            Rect scrollRect = Rect.MinMaxRect(0, searchRect.yMax, rect.xMax, rect.yMax);

            HandleKeyboard();
            DrawSearch(searchRect);
            DrawSelectionArea(scrollRect);
        }

        #endregion -- PopupWindowContent Overrides -------------------------------

        #region -- GUI --------------------------------------------------------

        private void DrawSearch(Rect rect)
        {
            if (Event.current.type == EventType.Repaint)
                EditorStyles.toolbar.Draw(rect, false, false, false, false);

            Rect searchRect = new Rect(rect);
            searchRect.xMin += 6;
            searchRect.xMax -= 6;
            searchRect.y += 2;
            searchRect.width -= CancelButton.fixedWidth;

            GUI.FocusControl(SEARCH_CONTROL_NAME);
            GUI.SetNextControlName(SEARCH_CONTROL_NAME);
            string newText = GUI.TextField(searchRect, list.Filter, SearchBox);

            if (list.UpdateFilter(newText))
            {
                hoverIndex = 0;
                scroll = Vector2.zero;
            }

            searchRect.x = searchRect.xMax;
            searchRect.width = CancelButton.fixedWidth;

            if (string.IsNullOrEmpty(list.Filter))
                GUI.Box(searchRect, GUIContent.none, DisabledCancelButton);
            else if (GUI.Button(searchRect, "x", CancelButton))
            {
                list.UpdateFilter("");
                scroll = Vector2.zero;
            }
        }

        private void DrawSelectionArea(Rect scrollRect)
        {
            Rect contentRect = new Rect(0, 0,
                scrollRect.width - GUI.skin.verticalScrollbar.fixedWidth,
                list.Entries.Count * ROW_HEIGHT);

            scroll = GUI.BeginScrollView(scrollRect, scroll, contentRect);

            Rect rowRect = new Rect(0, 0, scrollRect.width, ROW_HEIGHT);

            for (int i = 0; i < list.Entries.Count; i++)
            {
                if (scrollToIndex == i &&
                    (Event.current.type == EventType.Repaint
                     || Event.current.type == EventType.Layout))
                {
                    Rect r = new Rect(rowRect);
                    r.y += scrollOffset;
                    GUI.ScrollTo(r);
                    scrollToIndex = -1;
                    scroll.x = 0;
                }

                if (rowRect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseMove ||
                        Event.current.type == EventType.ScrollWheel)
                        hoverIndex = i;
                    if (Event.current.type == EventType.MouseDown)
                    {
                        onSelectionMade(list.Entries[i].Index);
                        EditorWindow.focusedWindow.Close();
                    }
                }

                DrawRow(rowRect, i);

                rowRect.y = rowRect.yMax;
            }

            GUI.EndScrollView();
        }

        private void DrawRow(Rect rowRect, int i)
        {
            if (list.Entries[i].Index == currentIndex)
                DrawBox(rowRect, Color.cyan);
            else if (i == hoverIndex)
                DrawBox(rowRect, Color.white);

            Rect labelRect = new Rect(rowRect);
            labelRect.xMin += ROW_INDENT;

            GUI.Label(labelRect, list.Entries[i].Text);
        }

        private void HandleKeyboard()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.DownArrow)
                {
                    hoverIndex = Mathf.Min(list.Entries.Count - 1, hoverIndex + 1);
                    Event.current.Use();
                    scrollToIndex = hoverIndex;
                    scrollOffset = ROW_HEIGHT;
                }

                if (Event.current.keyCode == KeyCode.UpArrow)
                {
                    hoverIndex = Mathf.Max(0, hoverIndex - 1);
                    Event.current.Use();
                    scrollToIndex = hoverIndex;
                    scrollOffset = -ROW_HEIGHT;
                }

                if (Event.current.keyCode == KeyCode.Return)
                {
                    if (hoverIndex >= 0 && hoverIndex < list.Entries.Count)
                    {
                        onSelectionMade(list.Entries[hoverIndex].Index);
                        EditorWindow.focusedWindow.Close();
                    }
                }

                if (Event.current.keyCode == KeyCode.Escape)
                {
                    EditorWindow.focusedWindow.Close();
                }
            }
        }

        #endregion -- GUI --------------------------------------------------------
    }

#endif

    #endregion Search Enum

    #region Help

    //EX:detail attributes
    /*
        [Help("This is bug !!!")]
        public int someVar;
     */

    public class HelpAttribute : PropertyAttribute
    {
        public string helpText;

        public HelpAttribute(string helpText)
        {
            this.helpText = helpText;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HelpAttribute))]
    public class HelpDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HelpAttribute helpAttribute = (HelpAttribute)attribute;

            // Resize the property field
            Rect propertyRect = new Rect(position.x, position.y, position.width - 30, position.height);
            EditorGUI.PropertyField(propertyRect, property, label);

            // Draw the help button
            Rect buttonRect = new Rect(position.x + position.width - 20, position.y, 20, 20);
            if (GUI.Button(buttonRect, "?"))
            {
                EditorUtility.DisplayDialog("Help", helpAttribute.helpText, "OK");
            }
        }
    }

#endif

    #endregion Help

    #region Read Only

    //EX:blur attribute
    /*
        [ReadOnly]
        public float CurrentHealth;
     */

    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

#endif

    #endregion Read Only

    #region Scene Game

    //EX:get list scene in build settings
    /*
    [Scene] public string sceneName;
     */

    public class SceneAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var sceneNames = new List<string>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            }

            int selectedIndex = Mathf.Max(sceneNames.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames.ToArray());
            property.stringValue = sceneNames[selectedIndex];
        }
    }

#endif

    #endregion Scene Game

    #endregion Attributes
}
