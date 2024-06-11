using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pingPongBall;
    public Vector3 velocity;
    public float lineDistance;
    public int linePoints;
    public Vector3 pos;
    public Vector3 rot;
    public float maximumOffset;
    public float maximumRotation;
    public float maximumSpeed;
    public float[] settingSpeeds;
    public float increaseVelocity;
    public bool rightMovement;
    public Vector3[] savedVectors;
    public enum stage{Position,Rotation,Speed };

    public stage curStage = stage.Position;
    void Start()
    {
        savedVectors = new Vector3[3];
        savedVectors[0] = pos = this.transform.position;
        savedVectors[1] = this.transform.rotation.eulerAngles;
        rot = new Vector3(0,this.transform.rotation.eulerAngles.y,0);
        savedVectors[2] = velocity;
    }

    public void throwingLoop()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (curStage != stage.Speed)
            {
                curStage++;
            }
            else
            {
                GameObject ball = Instantiate(pingPongBall, this.transform.position, Quaternion.Euler(rot));
                ball.GetComponent<Rigidbody>().AddForce(velocity * increaseVelocity, ForceMode.Impulse);
                curStage = 0;
                resetParams();
            }
            rightMovement = true;
        }
        if(curStage==stage.Position)
        {
            setPosition();
        }
        else if(curStage==stage.Rotation)
        {
            setRotation();
        }
        else
        {
            setSpeed();
        }
        getLine(GetComponent<LineRenderer>(), this.transform.position + new Vector3(0, -3, 0), new Vector3(this.transform.position.x, 0, this.transform.position.z) + (velocity * lineDistance),linePoints);

    }

    void getLine(LineRenderer line,Vector3 startPos,Vector3 endPos,int numOfPoints)
    {
        float totalDistance = 3*Mathf.PI/4;
        line.positionCount = numOfPoints;
        for(int i =0;i<numOfPoints;i++)
        {
            float curDistance = totalDistance * ((float)(i) / (float)numOfPoints);
            line.SetPosition(i, new Vector3(Mathf.Lerp(startPos.x, endPos.x, curDistance * lineDistance), startPos.y, Mathf.Lerp(startPos.z,endPos.z,curDistance * lineDistance)));
        }
    }

    void setPosition()
    {
        if (pos.x - this.transform.position.x > maximumOffset)
        {
            rightMovement = true;
        }
        else if (pos.x - this.transform.position.x < -maximumOffset)
        {
            rightMovement = false;
        }

        if (rightMovement)
        {
            this.transform.position += new Vector3(settingSpeeds[0] * Time.deltaTime,0,0);
        }
        else
        {
            this.transform.position -= new Vector3(settingSpeeds[0] * Time.deltaTime,0,0);
        }
    }

    void setRotation()
    {
        if (Mathf.Abs(rot.y - this.transform.rotation.eulerAngles.y) > maximumRotation && Mathf.Abs(rot.y - this.transform.rotation.eulerAngles.y) < maximumRotation+3)
        {
            rightMovement = false;
        }
        else if (Mathf.Abs(rot.y - this.transform.rotation.eulerAngles.y) < 360-maximumRotation && Mathf.Abs(rot.y - this.transform.rotation.eulerAngles.y) > 360-maximumRotation -3)
        {
            rightMovement = true;
        }
        if (rightMovement)
        {
            this.transform.Rotate(new Vector3( 0, 1, 0), settingSpeeds[1] * Time.deltaTime,Space.World);
        }
        else
        {
            this.transform.Rotate(new Vector3(0, 1, 0), -settingSpeeds[1] * Time.deltaTime, Space.World);
        }
    }

    void setSpeed()
    {
        if (velocity.z > maximumSpeed)
        {
            rightMovement = false;
        }
        else if (velocity.z < 0)
        {
            rightMovement = true;
        }

        if (rightMovement)
        {
            velocity.z += settingSpeeds[2] * Time.deltaTime;
        }
        else
        {
            velocity.z -= settingSpeeds[2] * Time.deltaTime;
        }
    }

    void resetParams()
    {

        this.transform.position = savedVectors[0];
        this.transform.rotation = Quaternion.Euler(savedVectors[1]);
         velocity = savedVectors[2];
    }
}
