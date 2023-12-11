using System;
using System.Collections.Generic;

public interface IStructur
{
    int Income { get; }

    void TakeDamage(int damage);
    void Destruct();
    void InitializeStruct(DataStructure dataStructure, string name);
}
