using System;
using System.Linq;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId unique = (UniqueId) target;

            if (string.IsNullOrEmpty(unique.Id))
            {
                Generate(unique);
            }
            else
            {
                UniqueId[] unigueIds = FindObjectsOfType<UniqueId>();
                if (unigueIds.Any(other => other != unique && other.Id == unique.Id))
                    Generate(unique);
            }

        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = uniqueId.gameObject.scene.name + "_" + Guid.NewGuid();

            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}