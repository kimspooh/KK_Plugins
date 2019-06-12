﻿using BepInEx;
using BepInEx.Bootstrap;
using KKAPI.Maker;
using KKAPI.Studio.SaveLoad;
using Studio;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KK_MaterialEditor
{
    [BepInPlugin(GUID, PluginName, Version)]
    public partial class KK_MaterialEditor : BaseUnityPlugin
    {
        public const string GUID = "com.deathweasel.bepinex.materialeditor";
        public const string PluginName = "Material Editor";
        public const string Version = "0.1";

        private void Main()
        {
            SceneManager.sceneLoaded += (s, lsm) => InitStudioUI(s.name);
            MakerAPI.MakerBaseLoaded += MakerAPI_MakerBaseLoaded;
            StudioSaveLoadApi.RegisterExtraBehaviour<MaterialEditorSceneController>(GUID);
        }

        private static void SetFloatProperty(GameObject go, Material mat, string property, string value)
        {
            float floatValue = float.Parse(value);

            foreach (var obj in go.GetComponentsInChildren<Renderer>())
                foreach (var objMat in obj.materials)
                    if (objMat.name == mat.name)
                        objMat.SetFloat($"_{property}", floatValue);
        }

        private static void SetColorRProperty(GameObject go, Material mat, string property, string value)
        {
            float floatValue = float.Parse(value);
            Color colorOrig = mat.GetColor($"_{property}");

            foreach (var obj in go.GetComponentsInChildren<Renderer>())
                foreach (var objMat in obj.materials)
                    if (objMat.name == mat.name)
                        objMat.SetColor($"_{property}", new Color(floatValue, colorOrig.g, colorOrig.b, colorOrig.a));
        }

        private static void SetColorGProperty(GameObject go, Material mat, string property, string value)
        {
            float floatValue = float.Parse(value);
            Color colorOrig = mat.GetColor($"_{property}");

            foreach (var obj in go.GetComponentsInChildren<Renderer>())
                foreach (var objMat in obj.materials)
                    if (objMat.name == mat.name)
                        objMat.SetColor($"_{property}", new Color(colorOrig.r, floatValue, colorOrig.b, colorOrig.a));
        }

        private static void SetColorBProperty(GameObject go, Material mat, string property, string value)
        {
            float floatValue = float.Parse(value);
            Color colorOrig = mat.GetColor($"_{property}");

            foreach (var obj in go.GetComponentsInChildren<Renderer>())
                foreach (var objMat in obj.materials)
                    if (objMat.name == mat.name)
                        objMat.SetColor($"_{property}", new Color(colorOrig.r, colorOrig.g, floatValue, colorOrig.a));
        }

        private static void SetColorAProperty(GameObject go, Material mat, string property, string value)
        {
            float floatValue = float.Parse(value);
            Color colorOrig = mat.GetColor($"_{property}");

            foreach (var obj in go.GetComponentsInChildren<Renderer>())
                foreach (var objMat in obj.materials)
                    if (objMat.name == mat.name)
                        objMat.SetColor($"_{property}", new Color(colorOrig.r, colorOrig.g, colorOrig.b, floatValue));
        }

        private static void SetMeshProperty(Renderer rend, RendererProperties property, int value)
        {
            if (property == RendererProperties.ShadowCastingMode)
                rend.shadowCastingMode = (UnityEngine.Rendering.ShadowCastingMode)value;
            else if (property == RendererProperties.ReceiveShadows)
                rend.receiveShadows = value == 1 ? true : false;
        }

        private static int GetObjectID(ObjectCtrlInfo oci) => Studio.Studio.Instance.dicObjectCtrl.First(x => x.Value == oci).Key;
        internal static string FormatObjectName(Object go) => go.name.Replace("(Instance)", "").Trim();
        public MaterialEditorSceneController GetSceneController() => Chainloader.ManagerObject.transform.GetComponentInChildren<MaterialEditorSceneController>();
        internal static void Log(string text) => BepInEx.Logger.Log(BepInEx.Logging.LogLevel.Info, text);
        internal static void Log(object text) => BepInEx.Logger.Log(BepInEx.Logging.LogLevel.Info, text?.ToString());
    }
}