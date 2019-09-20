using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PathingfindingTests
    {  
        [Test]
        public void GetDistanceTest()
        {
            int expectedValue = 10;
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            Node nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 0);

            int actualValue = Pathfinding.getDistance(nodeA, nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetDistanceDiagonalTest()
        {
            int expectedValue = 14;
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            Node nodeB = new Node(true, true, new Vector3(1, 0, 1), 1, 1);

            int actualValue = Pathfinding.getDistance(nodeA, nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void PathToVectorsTest()
        {
            bool areEqual = true;

            List<Node> testPath = new List<Node>();
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            testPath.Add(nodeA);
            Node nodeB = new Node(true, true, new Vector3(1, 0, 1), 1, 1);
            testPath.Add(nodeB);

            Vector3[] testVectorPath = Pathfinding.PathToVectors(testPath);

            for (int i = 0; i < testVectorPath.Length; i++)
            {
                if (testVectorPath[i] != testPath[i].worldPosition)
                {
                    areEqual = false;
                    break;
                }
            }

            Assert.IsTrue(areEqual);
        }
    }
}
