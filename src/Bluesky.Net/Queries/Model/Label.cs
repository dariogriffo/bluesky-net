namespace Bluesky.Net.Queries.Model;

using Models;
using System;

public record Label(Did Src, string Uri, string Cid, string Val, bool Neg, DateTime Cts){}
