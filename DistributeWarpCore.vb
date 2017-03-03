// Basis Trortuga 1881458
// Basis M 1451416
// Basis Despot 1453666

// Flotte Adrecs 141405
// Flotte Bandaris 141455

DistributeWarpCore(141405, 1451416); 
Function DistributeWarpCore(FleedID As Integer, DockShipID As Integer){
  Var Fleet As New CMyFleet(FleedID);
  Var SummWarpCore As Double = 0;
  Var AverageWarpCore As Double = 0;
  
  Var Ship As CMyShip;
  For(Each Ship In Fleet.Ships){
    SummWarpCore = SummWarpCore + Ship.WarpCore;
    If(DockShipID > 0){
      If(Ship.Docked){
        Ship.Action.Undock();
      }
    }
    If(Ship.WarpCoreIsActive = False){
      Ship.Action.ActivateWarpCore(True);
    }
  }
  AverageWarpCore = SummWarpCore / Fleet.Ships.Count;
  WriteLine("SummWarpCore=" & SummWarpCore);
  WriteLine("AverageWarpCore=" & AverageWarpCore);
  
  Var Dealer As CMyShip;
  Var Acceptor As CMyShip;
  For(Each Dealer In Fleet.Ships){
    If(Dealer.WarpCore > AverageWarpCore){
      For(Each Acceptor In Fleet.Ships){
        If(Acceptor.WarpCore + 1 < AverageWarpCore and Acceptor.WarpCore + 1 < Acceptor.Definition.WarpCore){ 
          If(Dealer.WarpCore > AverageWarpCore){
            Var Amount As Integer = 0;
            Var AvaiableAmount As Integer = 0;
            If(Dealer.WarpCore > AverageWarpCore){
              Var MinWK As Integer = Dealer.WarpCore - (Dealer.Definition.WarpCore / 10);
              AvaiableAmount = Dealer.WarpCore - MinWK;
            }
            Var NeededAmount As Integer = AverageWarpCore - Acceptor.WarpCore;
            If(AvaiableAmount > NeededAmount){
              Amount = NeededAmount;
            } Else {
              Amount = AvaiableAmount;
            }
            If(Amount >= 5){
              If(DockShipID > 0){
                SecureDock(Dealer, DockShipID);
                SecureDock(Acceptor, DockShipID);
                If(Dealer.DockedToShipID = DockShipID AND Acceptor.DockedToShipID = DockShipID){ 
                  //WriteLine("Dealer " & Dealer.GetNameText() & " schickt " & Amount & " Warpkern zu " & Acceptor.GetNameText());
                  Dealer.Action.TransferToShip(Acceptor.ShipID, Amount, EBeamResource.Warpcore);
                  Acceptor.Action.Undock();
                } Else {
                  WriteLine("Andocken war nicht möglich");
                  Exit;
                }
              } Else {
                Dealer.Action.TransferToShip(Acceptor.ShipID, Amount, EBeamResource.Warpcore);
              }
            }
          }
        }
      }
    }
    If(DockShipID > 0){
      Dealer.Action.Undock();
    }
  }
}

Function SecureDock(Ship As CMyShip, Target As Integer){
  If(Ship.ShieldsActive){
    Ship.Action.ActivateShields(False);
  }
  If(Ship.Docked = True){
    If(Ship.DockedToShipID = Target){
    } Else {
      Ship.Action.Undock();
    }
  }
  If(Ship.Docked = False){
    Ship.Action.DockTo(Target);
  }
}
