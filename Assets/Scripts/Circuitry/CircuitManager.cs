using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PinGroup {
    // Each group of pins is a separate connection.
    // (Each pin within a group is connected to each other pin in that group.)
    NULL,

    hi1,
    hi2,
    hi3,
    hi4,
    hi5,
    hi6,
    hi7,
    hi8,
    hi9,
    hi10,
    hi11,
    hi12,
    hi13,
    hi14,
    hi15,
    hi16,
    hi17,
    hi18,
    hi19,
    hi20,
    hi21,
    hi22,
    hi23,
    hi24,
    hi25,
    hi26,
    hi27,
    hi28,
    hi29,
    hi30,
    hiPos,
    hiNeg,

    lo1,
    lo2,
    lo3,
    lo4,
    lo5,
    lo6,
    lo7,
    lo8,
    lo9,
    lo10,
    lo11,
    lo12,
    lo13,
    lo14,
    lo15,
    lo16,
    lo17,
    lo18,
    lo19,
    lo20,
    lo21,
    lo22,
    lo23,
    lo24,
    lo25,
    lo26,
    lo27,
    lo28,
    lo29,
    lo30,
    loPos,
    loNeg
}

public class MyException : Exception {
    // A simple general exception to be thrown when something goes wrong.
    public MyException() {
    }
    public MyException(string str) : base(str){
    }
}

public class ResistiveElement {
    // Represents a resistive element in the breadboard circuit.
    // Contains both connected points, resistance, voltage, and current, and which component game object is connected.

    public PinGroup ptA = PinGroup.NULL;
    public PinGroup ptB = PinGroup.NULL;
    public GameObject component = null;
    public double resistance = 0;
    public double voltage = 0;
    public double current = 0;

    public ResistiveElement(GameObject component) {
        this.component = component;
    }

    public void AddPoint(PinGroup point) {
        // Add a connection to the first empty connection. Throws error if not possible.
        if (point == PinGroup.NULL) {
            throw new MyException("Point to be added shouldn't be null");
        }
        if (this.ptA == PinGroup.NULL) {
            this.ptA = point;
            return;
        }
        if (this.ptB == PinGroup.NULL) {
            this.ptB = point;
            return;
        }
        throw new MyException("Can't add point; both points are already filled");
    }

    public void RemovePoint(PinGroup point) {
        // Removes the specified connection. Throws error if not possible.
        if (point == PinGroup.NULL) {
            throw new MyException("Point to be removed shouldn't be null");
        }
        if (this.ptA == point) {
            this.ptA = PinGroup.NULL;
            return;
        }
        if (this.ptB == point) {
            this.ptB = PinGroup.NULL;
            return;
        }
        throw new MyException("Can't remove point; given point doesn't match either point in object");
    }
}

public class CircuitManager : MonoBehaviour
{
    public List<ResistiveElement> circuitComponents = new List<ResistiveElement>();

    public GameObject J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14, J15, J16, J17, J18, J19, J20, J21, J22, J23, J24, J25, J26, J27, J28, J29, J30;
    public GameObject I1, I2, I3, I4, I5, I6, I7, I8, I9, I10, I11, I12, I13, I14, I15, I16, I17, I18, I19, I20, I21, I22, I23, I24, I25, I26, I27, I28, I29, I30;
    public GameObject H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, H11, H12, H13, H14, H15, H16, H17, H18, H19, H20, H21, H22, H23, H24, H25, H26, H27, H28, H29, H30;
    public GameObject G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, G11, G12, G13, G14, G15, G16, G17, G18, G19, G20, G21, G22, G23, G24, G25, G26, G27, G28, G29, G30;
    public GameObject F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27, F28, F29, F30;
    public GameObject E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, E11, E12, E13, E14, E15, E16, E17, E18, E19, E20, E21, E22, E23, E24, E25, E26, E27, E28, E29, E30;
    public GameObject D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30;
    public GameObject C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, C16, C17, C18, C19, C20, C21, C22, C23, C24, C25, C26, C27, C28, C29, C30;
    public GameObject B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, B14, B15, B16, B17, B18, B19, B20, B21, B22, B23, B24, B25, B26, B27, B28, B29, B30;
    public GameObject A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16, A17, A18, A19, A20, A21, A22, A23, A24, A25, A26, A27, A28, A29, A30;
    public GameObject powerRailTopPos, powerRailTopNeg, powerRailBottomPos, powerRailBottomNeg;

