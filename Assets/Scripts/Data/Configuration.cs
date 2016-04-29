using UnityEngine;
using System.Collections.Generic;

public class Configuration {
    public Dictionary<object,object> configData { get; private set; }

    public Configuration( Dictionary<object , object> configData ) {
        this.configData = configData;
    }
}
