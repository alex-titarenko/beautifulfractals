using System;

namespace TAlex.BeautifulFractals.KeyGenerator
{
    /// <summary>
    /// Possible key generator exit codes.
    /// </summary>
    public enum ReturnCode : int
    {
        // Success
        ERC_SUCCESS = 00,
        ERC_SUCCESS_BIN = 01,
        // Failure
        ERC_ERROR = 10,
        ERC_MEMORY = 11,
        ERC_FILE_IO = 12,
        ERC_BAD_ARGS = 13,
        ERC_BAD_INPUT = 14,
        ERC_EXPIRED = 15,
        ERC_INTERNAL = 16
    }
}
