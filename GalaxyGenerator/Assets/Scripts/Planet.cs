using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet  {

    public string planetName { get; set; }
    public string planetType { get; set; }

    public Planet(string planetName, string planetType)
    {
        this.planetName = planetName;
        this.planetType = planetType;
    }
}
