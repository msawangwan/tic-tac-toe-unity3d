using UnityEngine;
using System.Collections.Generic;

public class GameConfiguration : IConfigureable {
    public List<IConfig> ObjectConfigData { get; private set; }

    public GameConfiguration() {

    }

    // implements IConfigureable
    public List<IConfig> Configure () {
        return ObjectConfigData;
    }
}

public class GameObjectData : IConfig {

}