/*
 * This script fills the warpcore of a station with deuterium provided from the colony below.
 * MinDeutOnColony specifys the remainder of deuterium to be lef ton the colony.
 */
Var MinDeutOnColony As Integer = 100;
Var MinAntiOnColony As Integer = 0;
Var MinDiliOnColony As Integer = 0;


Var SpaceStationID As Integer = 795987;
Var ColonyID As Integer = 21055;

Var Station As New CMyShip(SpaceStationID);
Var Colony As New CMyColony(ColonyID);

WriteLine(Colony.Name & " has " & Colony.StockRoom.Amount(EGoodsType.Deuterium) & " Deuterium");
WriteLine(Colony.Name & " has " & Colony.StockRoom.Amount(EGoodsType.Antimatter) & " Antimatter");
WriteLine(Colony.Name & " has " & Colony.StockRoom.Amount(EGoodsType.Dilithium) & " Dilithium");
WriteLine(Station.Name & " has " & Station.StockRoom.Amount(EGoodsType.Deuterium) & " Deuterium");
WriteLine(Station.Name & " has " & Station.StockRoom.Amount(EGoodsType.Antimatter) & " Antimatter");
WriteLine(Station.Name & " has " & Station.StockRoom.Amount(EGoodsType.Dilithium) & " Dilithium");

If(needsWarpCore(Station)){
  Station.Action.RefillWarpCore(Station.Definition.WarpCore, EWarpcoreFillType.dilliAmDeut):
}

// While(hasResources(Colony) AND needsWarpCore(Station)){
If(hasResources(Colony) AND needsWarpCore(Station)){
  Var deuteriumAmount As Integer = Colony.StockRoom.Amount(EGoodsType.Deuterium) - MinDeutOnColony;
  Var antimatterAmount As Integer = Colony.StockRoom.Amount(EGoodsType.Antimatter) - MinAntiOnColony;
  Var dilithiumAmount As Integer = Colony.StockRoom.Amount(EGoodsType.Dilithium) - MinDiliOnColony;
  WriteLine("deuteriumAmount: " & deuteriumAmount);
  WriteLine("antimatterAmount: " & antimatterAmount);
  WriteLine("dilithiumAmount: " & dilithiumAmount);
  
  Var minDeutAm As Integer = Math.Abs(Math.Min(deuteriumAmount, antimatterAmount) / 2);
  Var minResources As Integer = Math.Min(dilithiumAmount, minDeutAm);
  Var minFreeStorage As Integer = Math.Abs(Station.StockRoom.FreeStorage / 5);
  Var minAll As Integer = Math.Min(minResources, minFreeStorage);
  
  WriteLine("Minimum between Deuterium and Antimatter: " & minDeutAm);
  WriteLine("Minimum Dilithium: " & dilithiumAmount);
  WriteLine("Minimum Resources: " & minResources);
  WriteLine("FreeStorage: " & minFreeStorage);
  WriteLine("Minimum: " & minAll);
  
  Station.Action.TransferFromColony(ColonyID, minAll * 2, EBeamResource.Deuterium);
  Station.Action.TransferFromColony(ColonyID, minAll * 2, EBeamResource.Antimatter);
  Station.Action.TransferFromColony(ColonyID, minAll, EBeamResource.Dilithium);
  
  Station.Action.RefillWarpCore(minAll * 50, EWarpcoreFillType.dilliAmDeut):

}

WriteLine(Station.Name & " has " & Station.WarpCore & " warpcore");

Function needsWarpCore(sta As CMyShip) As Boolean {
  Return (Station.Definition.WarpCore - Station.WarpCore) >= 50;
}


Function hasResources(col As CMyColony) As Boolean {
  If(Colony.StockRoom.Amount(EGoodsType.Deuterium) <= MinDeutOnColony) {
    Return False;
  }
  If(Colony.StockRoom.Amount(EGoodsType.Antimatter) <= MinAntiOnColony) {
    Return False;
  }
  If(Colony.StockRoom.Amount(EGoodsType.Dilithium) <= MinDiliOnColony) {
    Return False;
  }
  Return True;
}
