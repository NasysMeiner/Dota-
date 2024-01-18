using System;
using System.Collections.Generic;

public interface IStructur
{
    int Income { get; }

    void Destruct();
    void InitializeStruct(DataStructure dataStructure, string name);
}
