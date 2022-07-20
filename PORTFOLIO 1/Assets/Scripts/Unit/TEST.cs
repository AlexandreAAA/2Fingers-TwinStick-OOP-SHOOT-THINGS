using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public int _a;
    public int _b;
    public int K;


    void Start()
    {
        Main();
    }

    // Update is called once per frame
    void Update()
    {
        Main();
        Debug.Log("Prout");
    }

    public int Solution(int A, int B, int k)
    {
        return (B / k) - (A < 0 ? (A - 1) / k : -1);
    }

    private void Main()
    {
       Debug.Log("prout " + Solution(_a, _b, K).ToString());
    
    }
}