    GameObject[] row1T = new GameObject[5];
    GameObject[] row2T = new GameObject[5];
    GameObject[] row3T = new GameObject[5];
    GameObject[] row4T = new GameObject[5];
    GameObject[] row5T = new GameObject[5];
    GameObject[] row6T = new GameObject[5];
    GameObject[] row7T = new GameObject[5];
    GameObject[] row8T = new GameObject[5];
    GameObject[] row9T = new GameObject[5];
    GameObject[] row10T = new GameObject[5];
    GameObject[] row11T = new GameObject[5];
    GameObject[] row12T = new GameObject[5];
    GameObject[] row13T = new GameObject[5];
    GameObject[] row14T = new GameObject[5];
    GameObject[] row15T = new GameObject[5];
    GameObject[] row16T = new GameObject[5];
    GameObject[] row17T = new GameObject[5];
    GameObject[] row18T = new GameObject[5];
    GameObject[] row19T = new GameObject[5];
    GameObject[] row20T = new GameObject[5];
    GameObject[] row21T = new GameObject[5];
    GameObject[] row22T = new GameObject[5];
    GameObject[] row23T = new GameObject[5];
    GameObject[] row24T = new GameObject[5];
    GameObject[] row25T = new GameObject[5];
    GameObject[] row26T = new GameObject[5];
    GameObject[] row27T = new GameObject[5];
    GameObject[] row28T = new GameObject[5];
    GameObject[] row29T = new GameObject[5];
    GameObject[] row30T = new GameObject[5];

    GameObject[] row1B = new GameObject[5];
    GameObject[] row2B = new GameObject[5];
    GameObject[] row3B = new GameObject[5];
    GameObject[] row4B = new GameObject[5];
    GameObject[] row5B = new GameObject[5];
    GameObject[] row6B = new GameObject[5];
    GameObject[] row7B = new GameObject[5];
    GameObject[] row8B = new GameObject[5];
    GameObject[] row9B = new GameObject[5];
    GameObject[] row10B = new GameObject[5];
    GameObject[] row11B = new GameObject[5];
    GameObject[] row12B = new GameObject[5];
    GameObject[] row13B = new GameObject[5];
    GameObject[] row14B = new GameObject[5];
    GameObject[] row15B = new GameObject[5];
    GameObject[] row16B = new GameObject[5];
    GameObject[] row17B = new GameObject[5];
    GameObject[] row18B = new GameObject[5];
    GameObject[] row19B = new GameObject[5];
    GameObject[] row20B = new GameObject[5];
    GameObject[] row21B = new GameObject[5];
    GameObject[] row22B = new GameObject[5];
    GameObject[] row23B = new GameObject[5];
    GameObject[] row24B = new GameObject[5];
    GameObject[] row25B = new GameObject[5];
    GameObject[] row26B = new GameObject[5];
    GameObject[] row27B = new GameObject[5];
    GameObject[] row28B = new GameObject[5];
    GameObject[] row29B = new GameObject[5];
    GameObject[] row30B = new GameObject[5];


