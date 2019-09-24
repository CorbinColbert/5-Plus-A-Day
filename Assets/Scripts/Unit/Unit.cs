using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameObject grid;
    public event Action onDeathEvent;
    public GameObject target;

    public float healthMax;
    public float healthCurrent;
    public float healthRegen;
    public float healthRegenRate = 1.0f;

    public float damageMin;
    public float damageMax;
    public float attackSpeed = 1.0f;
    public int attackRange = 14;
    private bool inRange;
    public float critChance = 0.0f;
    public float critDamageModifier = 2.0f;
    public Item item;
    public Node nodeUnitOnTopOf;

    void Start() {
        healthCurrent = healthMax;
        
        grid = FindObjectOfType<NodeGrid>().gameObject;

        ConfigureRigidBody();

        Regen();

        TryAttack();
    }

    void ConfigureRigidBody() {
        Rigidbody body;
        if (gameObject.TryGetComponent<Rigidbody>(out body)) {
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            body.freezeRotation = true;
        } else { 
            body = gameObject.AddComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            body.freezeRotation = true;
        }
    }

    void FixedUpdate() {
        if (healthCurrent <= 0) {
            if (onDeathEvent != null) {
                onDeathEvent();
            }    
            OnDeath();
            return;
        }
        UpdateNodePosition();

        if (gameObject.TryGetComponent<UnitPathing>(out UnitPathing pathing)) {
            try {
                if (target != null && !inRange) {
                    pathing.GetPathing(target);
                }
            } catch (InvalidOperationException e) {
                print("Path already present: "+e);
            }
        }
    }

    void UpdateNodePosition() {
        print("Node position being updated");
        Node oldNode;
        if (grid.TryGetComponent<NodeGrid>(out NodeGrid nodeGrid)) {
            if (nodeUnitOnTopOf == null) {
                nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
                oldNode = nodeUnitOnTopOf;
            } else {
                oldNode = nodeUnitOnTopOf;
                nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
            }

            print("I'm at x: "+nodeUnitOnTopOf.gridX+" y: "+nodeUnitOnTopOf.gridY);

            if (nodeUnitOnTopOf == oldNode) {
                oldNode.unitOnTop = false;
            } else {
                oldNode.unitOnTop = true;
                oldNode = nodeUnitOnTopOf;
                oldNode.unitOnTop = false;
            }
        }
    }

    void Update() {
        print("Update");
    }

    void FindTarget() {
        GameObject[] objects = null;
        if (CompareTag("EnemyTroop")) {
            objects = GameObject.FindGameObjectsWithTag("PlayerTroop");
        }
        if (CompareTag("PlayerTroop")) {
            objects = GameObject.FindGameObjectsWithTag("EnemyTroop");
        }

        if (objects != null) {
            int size = objects.Length;
            if (size == 0) {
                print("Victory! for "+gameObject.tag+" team");
            } else {
                SetTarget(objects[UnityEngine.Random.Range(0, size)]);
            }
        }
    }

    void Regen() {
        print("Regen called");
        if (healthCurrent + healthRegen > healthMax) {
            healthCurrent = healthMax;
        } else {
            healthCurrent += healthRegen;
        }
        Invoke("Regen", 1.0f/healthRegenRate);
    }

    void TryAttack() {
        Invoke("TryAttack", 1.0f/attackSpeed);

        if (target == null) {
            FindTarget();
            return;
        }

        Unit targetUnit = target.GetComponent<Unit>();

        if (nodeUnitOnTopOf != null && targetUnit.nodeUnitOnTopOf != null) {
            int distanceX = Mathf.Abs(nodeUnitOnTopOf.gridX - targetUnit.nodeUnitOnTopOf.gridX);
            int distanceY = Mathf.Abs(nodeUnitOnTopOf.gridY - targetUnit.nodeUnitOnTopOf.gridY);

            int distance = (distanceX > distanceY) ?
                14 * distanceY + 10 * (distanceX - distanceY) :
                14 * distanceX + 10 * (distanceY - distanceX);

            inRange = distance <= attackRange;
        }

        if (inRange) {
            //gameObject.GetComponent<UnitPathing>().Reset();
            Attack();
        }

        
    }

    void Attack()
    {
        Attack attack = new Attack(this);
        Unit other;
        if (target.TryGetComponent<Unit>(out other))
        {
            other.RecieveAttack(attack);
            Rigidbody body = gameObject.GetComponent<Rigidbody>();
            if (body != null) {
                body.AddForce(new Vector3(0, 100, 0));
            }
        }
    }

    public void RecieveAttack(Attack attack) {
        healthCurrent -= attack.damage;
    }

    void FlingUnit() {
        Rigidbody body;
        if (gameObject.TryGetComponent<Rigidbody>(out body)) {
            float xForce = UnityEngine.Random.Range(-200,200);
            float yForce = UnityEngine.Random.Range(-100,200);
            float zForce = UnityEngine.Random.Range(-200,200);

            Vector3 randomForce = new Vector3(xForce, yForce, zForce);

            float xRot = UnityEngine.Random.Range(-50, 50);
            float yRot = UnityEngine.Random.Range(-50, 50);
            float zRot = UnityEngine.Random.Range(-50, 50);

            Vector3 randomRotation = new Vector3(xRot, yRot, zRot);

            body.constraints = RigidbodyConstraints.None;
            body.freezeRotation = false;

            body.AddRelativeTorque(randomRotation);
            body.AddForce(randomForce);
        }
    }

    void OnDeath() {
        if (nodeUnitOnTopOf != null) {
            nodeUnitOnTopOf.unitOnTop = false;
        }

        FlingUnit();

        if (gameObject.TryGetComponent<UnitPathing>(out var pathing)) {
            Destroy(pathing);
        }
        if (gameObject.TryGetComponent<Unit>(out var unit)) {
            Destroy(unit);
        }
        if (gameObject.TryGetComponent<Collider>(out var collider)) {
            Destroy(collider, 2.0f);
        }
        if (CompareTag("EnemyTroop")) {
            GameManager.currency += 10;
        }
    }

    void OnTargetDeath() {
        target = null;
    }

    void SetTarget(GameObject target) {
        this.target = target;
        if (target.TryGetComponent<Unit>(out Unit unit)) {
            unit.onDeathEvent += OnTargetDeath;
        }
    }

    public Item Equip(Item item) {
        Item itemHolder = item;
        this.item = item;
        
        return itemHolder;
    }
}
