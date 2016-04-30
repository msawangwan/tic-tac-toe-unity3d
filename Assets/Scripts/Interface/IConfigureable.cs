﻿using UnityEngine;
using System.Collections.Generic;

public interface IConfigureable {
    List<IConfig> ObjectConfigData { get; }
    List<IConfig> Configure ( );
}

public interface IConfig { }
