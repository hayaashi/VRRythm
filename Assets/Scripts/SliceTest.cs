using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice; // Ezy-Slice �t���[�����[�N�𗘗p���邽�߂ɕK�v

public class SliceTest : MonoBehaviour
{
    // �ؒf�ʂ̐F
    public Material MaterialAfterSlice;
    // �ؒf���郌�C���[
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
        // space�{�^��������
        if (Input.GetKeyDown("space"))
        {
            // Physics.OverlapBox��p���Đؒf�p�̖ʂƏd�Ȃ��Ă���S�R���C�_�[�����o
            Collider[] objectsToSlice = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            // �S�R���C�_�[���Ƃɐؒf����
            foreach (Collider objectToSlice in objectsToSlice)
            {
                /*
                // ���I�u�W�F�N�g�̐ؒf
                SlicedHull slicedObject = SliceObject(objectToSlice.gameObject, MaterialAfterSlice);

                // ��ʑ��̃I�u�W�F�N�g�̐���
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
                MakeItPhysical(upperHullGameobject);

                // ���ʑ��̃I�u�W�F�N�g�̐���
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToSlice.GetComponent<Collider>().gameObject, MaterialAfterSlice);
                MakeItPhysical(lowerHullGameobject);

                // ���I�u�W�F�N�g�̍폜
                Destroy(objectToSlice.gameObject);
                */
            }
        }
    }

    // �ؒf���ɐ�������I�u�W�F�N�g��Ԃ�
    private SlicedHull SliceObject(GameObject obj, Vector3 position, Vector3 right, Material crossSectionMaterial = null)
    {
        // Ezy-Slice �t���[�����[�N �𗘗p���ăX���C�X���Ă���
        return obj.Slice(position, right, crossSectionMaterial);
    }

    // �I�u�W�F�N�g��������MeshCollider��Rigidbody���A�^�b�`����
    private void MakeItPhysical(GameObject obj, Material mat = null)
    {
        // MeshCollider �� Convex �� true �ɂ��Ȃ��ƁC���蔲���Ă��܂��̂Œ���
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

        // ���ʑ��̃I�u�W�F�N�g�̐���
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

        // �͈͓���Rigidbody��AddExplosionForce
        //Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radious);
        a.AddExplosionForce(force, this.transform.position, radious, 0.0f, ForceMode.Impulse);
    }
}