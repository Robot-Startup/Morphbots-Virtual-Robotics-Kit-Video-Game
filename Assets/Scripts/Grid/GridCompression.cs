using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vox {
    // Currently just a holder for a voxel's type, but present for future expansion of features
    // Type 0 is a yet "untyped" block
    // Type 1 is an unmoving block
    // Type 2 is a block moving against an unmoving block
    // Type 3 is a block connected to either type 2 or type 3 (and therefore moving by association)
    public int type;
    public Vox(int blockType)
    {
        this.type = blockType;
    }
}

public class Moves {

    // The data structure containing the moves and utility functions
    // Reverse() must be called before moves represent how to construct shape
    public List<List<Tuple<int,int,int>>> x = new List<List<Tuple<int,int,int>>>();
    public List<List<Tuple<int,int,int>>> y = new List<List<Tuple<int,int,int>>>();
    public List<List<Tuple<int,int,int>>> z = new List<List<Tuple<int,int,int>>>();
    public Tuple<string,string,string> order = new Tuple<string,string,string>("x","y","z");
    public List<Tuple<int,int,int>> start = new List<Tuple<int,int,int>>();
    public bool reversed = false;

    public Moves() {
        // Public constructor
        this.x = new List<List<Tuple<int,int,int>>>();
        this.y = new List<List<Tuple<int,int,int>>>();
        this.z = new List<List<Tuple<int,int,int>>>();
        this.order = new Tuple<string,string,string>("x","y","z");
        this.start = new List<Tuple<int,int,int>>();
        this.reversed = false;
    }

    private static int CompareX(Tuple<int,int,int> a, Tuple<int,int,int> b) {
        if (a.Item1 < b.Item1) {
            return -1;
        }
        if (a.Item1 == b.Item1) {
            return 0;
        }
        if (a.Item1 > b.Item1) {
            return 1;
        }
        return 0;
    }
    private static int CompareY(Tuple<int,int,int> a, Tuple<int,int,int> b) {
        if (a.Item2 < b.Item2) {
            return -1;
        }
        if (a.Item2 == b.Item2) {
            return 0;
        }
        if (a.Item2 > b.Item2) {
            return 1;
        }
        return 0;
    }
    private static int CompareZ(Tuple<int,int,int> a, Tuple<int,int,int> b) {
        if (a.Item3 < b.Item3) {
            return -1;
        }
        if (a.Item3 == b.Item3) {
            return 0;
        }
        if (a.Item3 > b.Item3) {
            return 1;
        }
        return 0;
    }

