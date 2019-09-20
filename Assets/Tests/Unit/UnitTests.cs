using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitTests
    {
<<<<<<< HEAD
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
=======
        
>>>>>>> 8dd9f808c7aae9e79d4b22cf8c5255f3458d6e3e
    }
}
