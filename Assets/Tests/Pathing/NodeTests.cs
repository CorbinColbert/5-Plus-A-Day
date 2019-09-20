using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NodeTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CompareToGreaterThanTest()
        {
            int expectedValue = 1;
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            nodeA.gCost = 10;
            nodeA.hCost = 10;
            Node nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 0);
            nodeB.gCost = 20;
            nodeB.hCost = 20;

            int actualValue = nodeA.CompareTo(nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void CompareToLessThanTest()
        {
            int expectedValue = -1;
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            nodeA.gCost = 20;
            nodeA.hCost = 20;
            Node nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 0);
            nodeB.gCost = 10;
            nodeB.hCost = 10;

            int actualValue = nodeA.CompareTo(nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }


        [Test]
        public void CompareToEqualTest()
        {
            int expectedValue = 0;
            Node nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            nodeA.gCost = 10;
            nodeA.hCost = 10;
            Node nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 0);
            nodeB.gCost = 10;
            nodeB.hCost = 10;

            int actualValue = nodeA.CompareTo(nodeB);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
