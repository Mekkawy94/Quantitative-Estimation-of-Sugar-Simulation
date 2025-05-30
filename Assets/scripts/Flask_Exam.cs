using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Flask_Exam : MonoBehaviour
{
    private Color FlaskLiquidColor;

    public int maxElementsInFlask = 4;
    private List<string> elementsInFlask = new List<string>();
    private GameObject flask;

    public float ExamTime = 1f;
    float totalSeconds;


    public Transform RobotPnlHolder;
    TextMeshProUGUI RobotText;


    public Transform NotePnlHolder;
    TextMeshProUGUI NoteText;

    public Transform BuretteNoteHolder;
    TextMeshProUGUI BuretteNoteText;

    public Transform FinalResultHolder;
    TextMeshProUGUI FinalResultText;

    public Transform ExamTimeHolder;
    TextMeshProUGUI ExamTimeCounterText;

    ParticleSystem BuretteParticle;


    GameObject BuretteFlow;
    Transform BoxDoor;
    GameObject enable_BoxCount;
    GameObject enable_BuretteNoteText;
    GameObject ResultCanvas;
    GameObject H2so4_flask;
    //public Transform BuretteCountHolder;
    TextMeshProUGUI BoxCount;
    public float CountVal = 10;
    public float BuretteTime = 5;
    public double BuretteQuantity = 0;

    public int NumOfStarchDrops;


    private float BoxDoorPosition_X;
    private float BoxDoorPosition_Y;
    private float BoxDoorPosition_z;
    private float FlaskPosition_X;
    private float FlaskPosition_Y;
    private float FlaskPosition_z;



    public bool s1 = true;
    public bool s2 = true;
    public bool s3 = true;
    public bool s4 = true;
    public bool s5 = true;
    public bool BlueLighted = true;
    public bool s6 = true;

    
    private bool boxStepRunning = true;
    private bool buretteStepRunning = true;

    private Color CurrentFlaskLiquidColor;


    public Transform CollectBuretteSound;
    public Transform CollectDropsSound;
    public Transform CollectBoxCounterSound;
    public Transform CollectDoneSound;
    public Transform CollectWrongSound;

    private Color lightBlue = new Color(135f / 255f, 206f / 255f, 235f / 255f);
    private Color Colorless = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);



    private int Final_Result = 10;
    private bool St1_Mistake = false;
    private bool St2_Mistake = false;
    private bool St3_Mistake = false;
    private bool St4_Mistake = false;
    private bool St5_Mistake = false;



    private void Start()
    {
        s1 = false;
        s2 = false;
        s3 = false;
        s4 = false;
        s5 = false;
        BlueLighted = false;
        s6 = false;
        boxStepRunning = false;
        buretteStepRunning = false;

        totalSeconds = ExamTime * 60;




        //elementsInFlask.Add("Gulocoze_Flask");
        //elementsInFlask.Add("NA2CO3_Flask");
        //elementsInFlask.Add("Iodine_Flask");


        flask = GameObject.Find("Flask");
        BuretteFlow = GameObject.Find("BuretteFlow");
        ResultCanvas = GameObject.Find("ResultCanvas");
        ResultCanvas.SetActive(false);
        RobotText = RobotPnlHolder.Find("RobotText").Find("Text").GetComponent<TextMeshProUGUI>();
        NoteText = NotePnlHolder.Find("NoteText").Find("Text").GetComponent<TextMeshProUGUI>();
        BuretteNoteText = BuretteNoteHolder.Find("BuretteNote").Find("Text").GetComponent<TextMeshProUGUI>();
        FinalResultText = FinalResultHolder.Find("FinalResult").Find("Text").GetComponent<TextMeshProUGUI>();
        ExamTimeCounterText = ExamTimeHolder.Find("Counter").Find("Text").GetComponent<TextMeshProUGUI>();
        BuretteParticle = GameObject.Find("BuretteFlow").transform.GetComponent<ParticleSystem>();
        //burettebuttonn.gameObject.SetActive(false);
        NumOfStarchDrops = 0;


        BoxDoor = GameObject.Find("Box").transform.Find("BoxDoor");
        try
        { BoxCount = GameObject.Find("BoxPnl").transform.Find("BoxCount").Find("Text").GetComponent<TextMeshProUGUI>(); }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        enable_BoxCount = GameObject.Find("BoxPnl").transform.Find("BoxCount").gameObject;
        enable_BuretteNoteText = GameObject.Find("BurettePnl").transform.Find("BuretteNote").gameObject;
        enable_BoxCount.SetActive(false);
        enable_BuretteNoteText.SetActive(false);
        BuretteParticle.Stop();
        BuretteFlow.SetActive(false);
    }

    private void Update()
    {
        TimeOver();
        Renderer liquidRend = transform.Find("FlaskLiquid").GetComponent<Renderer>();
        if (liquidRend == null) return;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        liquidRend.GetPropertyBlock(propBlock);

        CurrentFlaskLiquidColor = propBlock.GetColor("_SideColor");


        BoxDoorPosition_X = BoxDoor.position.x;
        BoxDoorPosition_Y = BoxDoor.position.y;
        BoxDoorPosition_z = BoxDoor.position.z;
        FlaskPosition_X = transform.position.x;
        FlaskPosition_Y = transform.position.y;
        FlaskPosition_z = transform.position.z;




        if (elementsInFlask.Contains("Gulocoze_Flask") && elementsInFlask.Contains("NA2CO3_Flask") && elementsInFlask.Contains("Iodine_Flask") && !s1 && !s2 && !s3 && !s4 && !s5)
        {
            Color brown_Color = new Color(0.6f, 0.4f, 0.2f);
            propBlock.SetFloat("_fill", 1.68f);
            propBlock.SetColor("_SideColor", brown_Color);
            propBlock.SetColor("_TopColor", brown_Color);
            liquidRend.SetPropertyBlock(propBlock);
            CollectingDoneSoundOn(transform.position);
            RobotText.text = "Step Done";
            s1 = true;
        }
        else if (NumOfStarchDrops == 5 && s1 && s2 && s3 && s4 && !s5 && !s6)
        {
            propBlock.SetColor("_SideColor", Color.blue);
            propBlock.SetColor("_TopColor", Color.blue);
            liquidRend.SetPropertyBlock(propBlock);
            CollectingDoneSoundOn(transform.position);
            RobotText.text = ("Starch added correctly");

            s5 = true;


        }
        else if (CurrentFlaskLiquidColor == lightBlue && s1 && s2 && s3 && s4 && s5 && !BlueLighted && !s6)
        {
            CollectingDoneSoundOn(transform.position);
            RobotText.text = ("the liquid now is light blue because there is still iodine");
            BlueLighted = true;

        }
        else if (FlaskPosition_X >= 3.9099 && FlaskPosition_X <= 3.9834 && FlaskPosition_Y >= 1.0413 && FlaskPosition_Y <= 1.0942 && FlaskPosition_z >= 5.3876 && FlaskPosition_z <= 5.4508 && BuretteQuantity < 5 && s1 && s2 && s3 && !s4 && !s5 && !s6)
        {
            CollectBuretteSound.gameObject.SetActive(true);
            buretteStepRunning = true;
            BuretteFlow.SetActive(true);
            BuretteParticle.Play();
            enable_BuretteNoteText.SetActive(true);
            BuretteQuantity += .01;
            BuretteNoteText.text = "Amount of Na2S2O3 Added To The Flask: " + ((int)BuretteQuantity).ToString() + "ml";
            RobotText.text = "NA2SO3 is currently getting added to the the flask...don't move away the flask till it's finished";
            if (BuretteQuantity > 5 && BuretteQuantity < 7.5)
            {
                CollectBuretteSound.gameObject.SetActive(false);
                buretteStepRunning = false;
                propBlock.SetColor("_SideColor", Color.yellow);
                propBlock.SetColor("_TopColor", Color.yellow);
                liquidRend.SetPropertyBlock(propBlock);
                BuretteParticle.Stop();
                BuretteFlow.SetActive(false);
                CollectingDoneSoundOn(transform.position);
                RobotText.text = "Step Done";
                s4 = true;


            }

        }
        else if ((FlaskPosition_X < 3.9099 || FlaskPosition_X > 3.9834 || FlaskPosition_Y < 1.0413 || FlaskPosition_Y > 1.0942 || FlaskPosition_z < 5.3876 || FlaskPosition_z > 5.4508 || BuretteQuantity < 5) && buretteStepRunning)
        {
            CollectBuretteSound.gameObject.SetActive(false);
            BuretteParticle.Stop();
            BuretteFlow.SetActive(false);
            RobotText.text = "get the flask back to the right position to complete!!";
        }

        else if (FlaskPosition_X >= 3.9099 && FlaskPosition_X <= 3.9834 && FlaskPosition_Y >= 1.0413 && FlaskPosition_Y <= 1.0942 && FlaskPosition_z >= 5.3876 && FlaskPosition_z <= 5.4508 && BuretteQuantity < 7.5 && s1 && s2 && s3 && s4 && s5 && !s6 && CurrentFlaskLiquidColor == lightBlue)
        {
            CollectBuretteSound.gameObject.SetActive(true);
            buretteStepRunning = true;
            BuretteFlow.SetActive(true);
            BuretteParticle.Play();
            enable_BuretteNoteText.SetActive(true);
            BuretteQuantity += .01;
            BuretteNoteText.text = "Amount of Na2S2O3 Added To The Flask: " + ((int)BuretteQuantity).ToString() + "ml";
            RobotText.text = "NA2SO3 is currently getting added to the the flask...don't move away the flask till it's finished";


            if (BuretteQuantity > 7.5)
            {
                CollectBuretteSound.gameObject.SetActive(false);
                buretteStepRunning = false;
                propBlock.SetColor("_SideColor", Colorless);
                propBlock.SetColor("_TopColor", Colorless);
                liquidRend.SetPropertyBlock(propBlock);
                BuretteParticle.Stop();
                BuretteFlow.SetActive(false);
                CollectingDoneSoundOn(transform.position);
                RobotText.text = "Finally we reach the endpoint after the sample became  colorless so interaction completed." +
                    "\n\nNow we can find the % of glucose by using the following law:\r\n((10-e.p)*.009*100)/v\r\nAs e.p is volume of thiosulfate used in titration(counted in note)\r\n And v is the volume taken of sugar solution (10ml)" +
                    "\n\nThe final result will be  .225";
                ResultCanvas.SetActive(true);
                FinalResultText.text = GetResult().ToString();
                BuretteNoteText.text = "Amount of Na2S2O3 Added To The Flask: " + "7.5ml";
                s6 = true;


            }
        }
        else if (FlaskPosition_X >= 3.9099 && FlaskPosition_X <= 3.9834 && FlaskPosition_Y >= 1.0413 && FlaskPosition_Y <= 1.0942 && FlaskPosition_z >= 5.3876 && FlaskPosition_z <= 5.4508 && BuretteQuantity < 5 && (!s1 || !s2 || !s3 || s5 || s6) && !s4)
        {
            CollectingWrongSoundOn(transform.position);
            St3_Mistake = true;
            RobotText.text = "Mistake Action";
        }
       











        NoteText.text = $"Num of elements in Flask:{elementsInFlask.Count}" +
        $"\nStarch Drops Added:{NumOfStarchDrops}";

        //#Box Step.....

        if (FlaskPosition_X >= 5.9188 && FlaskPosition_X <= 6.046281 && FlaskPosition_Y >= 1.004637 && FlaskPosition_Y <= 1.2232 && s1 && !s2 && !s3 && !s4 && !s5 && !s6)
        {

            //RobotText.text = "Flask now is in the box....Press on the 'o' button appeared to close the box";
            boxStepRunning = true;
            RobotText.text = "Flask now is in the box....click 'o' to close the box and start and wait till the counter finish and Continue";
            float tolerance = 0.001f;
            if (Mathf.Abs(BoxDoorPosition_X - 5.990258f) < tolerance && Mathf.Abs(BoxDoorPosition_Y - 1.248371f) < tolerance)
            {

                enable_BoxCount.SetActive(true);
                if (CountVal > 0)
                {
                    CollectBoxCounterSound.gameObject.SetActive(true);
                    CountVal -= Time.deltaTime;
                    BoxCount.text = (Mathf.FloorToInt(CountVal)).ToString();

                }
                else
                {
                    CollectBoxCounterSound.gameObject.SetActive(false);
                    boxStepRunning = false;
                    enable_BoxCount.SetActive(false);
                    CollectingDoneSoundOn(transform.position);
                    RobotText.text = "Step Done";
                    propBlock.SetColor("_SideColor", Color.yellow);
                    propBlock.SetColor("_TopColor", Color.yellow);
                    liquidRend.SetPropertyBlock(propBlock);
                    s2 = true;

                }

            }
            //else if (((BoxDoorPosition_X - 5.990258f) > tolerance || Mathf.Abs(BoxDoorPosition_Y - 1.248371f) > tolerance) && s2)
            //{
            //    RobotText.text = "Step Done...now you will notice shange of the liquid color to yellow" +
            //            "\n\nwe could go to the next step now by adding 10ml of H2SO4(2n) to the flask by dragging it to the top of flask and bend it by Left and Right arrows" +
            //            "\n\n ";
            //}


            else
            {
                CollectBoxCounterSound.gameObject.SetActive(false);
                BoxCount.text = "Door opened!!....please get close the door to complete";

            }


        }
        else if ((FlaskPosition_X < 5.9188 || FlaskPosition_X > 6.046281 || FlaskPosition_Y < 1.004637 || FlaskPosition_Y > 1.2232) && boxStepRunning)
        {
            CollectBoxCounterSound.gameObject.SetActive(false);
            RobotText.text = "Flask is not int right position..please drag it to suitable position in the box";
        }
        else if (FlaskPosition_X >= 5.9188 && FlaskPosition_X <= 6.046281 && FlaskPosition_Y >= 1.004637 && FlaskPosition_Y <= 1.2232 && (!s1  || s3 || s4 || s5 || s6) && !s2)
        {
            float tolerance = 0.001f;
            if (Mathf.Abs(BoxDoorPosition_X - 5.990258f) < tolerance && Mathf.Abs(BoxDoorPosition_Y - 1.248371f) < tolerance)
            {
                CollectingWrongSoundOn(transform.position);
                St2_Mistake = true;
                RobotText.text = "Mistake Action";

            }
        }




    }


    private void OnTriggerEnter(Collider other)
    {


        Renderer liquidRend = transform.Find("FlaskLiquid").GetComponent<Renderer>();
        if (liquidRend == null) return;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        liquidRend.GetPropertyBlock(propBlock);



        float other_Rotation_z = other.gameObject.transform.rotation.z;


        if (other.gameObject.CompareTag("Element") && elementsInFlask.Count <= maxElementsInFlask && ((other_Rotation_z >= .75 && other_Rotation_z <= .82) || (other_Rotation_z <= -.75 && other_Rotation_z >= -.82)))
        {
            Transform LiquidTrans = other.transform;
            if (!elementsInFlask.Contains(other.gameObject.name))
            {
                ////other.gameObject.transform.SetParent(flask.transform);
                //elementsInFlask.Add(other.gameObject);
                //other.gameObject.transform.Find("Liquid").SetParent(flask.transform);
                //Transform liquidTransform = FindChildWithTag(other.transform, "Liquid");
                //if (liquidTransform == null) return;
                //liquidTransform.parent = flask.transform;
                //liquidTransform.localPosition = Vector3.zero; 



                if (LiquidTrans.Find("Gulocoze") == true || LiquidTrans.Find("NA2CO3") == true || LiquidTrans.Find("Iodine") == true)
                {
                    if (LiquidTrans.Find("Gulocoze") == true)
                    {
                        LiquidTrans.Find("Gulocoze").gameObject.SetActive(false);
                    }
                    else if (LiquidTrans.Find("NA2CO3") == true)
                    {
                        LiquidTrans.Find("NA2CO3").gameObject.SetActive(false);
                    }
                    else if (LiquidTrans.Find("Iodine") == true)
                    {
                        Color brown_Color = new Color(0.6f, 0.4f, 0.2f);
                        LiquidTrans.Find("Iodine").gameObject.SetActive(false);
                        propBlock.SetColor("_SideColor", brown_Color);
                        propBlock.SetColor("_TopColor", brown_Color);
                        liquidRend.SetPropertyBlock(propBlock);
                    }

                    float currentFill = propBlock.GetFloat("_fill");
                    float addLiquidFill = .56f;
                    propBlock.SetFloat("_fill", currentFill + addLiquidFill);
                    liquidRend.SetPropertyBlock(propBlock);
                    Debug.Log(currentFill);
                    elementsInFlask.Add(other.gameObject.name);
                    RobotText.text = "element added correctly to the flask";
                    NoteText.text = "Num of elements in Flask:" + elementsInFlask.Count;


                }

                else if (LiquidTrans.Find("H2SO4") == true && s1 && s2 && !s3 && !s4 && !s5 && !s6)
                {
                    Color brown_Color = new Color(0.6f, 0.4f, 0.2f);
                    propBlock.SetColor("_SideColor", brown_Color);
                    propBlock.SetColor("_TopColor", brown_Color);
                    liquidRend.SetPropertyBlock(propBlock);
                    elementsInFlask.Add(other.gameObject.name);
                    LiquidTrans.Find("H2SO4").gameObject.SetActive(false);
                    CollectingDoneSoundOn(transform.position);
                    RobotText.text = "H2so4 added correctly to the flask";
                    NoteText.text = "Num of elements in Flask:" + elementsInFlask.Count;
                    s3 = true;
                }


                else
                {
                    CollectingWrongSoundOn(transform.position);
                    elementsInFlask.Remove(other.gameObject.name);
                    RobotText.text = "this element is the wrong element!!!";
                    St1_Mistake = true;
                    RobotText.text = "Mistake Action";

                }


            }



            else if (elementsInFlask.Contains("Gulocoze_Flask") || elementsInFlask.Contains("NA2CO3_Flask") || elementsInFlask.Contains("Iodine_Flask") || elementsInFlask.Contains("H2SO4_Flask"))
            {
                RobotText.text = "element is already in flask or this flask is empty";


            }




        }

        else if (other.gameObject.CompareTag("StarchDropper") && s1 && s2 && s3 && s4 && !s5 && !s6)
        {
            CollectingDropsSoundOn(flask.transform.position);

            if (NumOfStarchDrops <= 4)
            {
                RobotText.text = ("Drop of Starch added correctly");
                NumOfStarchDrops++;

            }


        }
        else if (other.gameObject.CompareTag("StarchDropper") && (!s1 || !s2 || !s3 || !s4 || s6) && !s5)
        {
            CollectingWrongSoundOn(transform.position);
            St4_Mistake = true;
            RobotText.text = "Mistake Action";




        }






    }



    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }

    private int GetResult()
    {
        int mis1 = Convert.ToInt16(St1_Mistake);
        int mis2 = Convert.ToInt16(St2_Mistake);
        int mis3 = Convert.ToInt16(St3_Mistake);
        int mis4 = Convert.ToInt16(St4_Mistake);
        int mis5 = Convert.ToInt16(St5_Mistake);
        int stp1 = Convert.ToInt16(s1);
        int stp2 = Convert.ToInt16(s2);
        int stp3 = Convert.ToInt16(s3);
        int stp4 = Convert.ToInt16(s4);
        int stp5 = Convert.ToInt16(s5);
        int stp6 = Convert.ToInt16(s6);
        int total_mistakes = mis1 + mis2 + mis3 + mis4 + mis5;
        int total_Correct = stp1 + stp2 + stp3 + stp4 + stp5 + stp6;
        if (total_Correct < 4 )
        {
            return total_Correct;
        }
        else
        {
            return (4 + total_Correct) - total_mistakes;
        }

    }
    private void TimeOver()
    {
        if (s6 == false)
        {
            if (totalSeconds >= 0)
            {
                int minutes = Mathf.FloorToInt(totalSeconds / 60);
                int seconds = Mathf.FloorToInt(totalSeconds % 60);

                ExamTimeCounterText.text =$"{minutes}:{seconds}";
                totalSeconds -= Time.deltaTime;
                if (totalSeconds < 0.1)
                {
                    Debug.Log("finished");
                    ResultCanvas.SetActive(true);
                    FinalResultText.text = GetResult().ToString();
                }
            }
            
        }
        
    }


    void CollectingBuretteSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(CollectBuretteSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }
    void CollectingDropsSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(CollectDropsSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }

    void CollectingDoneSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(CollectDoneSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }
    void CollectingWrongSoundOn(UnityEngine.Vector3 pos)
    {
        Transform obj = Instantiate(CollectWrongSound, pos, new UnityEngine.Quaternion());
        obj.gameObject.SetActive(true);
    }




    //bool IsCloseEnough(float a, float b, float tolerance = 0.0001f)
    //{
    //    return Math.Abs(a - b) < tolerance;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Element") && elementsInFlask.Contains(other.gameObject))
    //    {
    //        other.gameObject.transform.SetParent(null);
    //        elementsInFlask.Remove(other.gameObject);
    //    }
    //}
}
