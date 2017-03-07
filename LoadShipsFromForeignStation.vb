/*
 * This script does not work like that!.
 */
Var InOrbit As Boolean = True;
Var PosX As Integer = 115;
Var PosY As Integer = 526;
Var SpaceStationID As Integer = 795987;
Var AllID As Integer = 30;
Var All As New CMyFleet(AllID);

// WriteLine(station.Name & " has " & station.WarpCore & " warpcore");

Var ship As CMyShip;
For(Each ship In All.Ships) {
  If(canShipBeLoaded(ship)) { 
    If(ship.Docked = True AND ship.DockedToShipID <> SpaceStationID) {
      ship.Action.Undock();
    }
    If(ship.Docked = False){
      ship.Action.DockTo(SpaceStationID);
    }
    If(ship.Docked){
      Var Amount As Double = (ship.Definition.WarpCore - ship.WarpCore);
      ship.Action.TransferFromShip(SpaceStationID, Amount, EBeamResource.Warpcore);
      ship.Action.Undock();
    } Else {
      WriteLine("Failed to Dock " & ship.Name & " to the station");
    } 
  }
}

Function canShipBeLoaded(aship As CMyShip) As Boolean {
  If(aship.MapPosition.X <> PosX) {
    Return False;
  }
  If(aship.MapPosition.Y <> PosY) {
    Return False;
  }
  If(aship.MapPosition.InOrbit <> InOrbit) {
    Return False;
  }
  If(not aship.MainComputerIsActive) {
    WriteLine("**** Main Computer of " & aship.Name & " is offline");
    Return False;
  }
  If((aship.Definition.WarpCore - aship.WarpCore) <= 0){
    Return False;
  }
  Return True;
}


