using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(IOCcam))]
public class IocEditor : Editor {
	
	SerializedProperty LayerMsk;
	SerializedProperty TagMsk;
	SerializedProperty OccludeeLayer;
	SerializedProperty Samples;
	SerializedProperty RaysFov;
	SerializedProperty PreCullCheck;
	SerializedProperty ViewDistance;
	SerializedProperty HideDelay;
	SerializedProperty RealtimeShadows;
	SerializedProperty Lod1Distance;
	SerializedProperty Lod2Distance;
	SerializedProperty LodMargin;
	SerializedProperty LightProbes;
	SerializedProperty ProbeRadius;
	private Texture2D logo;
	
	void OnEnable() {
		LayerMsk = serializedObject.FindProperty("layerMsk");
		TagMsk = serializedObject.FindProperty("tagMsk");
		OccludeeLayer = serializedObject.FindProperty("occludeeLayer");
		Samples = serializedObject.FindProperty("samples");
		RaysFov = serializedObject.FindProperty("raysFov");
		PreCullCheck = serializedObject.FindProperty("preCullCheck");
		ViewDistance = serializedObject.FindProperty("viewDistance");
		HideDelay = serializedObject.FindProperty("hideDelay");
		RealtimeShadows = serializedObject.FindProperty("realtimeShadows");
		Lod1Distance = serializedObject.FindProperty("lod1Distance");
		Lod2Distance = serializedObject.FindProperty("lod2Distance");
		LodMargin = serializedObject.FindProperty("lodMargin");
		LightProbes = serializedObject.FindProperty("lightProbes");
		ProbeRadius = serializedObject.FindProperty("probeRadius");
		logo = (Texture2D) Resources.LoadAssetAtPath("Assets/InstantOC/Editor/Images/Logo.jpg", typeof(Texture2D));
	}
	
	override public void OnInspectorGUI () {
		serializedObject.Update();
		
		GUILayout.Label(logo);
		EditorGUILayout.LabelField("InstantOC parameters", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(LayerMsk, new GUIContent("Layer mask"));
		TagMsk.intValue = EditorGUILayout.MaskField("Tag mask", TagMsk.intValue, GetTags());
		OccludeeLayer.intValue = EditorGUILayout.LayerField("Occludee Layer", OccludeeLayer.intValue);
		EditorGUILayout.IntSlider(Samples, 10, 3000);
		EditorGUILayout.Slider(RaysFov, 1, 179, new GUIContent("Rays FOV"));
		EditorGUILayout.Slider(ViewDistance, 100, 5000);
		EditorGUILayout.IntSlider(HideDelay, 10, 500);
		EditorGUILayout.PropertyField(PreCullCheck, new GUIContent("PreCull Check"));
		EditorGUILayout.PropertyField(RealtimeShadows, new GUIContent("Realtime Shadows"));
		EditorGUILayout.Space();
		EditorGUILayout.Slider(Lod1Distance, 0, 1000, new GUIContent("Lod 1 distance"));
		EditorGUILayout.Slider(Lod2Distance, 0, 2000, new GUIContent("Lod 2 distance"));
		EditorGUILayout.PropertyField(LodMargin);
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(LightProbes, new GUIContent("Light Probes"));
		EditorGUILayout.PropertyField(ProbeRadius, new GUIContent("Probe Radius"));
		
		serializedObject.ApplyModifiedProperties();
	}

	private string[] GetTags() {
		return UnityEditorInternal.InternalEditorUtility.tags;
	}
}
