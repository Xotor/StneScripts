/*
 * This script fills the warpcore of a station with deuterium provided from the colony below.
 * MinDeutOnColony specifys the remainder of deuterium to be lef ton the colony.
 */
Var MinDeutOnColony As Integer = 4000;

Var SpaceStationID As Integer = 1940125;
Var ColonyID As Integer = 35662;

Var Station As New CMyShip(SpaceStationID);
Var Colony As New CMyColony(ColonyID);

WriteLine(Colony.Name & " has " & Colony.StockRoom.Amount(EGoodsType.Deuterium) & " Deuterium");
WriteLine(Station.Name & " has " & Station.StockRoom.Amount(EGoodsType.Deuterium) & " Deuterium");

If(Station.StockRoom.Amount(EGoodsType.Deuterium) > 0 AND (Station.Definition.WarpCore - Station.WarpCore) > 1){
  Station.Action.RefillWarpCore(1000, EWarpcoreFillType.deuterium):
}

While(Colony.StockRoom.Amount(EGoodsType.Deuterium) > MinDeutOnColony AND Station.StockRoom.FreeStorage() > 0 AND (Station.Definition.WarpCore - Station.WarpCore) > 1){
  Var Amount As Integer = Colony.StockRoom.Amount(EGoodsType.Deuterium) - MinDeutOnColony;
  Station.Action.TransferFromColony(ColonyID, Amount, EBeamResource.Deuterium);
  Station.Action.RefillWarpCore(Amount, EWarpcoreFillType.deuterium):
}
