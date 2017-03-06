/*
 * This script .
 */
Var StationMinWarpCore As Integer = 4000;
Var SpaceStationID As Integer = 1940125;
Var AllID As Integer = 20935;
Var station As New CMyShip(SpaceStationID);
Var All As New CMyFleet(AllID);

WriteLine(station.Name & " has " & station.WarpCore & " warpcore");



Var ship As CMyShip;
For(Each ship In All.Ships) {
  If(canShipBeLoaded(ship, station)) { 
    If(ship.Docked = True AND ship.DockedToShipID <> station.ShipID) {
      ship.Action.Undock();
    }
    If(ship.Docked = False){
      ship.Action.DockTo(station.ShipID);
    }
    If(ship.Docked){
      Var Amount As Double = (ship.Definition.WarpCore - ship.WarpCore);
      ship.Action.TransferFromShip(station.ShipID, Amount, EBeamResource.Warpcore);
      ship.Action.Undock();
    } Else {
      WriteLine("Failed to Dock " & ship.Name & " to the station");
    } 
  }
}

Function isPositionEquals(a As CMyShip, b As CMyShip) As Boolean {
  If(a.MapPosition.X <> b.MapPosition.X) {
    Return False;
  }
  If(a.MapPosition.Y <> b.MapPosition.Y) {
    Return False;
  }
  If(a.MapPosition.InOrbit <> b.MapPosition.InOrbit) {
    Return False;
  }
  If(a.MapPosition.MapID <> b.MapPosition.MapID) {
    WriteLine("MapID is not equals!!");
    Return False;
  }
  If(a.MapPosition.MapInstanceID <> b.MapPosition.MapInstanceID) {
    WriteLine("MapInstanceID is not equals!!");
    Return False;
  }
  Return True;
}

Function canShipBeLoaded(aship As CMyShip, astation As CMyShip) As Boolean {
  If( not isPositionEquals(aship, astation)) {
    Return False;
  }
  If(aship.ShipID = astation.ShipID) {
    Return False;
  }
  If(astation.WarpCore <= StationMinWarpCore) {
    WriteLine("*** The station has just " & station.WarpCore & " warpcore left");
    Return False;
  }
  If( not astation.FreeDockingPorts(aship)) {
    WriteLine("*** No free docking ports on " & astation.Name & " available!");
    Return False;
  }
  If( not astation.MainComputerIsActive) {4
    WriteLine("**** Main Computer of " & astation.Name & " is offlin");
    Return False;
  }
  If( not aship.MainComputerIsActive) {
    WriteLine("**** Main Computer of " & aship.Name & " is offline");
    Return False;
  }
  If((aship.Definition.WarpCore - aship.WarpCore) <= 0){
    Return False;
  }
  Return True;
}


