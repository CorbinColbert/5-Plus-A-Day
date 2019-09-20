using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HeapTests
    {

        Heap<Node> testHeap;
        Node nodeA;
        Node nodeB;

        [SetUp]
        public void SetUp()
        {
            testHeap = new Heap<Node>(64);

            nodeA = new Node(true, true, new Vector3(0, 0, 0), 0, 0);
            nodeA.gCost = 10;
            nodeA.hCost = 10;
            testHeap.Add(nodeA);
            nodeB = new Node(true, true, new Vector3(1, 0, 0), 1, 0);
            nodeB.gCost = 20;
            nodeB.hCost = 20;
            testHeap.Add(nodeB);
        }

        [Test]
        public void SwapTest()
        {
            int expectedValue = 0;

            testHeap.swap(nodeA, nodeB);

            int actualValue = nodeB.HeapIndex;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void UpdateItemTest()
        {
            int expectedValue = 0;

            nodeB.gCost = 5;
            nodeB.hCost = 5;

            testHeap.UpdateItem(nodeB);

            int actualValue = nodeB.HeapIndex;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void RemoveFirstTest()
        {
            Node expectedNode = nodeA;
            Node actualNode;

            actualNode = testHeap.RemoveFirst();

            Assert.AreSame(expectedNode, actualNode);
        }
    }
}
