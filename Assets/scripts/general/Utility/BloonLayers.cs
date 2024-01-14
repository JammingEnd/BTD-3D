using System.Collections.Generic;

public enum LayerNames
{
    Red,
    Blue,
    Green,
    Yellow,
    Pink,
    White,
    Black,
    Lead,
    Purple,
    Rainbow,
    Ceramic,
    Moab,
    BFB,
    Zomg,
    DDT,
    BAD
}
public class BloonLayers
{
    public Dictionary<LayerNames, int> LayerDef = new Dictionary<LayerNames, int>()
    {
        { LayerNames.Red, 1 },
        { LayerNames.Blue, 2 },
        { LayerNames.Green, 3 },
        //etc 
    };
}