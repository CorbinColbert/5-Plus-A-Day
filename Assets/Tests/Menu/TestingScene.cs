using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestingScene
    {
        [Test]
        public void TestingSceneSimplePasses()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().buildIndex + 1);
            _ = EditorSceneManager.GetActiveScene().buildIndex + 1;
        }

        [UnityTest]
        public IEnumerator TestingSceneWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
