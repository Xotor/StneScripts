Var Koordinaten As New CStringList();
Var Self As New CUser(276);

WriteLine("Kolonien:");
Var ColoniesIter As New CColonyEnumerator();
While(ColoniesIter.Next())
{
  Var CurrentColony As CMyColony;
  CurrentColony = ColoniesIter.CurrentColony
  Var CCKoordinate As String = "@" & CurrentColony.Coordinates.X & "|" & CurrentColony.Coordinates.Y;
  WriteLine(CurrentColony.Name & " scanning " & CCKoordinate);
  Koordinaten.Add(CCKoordinate);
}

WriteLine("");
WriteLine("Schiffe:");
Var ShipsIter As New CShipEnumerator();
While(ShipsIter.Next())
{
  Var CurrentShip As CMyShip;
  CurrentShip = ShipsIter.CurrentShip
  Var Koordinate As String = CurrentShip.MapPosition.X & "|" & CurrentShip.MapPosition.Y;
  If(CurrentShip.MapPosition.InOrbit)
  {
    Koordinate = "@" & Koordinate;
  }
  If(Koordinaten.Contains(Koordinate) = False)
  {
    WriteLine(CurrentShip.Name & " scanning " & Koordinate & " ...");
    Var Ship As CShip;
    For(Each Ship In CurrentShip.SRS)
    {
      Var Owner As New CUser(Ship.UserID);
      If("" & Owner.GetRelation(Self) <> "Freund")
      {
        If(Ship.AlertLevel = EAlertLevel.Green){
          WriteLine("Friendly: " & Koordinate & Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
        }
        If(Ship.AlertLevel = EAlertLevel.Yellow){
          WriteLine("Warning: " & Koordinate & Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
        }
        If(Ship.AlertLevel = EAlertLevel.Red){
          WriteLine("Critical: " & Koordinate & Ship.GetNameTextAndID & " von " & Owner.NameAndID & " Alarmstufe " & Ship.AlertLevel);
        }
      }
    }
    Koordinaten.Add(Koordinate);
  }
}
