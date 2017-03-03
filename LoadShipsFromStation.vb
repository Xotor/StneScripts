/*
 * This script .
 */
Var StationMinWarpCore As Integer = 4000;
Var SpaceStationID As Integer = 1940125;
Var AllID As Integer = 20935;
Var Station As New CMyShip(SpaceStationID);
Var All As New CMyFleet(AllID);

WriteLine(Station.Name & " has " & Station.WarpCore & " warpcore");

Var ship As CMyShip;
For(Each ship In All.Ships){
  If(ship.MapPosition.X = Station.MapPosition.X AND ship.MapPosition.Y = Station.MapPosition.Y AND ship.MapPosition.InOrbit = Station.MapPosition.InOrbit){
    //If(ship.Type <> EShipType.Adrec AND ship.Type <> EShipType.Bandari AND ship.Type <> EShipType.Tanker AND ship.Definition.IsSpaceStation = False){
    If(ship.Definition.IsSpaceStation = False){
      If(Station.WarpCore > StationMinWarpCore AND Station.FreeDockingPorts(ship) AND ship.MainComputerIsActive AND (ship.Definition.WarpCore - ship.WarpCore) > 0){
        // WriteLine(ship.Name & " " & ship.Type);
        If(ship.Docked = True AND ship.DockedToShipID <> Station.ShipID){
          ship.Action.Undock();
        }
        If(ship.Docked = False){
          ship.Action.DockTo(Station.ShipID);
        }
        If(ship.Docked){
          Var Amount As Double = (ship.Definition.WarpCore - ship.WarpCore);
          ship.Action.TransferFromShip(Station.ShipID, Amount, EBeamResource.Warpcore);
          ship.Action.Undock();
        } Else {
          WriteLine("Failed to Dock " & ship.Name & " to the Station");
        }
        
      } Else {
        If((ship.Definition.WarpCore - ship.WarpCore) > 0){
          If(Station.WarpCore <= StationMinWarpCore){
            WriteLine("*** The station has just " & Station.WarpCore & " warpcore left");
          }
          If(Station.FreeDockingPorts(ship) = False){
            WriteLine("*** No free docking ports available!");
          }
          If(ship.MainComputerIsActive = False){
            WriteLine("**** Main Computer of " & ship.Name & " is offline");
          }
        }
      }
    }
  }
}
