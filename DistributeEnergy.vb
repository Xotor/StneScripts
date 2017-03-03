DistributeEnergy(141455, 0, 2);

Function DistributeEnergy(FleedID As Integer, DockShipID As Integer, MinEnergy As Integer){
  Var Fleet As New CMyFleet(FleedID);
  Var SummEnergy As Double = 0;
  Var AverageEnergy As Double = 0;
  
  Var Ship As CMyShip;
  For(Each Ship In Fleet.Ships){
    SummEnergy = SummEnergy + Ship.Energy;
    If(DockShipID > 0){
      If(Ship.Docked){
        Ship.Action.Undock();
      }
    }
  }
  AverageEnergy = SummEnergy / Fleet.Ships.Count;
  WriteLine("SummEnergy =" & SummEnergy);
  WriteLine("AverageEnergy =" & AverageEnergy);
  
  Var Dealer As CMyShip;
  Var Acceptor As CMyShip;
  For(Each Dealer In Fleet.Ships){
    If(Dealer.Energy > AverageEnergy){
      For(Each Acceptor In Fleet.Ships){
        If(Acceptor.Energy + 1 < AverageEnergy and Acceptor.Energy + 1 < Acceptor.Definition.Energy){ 
          If(Dealer.Energy > AverageEnergy){
            Var Amount As Integer = 0;
            Var AvaiableAmount As Integer = 0;
            If(Dealer.Energy > AverageEnergy){
              AvaiableAmount = Dealer.Energy - AverageEnergy;
            }
            Var NeededAmount As Integer = AverageEnergy - Acceptor.Energy;
            If(AvaiableAmount > NeededAmount){
              Amount = NeededAmount;
            } Else {
              Amount = AvaiableAmount;
            }
            If(Amount >= MinEnergy){
              If(DockShipID > 0){
                SecureDock(Dealer, DockShipID);
                SecureDock(Acceptor, DockShipID);
                If(Dealer.DockedToShipID = DockShipID AND Acceptor.DockedToShipID = DockShipID){ 
                  //WriteLine("Dealer " & Dealer.Name & " schickt " & Amount & " Energie zu " & Acceptor.Name);
                  Dealer.Action.TransferToShip(Acceptor.ShipID, Amount, EBeamResource.Energy);
                  Acceptor.Action.Undock();
                } Else {
                  WriteLine("Andocken war nicht möglich");
                  Exit;
                }
              } Else {
                Dealer.Action.TransferToShip(Acceptor.ShipID, Amount, EBeamResource.Energy);
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