    public void Reverse() {
        // Reverse everything so that it can be used for construction.
        if (reversed) {
            return;
        }
        this.reversed = true;
        this.x.Reverse();
        this.y.Reverse();
        this.z.Reverse();
        this.order = Tuple.Create(this.order.Item3, this.order.Item2, this.order.Item1);
        for (int i = 0; i < this.x.Count; i++) {
            List<Tuple<int,int,int>> blocks = new List<Tuple<int,int,int>>();
            this.x[i].Sort(CompareX);
            this.x[i].Reverse();
            foreach (Tuple<int,int,int> tuple in this.x[i]) {
                blocks.Add(Tuple.Create(tuple.Item1 - 1, tuple.Item2, tuple.Item3));
            }
            this.x[i] = blocks;
        }
        for (int i = 0; i < this.y.Count; i++) {
            List<Tuple<int,int,int>> blocks = new List<Tuple<int,int,int>>();
            this.y[i].Sort(CompareY);
            this.y[i].Reverse();
            foreach (Tuple<int,int,int> tuple in this.y[i]) {
                blocks.Add(Tuple.Create(tuple.Item1, tuple.Item2 - 1, tuple.Item3));
            }
            this.y[i] = blocks;
        }
        for (int i = 0; i < this.z.Count; i++) {
            List<Tuple<int,int,int>> blocks = new List<Tuple<int,int,int>>();
            this.z[i].Sort(CompareZ);
            this.z[i].Reverse();
            foreach (Tuple<int,int,int> tuple in this.z[i]) {
                blocks.Add(Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3 - 1));
            }
            this.z[i] = blocks;
        }
    }

    public void RecordMoves(Vox[,,] shape, string axis) {
        // After every 1 block movement this function should be used to record what moved
        // in the proper order. (This order is to be reversed later to reconstruct shape)
        List<Tuple<int,int,int>> blocks = new List<Tuple<int,int,int>>();
        List<List<Tuple<int,int,int>>> toBeAdded = new List<List<Tuple<int,int,int>>>();
        if (axis == "x") {
            toBeAdded = this.x;
        }
        if (axis == "y") {
            toBeAdded = this.y;
        }
        if (axis == "z") {
            toBeAdded = this.z;
        }
        for (int x2 = 0; x2 < shape.GetLength(0); x2++) {
            for (int y2 = 0; y2 < shape.GetLength(1); y2++) {
                for (int z2 = 0; z2 < shape.GetLength(2); z2++) {
                    Vox block = shape[x2,y2,z2];
                    if ((block != null) && (block.type == 2 || block.type == 3)) {
                        blocks.Add(Tuple.Create(x2, y2, z2));
                    }
                }
            }
        }
        toBeAdded.Add(blocks);
        if (axis == "start") {
            this.start = blocks;
        }
    }

    public void SetOrder(Tuple<string,string,string> givenOrder) {
        // Set the order in which the dimensions were shifted e.g. ["z", "y", "x"]
        this.order = givenOrder;
    }

    public string ToString() {
        // Return a string representation of the current state of the moves object
        string ret = "";
        ret = ret + $"order: {this.order}\n";
        ret = ret + $"reversed: {this.reversed}\n";

        ret = ret + "start: [";
        foreach (Tuple<int,int,int> block in this.start) {
            ret = ret + " " + block.ToString() + " ";
        }
        ret = ret + "]\n";

        ret = ret + "x: [";
        foreach (List<Tuple<int,int,int>> blocks in this.x) {
            ret = ret + " [";
            foreach (Tuple<int,int,int> block in blocks) {
                ret = ret + " " + block.ToString() + " ";
            }
            ret = ret + "] ";
        }
        ret = ret + "]\n";

        ret = ret + "y: [";
        foreach (List<Tuple<int,int,int>> blocks in this.y) {
            ret = ret + " [";
            foreach (Tuple<int,int,int> block in blocks) {
                ret = ret + " " + block.ToString() + " ";
            }
            ret = ret + "] ";
        }
        ret = ret + "]\n";

        ret = ret + "z: [";
        foreach (List<Tuple<int,int,int>> blocks in this.z) {
            ret = ret + " [";
            foreach (Tuple<int,int,int> block in blocks) {
                ret = ret + " " + block.ToString() + " ";
            }
            ret = ret + "] ";
        }
        ret = ret + "]\n";

        return ret;
    }
}

public class GridCompression : MonoBehaviour
{
    [SerializeField] GridMain gridMain;
    [SerializeField] Moves moves = null;

    //------------------Axis specific helper functions ---------------------------------------

