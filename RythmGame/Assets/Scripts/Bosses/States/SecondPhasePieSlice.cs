using System.Collections;
using System.Collections.Generic;
using Bosses;
using Bosses.States;
using FMODUnity;
using UnityEngine;

public class SecondPhasePieSlice : SecondPhaseState
{
    private int numberOfBeatsWaited;
    private bool attackTelegraphStarted;
    private bool startedAttacking;
    private bool hasDamagedPlayer;
    private Mesh attackMesh;
    private GameObject[] telegraphs;
    private GameObject telegraphHolder;

    public override void Entry(BossBehaviour bossBehaviour, FirstPhaseStats firstPhase, SecondPhaseStats secondPhase, Health bossHealth, MusicEventPort beatPort)
    {
        base.Entry(bossBehaviour, firstPhase, secondPhase, bossHealth, beatPort);
        behaviour.ResetTelegraphPositions();
        attackMesh = new Mesh();
        telegraphs = new GameObject[secondPhaseStats.PieSliceAmountOfSlices];
        telegraphHolder = new GameObject("TelegraphHolder");
        telegraphHolder.transform.parent = behaviour.transform;
        telegraphHolder.transform.localPosition = Vector3.zero;
        Debug.Log("Boss pie slice");
    }

    public override void OnBeat()
    {
        if(!attackTelegraphStarted)
        {
            StartTelegraphAttack();
            return;
        }

        if(startedAttacking)
            return;
    
        numberOfBeatsWaited++;
        
        if(numberOfBeatsWaited == secondPhaseStats.PieSliceAmountOfBeatsWarning - secondPhaseStats.PieSliceAnimDurationBeforeImpact)
            behaviour.bossAnim.SetTrigger("GroundToss");
    
        if(numberOfBeatsWaited >= secondPhaseStats.PieSliceAmountOfBeatsWarning)
            StartAttack();
    }

    private void StartTelegraphAttack()
    {
        attackTelegraphStarted = true;
        RuntimeManager.PlayOneShot(secondPhaseStats.PieSliceTelegraphSFX);
        
        for(int i = 0; i < secondPhaseStats.PieSliceAmountOfSlices; i++)
        {
            telegraphs[i] = Object.Instantiate(behaviour.GenerateCircles[2].gameObject, telegraphHolder.transform);
            telegraphs[i].name = "Pie Slice " + i;
            attackMesh = behaviour.GenerateCircles[2].CreateCircleMesh(100,
                secondPhaseStats.PieSliceRange, 
                secondPhaseStats.PieSliceSectorAngle,
                secondPhaseStats.PieSliceSectorAngle * i + secondPhaseStats.PieSliceAngleBetweenSlices * i);
            
            telegraphs[i].GetComponent<GenerateCircle>().SetMesh(attackMesh);
            telegraphs[i].GetComponent<MeshCollider>().sharedMesh = attackMesh;
            telegraphs[i].GetComponent<MeshCollider>().enabled = false;
            telegraphs[i].transform.localPosition = new Vector3(secondPhaseStats.PieSliceOriginOffset.x, 0.05f, secondPhaseStats.PieSliceOriginOffset.y);
        }
        
        float angleTowardsPlayerOffset = Random.Range(0, secondPhaseStats.PieSliceMaxAngleDeviation * 2);
        float angleTowardsPlayer = GetAngleTowardsPlayerFromObject(telegraphHolder.transform);
        //We use -180 to turn everything back, because what i expected to be forward was in reality the opposite..
        angleTowardsPlayer += -secondPhaseStats.PieSliceMaxAngleDeviation + angleTowardsPlayerOffset;
        //telegraphHolder.transform.Rotate(Vector3.up, angleTowardsPlayer);
    }

    private float GetAngleTowardsPlayerFromObject(Transform obj)
    {
        Vector3 vectorTowardsPlayer = behaviour.GetPlayerPos() - behaviour.transform.position;
        return Vector3.SignedAngle(behaviour.transform.forward, vectorTowardsPlayer, Vector3.up);
    }

    private void StartAttack()
    {
        startedAttacking = true;
        timer.StartTimer(2);
        RuntimeManager.PlayOneShot(secondPhaseStats.PieSliceSFX);

        Vector3 directionTowardsPlayer = behaviour.GetPlayerPos() - behaviour.transform.position;
        directionTowardsPlayer.Normalize();
        
        for(int i = 0; i < secondPhaseStats.PieSliceAmountOfSlices; i++)
        {
            //telegraphs[i].GetComponent<MeshCollider>().enabled = true;
            //Måste egentligen beräkna exakta vinklar för att se om spelaren borde ta damage eller inte. Använd inte colliders för det
            //if(Vector3.Dot(directionTowardsPlayer, behaviour.transform.forward) )
        }
    }

    protected override void TimerDone()
    {
        behaviour.Transition(new IdleSecondPhase());
    }

    public override void OnCollisionStay(Collision other, string name)
    {
        if(hasDamagedPlayer || !startedAttacking)
            return;
        
        if(other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(secondPhaseStats.PieSliceCircleDamage);
            Debug.Log("Phase 2 " + name);
            hasDamagedPlayer = true;
            
            for(int i = 0; i < secondPhaseStats.PieSliceAmountOfSlices; i++)
            {
                telegraphs[i].GetComponent<MeshCollider>().enabled = false;
            }
        }
    }

    private void DestroyTelegraphs()
    {
        for(int i = 0; i < secondPhaseStats.PieSliceAmountOfSlices; i++)
        {
            GameObject.Destroy(telegraphs[i]);
        }
        GameObject.Destroy(telegraphHolder);
    }

    public override void Exit()
    {
        base.Exit();
        DestroyTelegraphs();
    }
}
