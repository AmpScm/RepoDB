﻿using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RepoDb.Contexts.Execution;

/// <summary>
/// 
/// </summary>
internal class UpdateExecutionContext
{
    /// <summary>
    /// The execution command text.
    /// </summary>
    public string CommandText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<DbField> InputFields { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Action<DbCommand, object> ParametersSetterFunc { get; set; }
}
