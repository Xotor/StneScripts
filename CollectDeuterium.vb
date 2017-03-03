Var SpaceStationID As Integer = 1940125;
Var ColonyID As Integer = 35662;

Var fleet1 As New CMyFleet(141404);
Var fleet2 As New CMyFleet(142487);
Var fleet3 As New CMyFleet(147503);

Var colShip1 As New CMyShip(1451134); // Meins
ProcessShip(colShip1);

Var colShip2 As New CMyShip(1683086); // Casa
ProcessShip(colShip2);

Var colShip3 As New CMyShip(1935448); // Gora
ProcessShip(colShip3);

Var aShip As CMyShip;

For(Each aShip In fleet1.Ships)
{ 
  ProcessShip(aShip);
}
For(Each aShip In fleet2.Ships)
{ 
  ProcessShip(aShip);
}
For(Each aShip In fleet3.Ships)
{ 
  ProcessShip(aShip);
}

WriteLine("ENDSCRIPT");

Function ProcessDeuteriumNebula(ship As CMyShip)
{
  If(ship.Energy > 8 AND (ship.Definition.BussardCollectorCapacity - ship.BussardCollectorHeating) > 0)
  {
    ship.Action.CollectDeuterium(ship.Energy - 8);
  } 
  If(ship.StockRoom.Amount(EGoodsType.Deuterium) > 0)
  {
    ship.Action.RefillWarpCore(1000, EWarpcoreFillType.deuterium):
    
  }
  If(ship.Energy > 8 AND (ship.Definition.BussardCollectorCapacity - ship.BussardCollectorHeating) > 0)
  {
    ship.Action.CollectDeuterium(ship.Energy - 8);
  } 
  If(ship.StockRoom.Amount(EGoodsType.Deuterium) > 0)
  {
    ship.Action.RefillWarpCore(1000, EWarpcoreFillType.deuterium):
    
  }
}

Function ProcessShip(ship As CMyShip)
{
  If(ship.MapPosition.X = 32 AND ship.MapPosition.Y = 367)
  {
    ProcessDeuteriumNebula(ship);
    ship.Action.FlyTo("@34|371");
    ship.Action.DockTo(SpaceStationID);
    ship.Action.TransferToColony(ColonyID, 1000, EBeamResource.Deuterium);
    ship.Action.Undock();
  }
  Else {
    If(ship.MapPosition.X = 34 AND ship.MapPosition.Y = 371 AND ship.MapPosition.InOrbit = True)
    {
      If(ship.StockRoom.Amount(EGoodsType.Deuterium) > 0)
      {
        ship.Action.RefillWarpCore(1000, EWarpcoreFillType.deuterium):
        If(ship.Docked = False)
        {
          ship.Action.DockTo(SpaceStationID);
        }
        ship.Action.TransferToColony(ColonyID, 1000, EBeamResource.Deuterium);
      } 
      Var heatingDiff As Integer = ship.Definition.BussardCollectorCapacity - ship.BussardCollectorHeating 
      //WriteLine(ship.GetNameText());
      //WriteLine("EPS: " & ship.Eps); 
      //WriteLine("Energy: " & ship.Energy);
      //WriteLine("Heating: " & ship.BussardCollectorHeating);
      //WriteLine("MaxHeating: " & ship.Definition.BussardCollectorCapacity);
      //WriteLine("HeatingDiff: " & heatingDiff);
      If(ship.StockRoom.Amount(EGoodsType.Deuterium) <= 0)
      {
        If(ship.Eps > 30 AND ship.Energy > 30 AND heatingDiff > 32)
        { 
          ship.Action.SetAlertLevel(EAlertLevel.Yellow)
          ship.Action.FlyTo("32|367");
          ProcessDeuteriumNebula(ship);
          ship.Action.FlyTo("@34|371");
          ship.Action.DockTo(SpaceStationID);
          ship.Action.TransferToColony(ColonyID, 1000, EBeamResource.Deuterium);
          ship.Action.Undock();
        }
      }
    } 
    Else
    {
      WriteLine(ship.GetNameText() & " is not in Orbit of 34|371 and not in the Nebula 32|367");
    }
  }
}
