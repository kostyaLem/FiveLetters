using FiveLetters.UI.Models;
using System.Collections.Generic;

namespace FiveLetters.UI.Services.Interfaces;

internal interface IGameState
{
    public IReadOnlyList<RequestedWord> Attempts { get; set; }
}