    // Start is called before the first frame update
    void Start()
    {
        this.row1T = new GameObject[]{this.F1, this.G1, this.H1, this.I1, this.J1};
        this.row2T = new GameObject[]{this.F2, this.G2, this.H2, this.I2, this.J2};
        this.row3T = new GameObject[]{this.F3, this.G3, this.H3, this.I3, this.J3};
        this.row4T = new GameObject[]{this.F4, this.G4, this.H4, this.I4, this.J4};
        this.row5T = new GameObject[]{this.F5, this.G5, this.H5, this.I5, this.J5};
        this.row6T = new GameObject[]{this.F6, this.G6, this.H6, this.I6, this.J6};
        this.row7T = new GameObject[]{this.F7, this.G7, this.H7, this.I7, this.J7};
        this.row8T = new GameObject[]{this.F8, this.G8, this.H8, this.I8, this.J8};
        this.row9T = new GameObject[]{this.F9, this.G9, this.H9, this.I9, this.J9};
        this.row10T = new GameObject[]{this.F10, this.G10, this.H10, this.I10, this.J10};
        this.row11T = new GameObject[]{this.F11, this.G11, this.H11, this.I11, this.J11};
        this.row12T = new GameObject[]{this.F12, this.G12, this.H12, this.I12, this.J12};
        this.row13T = new GameObject[]{this.F13, this.G13, this.H13, this.I13, this.J13};
        this.row14T = new GameObject[]{this.F14, this.G14, this.H14, this.I14, this.J14};
        this.row15T = new GameObject[]{this.F15, this.G15, this.H15, this.I15, this.J15};
        this.row16T = new GameObject[]{this.F16, this.G16, this.H16, this.I16, this.J16};
        this.row17T = new GameObject[]{this.F17, this.G17, this.H17, this.I17, this.J17};
        this.row18T = new GameObject[]{this.F18, this.G18, this.H18, this.I18, this.J18};
        this.row19T = new GameObject[]{this.F19, this.G19, this.H19, this.I19, this.J19};
        this.row20T = new GameObject[]{this.F20, this.G20, this.H20, this.I20, this.J20};
        this.row21T = new GameObject[]{this.F21, this.G21, this.H21, this.I21, this.J21};
        this.row22T = new GameObject[]{this.F22, this.G22, this.H22, this.I22, this.J22};
        this.row23T = new GameObject[]{this.F23, this.G23, this.H23, this.I23, this.J23};
        this.row24T = new GameObject[]{this.F24, this.G24, this.H24, this.I24, this.J24};
        this.row25T = new GameObject[]{this.F25, this.G25, this.H25, this.I25, this.J25};
        this.row26T = new GameObject[]{this.F26, this.G26, this.H26, this.I26, this.J26};
        this.row27T = new GameObject[]{this.F27, this.G27, this.H27, this.I27, this.J27};
        this.row28T = new GameObject[]{this.F28, this.G28, this.H28, this.I28, this.J28};
        this.row29T = new GameObject[]{this.F29, this.G29, this.H29, this.I29, this.J29};
        this.row30T = new GameObject[]{this.F30, this.G30, this.H30, this.I30, this.J30};

        this.row1B = new GameObject[]{this.A1, this.B1, this.C1, this.D1, this.E1};
        this.row2B = new GameObject[]{this.A2, this.B2, this.C2, this.D2, this.E2};
        this.row3B = new GameObject[]{this.A3, this.B3, this.C3, this.D3, this.E3};
        this.row4B = new GameObject[]{this.A4, this.B4, this.C4, this.D4, this.E4};
        this.row5B = new GameObject[]{this.A5, this.B5, this.C5, this.D5, this.E5};
        this.row6B = new GameObject[]{this.A6, this.B6, this.C6, this.D6, this.E6};
        this.row7B = new GameObject[]{this.A7, this.B7, this.C7, this.D7, this.E7};
        this.row8B = new GameObject[]{this.A8, this.B8, this.C8, this.D8, this.E8};
        this.row9B = new GameObject[]{this.A9, this.B9, this.C9, this.D9, this.E9};
        this.row10B = new GameObject[]{this.A10, this.B10, this.C10, this.D10, this.E10};
        this.row11B = new GameObject[]{this.A11, this.B11, this.C11, this.D11, this.E11};
        this.row12B = new GameObject[]{this.A12, this.B12, this.C12, this.D12, this.E12};
        this.row13B = new GameObject[]{this.A13, this.B13, this.C13, this.D13, this.E13};
        this.row14B = new GameObject[]{this.A14, this.B14, this.C14, this.D14, this.E14};
        this.row15B = new GameObject[]{this.A15, this.B15, this.C15, this.D15, this.E15};
        this.row16B = new GameObject[]{this.A16, this.B16, this.C16, this.D16, this.E16};
        this.row17B = new GameObject[]{this.A17, this.B17, this.C17, this.D17, this.E17};
        this.row18B = new GameObject[]{this.A18, this.B18, this.C18, this.D18, this.E18};
        this.row19B = new GameObject[]{this.A19, this.B19, this.C19, this.D19, this.E19};
        this.row20B = new GameObject[]{this.A20, this.B20, this.C20, this.D20, this.E20};
        this.row21B = new GameObject[]{this.A21, this.B21, this.C21, this.D21, this.E21};
        this.row22B = new GameObject[]{this.A22, this.B22, this.C22, this.D22, this.E22};
        this.row23B = new GameObject[]{this.A23, this.B23, this.C23, this.D23, this.E23};
        this.row24B = new GameObject[]{this.A24, this.B24, this.C24, this.D24, this.E24};
        this.row25B = new GameObject[]{this.A25, this.B25, this.C25, this.D25, this.E25};
        this.row26B = new GameObject[]{this.A26, this.B26, this.C26, this.D26, this.E26};
        this.row27B = new GameObject[]{this.A27, this.B27, this.C27, this.D27, this.E27};
        this.row28B = new GameObject[]{this.A28, this.B28, this.C28, this.D28, this.E28};
        this.row29B = new GameObject[]{this.A29, this.B29, this.C29, this.D29, this.E29};
        this.row30B = new GameObject[]{this.A30, this.B30, this.C30, this.D30, this.E30};

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ResistiveElement GetResistiveElement(GameObject component) {
        //Gets the first match in circuitComponents that contains component. SINGLE.
        foreach (ResistiveElement x in this.circuitComponents) {
            if (x.component == component) {
                return x;
            }
        }
        return null;
    }

    public List<ResistiveElement> GetResistiveElements(PinGroup connection) {
        //Gets all matches in circuitComponents that contains the pingroup as a connection. MULTIPLE.
        List<ResistiveElement> all = new List<ResistiveElement>();
        foreach (ResistiveElement x in this.circuitComponents) {
            if (x.ptA == connection || x.ptB == connection) {
                all.Add(x);
            }
        }
        return all;
    }

    PinGroup GetPinGroup(GameObject pin) {
        //Gets the connection group that the pin belongs to.
        if (pin.transform.parent.gameObject == this.powerRailTopPos) return PinGroup.hiPos;
        if (pin.transform.parent.gameObject == this.powerRailTopNeg) return PinGroup.hiNeg;
        if (pin.transform.parent.gameObject == this.powerRailBottomPos) return PinGroup.loPos;
        if (pin.transform.parent.gameObject == this.powerRailBottomNeg) return PinGroup.loNeg;

        if (Array.Exists(this.row1T, x => x == pin)) return PinGroup.hi1;
        if (Array.Exists(this.row2T, x => x == pin)) return PinGroup.hi2;
        if (Array.Exists(this.row3T, x => x == pin)) return PinGroup.hi3;
        if (Array.Exists(this.row4T, x => x == pin)) return PinGroup.hi4;
        if (Array.Exists(this.row5T, x => x == pin)) return PinGroup.hi5;
        if (Array.Exists(this.row6T, x => x == pin)) return PinGroup.hi6;
        if (Array.Exists(this.row7T, x => x == pin)) return PinGroup.hi7;
        if (Array.Exists(this.row8T, x => x == pin)) return PinGroup.hi8;
        if (Array.Exists(this.row9T, x => x == pin)) return PinGroup.hi9;
        if (Array.Exists(this.row10T, x => x == pin)) return PinGroup.hi10;
        if (Array.Exists(this.row11T, x => x == pin)) return PinGroup.hi11;
        if (Array.Exists(this.row12T, x => x == pin)) return PinGroup.hi12;
        if (Array.Exists(this.row13T, x => x == pin)) return PinGroup.hi13;
        if (Array.Exists(this.row14T, x => x == pin)) return PinGroup.hi14;
        if (Array.Exists(this.row15T, x => x == pin)) return PinGroup.hi15;
        if (Array.Exists(this.row16T, x => x == pin)) return PinGroup.hi16;
        if (Array.Exists(this.row17T, x => x == pin)) return PinGroup.hi17;
        if (Array.Exists(this.row18T, x => x == pin)) return PinGroup.hi18;
        if (Array.Exists(this.row19T, x => x == pin)) return PinGroup.hi19;
        if (Array.Exists(this.row20T, x => x == pin)) return PinGroup.hi20;
        if (Array.Exists(this.row21T, x => x == pin)) return PinGroup.hi21;
        if (Array.Exists(this.row22T, x => x == pin)) return PinGroup.hi22;
        if (Array.Exists(this.row23T, x => x == pin)) return PinGroup.hi23;
        if (Array.Exists(this.row24T, x => x == pin)) return PinGroup.hi24;
        if (Array.Exists(this.row25T, x => x == pin)) return PinGroup.hi25;
        if (Array.Exists(this.row26T, x => x == pin)) return PinGroup.hi26;
        if (Array.Exists(this.row27T, x => x == pin)) return PinGroup.hi27;
        if (Array.Exists(this.row28T, x => x == pin)) return PinGroup.hi28;
        if (Array.Exists(this.row29T, x => x == pin)) return PinGroup.hi29;
        if (Array.Exists(this.row30T, x => x == pin)) return PinGroup.hi30;

        if (Array.Exists(this.row1B, x => x == pin)) return PinGroup.lo1;
        if (Array.Exists(this.row2B, x => x == pin)) return PinGroup.lo2;
        if (Array.Exists(this.row3B, x => x == pin)) return PinGroup.lo3;
        if (Array.Exists(this.row4B, x => x == pin)) return PinGroup.lo4;
        if (Array.Exists(this.row5B, x => x == pin)) return PinGroup.lo5;
        if (Array.Exists(this.row6B, x => x == pin)) return PinGroup.lo6;
        if (Array.Exists(this.row7B, x => x == pin)) return PinGroup.lo7;
        if (Array.Exists(this.row8B, x => x == pin)) return PinGroup.lo8;
        if (Array.Exists(this.row9B, x => x == pin)) return PinGroup.lo9;
        if (Array.Exists(this.row10B, x => x == pin)) return PinGroup.lo10;
        if (Array.Exists(this.row11B, x => x == pin)) return PinGroup.lo11;
        if (Array.Exists(this.row12B, x => x == pin)) return PinGroup.lo12;
        if (Array.Exists(this.row13B, x => x == pin)) return PinGroup.lo13;
        if (Array.Exists(this.row14B, x => x == pin)) return PinGroup.lo14;
        if (Array.Exists(this.row15B, x => x == pin)) return PinGroup.lo15;
        if (Array.Exists(this.row16B, x => x == pin)) return PinGroup.lo16;
        if (Array.Exists(this.row17B, x => x == pin)) return PinGroup.lo17;
        if (Array.Exists(this.row18B, x => x == pin)) return PinGroup.lo18;
        if (Array.Exists(this.row19B, x => x == pin)) return PinGroup.lo19;
        if (Array.Exists(this.row20B, x => x == pin)) return PinGroup.lo20;
        if (Array.Exists(this.row21B, x => x == pin)) return PinGroup.lo21;
        if (Array.Exists(this.row22B, x => x == pin)) return PinGroup.lo22;
        if (Array.Exists(this.row23B, x => x == pin)) return PinGroup.lo23;
        if (Array.Exists(this.row24B, x => x == pin)) return PinGroup.lo24;
        if (Array.Exists(this.row25B, x => x == pin)) return PinGroup.lo25;
        if (Array.Exists(this.row26B, x => x == pin)) return PinGroup.lo26;
        if (Array.Exists(this.row27B, x => x == pin)) return PinGroup.lo27;
        if (Array.Exists(this.row28B, x => x == pin)) return PinGroup.lo28;
        if (Array.Exists(this.row29B, x => x == pin)) return PinGroup.lo29;
        if (Array.Exists(this.row30B, x => x == pin)) return PinGroup.lo30;

        return PinGroup.NULL;
    }

    //OnConnect and OnDisconnect should be called anytime a connection is made/broken
    public void OnConnect(GameObject pin, GameObject component) {
        ResistiveElement circuitComponent = this.GetResistiveElement(component);
        if (circuitComponent == null) {
            circuitComponent = new ResistiveElement(component);
            this.circuitComponents.Add(circuitComponent);
        }
        PinGroup point = this.GetPinGroup(pin);
        circuitComponent.AddPoint(point); // Has the potential to throw an exception. Currently uncaught.
        //this.circuitChanged = true; // do something to calcualte math aspect.
    }
    public void OnDisconnect(GameObject pin, GameObject component) {
        ResistiveElement circuitComponent = this.GetResistiveElement(component);
        if (circuitComponent == null) {
            //throw error? this implies that you thought you were disconnecting something that wasn't actually there, which would indicate a bug.
            Debug.Log("OnDisconnect failed to find component to disconnect. Investigate likely bug!");
            return;
        }
        PinGroup point = this.GetPinGroup(pin);
        circuitComponent.RemovePoint(point); // Has the potential to throw an exception. Currently uncaught.
        if (circuitComponent.ptA == PinGroup.NULL && circuitComponent.ptB == PinGroup.NULL) {
            this.circuitComponents.Remove(circuitComponent);
        }
        //this.circuitChanged = true; // do something to calculate math aspect.
    }
}
