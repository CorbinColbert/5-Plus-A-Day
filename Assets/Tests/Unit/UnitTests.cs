using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitTests
    {
        [UnityTest]
        public IEnumerator HealthStartsAtMax()
        {
            var unit = new GameObject().AddComponent<Unit>();
            yield return null;
            Assert.IsTrue(unit.health == unit.healthMax);          
        }

        [UnityTest]
        public IEnumerator UnitDestroyedOnDeath() {
            var unit = new GameObject().AddComponent<Unit>();

            unit.OnDeath();

            yield return new WaitForSecondsRealtime(2);

            Assert.True(unit == null);
        }
    }
}
