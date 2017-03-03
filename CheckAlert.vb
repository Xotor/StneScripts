Var MyShip As New CMyShip(1940125);
Var Self As New CUser(MyShip.UserID);

WriteLine("MyShip: " & MyShip.GetNameTextAndID());
WriteLine("Self: " & Self.NameAndID);

WriteLine("MyShip.LRSIsActive=" & MyShip.LRSIsActive);
WriteLine("MyShip.LRSShipSlots=" & MyShip.LRSShipSlots);
WriteLine("MyShip.LRSSize=" & MyShip.LRSSize);
WriteLine("MyShip.LRSNumericType=" & MyShip.LRSNumericType);

WriteLine("MyShip.SRSIsActive=" & MyShip.SRSIsActive);
WriteLine("MyShip.SRS=" & MyShip.SRS);

Var Ship As CShip;
For(Each Ship In MyShip.SRS){
  Var Owner As New CUser(Ship.UserID);
  If(Owner.UserID <> Self.UserID){
    If("" & Owner.GetRelation(Self) <> "Freund"){
      If(Ship.AlertLevel = EAlertLevel.Green){
        WriteLine(Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
      }
      If(Ship.AlertLevel = EAlertLevel.Yellow){
        WriteLine(Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
      }
      If(Ship.AlertLevel = EAlertLevel.Red){
        WriteLine(Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
      }
    } Else {
      WriteLine(Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Friendly (" & Ship.AlertLevel & ")");
    }
  }
}
