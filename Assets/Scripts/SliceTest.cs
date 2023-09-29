using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice; // Ezy-Slice フレームワークを利用するために必要

public class SliceTest : MonoBehaviour
{
    // 切断面の色
    public Material MaterialAfterSlice;
    // 切断するレイヤー
    public LayerMask sliceMask;
    public Vector3 startPoint;
    public float radious;
    public float force;
    int i;

    private void Start()
    {
        i = 0;
    }

    void Update()
    {
        // spaceボタン押下時
        if (Input.GetKeyDown("space"))
        {
            // Physics.OverlapBoxを用いて切断用の面と重なっている全コライダーを検出
            Collider[] objectsToSlice = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            // 全コライダーごとに切断する
            foreach (Collider objectToSlice in objectsToSlice)
            {
                /*
                // 元オブジェクトの切断
                SlicedHull slicedObject = SliceObject(objectToSlice.gameObject, MaterialAfterSlice);

                // 上面側のオブジェクトの生成
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
                MakeItPhysical(upperHullGameobject);

                // 下面側のオブジェクトの生成
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
                MakeItPhysical(lowerHullGameobject);

                // 元オブジェクトの削除
                Destroy(objectToSlice.gameObject);
                */
            }
        }
    }

    // 切断時に生成するオブジェクトを返す
    private SlicedHull SliceObject(GameObject obj, Vector3 position, Vector3 right, Material crossSectionMaterial = null)
    {
        // Ezy-Slice フレームワーク を利用してスライスしている
        return obj.Slice(position, right, crossSectionMaterial);
    }

    // オブジェクト生成時にMeshColliderとRigidbodyをアタッチする
    private void MakeItPhysical(GameObject obj, Material mat = null)
    {
        // MeshCollider の Convex を true にしないと，すり抜けてしまうので注意
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
    }

    public void Cut(GameObject objectToSlice, Vector3 position, Vector3 right)
    {
        SlicedHull slicedObject = SliceObject(objectToSlice.gameObject, position, right, MaterialAfterSlice);

        GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
        MakeItPhysical(upperHullGameobject);
        Explosion(upperHullGameobject.GetComponent<Rigidbody>(), position);
        upperHullGameobject.layer = 8;
        //upperHullGameobject.GetComponent<MeshCollider>().isTrigger = true;

        // 下面側のオブジェクトの生成
        GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
        MakeItPhysical(lowerHullGameobject);
        Explosion(lowerHullGameobject.GetComponent<Rigidbody>(), position);
        lowerHullGameobject.layer = 8;
        //lowerHullGameobject.GetComponent<MeshCollider>().isTrigger = true;

        Vector3 b = new Vector3(startPoint.x + Random.Range(-0.5f, 0.5f), startPoint.y, startPoint.z);
        Instantiate(this.gameObject,
            b, Quaternion.identity);
        Result.hits++;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OnDestroy")
        {
            if (i == 0)
            {
                Vector3 b = new Vector3(startPoint.x + Random.Range(-0.5f, 0.5f), startPoint.y, startPoint.z);
                Instantiate(this.gameObject,
                    b, Quaternion.identity);
                Destroy(this.gameObject);
                i++;
                Result.miss++;
            }
        }
        //entry = other.ClosestPointOnBounds(this.transform.position);
    }

    public void Explosion(Rigidbody a, Vector3 position)
    {
        //m_particle.Play();
        //m_position = m_particle.transform.position;

        // 範囲内のRigidbodyにAddExplosionForce
        //Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radious);
        a.AddExplosionForce(force, this.transform.position, radious, 0.0f, ForceMode.Impulse);
    }
}