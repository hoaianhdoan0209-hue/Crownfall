namespace Crownfall.Preparation {
public static class SellService { public static int ValueFor(int star)=>star switch{1=>1,2=>3,3=>7,4=>15,5=>31,_=>0}; }
}