    static void MoveBlocksByOneZ(Vox[,,] shape)
    {
        // Move all blocks that can move by one unit along the z axis
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                for (int z = 0; z < shape.GetLength(2); z++)
                {
                    Vox block = shape[x, y, z];
                    if ((block != null) && (block.type == 2 || block.type == 3))
                    {
                        shape[x, y, z - 1] = new Vox(0);
                        shape[x, y, z] = null;
                    }
                    if ((block != null) && block.type == 1)
                    {
                        shape[x, y, z] = new Vox(0);
                    }
                }
            }
        }
    }
    static void MoveBlocksByOneX(Vox[,,] shape)
    {
        // Move all blocks that can move by one unit along the x axis
        for (int z = 0; z < shape.GetLength(2); z++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                for (int x = 0; x < shape.GetLength(0); x++)
                {
                    Vox block = shape[x, y, z];
                    if ((block != null) && (block.type == 2 || block.type == 3))
                    {
                        shape[x - 1, y, z] = new Vox(0);
                        shape[x, y, z] = null;
                    }
                    if ((block != null) && block.type == 1)
                    {
                        shape[x, y, z] = new Vox(0);
                    }
                }
            }
        }
    }
    static void MoveBlocksByOneY(Vox[,,] shape)
    {
        // Move all blocks that can move by one unit along the y axis
        for (int z = 0; z < shape.GetLength(2); z++)
        {
            for (int x = 0; x < shape.GetLength(0); x++)
            {
                for (int y = 0; y < shape.GetLength(1); y++)
                {
                    Vox block = shape[x, y, z];
                    if ((block != null) && (block.type == 2 || block.type == 3))
                    {
                        shape[x, y - 1, z] = new Vox(0);
                        shape[x, y, z] = null;
                    }
                    if ((block != null) && block.type == 1)
                    {
                        shape[x, y, z] = new Vox(0);
                    }
                }
            }
        }
    }

    static bool EmptyBesideZ(Vox[,,] shape, Tuple<int, int, int> coords)
    {
        // Check if there's an empty block between the coords and the same x,y coords at z = 0
        int x = coords.Item1;
        int y = coords.Item2;
        for (int z = 0; z < coords.Item3; z++)
        {
            if (shape[x, y, z] == null)
            {
                return true;
            }
        }
        return false;
    }
    static bool EmptyBesideX(Vox[,,] shape, Tuple<int, int, int> coords)
    {
        // Check if there's an empty block between the coords and the same z,y coords at x = 0
        int z = coords.Item3;
        int y = coords.Item2;
        for (int x = 0; x < coords.Item1; x++)
        {
            if (shape[x, y, z] == null)
            {
                return true;
            }
        }
        return false;
    }
    static bool EmptyBesideY(Vox[,,] shape, Tuple<int, int, int> coords)
    {
        // Check if there's an empty block between the coords and the same x,z coords at y = 0
        int x = coords.Item1;
        int z = coords.Item3;
        for (int y = 0; y < coords.Item2; y++)
        {
            if (shape[x, y, z] == null)
            {
                return true;
            }
        }
        return false;
    }

    static void MarkPillarsZ(Vox[,,] shape)
    {
        // Mark the z pillars (blocks unshiftable on z axis) as type 1
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (shape[x, y, 0] != null)
                {
                    for (int z = 0; z < shape.GetLength(2); z++)
                    {
                        Vox block = shape[x, y, z];
                        if (block != null)
                        {
                            shape[x, y, z].type = 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
    static void MarkPillarsX(Vox[,,] shape)
    {
        // Mark the x pillars (blocks unshiftable on x axis) as type 1
        for (int z = 0; z < shape.GetLength(2); z++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                if (shape[0, y, z] != null)
                {
                    for (int x = 0; x < shape.GetLength(0); x++)
                    {
                        Vox block = shape[x, y, z];
                        if (block != null)
                        {
                            shape[x, y, z].type = 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
    static void MarkPillarsY(Vox[,,] shape)
    {
        // Mark the y pillars (blocks unshiftable on y axis) as type 1
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int z = 0; z < shape.GetLength(2); z++)
            {
                if (shape[x, 0, z] != null)
                {
                    for (int y = 0; y < shape.GetLength(1); y++)
                    {
                        Vox block = shape[x, y, z];
                        if (block != null)
                        {
                            shape[x, y, z].type = 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------

    //-----------------General Helper Functions-----------------------------------------------
    static void ResetTypes(Vox[,,] shape)
    {
        // Reset the type of all blocks to 0 (untyped)
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                for (int z = 0; z < shape.GetLength(2); z++)
                {
                    Vox block = shape[x, y, z];
                    if (block != null)
                    {
                        block.type = 0;
                    }
                }
            }
        }
    }

    static bool CheckForSpace(Vox[,,] shape, Tuple<int, int, int> coords, string axis)
    {
        // Check for empty space along the given axis to allow shifting along said axis
        if (axis == "x")
        {
            return EmptyBesideX(shape, coords);
        }
        if (axis == "y")
        {
            return EmptyBesideY(shape, coords);
        }
        if (axis == "z")
        {
            return EmptyBesideZ(shape, coords);
        }
        return false;
    }

    static void MarkType1(Vox[,,] shape, string axis)
    {
        // Mark the "pillar" (type 1) immovable blocks along an axis
        if (axis == "x")
        {
            MarkPillarsX(shape);
        }
        if (axis == "y")
        {
            MarkPillarsY(shape);
        }
        if (axis == "z")
        {
            MarkPillarsZ(shape);
        }
    }

    static bool TouchingType1(Vox[,,] shape, Tuple<int, int, int> coords, string axis)
    {
        // Check if the block at coords is touching a type1 block and has space to move
        int x = coords.Item1;
        int y = coords.Item2;
        int z = coords.Item3;

        if ((axis == "x" || axis == "y") && (z == 0) && (shape[x, y, z].type != 1))
        {
            return true; // The ground counts as Type 1 (unmoving) when shifting a horizontal axis.
        }

        bool left = false;
        bool right = false;
        bool front = false;
        bool back = false;
        bool up = false;
        bool down = false;

        if (x + 1 < shape.GetLength(0))
        {
            left = (shape[x + 1, y, z] != null) && (shape[x + 1, y, z].type == 1);
        }
        if (x - 1 >= 0)
        {
            right = (shape[x - 1, y, z] != null) && (shape[x - 1, y, z].type == 1);
        }
        if (y + 1 < shape.GetLength(1))
        {
            front = (shape[x, y + 1, z] != null) && (shape[x, y + 1, z].type == 1);
        }
        if (y - 1 >= 0)
        {
            back = (shape[x, y - 1, z] != null) && (shape[x, y - 1, z].type == 1);
        }
        if (z + 1 < shape.GetLength(2))
        {
            up = (shape[x, y, z + 1] != null) && (shape[x, y, z + 1].type == 1);
        }
        if (z - 1 >= 0)
        {
            down = (shape[x, y, z - 1] != null) && (shape[x, y, z - 1].type == 1);
        }

        bool touching = false;
        if (axis == "z")
        {
            touching = (left || right || front || back);
        }
        if (axis == "x")
        {
            touching = (up || down || front || back);
        }
        if (axis == "y")
        {
            touching = (left || right || up || down);
        }

        if (touching && CheckForSpace(shape, Tuple.Create(x, y, z), axis))
        {
            return true;
        }
        return false;
    }


    static bool MarkType2(Vox[,,] shape, string axis)
    {
        // Mark blocks (type 2) that shift against non-moving blocks (type 1) and return true if any are found
        bool found = false;
        for (int x = 0; x < shape.GetLength(0); x++)
        {
            for (int y = 0; y < shape.GetLength(1); y++)
            {
                for (int z = 0; z < shape.GetLength(2); z++)
                {
                    Vox block = shape[x, y, z];
                    if ((block != null) && TouchingType1(shape, Tuple.Create(x, y, z), axis))
                    {
                        block.type = 2;
                        found = true;
                    }
                }
            }
        }
        return found;
    }

    static bool TouchingType2Or3(Vox[,,] shape, Tuple<int, int, int> coords, string axis)
    {
        // Check if the block at coords is touching a type2 or type3 block and has space to move
        int x = coords.Item1;
        int y = coords.Item2;
        int z = coords.Item3;

        bool left = false;
        bool right = false;
        bool front = false;
        bool back = false;
        bool up = false;
        bool down = false;

        if (x + 1 < shape.GetLength(0))
        {
            left = shape[x + 1, y, z] != null;
            left = left && ((shape[x + 1, y, z].type == 2) || (shape[x + 1, y, z].type == 3));
        }
        if (x - 1 >= 0)
        {
            right = shape[x - 1, y, z] != null;
            right = right && ((shape[x - 1, y, z].type == 2) || (shape[x - 1, y, z].type == 3));
        }
        if (y + 1 < shape.GetLength(1))
        {
            front = shape[x, y + 1, z] != null;
            front = front && ((shape[x, y + 1, z].type == 2) || (shape[x, y + 1, z].type == 3));
        }
        if (y - 1 >= 0)
        {
            back = shape[x, y - 1, z] != null;
            back = back && ((shape[x, y - 1, z].type == 2) || (shape[x, y - 1, z].type == 3));
        }
        if (z + 1 < shape.GetLength(2))
        {
            up = shape[x, y, z + 1] != null;
            up = up && ((shape[x, y, z + 1].type == 2) || (shape[x, y, z + 1].type == 3));
        }
        if (z - 1 >= 0)
        {
            down = shape[x, y, z - 1] != null;
            down = down && ((shape[x, y, z - 1].type == 2) || (shape[x, y, z - 1].type == 3));
        }

        bool touching = (left || right || up || down || front || back);

        if (touching && CheckForSpace(shape, Tuple.Create(x, y, z), axis))
        {
            return true;
        }
        return false;
    }

    static void MarkType3(Vox[,,] shape, string axis)
    {
        //Mark blocks (type3) directly connected to shifting blocks (type2 or type3)
        bool changed = true;
        while (changed)
        {
            changed = false;
            for (int x = 0; x < shape.GetLength(0); x++)
            {
                for (int y = 0; y < shape.GetLength(1); y++)
                {
                    for (int z = 0; z < shape.GetLength(2); z++)
                    {
                        Vox block = shape[x, y, z];
                        bool unmarked = (block != null) && (block.type != 2) && (block.type != 3);
                        if (unmarked && TouchingType2Or3(shape, Tuple.Create(x, y, z), axis))
                        {
                            block.type = 3;
                            changed = true;
                        }
                    }
                }
            }
        }
    }

    static void MoveBlocks(Vox[,,] shape, string axis)
    {
        // Move all possible blocks one unit along an axis towards the origin
        if (axis == "x")
        {
            MoveBlocksByOneX(shape);
        }
        if (axis == "y")
        {
            MoveBlocksByOneY(shape);
        }
        if (axis == "z")
        {
            MoveBlocksByOneZ(shape);
        }
    }
    //----------------------------------------------------------------------------------------

    //------------------Main Algorithm--------------------------------------------------------
    static void Squeeze(Vox[,,] shape, Moves moves, string axis)
    {
        // Squeeze blocks together along an axis as much as possible, recording moves along the way
        while (true)
        {
            MarkType1(shape, axis);


            bool foundType2 = false;
            foundType2 = MarkType2(shape, axis);
            if (!foundType2)
            {
                break;
            }

            MarkType3(shape, axis);

            moves.RecordMoves(shape, axis);
            MoveBlocks(shape, axis);
        }
        ResetTypes(shape);
    }

    static void SqueezeToOrigin(Vox[,,] shape, Moves moves)
    {
        Squeeze(shape, moves, "z"); //The entire algorithm assumes that z is vertical, and that it gets squeezed first.
        Squeeze(shape, moves, "y");
        Squeeze(shape, moves, "x");

        moves.SetOrder(Tuple.Create("z", "y", "x"));
        foreach (Vox block in shape)
        {
            if (block != null)
            {
                block.type = 3;
            }
        }
        moves.RecordMoves(shape, "start");
        ResetTypes(shape);
    }

    static Moves DeconstructShape(Vox[,,] shape)
    {
        // Deconstruct a shape and return the moves it takes to reconstruct it
        Moves moves = new Moves();
        SqueezeToOrigin(shape, moves);
        moves.Reverse();
        return moves;
    }
    //----------------------------------------------------------------------------------------

    /** example how to use
        static void Main(string[] args) {
            Vox[,,] chair = {{
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {new Vox(0), new Vox(0), null, null},
                {null, new Vox(0), null, null},
                {new Vox(0), new Vox(0), new Vox(0), new Vox(0)}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, new Vox(0), null, null},
                {null, new Vox(0), null, null},
                {null, new Vox(0), null, new Vox(0)}},

                {{null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {new Vox(0), new Vox(0), null, null},
                {null, new Vox(0), null, null},
                {new Vox(0), new Vox(0), new Vox(0), new Vox(0)}}};

            Moves moves = DeconstructShape(chair);

            Console.WriteLine(moves.ToString());
        }
    **/

    public Vox[,,] ConvertGridToVoxels(GridNode[,,] grid)
    {
        int maxX = 0;
        int maxY = 0;
        int maxZ = 0;

        foreach (GridNode node in grid)
        {
            int x = node.localPos.x;
            int y = node.localPos.z;
            int z = node.localPos.y; // z is vertical in the compression algorithm
            if (x > maxX)
            {
                maxX = x;
            }
            if (y > maxY)
            {
                maxY = y;
            }
            if (z > maxZ)
            {
                maxZ = z;
            }
        }

        Vox[,,] voxels = new Vox[maxX + 1, maxY + 1, maxZ + 1];

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                for (int z = 0; z <= maxZ; z++)
                {
                    voxels[x, y, z] = null;
                }
            }
        }
        foreach (GridNode node in grid)
        {
            int x = node.localPos.x;
            int y = node.localPos.z;
            int z = node.localPos.y; // z is vertical in the compression algorithm
            if (z > 0 && node.isTaken)
            {
                voxels[x, y, z - 1] = new Vox(0); // ignore floor of blocks
            }
        }

        return voxels;
    }

    public void Compress()
    {
        if (gridMain.grid != null)
        {
            Vox[,,] shape = ConvertGridToVoxels(gridMain.grid);
            Moves moves = DeconstructShape(shape);
            this.moves = moves;
            Debug.Log(moves.ToString());

            GetComponent<GridMovement>().BeginMovement(moves);
        }
    }
}
