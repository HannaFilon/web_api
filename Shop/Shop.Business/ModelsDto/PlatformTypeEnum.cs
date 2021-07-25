using System;

namespace Shop.Business.ModelsDto
{
    [Flags]
    public enum PlatformTypeEnum : short
    {
        PC = 1,
        XBox = 2,
        PlayStation = 4,
        Phone = 8,
        Switch = 16
    }
}