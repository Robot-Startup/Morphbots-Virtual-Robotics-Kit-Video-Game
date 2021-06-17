using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    [SerializeField] CircuitManager circuitManager;



    PinGroup pos = PinGroup.NULL;
    PinGroup neg = PinGroup.NULL;
    double calculationDelta = 0.0;

    bool battery = false;
    bool simpleSeries = false;
    bool resistor = false;
    bool led = false;

    public bool task1 = false;
    public bool task2 = false;
    public bool task3 = false;
    public bool task4 = false;


    // Update is called once per frame
    void Update()
    {

        /* Light up LED and trigger win screen */
        if (task1 == true
            && task2 == true
            && task3 == true 
            && task4 == true)
        {

        }

        double calculateRate = 0.3; // how frequent to recalculate circuit
        this.calculationDelta = this.calculationDelta + Time.deltaTime;
        if (this.calculationDelta > calculateRate) {
            this.calculationDelta = 0.0;
            this.updateCalculations();
            this.updateTasks();
        }
    }

    // Update the state of calculations
    void updateCalculations() {
        this.battery = this.checkBattery();
        this.simpleSeries = this.checkSingleSeries();
        this.resistor = this.checkComponents("Resistor", 1);
        this.led = this.checkComponents("LED", 1);
    }

    // Update the state of tasks
    void updateTasks() {
        this.task1 = this.battery;
        this.task2 = this.task1 && this.simpleSeries;
        this.task3 = this.task2 && this.resistor;
        this.task4 = this.task2 && this.led;
    }

    // Check battery present and hooked up to opposing power rails
    bool checkBattery() {
        foreach (ResistiveElement x in circuitManager.circuitComponents) {
            if (x.component != null && x.component.tag == "Battery") {
                bool posNeg = (x.ptA == PinGroup.hiPos && x.ptB == PinGroup.hiNeg);
                posNeg = posNeg || (x.ptA == PinGroup.loPos && x.ptB == PinGroup.hiNeg);
                posNeg = posNeg || (x.ptA == PinGroup.loPos && x.ptB == PinGroup.loNeg);
                posNeg = posNeg || (x.ptA == PinGroup.hiPos && x.ptB == PinGroup.loNeg);

                bool aPos = posNeg;

                // Assumes no set pos/neg terminal on the battery for now. (Bidirectional)
                posNeg = posNeg || (x.ptB == PinGroup.hiPos && x.ptA == PinGroup.hiNeg);
                posNeg = posNeg || (x.ptB == PinGroup.loPos && x.ptA == PinGroup.hiNeg);
                posNeg = posNeg || (x.ptB == PinGroup.loPos && x.ptA == PinGroup.loNeg);
                posNeg = posNeg || (x.ptB == PinGroup.hiPos && x.ptA == PinGroup.loNeg);
                if (posNeg) {
                    if (aPos) {
                        this.pos = x.ptA;
                        this.neg = x.ptB;
                    } else {
                        this.neg = x.ptA;
                        this.pos = x.ptB;
                    }
                    return true;
                }
            }
        }
        this.pos = PinGroup.NULL;
        this.neg = PinGroup.NULL;
        return false;
    }

    // Follow linked list through from one of the Pos rails to the Neg rail. If it branches or doesn't touch neg, return false.
    // Also check for no branching on Neg.
    bool checkSingleSeries(){
        if (!this.battery || this.pos == PinGroup.NULL || this.neg == PinGroup.NULL) {
            return false;
        }
        int count = circuitManager.circuitComponents.Count;
        int total = 0;
        PinGroup nextTarget = PinGroup.NULL;
        ResistiveElement lastElement = null;
        List<ResistiveElement> matches = circuitManager.GetResistiveElements(this.pos);
        foreach (ResistiveElement x in matches) {
            total = total + 1;
            if (x.component.tag != "Battery") {
                lastElement = x;
                if (x.ptA != this.pos) {
                    nextTarget = x.ptA;
                } else {
                    nextTarget = x.ptB;
                }
            }
        }

        if (total != 2 || nextTarget == PinGroup.NULL) return false;

        while (nextTarget != this.neg && total < count) {
            matches = circuitManager.GetResistiveElements(nextTarget);
            if (matches.Count != 2) return false;
            foreach (ResistiveElement y in matches) {
                if (y != lastElement) {
                    lastElement = y;
                    total = total + 1;
                    if (y.ptA != nextTarget) {
                        nextTarget = y.ptA;
                    } else {
                        nextTarget = y.ptB;
                    }
                }
            }
        }
        if (total == count && nextTarget == this.neg) {
            return true;
        }

        return false;
    }

    // Check that num component present in list and connected to this.pos and this.neg eventually.
    bool checkComponents(string component, int num){
        if (!this.battery || this.pos == PinGroup.NULL || this.neg == PinGroup.NULL) {
            return false;
        }

        int count = 0;
        List<ResistiveElement> matches = new List<ResistiveElement>();

        foreach (ResistiveElement x in circuitManager.circuitComponents) {
            if (x.component != null && x.component.tag == component) {
                count = count + 1;
                matches.Add(x);
                if (count > num) {
                    return false;
                }
            }
        }
        if (count != num) {
            return false;
        }

        return true;
    }

    // Find out through breadth-first-search on both the components connections if it is somehow connected to connection
    bool isConnected(ResistiveElement component, PinGroup connection) {
        // Not yet implemented
        return false;
    }
}
