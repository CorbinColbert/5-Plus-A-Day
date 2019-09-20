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
            Node nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 1);

            int actualValue = Pathfinding.getDistance(nodeA, nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
