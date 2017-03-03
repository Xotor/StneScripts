RenameFleet("SI", "11", "Adrec", 141405);
RenameFleet("SI", "12", "Bandari", 141455);
RenameFleet("SI", "13", "Darinaya", 143979);

Function RenameFleet (tagS As String, nummerS As String, klasseS As String, flottenID As Integer) As Integer
{
  Var tag As String;
  Var flottenNummer As String;
  Var klasse As String;
  Var i As Integer;
  Var flotte As Integer;
  
  tag = tagS ;
  flottenNummer = nummerS; 
  klasse = klasseS ;
  flotte = flottenID ; 
  i = 1;
  
  Var fleet As New CMyFleet(flotte);
  Var shiplist As CShipList;
  shiplist = fleet.Ships;
  Var ship As CMyShip;
  Var firstship As CMyShip;
  
  While (shiplist.Count > 0)
  {
    firstship = shiplist.Item(0);
    For (Each ship In shiplist)
    { 
      If (ship.ShipID < firstship.ShipID)
      {
        firstship = ship;
      }
    }
    If (i < 10) {
      firstship.Name = tag & '-' & flottenNummer & '-' & '0' & i & ' ' & klasse;
      i = i + 1;
    }
    Else {
      firstship.Name = tag & '-' & flottenNummer & '-' & i & ' ' & klasse;
      i = i + 1;
    }
    shiplist.Remove(firstship);
    WriteLine(firstship.Name & ' (NCC: ' & firstship.ShipID & ') benannt - ' & shiplist.Count & ' Schiffe verbleiben.'); 
  }
}
WriteLine('');
WriteLine('Alle Schiffe benannt.');
